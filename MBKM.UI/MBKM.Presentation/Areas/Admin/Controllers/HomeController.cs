using MBKM.Entities.Models;
using MBKM.Entities.ViewModel;
using MBKM.Presentation.Helper;
using MBKM.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MBKM.Presentation.Areas.Admin.Controllers
{
    [MBKMAuthorize]
    public class HomeController : Controller
    {
        private IMenuService _menuService;
        private ILookupService _lookupService;
        public HomeController(IMenuService menuService,
            ILookupService lookupService)
        {
            _menuService = menuService;
            _lookupService = lookupService;
        }
        // GET: Admin/Home
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Home()
        {
            return View();
        }

        public ActionResult PerjanjianKerjasama()
        {
            return View();
        }

    }
}