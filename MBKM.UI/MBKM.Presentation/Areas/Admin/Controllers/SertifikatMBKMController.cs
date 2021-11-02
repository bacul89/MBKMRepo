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
        private IPendaftaranMataKuliahService _pendaftaranMataKuliahService;

        public SertifikatMBKMController(IJadwalUjianMBKMService jadwalUjianMBKMService, IPendaftaranMataKuliahService pendaftaranMataKuliahService)
        {
            _jadwalUjianMBKMService = jadwalUjianMBKMService;
            _pendaftaranMataKuliahService = pendaftaranMataKuliahService;
        }


        // GET: Admin/SertifikatMBKM
        public ActionResult Index()
        {
            IEnumerable<VMSemester> data = _jadwalUjianMBKMService.getAllSemester();
            ViewData["semester"] = data;
            return View();
        }

        [HttpPost]
        public ActionResult GetDataTable(int strm)
        {
            var data = _pendaftaranMataKuliahService.Find(x => x.JadwalKuliahs.STRM == strm).ToList();
            return Json(data);
        }
    }
}