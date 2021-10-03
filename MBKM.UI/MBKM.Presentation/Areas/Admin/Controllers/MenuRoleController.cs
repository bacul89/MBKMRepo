using MBKM.Common.Helpers;
using MBKM.Entities.Models;
using MBKM.Entities.ViewModel;
using MBKM.Presentation.Helper;
using MBKM.Repository.BaseRepository;
using MBKM.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MBKM.Presentation.Areas.Admin.Controllers
{
    [MBKMAuthorize]
    public class MenuRoleController : Controller
    {
        private IMenuRoleService _menuRoleService;
        private IMenuService _menuService;
        private IRoleService _roleService;
        public MenuRoleController(IMenuRoleService menuRoleService, IMenuService menuService, IRoleService roleService)
        {
            _menuRoleService = menuRoleService;
            _menuService = menuService;
            _roleService = roleService;
        }
        // GET: Admin/MenuRole
        public ActionResult Index()
        {
            //IEnumerable<VMMenu> listMenu = _menuService.getListMenu();
            //ViewBag.listMenu = new SelectList(listMenu, "Nilai", "Nama");
            var listRole = _roleService.getLookupRole();
            ViewData["listRole"] = listRole;
            return View();
        }

        //get data table
        [HttpPost]
        public JsonResult GetDataMenuRole(DataTableAjaxPostModel model)
        {
            VMListMenuRole vMListMenuRole = _menuRoleService.getListMRGrid(model);
            return Json(new
            {
                // this is what datatables wants sending back
                draw = model.draw,
                recordsTotal = vMListMenuRole.TotalCount,
                recordsFiltered = vMListMenuRole.TotalFilterCount,
                data = vMListMenuRole.gridDatas
            });
        }
        public ActionResult ModalAddMenuRole()
        {
            var listRole = _roleService.getLookupRole();
            ViewData["listRole"] = listRole;
            var listMenu = _menuService.getListMenu();
            ViewData["listMenu"] = listMenu;
            return View("AddMenuRole");
        }
        [HttpPost]
        public ActionResult PostDataMenuRole(MenuRole menuRole)
        {
            /*            Console.WriteLine("Test");
                        Console.WriteLine(lookup);*/
            menuRole.CreatedBy = Session["username"] as string;
            menuRole.UpdatedBy = Session["username"] as string;
            _menuRoleService.Save(menuRole);
            return Json(menuRole);
        }
        public ActionResult ModalDetailMenuRole(int id)
        {
            var model = _menuRoleService.Get(id);

            return View("DetailMenuRole",model);
        }
        /*Delete*/
        [HttpPost]
        public ActionResult PostDeleteMenuRole(int id)
        {
            var context = new MBKMContext();
            context.Configuration.ProxyCreationEnabled = false;
            var data = _menuRoleService.Get(id);
            data.IsDeleted = true;
            data.UpdatedBy = Session["username"] as string;

            _menuRoleService.Save(data);
            return Json(data);
        }

    }
}