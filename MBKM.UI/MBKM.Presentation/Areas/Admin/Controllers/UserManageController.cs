﻿using MBKM.Common.Helpers;
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

            //List<VMListProdi> listProdi = _lookupService.getListProdi();
            //ViewBag.listProdi = new SelectList(listProdi);

            Debug.WriteLine("akses index");
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
            //model.NoPegawai = model.NoPegawai;
            //model.UserName = model.UserName;
            //model.Email = model.Email;
            //model.Password = model.Password;
            //model.RoleID = model.RoleID
            //model.NamaProdi = model.NamaProdi;

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