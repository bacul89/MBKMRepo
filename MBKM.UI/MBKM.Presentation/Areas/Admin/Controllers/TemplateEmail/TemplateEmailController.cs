using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MBKM.Presentation.Areas.Admin.Controllers.TemplateEmail
{
    public class TemplateEmailController : Controller
    {
        // GET: Admin/EmailTemplate
        public ActionResult Index()
        {
            return View();
        }
    }
}