using MBKM.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MBKM.Presentation.Areas.Admin.Controllers.PerjanjianKerjasama
{
    public class PerjanjianKerjasamaController : Controller
    {
        private IMenuService _menuService;
        public PerjanjianKerjasamaController(IMenuService menuService)
        {
            _menuService = menuService;
        }
        // GET: Admin/PerjanjianKerjasama
        public ActionResult Index()
        {
            return View();
        }
    }
}