using MBKM.Entities.Models;
using MBKM.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MBKM.Presentation.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
        private IMenuService _menuService;
        public HomeController(IMenuService menuService)
        {
            _menuService = menuService;
        }
        // GET: Admin/Home
        public ActionResult Index()
        {
            //IEnumerable<Menu> a = _menuService.GetAll().ToList();
            //Menu model = _menuService.Get(1);
            return View();
        }
        public ActionResult Home()
        {
            return View();
        }
    }
}