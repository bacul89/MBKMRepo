using MBKM.Entities.Models.MBKM;
using MBKM.Services.MBKMServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MBKM.Presentation.Areas.Portal.Controllers
{
    public class DataDiriController : Controller
    {
        private IMahasiswaService _mahasiswaService;
        public DataDiriController(IMahasiswaService mahasiswaService)
        {
            _mahasiswaService = mahasiswaService;
        }
        // GET: Portal/Home
        public ActionResult Index()
        {
            bool isLogin = (bool)Session["isLogin"] ;
            if (isLogin)
            {
                return View();
            }
            return RedirectToAction("Index", "Home");
        }
    }
}