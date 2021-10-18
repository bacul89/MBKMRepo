using MBKM.Entities.Models.MBKM;
using MBKM.Entities.ViewModel;
using MBKM.Services.MBKMServices;
using System;
using System.Collections.Generic;
using System.Linq;
using MBKM.Presentation.Helper;
using System.Web;
using System.Web.Mvc;

namespace MBKM.Presentation.Areas.Portal.Controllers
{
    [MBKMAuthorize]
    public class JadwalUjianController : Controller
    {
        private IJadwalUjianMBKMDetailService _jadwalUjianMBKMDetailService;
        private IJadwalUjianMBKMService _jadwalUjianMBKMService;

        public JadwalUjianController(IJadwalUjianMBKMDetailService jadwalUjianMBKMDetailService, IJadwalUjianMBKMService jadwalUjianMBKMService)
        {
            _jadwalUjianMBKMDetailService = jadwalUjianMBKMDetailService;
            _jadwalUjianMBKMService = jadwalUjianMBKMService;
        }



        // GET: Portal/JadwalUjian
        public ActionResult Index()
        {
            IEnumerable<VMSemester> data = _jadwalUjianMBKMService.getAllSemester();
            ViewData["semester"] = data;
            return View();
        }

        [HttpPost]
        public ActionResult DaftarJadwalUjian(string semester)
        {
            IList<JadwalUjianMBKMDetail> data = _jadwalUjianMBKMDetailService.Find(
                x => x.Mahasiswas.Email == HttpContext.Session["email"].ToString()
                && x.JadwalUjianMBKMs.STRM == semester
                ).ToList();

            List<String[]> final = new List<String[]>();

            foreach (var dt in data)
            {
                var ddd = dt.MahasiswaID.ToString();

                final.Add(new String[]
                {
                    ddd,
                    dt.JadwalUjianMBKMs.STRM,
                    dt.JadwalUjianMBKMs.NamaMatkul,
                    dt.JadwalUjianMBKMs.TanggalUjian.ToString("dd/MM/yyyy"),
                    dt.JadwalUjianMBKMs.JamMulai,
                    dt.JadwalUjianMBKMs.JamAkhir,
                    dt.JadwalUjianMBKMs.JamAkhir,
                    dt.JadwalUjianMBKMs.Lokasi,
                    dt.JadwalUjianMBKMs.RuangUjian,
                    dt.JadwalUjianMBKMs.ClassSection,
                    dt.JadwalUjianMBKMs.NamaProdi,
                });
                

            }
            return Json(final);
        }

    }
}