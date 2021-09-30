using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MBKM.Presentation.Helper;
using System.Web.Mvc;

namespace MBKM.Presentation.Areas.Admin.Controllers
{
    public class ApprovalPendaftaranMatakuliahController : Controller
    {
        [MBKMAuthorize]
        // GET: Admin/ApprovalPendaftaranMatakuliah
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult DetailCPL()
        {
            return View();
        }
    }
}