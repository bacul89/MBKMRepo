using MBKM.Entities.ViewModel;
using MBKM.Services;
using MBKM.Services.MBKMServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MBKM.Presentation.Areas.Admin.Controllers
{
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
            ViewData["role"] = /*HttpContext.Session["RoleName"].ToString()*/"Admin";
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
                var jumlahMahasiswa = _pendaftaranMataKuliahService.Find(x => x.JadwalKuliahID == d.JadwalId && x.JadwalKuliahs.STRM == tahunSemester && x.StatusPendaftaran.Contains("ACCEPTED")).Count();

                final.Add(new String[]{
                    d.JadwalId.ToString(),
                    dataSemester.Nama,
                    d.dosenId,
                    d.namaDosen,
                    d.Jadwals.KodeMataKuliah,
                    d.Jadwals.NamaMataKuliah,
                    d.Jadwals.ClassSection,
                    jumlahMahasiswa.ToString(),
                });
                
            }

            return Json(final);
        }

        [HttpPost]
        public ActionResult GetDataTableDosen(int tahunSemester)
        {
            var idDosen = /*HttpContext.Session["nopegawai"].ToString()*/"120131524";
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
                var jumlahMahasiswa = _pendaftaranMataKuliahService.Find(x => x.JadwalKuliahID == d.JadwalId && x.JadwalKuliahs.STRM == tahunSemester && x.StatusPendaftaran.Contains("ACCEPTED")).Count();
                final.Add(new String[]{
                    d.JadwalId.ToString(),
                    dataSemester.Nama,
                    d.Jadwals.KodeMataKuliah,
                    d.Jadwals.NamaMataKuliah,
                    d.Jadwals.ClassSection,
                    jumlahMahasiswa.ToString(),
                });

            }

            return Json(final);
        }


        [HttpPost]
        public ActionResult GetPersentationData(int idData, string dosenId)
        {
            if(dosenId == "")
            {
                dosenId = /*HttpContext.Session["nopegawai"].ToString()*/"120131524";
            }
            var countRespondent = _feedbackMatkulService.Find(x => x.DosenID == dosenId && x.JadwalKuliahID == idData).Count();
            var data = _feedbackMatkulDetailService.Find(x => x.FeedbackMatkuls.JadwalKuliahID == idData && x.FeedbackMatkuls.DosenID == dosenId)
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
            ViewData["rata"] = rata2;

            return View();
        }

    }
}