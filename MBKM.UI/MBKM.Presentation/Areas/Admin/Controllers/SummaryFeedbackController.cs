using MBKM.Entities.ViewModel;
using MBKM.Services;
using MBKM.Services.MBKMServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MBKM.Presentation.Helper;
using System.Web.Mvc;

namespace MBKM.Presentation.Areas.Admin.Controllers
{
    [MBKMAuthorize]
    public class SummaryFeedbackController : Controller
    {
        private IPendaftaranMataKuliahService _pendaftaranMataKuliahService;
        private IJadwalKuliahService _jadwalKuliahService;
        private IFeedbackMatkulService _feedbackMatkulService;
        private IMahasiswaService _mahasiswaService;
        private IJadwalUjianMBKMService _jadwalUjianMBKMService;
        private IMasterCapaianPembelajaranService _masterCapaianPembelajaranService;
        private IFeedbackMatkulDetailService _feedbackMatkulDetailService;
        private ILookupService _lookupService;

        public SummaryFeedbackController(IPendaftaranMataKuliahService pendaftaranMataKuliahService, IJadwalKuliahService jadwalKuliahService, IFeedbackMatkulService feedbackMatkulService, IMahasiswaService mahasiswaService, IJadwalUjianMBKMService jadwalUjianMBKMService, IMasterCapaianPembelajaranService masterCapaianPembelajaranService, IFeedbackMatkulDetailService feedbackMatkulDetailService, ILookupService lookupService)
        {
            _pendaftaranMataKuliahService = pendaftaranMataKuliahService;
            _jadwalKuliahService = jadwalKuliahService;
            _feedbackMatkulService = feedbackMatkulService;
            _mahasiswaService = mahasiswaService;
            _jadwalUjianMBKMService = jadwalUjianMBKMService;
            _masterCapaianPembelajaranService = masterCapaianPembelajaranService;
            _feedbackMatkulDetailService = feedbackMatkulDetailService;
            _lookupService = lookupService;
        }



        // GET: Admin/SummaryFeedback
        public ActionResult Index()
        {
            ViewData["role"] = HttpContext.Session["RoleName"].ToString();
            var data1 = HttpContext.Session["isCreate"].ToString();
            var data2 = HttpContext.Session["isUpdate"].ToString();
            var data3 = HttpContext.Session["IsDelete"].ToString();
            IEnumerable<VMLookup> tempJenjang = _lookupService.getLookupByTipe("JenjangStudi");
            ViewData["Jenjang"] = tempJenjang;
            IEnumerable<VMSemester> data = _jadwalUjianMBKMService.getAllSemester();
            ViewData["semester"] = data;
            return View();
        }

        [HttpPost]
        public ActionResult GetFakultas(string search, string JenjangStudi)
        {
            return Json(_masterCapaianPembelajaranService.GetFakultas(JenjangStudi, search));
        }

