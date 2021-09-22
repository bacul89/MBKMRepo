using MBKM.Entities.Models.MBKM;
using MBKM.Presentation.models;
using MBKM.Services;
using MBKM.Services.MBKMServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MBKM.Presentation.Areas.Portal.Controllers
{
    public class DataDiriController : Controller
    {
        private IMahasiswaService _mahasiswaService;
        private ILookupService _lookupService;
        public DataDiriController(IMahasiswaService mahasiswaService, ILookupService lookupService)
        {
            _mahasiswaService = mahasiswaService;
            _lookupService = lookupService;
        }
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult GetDataMahasiswa(Mahasiswa mahasiswa)
        {
            string email = Session["email"] as string;
            return Json(GetMahasiswaByEmail(email), JsonRequestBehavior.AllowGet);
        }
        public JsonResult getLookupByValue(string tipe, string value)
        {
            return Json(_lookupService.Find(l => l.Nilai == value && l.Tipe == tipe).FirstOrDefault(), JsonRequestBehavior.AllowGet); ;
        }
        public Mahasiswa GetMahasiswaByEmail(string email)
        {
            return _mahasiswaService.Find(m => m.Email == email).FirstOrDefault();
        }
        public JsonResult UpdateDataDiri(Mahasiswa mahasiswa)
        {
            try
            {
                mahasiswa.StatusVerifikasi = "MENUNGGU VERIFIKASI";
                mahasiswa.UpdatedDate = DateTime.Now;
                _mahasiswaService.Save(mahasiswa);
                Session["status"] = "MENUNGGU VERIFIKASI";
                return Json(new ServiceResponse { status = 200, message = "Data diri berhasil diupdate!" });
            }
            catch (Exception e)
            {
                return Json(new ServiceResponse { status = 500, message = e.Message });
            }
        }
    }
}