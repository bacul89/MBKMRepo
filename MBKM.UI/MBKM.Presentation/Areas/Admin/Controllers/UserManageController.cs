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
        public UserManageController(IUserService userService)
        {
            _userService = userService;
        }
        // GET: Admin/UserManage
        public ActionResult Index()
        {
            return View();
        }

    }
}