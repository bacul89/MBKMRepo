using MBKM.Entities.ViewModel;
using MBKM.Presentation.Helper;
using MBKM.Services;
using MBKM.Services.MBKMServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MBKM.Presentation.Areas.Admin.Controllers
{
    [MBKMAuthorize]
    public class ReportInternalKeluarController : Controller
    {
        // GET: Admin/ReportInternalKeluar
        private IJadwalUjianMBKMService _jadwalUjianMBKMService;
        private ILookupService _lookupService;

        public ReportInternalKeluarController(IJadwalUjianMBKMService jadwalUjianMBKMService, ILookupService lookupService)
        {
            _jadwalUjianMBKMService = jadwalUjianMBKMService;
            _lookupService = lookupService;
        }

        public ActionResult Index()
        {
            ViewData["role"] = HttpContext.Session["RoleName"].ToString();
            IEnumerable<VMLookup> tempJenjang = _lookupService.getLookupByTipe("JenjangStudi");
            ViewData["Jenjang"] = tempJenjang;
            IEnumerable<VMSemester> data = _jadwalUjianMBKMService.getAllSemester();
            ViewData["semester"] = data;
            return View();
        }
    }
}