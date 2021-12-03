using MBKM.Entities.Models;
using MBKM.Presentation.Models;
using MBKM.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MBKM.Presentation.Areas.Admin.Controllers
{
    
    public class AdminLoginController : Controller
    {
        private readonly IUserService _userService;
        public AdminLoginController(IUserService userService)
        {
            _userService = userService;
        }
        // GET: Admin/AdminLogin
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginVMAdmin model)
        {
            
            if (ModelState.IsValid)
            {
                MBKM.Entities.Models.User modeldata = _userService.Find(x => x.NoPegawai == model.Username && x.IsDeleted==false).FirstOrDefault();
                if (modeldata != null)
                {
                    if (HashPasswordService.ValidatePassword(model.Password, modeldata.Password))
                    {

                        Session["userid"] = modeldata.ID.ToString();
                        Session["username"] = modeldata.UserName;
                        Session["nopegawai"] = modeldata.NoPegawai.ToString();
                        Session["email"] = modeldata.Email;
                        Session["RoleName"] = modeldata.Roles.RoleName;
                        Session["RoleID"] = modeldata.RoleID.ToString();
                        Session["NamaProdi"] = modeldata.NamaProdi.ToString();
                        Session["KodeProdi"] = modeldata.KodeProdi.ToString();
                        Session["NamaFakultas"] = (modeldata.NamaFakultas == null) ? "" : modeldata.NamaFakultas.ToString();
                        Session["KodeFakultas"] = (modeldata.KodeFakultas == null) ? "" : modeldata.KodeFakultas.ToString();
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Invalid Password.");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Username Not Found");
                }
            }
            return View(model);
        }
        public ActionResult Logout()
        {
            Session["userid"] = null;
            Session["username"] = null;
            Session["nopegawai"] = null;
            Session["email"] = null;
            Session["MenuList"] = null;
            Session["NamaProdi"] = null;
            Session["KodeProdi"] = null;
            Session["NamaFakultas"] = null;
            Session["KodeFakultas"] = null;
            return RedirectToAction("Login", "AdminLogin");
        }
    }
}