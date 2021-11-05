using MBKM.Presentation.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MBKM.Presentation.Areas.Admin.Controllers
{
    [MBKMAuthorize]
    public class DaftarHadirController : Controller
    {
        // GET: Admin/DaftarHadir
        public ActionResult Index()
        {
            return View();
        }
    }
}