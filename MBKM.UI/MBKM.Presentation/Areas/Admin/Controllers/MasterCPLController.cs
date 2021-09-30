using MBKM.Entities.ViewModel;
using MBKM.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MBKM.Presentation.Areas.Admin.Controllers
{
    public class MasterCPLController : Controller
    {
        private ILookupService _lookupService;

        public MasterCPLController(ILookupService lookupService)
        {
            _lookupService = lookupService;
        }

        // GET: Admin/MasterCPL
        public ActionResult Index()
        {
            Session["username"] = "Smitty Werben Jeger Man Jensen";
            return View();
        }
        public class MyJsonObject
        {
            [Newtonsoft.Json.JsonProperty("ID")]
            public string ID { get; set; }

            [Newtonsoft.Json.JsonProperty("Kode")]
            public string Kode { get; set; }

            [Newtonsoft.Json.JsonProperty("Kelompok")]
            public string Kelompok { get; set; }

            [Newtonsoft.Json.JsonProperty("CapaianPembelajaran")]
            public string CapaianPembelajaran { get; set; }

           

        }
        public ActionResult GetDataMasterCpl()
        {
            var jsonString = "[{\"ID\":\"1\",\"Kode\":\"john\",\"Kelompok\":22,\"CapaianPembelajaran\":\"mca\"},{\"ID\":\"2\",\"Kode\":\"john\",\"Kelompok\":22,\"CapaianPembelajaran\":\"mca\"},{\"ID\":\"3\",\"Kode\":\"john\",\"Kelompok\":22,\"CapaianPembelajaran\":\"mca\"},{\"ID\":\"4\",\"Kode\":\"john\",\"Kelompok\":22,\"CapaianPembelajaran\":\"mca\"},{\"ID\":\"5\",\"Kode\":\"john\",\"Kelompok\":22,\"CapaianPembelajaran\":\"mca\"},{\"ID\":\"6\",\"Kode\":\"john\",\"Kelompok\":22,\"CapaianPembelajaran\":\"mca\"},{\"ID\":\"7\",\"Kode\":\"john\",\"Kelompok\":22,\"CapaianPembelajaran\":\"mca\"},{\"ID\":\"1\",\"Kode\":\"john\",\"Kelompok\":22,\"CapaianPembelajaran\":\"mca\"},{\"ID\":\"1\",\"Kode\":\"john\",\"Kelompok\":22,\"CapaianPembelajaran\":\"mca\"},{\"ID\":\"1\",\"Kode\":\"john\",\"Kelompok\":22,\"CapaianPembelajaran\":\"mca\"},{\"ID\":\"1\",\"Kode\":\"john\",\"Kelompok\":22,\"CapaianPembelajaran\":\"mca\"},{\"ID\":\"1\",\"Kode\":\"john\",\"Kelompok\":22,\"CapaianPembelajaran\":\"mca\"},{\"ID\":\"1\",\"Kode\":\"john\",\"Kelompok\":22,\"CapaianPembelajaran\":\"mca\"},{\"ID\":\"1\",\"Kode\":\"john\",\"Kelompok\":22,\"CapaianPembelajaran\":\"mca\"},{\"ID\":\"1\",\"Kode\":\"john\",\"Kelompok\":22,\"CapaianPembelajaran\":\"mca\"},{\"ID\":\"1\",\"Kode\":\"john\",\"Kelompok\":22,\"CapaianPembelajaran\":\"mca\"},{\"ID\":\"1\",\"Kode\":\"john\",\"Kelompok\":22,\"CapaianPembelajaran\":\"mca\"},{\"ID\":\"1\",\"Kode\":\"john\",\"Kelompok\":22,\"CapaianPembelajaran\":\"mca\"},{\"ID\":\"1\",\"Kode\":\"john\",\"Kelompok\":22,\"CapaianPembelajaran\":\"mca\"},{\"ID\":\"1\",\"Kode\":\"john\",\"Kelompok\":22,\"CapaianPembelajaran\":\"mca\"}]";
            List<MyJsonObject> myJsonObjects = Newtonsoft.Json.JsonConvert.DeserializeObject<List<MyJsonObject>>(jsonString);

            /*var data = _lookupService.Find(x => x.IsDeleted == false);*/
            return Json(myJsonObjects, JsonRequestBehavior.AllowGet);
        }
        public ActionResult ModaladdCPL()
        {
            IEnumerable<VMListProdi> listProdi = _lookupService.getListProdi();
            ViewBag.listProdi = new SelectList(listProdi, "IDProdi", "NamaProdi");

            IEnumerable<VMListProdi> listNProdi = _lookupService.getListProdi();
            ViewBag.listNProdi = new SelectList(listNProdi, "NamaProdi", "IDProdi");
            return View("ModalCreateCPL");
        }
        public ActionResult ModalDetailMasterCpl()
        {

            return View();
        }
        public ActionResult ModalUpdateMasterCpl(int id)
        {
            /*  var data = "";
                      if (id != null)
                        {*/
            var json = "[{\"Kode\":\"john\",\"Kelompok\":22,\"CapaianPembelajaran\":\"mca\"}]";
            List<MyJsonObject> myJsonObjects = Newtonsoft.Json.JsonConvert.DeserializeObject<List<MyJsonObject>>(json);
            var data = myJsonObjects[0];

            /* 
          }*/
            /*
                        var myJsonObjects = Newtonsoft.Json.JsonConvert.DeserializeObject(data);*/

            return View(data);
        }
    }
}