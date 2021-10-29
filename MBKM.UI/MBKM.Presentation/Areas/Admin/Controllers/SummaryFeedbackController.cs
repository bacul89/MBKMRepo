using MBKM.Entities.ViewModel;
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

        public SummaryFeedbackController(IPendaftaranMataKuliahService pendaftaranMataKuliahService, IJadwalKuliahService jadwalKuliahService, IFeedbackMatkulService feedbackMatkulService, IMahasiswaService mahasiswaService, IJadwalUjianMBKMService jadwalUjianMBKMService)
        {
            _pendaftaranMataKuliahService = pendaftaranMataKuliahService;
            _jadwalKuliahService = jadwalKuliahService;
            _feedbackMatkulService = feedbackMatkulService;
            _mahasiswaService = mahasiswaService;
            _jadwalUjianMBKMService = jadwalUjianMBKMService;
        }



        // GET: Admin/SummaryFeedback
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult GetDataTable(string semester)
        {
            var email = /*HttpContext.Session["email"].ToString()*/"anindyasabrinap@gmail.com";
            var data1 = _pendaftaranMataKuliahService.Find(x => x.mahasiswas.Email == email && x.JadwalKuliahs.STRM == 2110).ToList();
            List<String[]> final = new List<String[]>();
            foreach (var d in data1)
            {
                var idJadwalKuliah = d.JadwalKuliahID.ToString();
                var strm = d.JadwalKuliahs.STRM.ToString();
                var checkStatus = _feedbackMatkulService.Find(x => x.JadwalKuliahID == d.JadwalKuliahID).Count();
                if (checkStatus != 0)
                {
                    final.Add(new String[]{
                        idJadwalKuliah,
                        strm,
                        d.JadwalKuliahs.KodeMataKuliah,
                        d.JadwalKuliahs.NamaMataKuliah,
                        d.JadwalKuliahs.SKS,
                        d.JadwalKuliahs.ClassSection,
                        "Sudah"
                    });
                }
                else
                {
                    final.Add(new String[]{
                        idJadwalKuliah,
                        strm,
                        d.JadwalKuliahs.KodeMataKuliah,
                        d.JadwalKuliahs.NamaMataKuliah,
                        d.JadwalKuliahs.SKS,
                        d.JadwalKuliahs.ClassSection,
                        "Belum"
                    });
                }
            }

            return Json(final);
        }

    }
}