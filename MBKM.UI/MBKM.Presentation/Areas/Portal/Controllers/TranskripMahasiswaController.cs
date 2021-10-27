using MBKM.Services.MBKMServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MBKM.Entities.ViewModel;
using MBKM.Presentation.models;

using MBKM.Presentation.Helper;
using MBKM.Common.Helpers;
using MBKM.Entities.Models.MBKM;
using Newtonsoft.Json;

namespace MBKM.Presentation.Areas.Portal.Controllers
{
    public class TranskripMahasiswaController : Controller
    {



        private INilaiKuliahService _transkripService;
        private IJadwalKuliahMahasiswaService _jkMhsService;
        private IMahasiswaService _mahasiswaService;

        public TranskripMahasiswaController(INilaiKuliahService transkripService, IJadwalKuliahMahasiswaService jkMhsService, IMahasiswaService mahasiswaService)
        {
            
            _transkripService = transkripService;
            _mahasiswaService = mahasiswaService;
            _jkMhsService = jkMhsService;
        }



        // GET: Portal/Transkrip
        public ActionResult Index()
        {
            Session["nama"] = "Smitty Swagger Werben Jeger Man Jensen";
            Session["email"] = "sabangsasabana@gmail.com";
            //var mahasiswa = GetMahasiswaByEmail(Session["email"] as string);
            string email = Session["email"] as string;
            
            Mahasiswa model = _mahasiswaService.Find(m => m.Email == email).FirstOrDefault();
            return View(model);
        }


        public Mahasiswa GetMahasiswaByEmail(string email)
        {
            return _mahasiswaService.Find(m => m.Email == email).FirstOrDefault();
        }
    }
}