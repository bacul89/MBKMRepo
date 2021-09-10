using MBKM.Entities.Models.MBKM;
using MBKM.Services.MBKMServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MBKM.Presentation.Areas.Portal.Controllers
{
    public class HomeController : Controller
    {
        private IMahasiswaService _mahasiswaService;
        public HomeController(IMahasiswaService mahasiswaService)
        {
            _mahasiswaService = mahasiswaService;
        }
        // GET: Portal/Home
        public ActionResult Index()
        {
            //var a = _mahasiswaService.getLoginInternal("11998000648", "126019");
            return View();
        }
        public ActionResult DataDiri()
        {
            return View();
        }
        public void registerExternal(Mahasiswa mahasiswa)
        {
            _mahasiswaService.Save(mahasiswa);
        }
    }
}