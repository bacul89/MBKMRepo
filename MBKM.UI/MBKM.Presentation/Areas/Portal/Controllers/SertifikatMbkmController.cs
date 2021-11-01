using MBKM.Services.MBKMServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MBKM.Presentation.Areas.Portal.Controllers
{
    public class SertifikatMbkmController : Controller
    {
        private IMahasiswaService _mahasiswaService;
        private IPendaftaranMataKuliahService _pendaftaranMataKuliahService;

        public SertifikatMbkmController(IMahasiswaService mahasiswaService, IPendaftaranMataKuliahService pendaftaranMataKuliahService)
        {
            _mahasiswaService = mahasiswaService;
            _pendaftaranMataKuliahService = pendaftaranMataKuliahService;
        }


        // GET: Portal/SertifikatMbkm
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult PrintData()
        {
            return Json(new { dsata = "sds" });
        }
    }
}