        [HttpPost]
        public ActionResult GetDataTableAdmin(int tahunSemester, string jenjangStudi, int fakultas)
        {
            var data1 = _feedbackMatkulService.Find(x => x.JadwalKuliahs.STRM == tahunSemester 
                && x.JadwalKuliahs.JenjangStudi == jenjangStudi 
                && x.JadwalKuliahs.FakultasID == fakultas)
                .GroupBy(g => new
                {
                    g.DosenID,
                    g.JadwalKuliahID,
                    g.JadwalKuliahs,
                    g.NamaDosen,
                }).Select(s => new
                {
                    dosenId = s.Key.DosenID,
                    namaDosen = s.Key.NamaDosen,
                    JadwalId = s.Key.JadwalKuliahID,
                    Jadwals = s.Key.JadwalKuliahs
                }).ToList();
            var dataSemester = _feedbackMatkulService.GetSemesterByStrm(tahunSemester.ToString());
            List<String[]> final = new List<String[]>();
            foreach (var d in data1)
            {
                var jumlahMahasiswa = 0;
                 jumlahMahasiswa = _pendaftaranMataKuliahService.Find(x => x.JadwalKuliahID == d.JadwalId && x.JadwalKuliahs.STRM == tahunSemester && x.StatusPendaftaran.Contains("ACCEPTED") && x.mahasiswas.NIM != x.mahasiswas.NIMAsal).Count();
                var dosenTmp = d.namaDosen.Split('-');
                var countRespondent = _feedbackMatkulService.Find(x => x.DosenID == d.dosenId && x.JadwalKuliahID == d.JadwalId && x.Mahasiswas.NIM != x.Mahasiswas.NIMAsal).Count();
                var data = _feedbackMatkulDetailService.Find(x => x.FeedbackMatkuls.JadwalKuliahID == d.JadwalId && x.FeedbackMatkuls.DosenID == d.dosenId && x.FeedbackMatkuls.Mahasiswas.NIM != x.FeedbackMatkuls.Mahasiswas.NIMAsal)
                    .GroupBy(g => new { g.KategoriPertanyaan }).Select(s => new
                    {
                        col1 = s.Sum(c => c.Nilai),
                        col2 = s.Key.KategoriPertanyaan,
                        col3 = s.Count()
                    }).ToList();

                /*total jawaban per kategori semua siswa / (jml pertanyaan per kateogri * 4 * jml siswa)*/
                double countResult = 0;
                List<String[]> tmpfinal= new List<String[]>();
                foreach (var q in data)
                {
                    var getCountAsk = _feedbackMatkulDetailService.Find(x => x.FeedbackMatkuls.JadwalKuliahID == d.JadwalId && x.FeedbackMatkuls.DosenID == d.dosenId && x.KategoriPertanyaan == q.col2)
                        .GroupBy(s => new { s.PertanyaanID }).Count();

                    double perhitungan = q.col1 / (double)(getCountAsk * 4 * countRespondent);
                    countResult = countResult + perhitungan;
                }
                double rata2 = countResult / (double)data.Count();

                if (dosenTmp.Count() == 1)
                {
                    final.Add(new String[]{
                        d.JadwalId.ToString(),
                        dataSemester.Nama,
                        d.dosenId,
                        dosenTmp[0],
                        d.Jadwals.KodeMataKuliah,
                        d.Jadwals.NamaMataKuliah,
                        d.Jadwals.ClassSection,
                        jumlahMahasiswa.ToString(),
                        countRespondent.ToString(),
                        Math.Round(rata2, 2).ToString(),
                });
                }
                else
                {
                    final.Add(new String[]{
                        d.JadwalId.ToString(),
                        dataSemester.Nama,
                        d.dosenId,
                        dosenTmp[1],
                        d.Jadwals.KodeMataKuliah,
                        d.Jadwals.NamaMataKuliah,
                        d.Jadwals.ClassSection,
                        jumlahMahasiswa.ToString(),
                        countRespondent.ToString(),
                        Math.Round(rata2, 2).ToString(),
                    }); 
                }
                
                
            }

            return Json(final);
        }

