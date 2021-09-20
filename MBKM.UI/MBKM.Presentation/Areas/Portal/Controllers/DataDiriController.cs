using MBKM.Entities.Models.MBKM;
using MBKM.Presentation.models;
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
        public DataDiriController(IMahasiswaService mahasiswaService)
        {
            _mahasiswaService = mahasiswaService;
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
        public Mahasiswa GetMahasiswaByEmail(string email)
        {
            return _mahasiswaService.Find(m => m.Email == email).FirstOrDefault();
        }
        public JsonResult UpdateDataDiri(Mahasiswa mahasiswa)
        {
            try
            {
                mahasiswa.UpdatedDate = DateTime.Now;
                _mahasiswaService.Save(mahasiswa);
                return Json(new ServiceResponse { status = 200, message = "Data diri berhasil diupdate!" });
            }
            catch (Exception e)
            {
                return Json(new ServiceResponse { status = 500, message = e.Message });
            }
        }
    }
}