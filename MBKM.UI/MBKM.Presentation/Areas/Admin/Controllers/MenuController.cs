using MBKM.Common.Helpers;
using MBKM.Entities.Models;
using MBKM.Entities.ViewModel;
using MBKM.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MBKM.Presentation.Areas.Admin.Controllers
{
    public class MenuController : Controller
    {
        private IMenuService _menuService;
        public MenuController(IMenuService menuService)
        {
            _menuService = menuService;
        }
        // GET: Admin/Menu
        public ActionResult Index()
        {
            return View();
        }
        //get data table
        [HttpPost]
        public JsonResult GetDataMenu(DataTableAjaxPostModel model)
        {
            VMListMenu vMListMenu = _menuService.getMenu(model);
            return Json(new
            {
                // this is what datatables wants sending back
                draw = model.draw,
                recordsTotal = vMListMenu.TotalCount,
                recordsFiltered = vMListMenu.TotalFilterCount,
                data = vMListMenu.gridDatas
            });
        }
        /*Modal Created*/
        public ActionResult ModalCreateMenu()
        {
            return View("ModalCreateMenu");
        }
        [HttpPost]
        public ActionResult PostDataMenu(Menu menu)
        {
            /*            Console.WriteLine("Test");
                        Console.WriteLine(lookup);*/
            menu.CreatedBy = Session["username"] as string;
            menu.UpdatedBy = Session["username"] as string;
            _menuService.Save(menu);
            return Json(menu);
        }
        /*Modal Detail*/
        public ActionResult ModalDetailMenu(int id)
        {
            var data = _menuService.Get(id);
            return View(data);
        }
        /*Delete*/
        [HttpPost]
        public ActionResult PostDeleteMenu(int id)
        {
            var data = _menuService.Get(id);
            data.IsDeleted = true;
            data.UpdatedBy = Session["username"] as string;

            _menuService.Save(data);
            return Json(data);
        }
        /*Modal Update*/
        public ActionResult ModalUpdateMenu(int id)
        {
            var data = _menuService.Get(id);
            return View(data);
        }
        [HttpPost]
        public ActionResult PostUpdateMenu(Menu menu)
        {

            Menu data = _menuService.Get(menu.ID);
            data.MenuName = menu.MenuName;
            data.MenuDescription = menu.MenuDescription;
            data.MenuUrl = menu.MenuUrl;
            data.IsActive = menu.IsActive;
            data.UpdatedBy = Session["username"] as string;



            _menuService.Save(data);

            return Json(data);
        }
    }
}