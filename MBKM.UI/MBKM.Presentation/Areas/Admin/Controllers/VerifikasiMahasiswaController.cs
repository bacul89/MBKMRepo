using MBKM.Entities.Models.MBKM;
using MBKM.Services;
using MBKM.Services.MBKMServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MBKM.Presentation.Areas.Admin.Controllers
{
    public class VerifikasiMahasiswaController : Controller
    {
        private IMahasiswaService _mahasiswaService;
        private IEmailTemplateService _emailTemplateService;

        public VerifikasiMahasiswaController(IMahasiswaService mahasiswaService, IEmailTemplateService emailTemplateService)
        {
            _mahasiswaService = mahasiswaService;
            _emailTemplateService = emailTemplateService;
        }


        // GET: Admin/VerifikasiMahasiswa
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetAllMahasiswa()
        {
            var data = _mahasiswaService.GetAll();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult IndexDetailMahasiswa(int id)
        {
            var data = _mahasiswaService.Get(id);
            return View(data);
        }
    }
}