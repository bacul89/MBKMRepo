using MBKM.Common.Helpers;
using MBKM.Entities.Models;
using MBKM.Entities.ViewModel;
using MBKM.Presentation.Helper;
using MBKM.Presentation.models;
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
            try {
                if (GetMenuByMenuIDandRoleID( menuRole.MenuID,menuRole.RoleID) == null)
                {
                    menuRole.CreatedBy = Session["username"] as string;
                    menuRole.UpdatedBy = Session["username"] as string;
                    _menuRoleService.Save(menuRole);
                    return Json(new ServiceResponse { status = 200, message = "Pendaftaran Menu Role Berhasil!" });
                }
                else { return Json(new ServiceResponse { status = 400, message = "Gagal! Menu Role Ini Telah Tersedia!" }); }
                //return Json(menuRole);
                
            }
            catch (Exception e)
            {
                return Json(new ServiceResponse { status = 500, message = e.Message });
            }
            
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
        /*Modal Update*/
        public ActionResult ModalUpdateMenuRole(int id)
        {
            var data = _menuRoleService.Get(id);
            var listRole = _roleService.getLookupRole();
            ViewData["listRole"] = listRole;
            var listMenu = _menuService.getListMenu();
            ViewData["listMenu"] = listMenu;
            return View(data);
        }
        [HttpPost]
        public ActionResult PostUpdateMenuRole(MenuRole menuRole)
        {
            MenuRole data = _menuRoleService.Get(menuRole.ID);
            try
            {
                if (GetMenuByMenuIDandRoleID(menuRole.MenuID, menuRole.RoleID) == null) //if editing menu dan role seluruhnya berbeda
                {

                    data.MenuID = menuRole.MenuID;
                    data.RoleID = menuRole.RoleID;
                    data.IsActive = menuRole.IsActive;
                    data.IsCreate = menuRole.IsCreate;
                    data.IsView = menuRole.IsView;
                    data.IsUpdate = menuRole.IsUpdate;
                    data.IsDelete = menuRole.IsDelete;
                    data.UpdatedBy = Session["username"] as string;



                    _menuRoleService.Save(data);

                    return Json(new ServiceResponse { status = 200, message = "Update Menu Role Berhasil!" });
                }
                else if (menuRole.MenuID == data.MenuID && menuRole.RoleID == data.RoleID) {
                    data.MenuID = menuRole.MenuID;
                    data.RoleID = menuRole.RoleID;
                    data.IsActive = menuRole.IsActive;
                    data.IsCreate = menuRole.IsCreate;
                    data.IsView = menuRole.IsView;
                    data.IsUpdate = menuRole.IsUpdate;
                    data.IsDelete = menuRole.IsDelete;
                    data.UpdatedBy = Session["username"] as string;



                    _menuRoleService.Save(data);

                    return Json(new ServiceResponse { status = 200, message = "Update Menu Role Berhasil!" });
                }
                else { return Json(new ServiceResponse { status = 400, message = "Gagal! Menu Role Ini Telah Tersedia!" }); }


            }
            catch (Exception e)
            {
                return Json(new ServiceResponse { status = 500, message = e.Message });
            }

           
        }
        public MenuRole GetMenuByMenuIDandRoleID(long menuID, long roleID)
        {
            //ini untuk add
            return _menuRoleService.Find(m => m.RoleID == roleID && m.MenuID == menuID && m.IsDeleted == false).FirstOrDefault();
        }

    }
}