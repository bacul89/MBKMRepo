using MBKM.Common.Helpers;
using MBKM.Entities.ViewModel;
using MBKM.Services.MBKMServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MBKM.Presentation.Areas.Admin.Controllers
{
    public class DaftarSeluruhMahasiswaController : Controller
    {
        // GET: Admin/DaftarSeluruhMahasiswa
        private readonly IMahasiswaService _mahasiswaService;

        public DaftarSeluruhMahasiswaController(IMahasiswaService mahasiswaService)
        {
            _mahasiswaService = mahasiswaService;
        }

        public ActionResult Index()
        {
            Session["username"] = "Smitty Werben Jeger Man Jensen";
            return View();
        }
        public class MyJsonObject
        {
            [Newtonsoft.Json.JsonProperty("ID")]
            public string ID { get; set; }

            [Newtonsoft.Json.JsonProperty("NamaUniversitas")]
            public string NamaUniversitas { get; set; }

            [Newtonsoft.Json.JsonProperty("JenjangStudi")]
            public string JenjangStudi { get; set; }

            [Newtonsoft.Json.JsonProperty("ProdiAsal")]
            public string ProdiAsal { get; set; }

            [Newtonsoft.Json.JsonProperty("NIMAsal")]
            public string NIMAsal { get; set; }

            [Newtonsoft.Json.JsonProperty("Gender")]
            public string Gender { get; set; }

            [Newtonsoft.Json.JsonProperty("Nama")]
            public string Nama { get; set; }

            [Newtonsoft.Json.JsonProperty("NoKerjasama")]
            public string NoKerjasama { get; set; }

            [Newtonsoft.Json.JsonProperty("StatusKerjasama")]
            public string StatusKerjasama { get; set; }

            [Newtonsoft.Json.JsonProperty("StatusVerifikasi")]
            public string StatusVerifikasi { get; set; }

        }
        //,\"Nama\":\"bang toib\",\"NoKerjasama\":\"xxxx\",\"StatusKerjasama\":\"ADA KERJASAMA\",\"StatusVerifikasi\":\"DAFTAR\"

        public ActionResult GetDataMasterMapingCpl()
        {
            var jsonString = "[{\"ID\":\"1\",\"NamaUniversitas\":\"john\",\"JenjangStudi\":22,\"ProdiAsal\":\"mca\",\"NIMAsal\":\"test\",\"Gender\":\"hello\",\"Nama\":\"bang toib\",\"NoKerjasama\":\"xxxx\",\"StatusKerjasama\":\"ADA KERJASAMA\",\"StatusVerifikasi\":\"DAFTAR\"},{\"ID\":\"2\",\"NamaUniversitas\":\"john\",\"JenjangStudi\":22,\"ProdiAsal\":\"mca\",\"NIMAsal\":\"test\",\"Gender\":\"hello\",\"Nama\":\"bang toib\",\"NoKerjasama\":\"xxxx\",\"StatusKerjasama\":\"ADA KERJASAMA\",\"StatusVerifikasi\":\"DAFTAR\"},{\"ID\":\"3\",\"NamaUniversitas\":\"john\",\"JenjangStudi\":22,\"ProdiAsal\":\"mca\",\"NIMAsal\":\"test\",\"Gender\":\"hello\",\"Nama\":\"bang toib\",\"NoKerjasama\":\"xxxx\",\"StatusKerjasama\":\"ADA KERJASAMA\",\"StatusVerifikasi\":\"DAFTAR\"},{\"ID\":\"4\",\"NamaUniversitas\":\"john\",\"JenjangStudi\":22,\"ProdiAsal\":\"mca\",\"NIMAsal\":\"test\",\"Gender\":\"hello\",\"Nama\":\"bang toib\",\"NoKerjasama\":\"xxxx\",\"StatusKerjasama\":\"ADA KERJASAMA\",\"StatusVerifikasi\":\"DAFTAR\"},{\"ID\":\"5\",\"NamaUniversitas\":\"john\",\"JenjangStudi\":22,\"ProdiAsal\":\"mca\",\"NIMAsal\":\"test\",\"Gender\":\"hello\",\"Nama\":\"bang toib\",\"NoKerjasama\":\"xxxx\",\"StatusKerjasama\":\"ADA KERJASAMA\",\"StatusVerifikasi\":\"DAFTAR\"},{\"ID\":\"6\",\"NamaUniversitas\":\"john\",\"JenjangStudi\":22,\"ProdiAsal\":\"mca\",\"NIMAsal\":\"test\",\"Gender\":\"hello\",\"Nama\":\"bang toib\",\"NoKerjasama\":\"xxxx\",\"StatusKerjasama\":\"ADA KERJASAMA\",\"StatusVerifikasi\":\"DAFTAR\"},{\"ID\":\"7\",\"NamaUniversitas\":\"john\",\"JenjangStudi\":22,\"ProdiAsal\":\"mca\",\"NIMAsal\":\"test\",\"Gender\":\"hello\",\"Nama\":\"bang toib\",\"NoKerjasama\":\"xxxx\",\"StatusKerjasama\":\"ADA KERJASAMA\",\"StatusVerifikasi\":\"DAFTAR\"},{\"ID\":\"1\",\"NamaUniversitas\":\"john\",\"JenjangStudi\":22,\"ProdiAsal\":\"mca\",\"NIMAsal\":\"test\",\"Gender\":\"hello\",\"Nama\":\"bang toib\",\"NoKerjasama\":\"xxxx\",\"StatusKerjasama\":\"ADA KERJASAMA\",\"StatusVerifikasi\":\"DAFTAR\"},{\"ID\":\"1\",\"NamaUniversitas\":\"john\",\"JenjangStudi\":22,\"ProdiAsal\":\"mca\",\"NIMAsal\":\"test\",\"Gender\":\"hello\",\"Nama\":\"bang toib\",\"NoKerjasama\":\"xxxx\",\"StatusKerjasama\":\"ADA KERJASAMA\",\"StatusVerifikasi\":\"DAFTAR\"},{\"ID\":\"1\",\"NamaUniversitas\":\"john\",\"JenjangStudi\":22,\"ProdiAsal\":\"mca\",\"NIMAsal\":\"test\",\"Gender\":\"hello\",\"Nama\":\"bang toib\",\"NoKerjasama\":\"xxxx\",\"StatusKerjasama\":\"ADA KERJASAMA\",\"StatusVerifikasi\":\"DAFTAR\"},{\"ID\":\"1\",\"NamaUniversitas\":\"john\",\"JenjangStudi\":22,\"ProdiAsal\":\"mca\",\"NIMAsal\":\"test\",\"Gender\":\"hello\",\"Nama\":\"bang toib\",\"NoKerjasama\":\"xxxx\",\"StatusKerjasama\":\"ADA KERJASAMA\",\"StatusVerifikasi\":\"DAFTAR\"},{\"ID\":\"1\",\"NamaUniversitas\":\"john\",\"JenjangStudi\":22,\"ProdiAsal\":\"mca\",\"NIMAsal\":\"test\",\"Gender\":\"hello\",\"Nama\":\"bang toib\",\"NoKerjasama\":\"xxxx\",\"StatusKerjasama\":\"ADA KERJASAMA\",\"StatusVerifikasi\":\"DAFTAR\"},{\"ID\":\"1\",\"NamaUniversitas\":\"john\",\"JenjangStudi\":22,\"ProdiAsal\":\"mca\",\"NIMAsal\":\"test\",\"Gender\":\"hello\",\"Nama\":\"bang toib\",\"NoKerjasama\":\"xxxx\",\"StatusKerjasama\":\"ADA KERJASAMA\",\"StatusVerifikasi\":\"DAFTAR\"},{\"ID\":\"1\",\"NamaUniversitas\":\"john\",\"JenjangStudi\":22,\"ProdiAsal\":\"mca\",\"NIMAsal\":\"test\",\"Gender\":\"hello\",\"Nama\":\"bang toib\",\"NoKerjasama\":\"xxxx\",\"StatusKerjasama\":\"ADA KERJASAMA\",\"StatusVerifikasi\":\"DAFTAR\"},{\"ID\":\"1\",\"NamaUniversitas\":\"john\",\"JenjangStudi\":22,\"ProdiAsal\":\"mca\",\"NIMAsal\":\"test\",\"Gender\":\"hello\",\"Nama\":\"bang toib\",\"NoKerjasama\":\"xxxx\",\"StatusKerjasama\":\"ADA KERJASAMA\",\"StatusVerifikasi\":\"DAFTAR\"},{\"ID\":\"1\",\"NamaUniversitas\":\"john\",\"JenjangStudi\":22,\"ProdiAsal\":\"mca\",\"NIMAsal\":\"test\",\"Gender\":\"hello\",\"Nama\":\"bang toib\",\"NoKerjasama\":\"xxxx\",\"StatusKerjasama\":\"ADA KERJASAMA\",\"StatusVerifikasi\":\"DAFTAR\"},{\"ID\":\"1\",\"NamaUniversitas\":\"john\",\"JenjangStudi\":22,\"ProdiAsal\":\"mca\",\"NIMAsal\":\"test\",\"Gender\":\"hello\",\"Nama\":\"bang toib\",\"NoKerjasama\":\"xxxx\",\"StatusKerjasama\":\"ADA KERJASAMA\",\"StatusVerifikasi\":\"DAFTAR\"},{\"ID\":\"1\",\"NamaUniversitas\":\"john\",\"JenjangStudi\":22,\"ProdiAsal\":\"mca\",\"NIMAsal\":\"test\",\"Gender\":\"hello\",\"Nama\":\"bang toib\",\"NoKerjasama\":\"xxxx\",\"StatusKerjasama\":\"ADA KERJASAMA\",\"StatusVerifikasi\":\"DAFTAR\"},{\"ID\":\"1\",\"NamaUniversitas\":\"john\",\"JenjangStudi\":22,\"ProdiAsal\":\"mca\",\"NIMAsal\":\"test\",\"Gender\":\"hello\",\"Nama\":\"bang toib\",\"NoKerjasama\":\"xxxx\",\"StatusKerjasama\":\"ADA KERJASAMA\",\"StatusVerifikasi\":\"DAFTAR\"},{\"ID\":\"1\",\"NamaUniversitas\":\"john\",\"JenjangStudi\":22,\"ProdiAsal\":\"mca\",\"NIMAsal\":\"test\",\"Gender\":\"hello\",\"Nama\":\"bang toib\",\"NoKerjasama\":\"xxxx\",\"StatusKerjasama\":\"ADA KERJASAMA\",\"StatusVerifikasi\":\"DAFTAR\"}]";
            List<MyJsonObject> myJsonObjects = Newtonsoft.Json.JsonConvert.DeserializeObject<List<MyJsonObject>>(jsonString);

            /*var data = _lookupService.Find(x => x.IsDeleted == false);*/
            return Json(myJsonObjects, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult GetList(DataTableAjaxPostModel model)
        {
            VMListMahasiswa vMListMahasiswa = _mahasiswaService.getMahasiswasNotYetVer(model);
            return Json(new
            {
                // this is what datatables wants sending back
                draw = model.draw,
                recordsTotal = vMListMahasiswa.TotalCount,
                recordsFiltered = vMListMahasiswa.TotalFilterCount,
                data = vMListMahasiswa.gridDatas
            });
        }
    }
}