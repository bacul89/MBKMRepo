﻿using MBKM.Entities.Models;
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
            //return RedirectToAction("Index", "Home");
            if (ModelState.IsValid)
            {
                MBKM.Entities.Models.User modeldata = _userService.Find(x => x.NoPegawai == model.Username).FirstOrDefault();
                if (modeldata != null)
                {
                    if (HashPasswordService.ValidatePassword(model.Password, modeldata.Password))
                    {

                        Session["userid"] = modeldata.ID.ToString();
                        Session["username"] = modeldata.UserName.ToString();
                        Session["nopegawai"] = modeldata.NoPegawai.ToString();
                        Session["email"] = modeldata.Email.ToString();
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
    }
}