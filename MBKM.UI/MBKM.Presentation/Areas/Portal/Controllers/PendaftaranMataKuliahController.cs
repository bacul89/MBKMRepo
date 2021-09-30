using MBKM.Common.Helpers;
using MBKM.Entities.Models.MBKM;
using MBKM.Entities.ViewModel;
using MBKM.Presentation.models;
using MBKM.Services;
using MBKM.Services.MBKMServices;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MBKM.Presentation.Areas.Portal.Controllers
{
    public class PendaftaranMataKuliahController : Controller
    {
        private IPendaftaranMataKuliahService _pmkService;
        private IMahasiswaService _mahasiswaService;
        public PendaftaranMataKuliahController(IPendaftaranMataKuliahService pmkService, IMahasiswaService mahasiswaService)
        {
            _pmkService = pmkService;
            _mahasiswaService = mahasiswaService;
        }
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GetFakultas(string search)
        {
            string email = Session["email"] as string;
            var result = GetMahasiswaByEmail(email);
            return Json(_pmkService.GetFakultas(result.JenjangStudi, search), JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetProdiByFakultas(string idFakultas, string search)
        {
            string email = Session["email"] as string;
            var result = GetMahasiswaByEmail(email);
            return Json(_pmkService.GetProdiByFakultas(result.JenjangStudi, idFakultas, search), JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetLokasiByProdi(string idProdi, string search)
        {
            string email = Session["email"] as string;
            var result = GetMahasiswaByEmail(email);
            return Json(_pmkService.GetProdiByFakultas(result.JenjangStudi, idProdi, search), JsonRequestBehavior.AllowGet);
        }
        public Mahasiswa GetMahasiswaByEmail(string email)
        {
            return _mahasiswaService.Find(m => m.Email == email).FirstOrDefault();
        }
    }
}