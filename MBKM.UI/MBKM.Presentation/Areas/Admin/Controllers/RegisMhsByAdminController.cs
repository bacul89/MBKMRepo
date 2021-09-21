using MBKM.Services;
using MBKM.Services.MBKMServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MBKM.Presentation.Areas.Admin.Controllers
{
    public class RegisMhsByAdminController : Controller
    {
        // GET: Admin/RegisMhsByAdmin
        private IPerjanjianKerjasamaService _perjanjianKerjasamaService;
        private ILookupService _lookupService;

        public RegisMhsByAdminController(IPerjanjianKerjasamaService perjanjianKerjasamaService, ILookupService lookupService)
        {
            _perjanjianKerjasamaService = perjanjianKerjasamaService;
            _lookupService = lookupService;
        }

        public ActionResult Index()
        {
            return View();
        }
        public JsonResult GetNamaInstansi(int Skip, int Length, string Search)
        {
            return Json(_perjanjianKerjasamaService.getNamaInstansi(Skip, Length, Search), JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetNoKerjasama(int Skip, int Length, string Search, string NamaInstansi)
        {
            return Json(_perjanjianKerjasamaService.getNoKerjasama(Skip, Length, Search, NamaInstansi), JsonRequestBehavior.AllowGet);
        }
        public JsonResult getLookupByTipe(string tipe)
        {
            return Json(_lookupService.getLookupByTipe(tipe), JsonRequestBehavior.AllowGet);
        }
    }
}