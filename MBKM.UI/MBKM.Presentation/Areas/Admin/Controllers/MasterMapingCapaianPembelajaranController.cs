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
using MBKM.Presentation.Helper;


namespace MBKM.Presentation.Areas.Admin.Controllers
{

    [MBKMAuthorize]
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


        // GET: Admin/MasterMapingCapaianPembelajaran
        public ActionResult Index()
        {
            Session["username"] = "Smitty Werben Jeger Man Jensen";
            return View();
        }

/*        public class MyJsonObject
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

        }*/


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

        public JsonResult SearchList(DataTableAjaxPostModel model, string idProdi, string lokasi, string idFakultas, string jenjangStudi, string idMatakuliah)
        {
            VMListMapingCPL vMListCPL = _cplMatakuliah.SearchListMapingCPL(model, idProdi, lokasi, idFakultas, jenjangStudi,idMatakuliah);
            return Json(new
            {
                // this is what datatables wants sending back
                draw = model.draw,
                recordsTotal = vMListCPL.TotalCount,
                recordsFiltered = vMListCPL.TotalFilterCount,
                data = vMListCPL.gridDatas
            });
        }

        public ActionResult GetSearch(string idProdi, string lokasi, string idFakultas, string jenjangStudi, string idMatakuliah)
        {
            List<CPLMatakuliah> MVCpl = new List<CPLMatakuliah>();
            List<string> mapCPL = new List<string>();
            foreach (var item in _cplMatakuliah.Find(MapCpl => MapCpl.IDMataKUliah == idMatakuliah && MapCpl.MasterCapaianPembelajarans.Lokasi == lokasi && MapCpl.MasterCapaianPembelajarans.ProdiID == idProdi && MapCpl.MasterCapaianPembelajarans.FakultasID == idFakultas && MapCpl.MasterCapaianPembelajarans.JenjangStudi == jenjangStudi ).ToList())
            {
                if (!mapCPL.Contains(item.NamaMataKuliah))
                {
                    MVCpl.Add(item);
                    mapCPL.Add(item.NamaMataKuliah);
                }
            }
            return new ContentResult { Content = JsonConvert.SerializeObject(MVCpl), ContentType = "application/json" };
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
            cpl.CreatedBy = Session["username"] as string;
            cpl.UpdatedBy = Session["username"] as string;
            _cplMatakuliah.Save(cpl);
            return Json(cpl);
        }

        /*Modal Update*/
        public ActionResult ModalUpdateMasterMapingCapaianPembelajaran(int id)
        {
            /*  var data = "";
                      if (id != null)
                        {*/
            /*var json = "[{\"arr\":\"john\",\"brr\":22,\"crr\":\"mca\",\"drr\":\"test\",\"err\":\"hello\"}]";
            List<MyJsonObject> myJsonObjects = Newtonsoft.Json.JsonConvert.DeserializeObject<List<MyJsonObject>>(json);
            var data = myJsonObjects[0];*/

            /* 
          }*/
            /*
                        var myJsonObjects = Newtonsoft.Json.JsonConvert.DeserializeObject(data);*/
            var data = _cplMatakuliah.Get(id);
            return View(data);
        }

        [HttpPost]
        public ActionResult PostUpdateMasterMapingCapaianPembelajaran(CPLMatakuliah cplMatakuliah)
        {

            try
            {
                CPLMatakuliah data = _cplMatakuliah.Get(cplMatakuliah.ID);

                data.KodeMataKuliah = cplMatakuliah.KodeMataKuliah;
                data.IDMataKUliah = cplMatakuliah.IDMataKUliah;
                data.NamaMataKuliah = cplMatakuliah.NamaMataKuliah;
                data.IsActive = cplMatakuliah.IsActive;
                data.MasterCapaianPembelajaranID = cplMatakuliah.MasterCapaianPembelajaranID;
                data.Kelompok = cplMatakuliah.Kelompok;
                data.UpdatedBy = Session["username"] as string;

                _cplMatakuliah.Save(data);
            }
            catch (Exception e)
            {
                return Json(new ServiceResponse { status = 500, message = "Data Gagal dihapus!!!" });
            }

            return Json(new ServiceResponse { status = 200, message = "Data Berhasil dihapus.." });
        }

        [HttpPost]
        public ActionResult PostDeleteMasterMapingCapaianPembelajaran(int id)
        {

            try
            {
                var data = _cplMatakuliah.Get(id);
                data.IsDeleted = true;
                data.UpdatedBy = Session["username"] as string;

                _cplMatakuliah.Save(data);
            }
            catch (Exception e)
            {
                return Json(new ServiceResponse { status = 500, message = "Data Gagal dihapus!!!" });
            }

            return Json(new ServiceResponse { status = 200, message = "Data Berhasil dihapus.." });

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

        public ActionResult GetLokasiByProdi(string JenjangStudi, string namaProdi, string search)
        {
            return Json(_mcpService.GetLokasiByProdi(JenjangStudi, namaProdi, search), JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetMataKuliahByProdi(string namaProdi, string lokasi)
        {
            //return new ContentResult { Content = JsonConvert.SerializeObject(_jkService.Find(jk => jk.NamaProdi == namaProdi && jk.Lokasi == lokasi).ToList()), ContentType = "application/json" };
            List<JadwalKuliah> jks = new List<JadwalKuliah>();
            List<string> jadwalKuliahs = new List<string>();
            foreach (var item in _jkService.Find(jk => jk.NamaProdi == namaProdi && jk.Lokasi == lokasi).ToList())
            {
                if (!jadwalKuliahs.Contains(item.NamaMataKuliah))
                {
                    jks.Add(item);
                    jadwalKuliahs.Add(item.NamaMataKuliah);
                }
            }
            return new ContentResult { Content = JsonConvert.SerializeObject(jks), ContentType = "application/json" };

        }

        public ActionResult GetMataKuliahByID(string MataKuliahID)
        {
            return new ContentResult { Content = JsonConvert.SerializeObject(_jkService.Find(jk => jk.MataKuliahID == MataKuliahID).ToList()), ContentType = "application/json" };
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

            var data = _mcpService.Get(id);
            return Json(new
                    {
                        Capaian = data.Capaian,
                        Kelompok = data.Kelompok,
                        ID = data.ID
                    }, JsonRequestBehavior.AllowGet);
            /*var vMListCPL = ;*/
            /*            return new ContentResult
                        {
                            Content = JsonConvert.SerializeObject(),
                            ContentType = "application/json"
                        };*/
        }



    }
}