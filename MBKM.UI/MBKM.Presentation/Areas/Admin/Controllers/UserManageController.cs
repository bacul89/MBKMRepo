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
    public class UserManageController : Controller
    {
        private readonly IUserService _userService;
        private readonly IRoleService _roleService;
        public UserManageController(IUserService userService, IRoleService roleService)
        {
            _userService = userService;
            _roleService = roleService;
        }
        // GET: Admin/UserManage
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ModaladdUser()
        {
            return View("_AddUser");
        }
        public ActionResult ModalEditUser(int id)
        {
            var model = _userService.Get(id);
            return View("_EditUser", model);
        }
        public ActionResult ModalDetailUser(int id)
        {
            var model = _userService.Get(id);
            return View("_DetailUser", model);
        }

        [HttpPost]
        public ActionResult PostDataUser(User model)
        {
            _userService.Save(model);
            return Json(model);
        }

        [HttpPost]
        public JsonResult GetList(DataTableAjaxPostModel model)
        {
            VMListUser vMListUser = _userService.getListUserGrid(model);
            return Json(new
            {
                // this is what datatables wants sending back
                draw = model.draw,
                recordsTotal = vMListUser.TotalCount,
                recordsFiltered = vMListUser.TotalFilterCount,
                data = vMListUser.gridDatas
            });
        }

    }
}