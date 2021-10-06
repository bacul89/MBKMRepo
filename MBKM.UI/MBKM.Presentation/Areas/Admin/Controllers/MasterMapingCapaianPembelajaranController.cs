using MBKM.Common.Helpers;
using MBKM.Common.Interfaces.RepoInterfaces.MBKMRepoInterfaces;
using MBKM.Entities.Models.MBKM;
using MBKM.Entities.ViewModel;
using MBKM.Presentation.models;
using MBKM.Services;
using MBKM.Services.MBKMServices;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;



using MBKM.Entities.Models;
using MBKM.Repository.Repositories;
using MBKM.Presentation.Helper;

namespace MBKM.Presentation.Areas.Admin.Controllers
{
    public class MasterMapingCapaianPembelajaranController : Controller
    {


        private ICPLMatakuliahService _cplMatakuliah;
        private ILookupService _lookupService;
        private IJadwalKuliahService _jkService;
        private IMasterCapaianPembelajaranService _mcpService;

        public MasterMapingCapaianPembelajaranController(ICPLMatakuliahService cplMatakuliah, ILookupService lookupService, IJadwalKuliahService jkService, IMasterCapaianPembelajaranService mcpService)
        {
            _cplMatakuliah = cplMatakuliah;
            _lookupService = lookupService;
            _jkService = jkService;
            _mcpService = mcpService;
        }

        /*        public string arr { get; set; }
                public string brr { get; set; }
                public string crr { get; set; }
                public string drr { get; set; }
                public string err { get; set; }*/

        // GET: Admin/MasterMapingCapaianPembelajaran
        public ActionResult Index()
        {
            Session["username"] = "Smitty Werben Jeger Man Jensen";
            return View();
        }

        public class MyJsonObject
        {
            [Newtonsoft.Json.JsonProperty("ID")]
            public string ID { get; set; }

            [Newtonsoft.Json.JsonProperty("arr")]
            public string arr { get; set; }

            [Newtonsoft.Json.JsonProperty("brr")]
            public string brr { get; set; }

            [Newtonsoft.Json.JsonProperty("crr")]
            public string crr { get; set; }

            [Newtonsoft.Json.JsonProperty("drr")]
            public string drr { get; set; }

            [Newtonsoft.Json.JsonProperty("err")]
            public string err { get; set; }

        }


