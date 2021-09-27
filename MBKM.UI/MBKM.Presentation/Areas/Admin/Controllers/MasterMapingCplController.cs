using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MBKM.Presentation.Areas.Admin.Controllers
{
    public class MasterMapingCplController : Controller
    {

/*        public string arr { get; set; }
        public string brr { get; set; }
        public string crr { get; set; }
        public string drr { get; set; }
        public string err { get; set; }*/

        // GET: Admin/MasterMapingCpl
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


        public ActionResult GetDataMasterMapingCpl()
        {
            var jsonString = "[{\"ID\":\"1\",\"arr\":\"john\",\"brr\":22,\"crr\":\"mca\",\"drr\":\"test\",\"err\":\"hello\"},{\"ID\":\"2\",\"arr\":\"john\",\"brr\":22,\"crr\":\"mca\",\"drr\":\"test\",\"err\":\"hello\"},{\"ID\":\"3\",\"arr\":\"john\",\"brr\":22,\"crr\":\"mca\",\"drr\":\"test\",\"err\":\"hello\"},{\"ID\":\"4\",\"arr\":\"john\",\"brr\":22,\"crr\":\"mca\",\"drr\":\"test\",\"err\":\"hello\"},{\"ID\":\"5\",\"arr\":\"john\",\"brr\":22,\"crr\":\"mca\",\"drr\":\"test\",\"err\":\"hello\"},{\"ID\":\"6\",\"arr\":\"john\",\"brr\":22,\"crr\":\"mca\",\"drr\":\"test\",\"err\":\"hello\"},{\"ID\":\"7\",\"arr\":\"john\",\"brr\":22,\"crr\":\"mca\",\"drr\":\"test\",\"err\":\"hello\"},{\"ID\":\"1\",\"arr\":\"john\",\"brr\":22,\"crr\":\"mca\",\"drr\":\"test\",\"err\":\"hello\"},{\"ID\":\"1\",\"arr\":\"john\",\"brr\":22,\"crr\":\"mca\",\"drr\":\"test\",\"err\":\"hello\"},{\"ID\":\"1\",\"arr\":\"john\",\"brr\":22,\"crr\":\"mca\",\"drr\":\"test\",\"err\":\"hello\"},{\"ID\":\"1\",\"arr\":\"john\",\"brr\":22,\"crr\":\"mca\",\"drr\":\"test\",\"err\":\"hello\"},{\"ID\":\"1\",\"arr\":\"john\",\"brr\":22,\"crr\":\"mca\",\"drr\":\"test\",\"err\":\"hello\"},{\"ID\":\"1\",\"arr\":\"john\",\"brr\":22,\"crr\":\"mca\",\"drr\":\"test\",\"err\":\"hello\"},{\"ID\":\"1\",\"arr\":\"john\",\"brr\":22,\"crr\":\"mca\",\"drr\":\"test\",\"err\":\"hello\"},{\"ID\":\"1\",\"arr\":\"john\",\"brr\":22,\"crr\":\"mca\",\"drr\":\"test\",\"err\":\"hello\"},{\"ID\":\"1\",\"arr\":\"john\",\"brr\":22,\"crr\":\"mca\",\"drr\":\"test\",\"err\":\"hello\"},{\"ID\":\"1\",\"arr\":\"john\",\"brr\":22,\"crr\":\"mca\",\"drr\":\"test\",\"err\":\"hello\"},{\"ID\":\"1\",\"arr\":\"john\",\"brr\":22,\"crr\":\"mca\",\"drr\":\"test\",\"err\":\"hello\"},{\"ID\":\"1\",\"arr\":\"john\",\"brr\":22,\"crr\":\"mca\",\"drr\":\"test\",\"err\":\"hello\"},{\"ID\":\"1\",\"arr\":\"john\",\"brr\":22,\"crr\":\"mca\",\"drr\":\"test\",\"err\":\"hello\"}]";
            List<MyJsonObject> myJsonObjects = Newtonsoft.Json.JsonConvert.DeserializeObject<List<MyJsonObject>>(jsonString);

            /*var data = _lookupService.Find(x => x.IsDeleted == false);*/
            return Json(myJsonObjects, JsonRequestBehavior.AllowGet);
        }

        /*Modal Created*/
        public ActionResult ModalCreateMasterMapingCpl()
        {
            return View("ModalCreateMasterMapingCpl");
        }

/*        [HttpPost]
        public ActionResult PostDataMasterMapingCpl()
        {
            lookup.CreatedBy = Session["username"] as string;
            lookup.UpdatedBy = Session["username"] as string;
            _lookupService.Save(lookup);
            return Json(lookup);
        }*/

        /*Modal Update*/
        public ActionResult ModalUpdateMasterMapingCpl(int id)
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
        public ActionResult PostUpdateMasterMapingCpl(Lookup lookup)
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

/*        [HttpPost]
        public ActionResult PostDeleteMasterMapingCpl(int id)
        {
            var data = _lookupService.Get(id);
            data.IsDeleted = true;
            data.UpdatedBy = Session["username"] as string;

            _lookupService.Save(data);
            return Json(data);
        }*/



        /*Modal Detail*/
        public ActionResult ModalDetailMasterMapingCpl()
        {
            
            return View();
        }



    }
}