using MBKM.Entities.Models;
using MBKM.Repository.Repositories;
using MBKM.Services;
using MBKM.Services.MBKMServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MBKM.Presentation.Helper;
using System.Web.Mvc;

namespace MBKM.Presentation.Areas.Admin.Controllers.TemplateEmail
{
    [MBKMAuthorize]
    public class TemplateEmailController : Controller
    {

        private IEmailTemplateService _emailTemplateService;

        public TemplateEmailController(IEmailTemplateService emailTemplateService)
        {
            _emailTemplateService = emailTemplateService;
        }


        // GET: Admin/EmailTemplate
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetDataEmailTemplate()
        {
            var data = _emailTemplateService.GetAll();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        /*Modal Created*/
        public ActionResult ModalCreateEmailTemplate()
        {
            return View("ModalCreateEmailTemplate");
        }

        [HttpPost]
        public ActionResult PostDataEmailTemplate(EmailTemplate email)
        {
            email.CreatedBy = HttpContext.Session["nopegawai"].ToString();
            email.CreatedDate = DateTime.Now;
            _emailTemplateService.Save(email);
            return Json(email);
        }

        /*Modal Update*/
        public ActionResult ModalUpdateEmailTemplate(int id)
        {
            var data = _emailTemplateService.Get(id);
            return View(data);
        }

        [HttpPost]
        public ActionResult PostUpdateEmailTemplate(EmailTemplate emailTemplate)
        {
            EmailTemplate data = _emailTemplateService.Get(emailTemplate.ID);
            data.TipeMail = emailTemplate.TipeMail;
            data.SubjectMail = emailTemplate.SubjectMail;
            data.BodyMail = emailTemplate.BodyMail;
            data.IsActive = emailTemplate.IsActive;
            data.UpdatedBy = HttpContext.Session["nopegawai"].ToString();
            data.UpdatedDate = DateTime.Now;
            _emailTemplateService.Save(data);

            return Json(data);
        }


        /*Modal Detail*/
        public ActionResult ModalDetailEmailTemplate(int id)
        {
            var data = _emailTemplateService.Get(id);
            return View(data);
        }

        [HttpPost]
        public ActionResult PostDeleteEmailTemplate(int id)
        {
            EmailTemplate data = _emailTemplateService.Get(id);
            data.IsDeleted = true;

            _emailTemplateService.Save(data);

            return Json(data);
        }

    }
}