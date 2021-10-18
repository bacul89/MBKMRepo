using MBKM.Entities.Models.MBKM;
using MBKM.Entities.ViewModel;
using MBKM.Services.MBKMServices;
using System;
using System.Collections.Generic;
using System.Linq;
using MBKM.Presentation.Helper;
using System.Web;
using System.Web.Mvc;
using MBKM.Presentation.models;

namespace MBKM.Presentation.Areas.Portal.Controllers
{
    [MBKMAuthorize]
    public class JadwalUjianController : Controller
    {
        private IJadwalUjianMBKMDetailService _jadwalUjianMBKMDetailService;
        private IJadwalUjianMBKMService _jadwalUjianMBKMService;
        private IJadwalKuliahService _jadwalKuliahService;

        public JadwalUjianController(IJadwalUjianMBKMDetailService jadwalUjianMBKMDetailService, IJadwalUjianMBKMService jadwalUjianMBKMService, IJadwalKuliahService jadwalKuliahService)
        {
            _jadwalUjianMBKMDetailService = jadwalUjianMBKMDetailService;
            _jadwalUjianMBKMService = jadwalUjianMBKMService;
            _jadwalKuliahService = jadwalKuliahService;
        }


        // GET: Portal/JadwalUjian
        public ActionResult Index()
        {
            IEnumerable<VMSemester> data = _jadwalUjianMBKMService.getAllSemester();
            ViewData["semester"] = data;
            return View();
        }

        public ActionResult CheckData(string semester)
        {
            var email = HttpContext.Session["email"].ToString();
            var data = _jadwalUjianMBKMDetailService.Find(
                x => x.Mahasiswas.Email == email
                && x.JadwalUjianMBKMs.STRM == semester
                ).Count();
            if(data == 0)
            {
                return Json(new ServiceResponse { status = 500, message = "Maaf Belum Ada Jadwal Ujian" });
            }
            else
            {
                return Json(new ServiceResponse { status = 200, message = "Maaf Belum Ada Jadwal Ujian" });
            }
        }

        [HttpPost]
        public ActionResult DaftarJadwalUjian(string semester)
        {
            var email = HttpContext.Session["email"].ToString();
            IList<JadwalUjianMBKMDetail> data = _jadwalUjianMBKMDetailService.Find(
                x => x.Mahasiswas.Email == email
                && x.JadwalUjianMBKMs.STRM == semester
                ).ToList();
            IList<JadwalKuliah> dataSks = _jadwalKuliahService.GetAll().ToList();
            
            List<String[]> final = new List<String[]>();

            foreach (var dt in data)
            {
                var ddd = dt.MahasiswaID.ToString();
                var sks = dataSks.Where(x => x.MataKuliahID == dt.JadwalUjianMBKMs.IDMatkul).First().SKS;
                final.Add(new String[]
                {
                    ddd,
                    dt.JadwalUjianMBKMs.STRM,
                    dt.JadwalUjianMBKMs.NamaMatkul,
                    dt.JadwalUjianMBKMs.TanggalUjian.ToString("dd/MM/yyyy"),
                    dt.JadwalUjianMBKMs.JamMulai,
                    dt.JadwalUjianMBKMs.JamAkhir,
                    sks,
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