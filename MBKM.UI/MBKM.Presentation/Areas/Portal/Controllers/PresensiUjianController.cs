using MBKM.Entities.Models.MBKM;
using MBKM.Presentation.Helper;
using MBKM.Presentation.models;
using MBKM.Presentation.Models;
using MBKM.Services;
using MBKM.Services.MBKMServices;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MBKM.Presentation.Areas.Portal.Controllers
{
    [MBKMAuthorize]
    public class PresensiUjianController : Controller
    {
        private IMahasiswaService _mahasiswaService;
        private ILookupService _lookupService;
        private IAttachmentService _attachmentService;
        private IPerjanjianKerjasamaService _perjanjianKerjasamaService;
        public PresensiUjianController(IMahasiswaService mahasiswaService, ILookupService lookupService, IAttachmentService attachmentService, IPerjanjianKerjasamaService perjanjianKerjasamaService)
        {
            _mahasiswaService = mahasiswaService;
            _lookupService = lookupService;
            _attachmentService = attachmentService;
            _perjanjianKerjasamaService = perjanjianKerjasamaService;
        }
        public ActionResult Index()
        {
            return View();
        }
        public Mahasiswa GetMahasiswaByEmail(string email)
        {
            return _mahasiswaService.Find(m => m.Email == email).FirstOrDefault();
        }
    }
}