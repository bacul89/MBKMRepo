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

        public RegisMhsByAdminController(IPerjanjianKerjasamaService perjanjianKerjasamaService)
        {
            _perjanjianKerjasamaService = perjanjianKerjasamaService;
        }

        public ActionResult Index()
        {
            return View();
        }
        public JsonResult GetNamaInstansi(int Skip, int Length, string Search)
        {
            return Json(_perjanjianKerjasamaService.getNamaInstansi(Skip, Length, Search), JsonRequestBehavior.AllowGet);
        }
    }
}