using MBKM.Common.Helpers;
using MBKM.Entities.Models;
using MBKM.Entities.ViewModel;
using MBKM.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MBKM.Presentation.Areas.Admin.Controllers
{
    public class UserManageController : Controller
    {
        private readonly IUserService _userService;
        private ILookupService _lookupService;
        //private IRoleService _roleService;
        private readonly IRoleService _roleService;
        public UserManageController(ILookupService lookupService,IUserService userService, IRoleService roleService)
        {
            _userService = userService;
            _roleService = roleService;
            _lookupService = lookupService;
        }
        // GET: Admin/UserManage
        public ActionResult Index()
        {

           
            return View();
        }
        public ActionResult ModaladdUser()
        {
            IEnumerable<VMLookup> listJabatan = _roleService.getLookupRole();
            ViewBag.listJabatan = new SelectList(listJabatan, "Nilai", "Nama");

            IEnumerable<VMListProdi> listProdi = _lookupService.getListProdi();
            ViewBag.listProdi = new SelectList(listProdi, "IDProdi", "NamaProdi");

            IEnumerable<VMListProdi> listNProdi = _lookupService.getListProdi();
            ViewBag.listNProdi = new SelectList(listNProdi, "NamaProdi", "IDProdi");


            Debug.WriteLine("akses index");
            return View("_AddUser");
        }
        public ActionResult ModalEditUser(int id)
        {
            IEnumerable<VMLookup> listJabatan = _roleService.getLookupRole();
            ViewBag.listJabatan = new SelectList(listJabatan, "Nilai", "Nama");
            IEnumerable<VMListProdi> listProdi = _lookupService.getListProdi();
            ViewBag.listProdi = new SelectList(listProdi, "IDProdi", "NamaProdi");

            var model = _userService.Get(id);
            return View("_EditUser", model);
        }
        public ActionResult ModalDetailUser(int id)
        {
            IEnumerable<VMListProdi> listProdi = _lookupService.getListProdi();
            ViewBag.listProdi = new SelectList(listProdi, "IDProdi", "NamaProdi");
            var model = _userService.Get(id);

            return View("_DetailUser", model);
        }

        [HttpPost]
        public ActionResult PostDataUser(User model)
        {
          
            model.NoTelp = "123";
            //model.Password = HashPasswordService.HashPassword(model.Password);
            model.CreatedDate = DateTime.Now;
            model.UpdatedDate = DateTime.Now;
            model.IsDeleted = false;
            model.IsActive = model.IsActive;

            _userService.Save(model);
            return Json(model);
        }

        //edit user
        [HttpPost]
        public ActionResult PostUpdateDataUser(User model)
        {
            User data = _userService.Get(model.ID);
            data.NoPegawai = model.NoPegawai;
            data.UserName = model.UserName;
            data.Email = model.Email;
            data.NoTelp = data.NoTelp;
            data.Password = model.Password;
            data.RoleID = model.RoleID;
            //data.Password = HashPasswordService.HashPassword(model.Password);
            data.CreatedDate = data.CreatedDate;
            data.UpdatedDate = DateTime.Now;
            data.IsActive = model.IsActive;
            data.KodeProdi = model.KodeProdi;
            data.NamaProdi = model.NamaProdi;
            _userService.Save(data);

            return Json(data);
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