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
        private ILookupService _lookupService;

        public SummaryFeedbackController(IPendaftaranMataKuliahService pendaftaranMataKuliahService, IJadwalKuliahService jadwalKuliahService, IFeedbackMatkulService feedbackMatkulService, IMahasiswaService mahasiswaService, IJadwalUjianMBKMService jadwalUjianMBKMService, IMasterCapaianPembelajaranService masterCapaianPembelajaranService, ILookupService lookupService)
        {
            _pendaftaranMataKuliahService = pendaftaranMataKuliahService;
            _jadwalKuliahService = jadwalKuliahService;
            _feedbackMatkulService = feedbackMatkulService;
            _mahasiswaService = mahasiswaService;
            _jadwalUjianMBKMService = jadwalUjianMBKMService;
            _masterCapaianPembelajaranService = masterCapaianPembelajaranService;
            _lookupService = lookupService;
        }

        // GET: Admin/SummaryFeedback
        public ActionResult Index()
        {
            ViewData["role"] = /*HttpContext.Session["RoleName"].ToString()*/"DDD";
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
            var data1 = _feedbackMatkulService.Find(x => x.JadwalKuliahs.STRM == tahunSemester && x.JadwalKuliahs.JenjangStudi == jenjangStudi && x.JadwalKuliahs.FakultasID == fakultas).ToList();
            var dataSemester = _feedbackMatkulService.GetSemesterByStrm(tahunSemester.ToString());
            List<String[]> final = new List<String[]>();
            foreach (var d in data1)
            {
                var jumlahMahasiswa = _pendaftaranMataKuliahService.Find(x => x.JadwalKuliahID == d.JadwalKuliahID && x.JadwalKuliahs.STRM == tahunSemester && x.StatusPendaftaran.Contains("ACCEPTED")).Count();
                var idJadwalKuliah = d.JadwalKuliahID.ToString();

                final.Add(new String[]{
                    d.ID.ToString(),
                    dataSemester.Nama,
                    d.DosenID,
                    d.NamaDosen,
                    d.JadwalKuliahs.KodeMataKuliah,
                    d.JadwalKuliahs.NamaMataKuliah,
                    d.JadwalKuliahs.ClassSection,
                    jumlahMahasiswa.ToString(),
                });
                
            }

            return Json(final);
        }

    }
}