        [HttpPost]
        public ActionResult GetDataTableDosen(int tahunSemester)
        {
            var idDosen = HttpContext.Session["nopegawai"].ToString();
            var data1 = _feedbackMatkulService.Find(x => x.JadwalKuliahs.STRM == tahunSemester && x.DosenID == idDosen)
                .GroupBy(g => new
                {
                    g.DosenID,
                    g.JadwalKuliahID,
                    g.JadwalKuliahs,
                    g.NamaDosen,
                }).Select(s => new
                {
                    dosenId = s.Key.DosenID,
                    namaDosen = s.Key.NamaDosen,
                    JadwalId = s.Key.JadwalKuliahID,
                    Jadwals = s.Key.JadwalKuliahs
                }).ToList();
            var dataSemester = _feedbackMatkulService.GetSemesterByStrm(tahunSemester.ToString());
            List<String[]> final = new List<String[]>();
            foreach (var d in data1)
            {

                var countRespondent = _feedbackMatkulService.Find(x => x.DosenID == idDosen && x.JadwalKuliahID == d.JadwalId && x.Mahasiswas.NIM != x.Mahasiswas.NIMAsal).Count();
                var data = _feedbackMatkulDetailService.Find(x => x.FeedbackMatkuls.JadwalKuliahID == d.JadwalId && x.FeedbackMatkuls.DosenID == idDosen && x.FeedbackMatkuls.Mahasiswas.NIM != x.FeedbackMatkuls.Mahasiswas.NIMAsal)
                    .GroupBy(g => new { g.KategoriPertanyaan }).Select(s => new
                    {
                        col1 = s.Sum(c => c.Nilai),
                        col2 = s.Key.KategoriPertanyaan,
                        col3 = s.Count()
                    }).ToList();

                /*total jawaban per kategori semua siswa / (jml pertanyaan per kateogri * 4 * jml siswa)*/
                double countResult = 0;
                List<String[]> tmpfinal = new List<String[]>();
                foreach (var q in data)
                {
                    var getCountAsk = _feedbackMatkulDetailService.Find(x => x.FeedbackMatkuls.JadwalKuliahID == d.JadwalId && x.FeedbackMatkuls.DosenID == idDosen && x.KategoriPertanyaan == q.col2)
                        .GroupBy(s => new { s.PertanyaanID }).Count();

                    double perhitungan = q.col1 / (double)(getCountAsk * 4 * countRespondent);
                    countResult = countResult + perhitungan;
                }
                double rata2 = countResult / (double)data.Count();


                var jumlahMahasiswa = _pendaftaranMataKuliahService.Find(x => x.JadwalKuliahID == d.JadwalId && x.JadwalKuliahs.STRM == tahunSemester && x.StatusPendaftaran.Contains("ACCEPTED") && x.mahasiswas.NIM != x.mahasiswas.NIMAsal).Count();
                final.Add(new String[]{
                    d.JadwalId.ToString(),
                    dataSemester.Nama,
                    d.Jadwals.KodeMataKuliah,
                    d.Jadwals.NamaMataKuliah,
                    d.Jadwals.ClassSection,
                    jumlahMahasiswa.ToString(),
                    countRespondent.ToString(),
                    Math.Round(rata2, 2).ToString(),
                });

            }

            return Json(final);
        }


        [HttpPost]
        public ActionResult GetPersentationData(int idData, string dosenId)
        {
            if(dosenId == "")
            {
                dosenId = HttpContext.Session["nopegawai"].ToString();
            }
            var countRespondent = _feedbackMatkulService.Find(x => x.DosenID == dosenId && x.JadwalKuliahID == idData && x.Mahasiswas.NIM != x.Mahasiswas.NIMAsal).Count();
            var data = _feedbackMatkulDetailService.Find(x => x.FeedbackMatkuls.JadwalKuliahID == idData && x.FeedbackMatkuls.DosenID == dosenId && x.FeedbackMatkuls.Mahasiswas.NIM != x.FeedbackMatkuls.Mahasiswas.NIMAsal)
                .GroupBy(g => new { g.KategoriPertanyaan }).Select(s => new
                {
                    col1 = s.Sum(c => c.Nilai),
                    col2 = s.Key.KategoriPertanyaan,
                    col3 = s.Count()
                }).ToList();

            /*total jawaban per kategori semua siswa / (jml pertanyaan per kateogri * 4 * jml siswa)*/
            double countResult = 0;
            List<String[]> final = new List<String[]>();
            foreach (var d in data)
            {
                var getCountAsk = _feedbackMatkulDetailService.Find(x => x.FeedbackMatkuls.JadwalKuliahID == idData && x.FeedbackMatkuls.DosenID == dosenId && x.KategoriPertanyaan == d.col2)
                    .GroupBy(s => new { s.PertanyaanID }).Count();

                double perhitungan = d.col1 / (double)(getCountAsk * 4 * countRespondent);
                final.Add(new String[]{
                    d.col2,
                    Math.Round(perhitungan,2).ToString()
                });
                countResult = countResult + perhitungan;
            }
            double rata2 = countResult / (double)final.Count();

            ViewData["dataResult"] = final;
            ViewData["rata"] = Math.Round(rata2, 2).ToString();

            return View();
        }

    }
}