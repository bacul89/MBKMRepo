using MBKM.Common.Helpers;
using MBKM.Entities.Models;
using MBKM.Entities.ViewModel;
using MBKM.Presentation.Helper;
using MBKM.Presentation.models;
using MBKM.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MBKM.Presentation.Areas.Admin.Controllers
{
    [MBKMAuthorize]
    public class RoleController : Controller
    {
        private IRoleService _roleService;
        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }
        // GET: Admin/Role
        public ActionResult Index()
        {
            return View();
        }
        //get data table
        [HttpPost]
        public JsonResult GetDataRole(DataTableAjaxPostModel model)
        {
            VMListRole vMListRole = _roleService.getRole(model);
            return Json(new
            {
                // this is what datatables wants sending back
                draw = model.draw,
                recordsTotal = vMListRole.TotalCount,
                recordsFiltered = vMListRole.TotalFilterCount,
                data = vMListRole.gridDatas
            });
        }
        /*Modal Created*/
        public ActionResult ModalCreateRole()
        {
            return View("ModalCreateRole");
        }
        [HttpPost]
        public ActionResult PostDataRole(Role role)
        {
            /*            Console.WriteLine("Test");
                        Console.WriteLine(lookup);*/
            role.CreatedBy = Session["username"] as string;
            role.UpdatedBy = Session["username"] as string;
            role.CreatedDate = DateTime.Now;
            role.UpdatedDate = DateTime.Now;
            _roleService.Save(role);
            return Json(role);
        }
        /*Modal Detail*/
        public ActionResult ModalDetailRole(int id)
        {
            var data = _roleService.Get(id);
            return View(data);
        }
        /*Delete*/
        [HttpPost]
        public ActionResult PostDeleteRole(int id)
        {
            var data = _roleService.Get(id);
            data.IsDeleted = true;
            data.UpdatedBy = Session["username"] as string;
            data.UpdatedDate = DateTime.Now;

            _roleService.Save(data);
            return Json(data);
        }

        /*Modal Update*/
        public ActionResult ModalUpdateRole(int id)
        {
            var data = _roleService.Get(id);
            return View(data);
        }
        [HttpPost]
        public ActionResult PostUpdateRole(Role role)
        {
            try
            {
                Role data = _roleService.Get(role.ID);
                data.Code = role.Code;
                data.RoleName = role.RoleName;
                data.IsActive = role.IsActive;
                data.UpdatedBy = Session["username"] as string;
                data.UpdatedDate = DateTime.Now;



                _roleService.Save(data);
                return Json(new ServiceResponse { status = 200, message = "Pendaftaran Role Berhasil!" });
            }
            catch (Exception e)
            {
                return Json(new ServiceResponse { status = 500, message = e.Message });
            }
            

            //return Json(data);
        }
    }
}