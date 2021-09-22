using MBKM.Common.Helpers;
using MBKM.Entities.Models;
using MBKM.Entities.ViewModel;
using MBKM.Presentation.models;
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

            try
            {
                if(GetUserByEmail(model.Email)==null)
                {
                    if(GetUserByNip(model.NoPegawai)==null)
                    {
                        model.NoTelp = "123";
                        model.Password = HashPasswordService.HashPassword(model.Password);
                        model.CreatedDate = DateTime.Now;
                        model.UpdatedDate = DateTime.Now;
                        model.IsDeleted = false;
                        model.IsActive = model.IsActive;

                        _userService.Save(model);
                        return Json(new ServiceResponse { status = 200, message = "Pendaftaran mahasiswa berhasil, tolong cek email dan konfirmasi akunmu!" });
                    }
                    else { return Json(new ServiceResponse { status = 400, message = "NIP sudah terdaftar!" }); }

                }
                else { return Json(new ServiceResponse { status = 400, message = "Email sudah digunakan!" }); }
                
            }
            catch (Exception e)
            {
                return Json(new ServiceResponse { status = 500, message = e.Message });
            }
            
        }

        //edit user
        [HttpPost]
        public ActionResult PostUpdateDataUser(User model)
        {
            try
            {
                User data = _userService.Get(model.ID);
                if (GetUserByEmail(model.Email) == null)
                {
                    if (GetUserByNip(model.NoPegawai) == null)
                    {
                       
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
                        //return Json(data);
                        return Json(new ServiceResponse { status = 200, message = "Pendaftaran mahasiswa berhasil, tolong cek email dan konfirmasi akunmu!" });

                    }
                    else
                    {
                        if (data.NoPegawai == model.NoPegawai && GetUserByEmail(model.Email) == null)
                        {
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
                            return Json(new ServiceResponse { status = 200, message = "Pendaftaran mahasiswa berhasil, tolong cek email dan konfirmasi akunmu!" }); ;
                        }
                    }
                    
                        return Json(new ServiceResponse { status = 400, message = "NIP sudah terdaftar!" }); 
                    


                }
                else {
                    if (data.Email==model.Email && GetUserByNip(model.NoPegawai) == null)
                    {
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
                        return Json(new ServiceResponse { status = 200, message = "Pendaftaran mahasiswa berhasil, tolong cek email dan konfirmasi akunmu!" });

                    }
                    else if(data.Email == model.Email && data.NoPegawai == model.NoPegawai)
                    {
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
                        return Json(new ServiceResponse { status = 200, message = "Pendaftaran mahasiswa berhasil, tolong cek email dan konfirmasi akunmu!" });

                    }


                    return Json(new ServiceResponse { status = 400, message = "Email sudah digunakan!" }); 
                }


                
            }
            catch (Exception e)
            {
                return Json(new ServiceResponse { status = 500, message = e.Message });
            }
            
        }
        //delete
      
        [HttpPost]
        public ActionResult PostDeleteUser(User user)
        {
            User data = _userService.Get(user.ID);
            data.NoPegawai = data.NoPegawai;
            data.UserName = data.UserName;
            data.Email = data.Email;
            data.NoTelp = data.NoTelp;
            data.Password = data.Password;
            data.RoleID = data.RoleID;
            //data.Password = HashPasswordService.HashPassword(model.Password);
            data.CreatedDate = data.CreatedDate;
            data.UpdatedDate = data.UpdatedDate;
            data.IsActive = data.IsActive;
            data.KodeProdi = data.KodeProdi;
            data.NamaProdi = data.NamaProdi;

            data.IsDeleted = true;

            //var model = _userService.Get(id);
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
        public User GetUserByEmail(string email)
        {
            return _userService.Find(m => m.Email == email && m.IsDeleted==false).FirstOrDefault();
        }
        public User GetUserByNip(string nip)
        {
            return _userService.Find(m => m.NoPegawai == nip && m.IsDeleted == false).FirstOrDefault();
        }

    }
}