        public JsonResult GetList(DataTableAjaxPostModel model)
        {
            VMListMapingCPL vMListCPL = _cplMatakuliah.GetListMapingCPL(model);
            return Json(new
            {
                // this is what datatables wants sending back
                draw = model.draw,
                recordsTotal = vMListCPL.TotalCount,
                recordsFiltered = vMListCPL.TotalFilterCount,
                data = vMListCPL.gridDatas
            });
        }



/*        public ActionResult GetDataMasterMapingCapaianPembelajaran()
        {
            //var jsonString = "[{\"ID\":\"1\",\"arr\":\"john\",\"brr\":22,\"crr\":\"mca\",\"drr\":\"test\",\"err\":\"hello\"},{\"ID\":\"2\",\"arr\":\"john\",\"brr\":22,\"crr\":\"mca\",\"drr\":\"test\",\"err\":\"hello\"},{\"ID\":\"3\",\"arr\":\"john\",\"brr\":22,\"crr\":\"mca\",\"drr\":\"test\",\"err\":\"hello\"},{\"ID\":\"4\",\"arr\":\"john\",\"brr\":22,\"crr\":\"mca\",\"drr\":\"test\",\"err\":\"hello\"},{\"ID\":\"5\",\"arr\":\"john\",\"brr\":22,\"crr\":\"mca\",\"drr\":\"test\",\"err\":\"hello\"},{\"ID\":\"6\",\"arr\":\"john\",\"brr\":22,\"crr\":\"mca\",\"drr\":\"test\",\"err\":\"hello\"},{\"ID\":\"7\",\"arr\":\"john\",\"brr\":22,\"crr\":\"mca\",\"drr\":\"test\",\"err\":\"hello\"},{\"ID\":\"1\",\"arr\":\"john\",\"brr\":22,\"crr\":\"mca\",\"drr\":\"test\",\"err\":\"hello\"},{\"ID\":\"1\",\"arr\":\"john\",\"brr\":22,\"crr\":\"mca\",\"drr\":\"test\",\"err\":\"hello\"},{\"ID\":\"1\",\"arr\":\"john\",\"brr\":22,\"crr\":\"mca\",\"drr\":\"test\",\"err\":\"hello\"},{\"ID\":\"1\",\"arr\":\"john\",\"brr\":22,\"crr\":\"mca\",\"drr\":\"test\",\"err\":\"hello\"},{\"ID\":\"1\",\"arr\":\"john\",\"brr\":22,\"crr\":\"mca\",\"drr\":\"test\",\"err\":\"hello\"},{\"ID\":\"1\",\"arr\":\"john\",\"brr\":22,\"crr\":\"mca\",\"drr\":\"test\",\"err\":\"hello\"},{\"ID\":\"1\",\"arr\":\"john\",\"brr\":22,\"crr\":\"mca\",\"drr\":\"test\",\"err\":\"hello\"},{\"ID\":\"1\",\"arr\":\"john\",\"brr\":22,\"crr\":\"mca\",\"drr\":\"test\",\"err\":\"hello\"},{\"ID\":\"1\",\"arr\":\"john\",\"brr\":22,\"crr\":\"mca\",\"drr\":\"test\",\"err\":\"hello\"},{\"ID\":\"1\",\"arr\":\"john\",\"brr\":22,\"crr\":\"mca\",\"drr\":\"test\",\"err\":\"hello\"},{\"ID\":\"1\",\"arr\":\"john\",\"brr\":22,\"crr\":\"mca\",\"drr\":\"test\",\"err\":\"hello\"},{\"ID\":\"1\",\"arr\":\"john\",\"brr\":22,\"crr\":\"mca\",\"drr\":\"test\",\"err\":\"hello\"},{\"ID\":\"1\",\"arr\":\"john\",\"brr\":22,\"crr\":\"mca\",\"drr\":\"test\",\"err\":\"hello\"}]";
            //List<MyJsonObject> myJsonObjects = Newtonsoft.Json.JsonConvert.DeserializeObject<List<MyJsonObject>>(jsonString);

            var data = _cplMatakuliah.Find(x => x.IsDeleted == false).ToList();
            *//*return Json(data, JsonRequestBehavior.AllowGet);*/

            /*            return Json(new
                        {
                            KodeMataKuliah = data.MataKuliah,
                            NamaMataKuliah = data.NamaMataKuliah,
                            MasterCapaianPembelajaranID = data.MasterCapaianPembelajaranID,
                            Kelompok = data.Kelompok,
                            capaian = data.MasterCapaianPembelajaranID.capaian
                        }, JsonRequestBehavior.AllowGet);
                        *//*

            return Json(data, JsonRequestBehavior.AllowGet);


        }*/

        /*Modal Created*/
        public ActionResult ModalCreateMasterMapingCapaianPembelajaran()
        {
            return View("ModalCreateMasterMapingCapaianPembelajaran");
        }

        [HttpPost]
        public ActionResult PostDataMasterMapingCapaianPembelajaran(CPLMatakuliah cpl)
        {
/*            cpl.CreatedBy = Session["username"] as string;
            cpl.UpdatedBy = Session["username"] as string;*/


            _cplMatakuliah.Save(cpl);
            return Json(cpl);
        }

