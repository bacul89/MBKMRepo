using MBKM.Entities.ViewModel;
using MBKM.Services.MBKMServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MBKM.Presentation.Areas.Admin.Controllers
{
    public class SertifikatMBKMController : Controller
    {
        private IJadwalUjianMBKMService _jadwalUjianMBKMService;

        public SertifikatMBKMController(IJadwalUjianMBKMService jadwalUjianMBKMService)
        {
            _jadwalUjianMBKMService = jadwalUjianMBKMService;
        }


        // GET: Admin/SertifikatMBKM
        public ActionResult Index()
        {
            IEnumerable<VMSemester> data = _jadwalUjianMBKMService.getAllSemester();
            ViewData["semester"] = data;
            return View();
        }
    }
}