using MBKM.Common.Helpers;
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

namespace MBKM.Presentation.Areas.Admin.Controllers
{
    public class MasterCPLController : Controller
    {
        private ILookupService _lookupService;
        private IMasterCapaianPembelajaranService _mcpService;
        public MasterCPLController(ILookupService lookupService,IMasterCapaianPembelajaranService mcpService)
        {
            _lookupService = lookupService;
            _mcpService = mcpService;
        }

        // GET: Admin/MasterCPL
        public ActionResult Index()
        {
            Session["username"] = "Smitty Werben Jeger Man Jensen";
            return View();
        }
        [HttpPost]
        public JsonResult GetList(DataTableAjaxPostModel model)
        {
            VMListMasterCPL vMListCPL = _mcpService.GetListMasterCPL(model);
            return Json(new
            {
                // this is what datatables wants sending back
                draw = model.draw,
                recordsTotal = vMListCPL.TotalCount,
                recordsFiltered = vMListCPL.TotalFilterCount,
                data = vMListCPL.gridDatas
            });
        }

        [HttpPost]
        public ActionResult PostDataCPL(MasterCapaianPembelajaran model)
        {

            try
            {
               
                        //model.NoTelp = "123";
                       
                        model.CreatedDate = DateTime.Now;
                        model.UpdatedDate = DateTime.Now;
                        model.IsDeleted = false;
                        model.IsActive = model.IsActive;
                        model.CreatedBy = "admin";

                        _mcpService.Save(model);
                        return Json(new ServiceResponse { status = 200, message = "Pendaftaran CPL Berhasil!" });
                   

            }
            catch (Exception e)
            {
                return Json(new ServiceResponse { status = 500, message = e.Message });
            }

        }
        //public ActionResult ModaladdCPL()
        //{
        //    IEnumerable<VMListProdi> listProdi = _lookupService.getListProdi();
        //    ViewBag.listProdi = new SelectList(listProdi, "IDProdi", "NamaProdi");


        //    return View("ModalCreateCPL");
        //}
        //public ActionResult ModalDetailMasterCpl(int id)
        //{
        //    var model = _mcpService.Get(id);

        //    return Json(model);

        //    //return View(model);
        //}
        public ActionResult ModalDetailMasterCpl(int id)
        {
            var data = _mcpService.Get(id);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public ActionResult ModalEditMasterCpl(int id)
        {
     

            var data = _mcpService.Get(id);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        //public ActionResult ModalUpdateMasterCpl(int id)
        //{
        //    /*  var data = "";
        //              if (id != null)
        //                {*/
        //    var json = "[{\"Kode\":\"john\",\"Kelompok\":22,\"CapaianPembelajaran\":\"mca\"}]";
        //    List<MyJsonObject> myJsonObjects = Newtonsoft.Json.JsonConvert.DeserializeObject<List<MyJsonObject>>(json);
        //    var data = myJsonObjects[0];

        //    /* 
        //  }*/
        //    /*
        //                var myJsonObjects = Newtonsoft.Json.JsonConvert.DeserializeObject(data);*/

        //    return View(data);
        //}
        public ActionResult GetFakultas(string search,string jenjang)
        {
            //string email = Session["email"] as string;
            //var result = GetMahasiswaByEmail(email);
            return Json(_mcpService.GetFakultas(jenjang, search), JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetProdiByFakultas(string idFakultas, string search, string jenjang)
        {
            //string email = Session["email"] as string;
            //var result = GetMahasiswaByEmail(email);
            //return Json(_pmkService.GetFakultas(result.JenjangStudi, search), JsonRequestBehavior.AllowGet);
            return Json(_mcpService.GetProdiByFakultas(jenjang,idFakultas, search), JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetLokasiByProdi(string idProdi, string search,string jenjang)
        {
            //string email = Session["email"] as string;
            //var result = GetMahasiswaByEmail(email);
            return Json(_mcpService.GetLokasiByProdi( jenjang,idProdi, search), JsonRequestBehavior.AllowGet);
        }
        public ActionResult getLookupByTipe(string tipe)
        {
            return Json(_lookupService.getLookupByTipe(tipe), JsonRequestBehavior.AllowGet);
        }
    }
}