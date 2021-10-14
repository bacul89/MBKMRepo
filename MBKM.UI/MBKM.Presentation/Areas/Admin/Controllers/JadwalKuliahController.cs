using System;
using System.Collections.Generic;
using MBKM.Common.Interfaces.RepoInterfaces.MBKMRepoInterfaces;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MBKM.Services;
using MBKM.Services.MBKMServices;


using MBKM.Entities.Models;
using MBKM.Repository.Repositories;

using MBKM.Presentation.Helper;

namespace MBKM.Presentation.Areas.Admin.Controllers
{
    public class JadwalKuliahController : Controller
    {

        private IJadwalKuliahService _jkService;
        private ILookupService _lookupService;
        private IMasterCapaianPembelajaranService _mcpService;




        public JadwalKuliahController(IJadwalKuliahService jkService, ILookupService lookupService, IMasterCapaianPembelajaranService mcpService)
        {
            _jkService = jkService;
            _lookupService = lookupService;            
            _mcpService = mcpService;
        }


        // GET: Admin/JadwalKuliah
        public ActionResult Index()
        {
            Session["username"] = "Smitty Werben Jeger Man Jensen";
            return View();
        }


        /* Lookup --<> */
        public ActionResult getLookupByTipe(string tipe)
        {
            return Json(_lookupService.getLookupByTipe(tipe), JsonRequestBehavior.AllowGet);
        }


        /* Attribute Kuliah --<> */
        public ActionResult GetFakultas(string search, string JenjangStudi)
        {

            return Json(_mcpService.GetFakultas(JenjangStudi, search), JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetProdiByFakultas(string JenjangStudi, string idFakultas, string search)
        {
            return Json(_mcpService.GetProdiByFakultas(JenjangStudi, idFakultas, search), JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetLokasiByProdi(string JenjangStudi, string namaProdi, string search)
        {
            return Json(_mcpService.GetLokasiByProdi(JenjangStudi, namaProdi, search), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult GetSemesterAll(int skip, int take, string search)
        {
            //return Json(, JsonRequestBehavior.AllowGet);



            var final = _jkService.GetSemesterAll(skip, take, search);
            List<object> data = new List<object>();
            foreach (var p in final)
            {
                var q = new
                {
                    Nama = p.Nama,
                    ID = p.ID
                };
                data.Add(q);
            }

            return Json(data);


        }

    }
}