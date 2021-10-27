using MBKM.Entities.Models.MBKM;
using MBKM.Services.MBKMServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MBKM.Presentation.Areas.Portal.Controllers
{
    public class KHSController : Controller
    {
        private INilaiKuliahService _transkripService;
        private IJadwalKuliahMahasiswaService _jkMhsService;
        private IMahasiswaService _mahasiswaService;

        public KHSController(INilaiKuliahService transkripService, IJadwalKuliahMahasiswaService jkMhsService, IMahasiswaService mahasiswaService)
        {
            _transkripService = transkripService;
            _jkMhsService = jkMhsService;
            _mahasiswaService = mahasiswaService;
        }

        // GET: Portal/KHS
        public ActionResult Index()
        {
            string email = Session["email"] as string;

            Mahasiswa model = _mahasiswaService.Find(m => m.Email == email).FirstOrDefault();
            return View(model);
        }
    }
}