        /*Modal Update*/
        public ActionResult ModalUpdateMasterMapingCapaianPembelajaran(int id)
        {
            /*  var data = "";
                      if (id != null)
                        {*/
            var json = "[{\"arr\":\"john\",\"brr\":22,\"crr\":\"mca\",\"drr\":\"test\",\"err\":\"hello\"}]";
            List<MyJsonObject> myJsonObjects = Newtonsoft.Json.JsonConvert.DeserializeObject<List<MyJsonObject>>(json);
            var data = myJsonObjects[0];

            /* 
          }*/
            /*
                        var myJsonObjects = Newtonsoft.Json.JsonConvert.DeserializeObject(data);*/

            return View(data);
        }

        /*        [HttpPost]
                public ActionResult PostUpdateMasterMapingCapaianPembelajaran(Lookup lookup)
                {

                    Lookup data = _lookupService.Get(lookup.ID);
                    data.Tipe = lookup.Tipe;
                    data.Nama = lookup.Nama;
                    data.Nilai = lookup.Nilai;
                    data.IsActive = lookup.IsActive;
                    data.UpdatedBy = Session["username"] as string;



                    _lookupService.Save(data);

                    return Json(data);
                }*/

        [HttpPost]
        public ActionResult PostDeleteMasterMapingCapaianPembelajaran(int id)
        {
            var data = _cplMatakuliah.Get(id);
            data.IsDeleted = true;
            /*data.UpdatedBy = Session["username"] as string;*/

            _cplMatakuliah.Save(data);
            return Json(data);
        }



        /*Modal Detail*/
        public ActionResult ModalDetailMasterMapingCapaianPembelajaran(int id)
        {
            var data = _cplMatakuliah.Get(id);
            return View(data);
        }

        public ActionResult GetMataKuliah(string search, int skip, int length)
        {


            int pageNumber = skip;
            int pageSize = length;
            /*string search = "";*/
            /*string email = Session["email"] as string;*/
            /*var result = GetMa=takuliah(email);*/
            return Json(_mcpService.GetMatkul(pageNumber, pageSize, search), JsonRequestBehavior.AllowGet);
        }

        /*        public ActionResult getLookupByValue(string tipe, string value)
                {
                    return Json(_lookupService.Find(l => l.Nilai == value && l.Tipe == tipe).FirstOrDefault(), JsonRequestBehavior.AllowGet); ;
                }*/



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

        public ActionResult GetLokasiByProdi(string JenjangStudi, string idProdi, string search)
        {
            return Json(_mcpService.GetLokasiByProdi(JenjangStudi, idProdi, search), JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetMataKuliahByProdi(int idProdi, string lokasi)
        {
            return new ContentResult { Content = JsonConvert.SerializeObject(_jkService.Find(jk => jk.ProdiID == idProdi && jk.Lokasi == lokasi).ToList()), ContentType = "application/json" };
        }

        public ActionResult GetMataKuliahByID(int idProdi, string lokasi, string MataKuliahID)
        {
            return new ContentResult { Content = JsonConvert.SerializeObject(_jkService.Find(jk => jk.ProdiID == idProdi && jk.Lokasi == lokasi && jk.MataKuliahID == MataKuliahID).ToList()), ContentType = "application/json" };
        }

        public ActionResult GetMasterCPL(string idProdi, string idFakultas, string Kelompok)
        {

            /*var vMListCPL = ;*/
            return new ContentResult
            {
                Content = JsonConvert.SerializeObject(_mcpService.Find(mcp => mcp.ProdiID == idProdi && mcp.FakultasID == idFakultas && mcp.Kelompok == Kelompok).ToList()),ContentType = "application/json"
            };
        }
        public ActionResult GetMasterCPLByID(int id)
        {
/*
            var data = _mcpService.Get(id);*/
            return Json(_mcpService.Get(id), JsonRequestBehavior.AllowGet);
            /*var vMListCPL = ;*/
/*            return new ContentResult
            {
                Content = JsonConvert.SerializeObject(),
                ContentType = "application/json"
            };*/
        }
        


    }
}