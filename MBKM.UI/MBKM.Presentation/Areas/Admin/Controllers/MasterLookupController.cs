using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MBKM.Entities.Models;
using MBKM.Repository.Repositories;
using MBKM.Services;
using MBKM.Services.MBKMServices;
using MBKM.Presentation.Helper;


namespace MBKM.Presentation.Areas.Admin.Controllers
{
    [MBKMAuthorize]
    public class MasterLookupController : Controller
    {
        
        private Services.ILookupService _lookupService;

        public MasterLookupController(ILookupService lookupService)
        {
            _lookupService = lookupService;
        }



        // GET: Admin/MasterLookup
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetDataMasterLookup()
        {
            var data = _lookupService.Find(x => x.IsDeleted == false);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        /*Modal Created*/
        public ActionResult ModalCreateMasterLookup()
        {
            return View("ModalCreateMasterLookup");
        }

        [HttpPost]
        public ActionResult PostDataMasterLookup(Lookup lookup)
        {
            /*            Console.WriteLine("Test");
                        Console.WriteLine(lookup);*/
            lookup.CreatedBy = Session["username"] as string;
            lookup.UpdatedBy = Session["username"] as string;
            lookup.UpdatedDate = DateTime.Now;
            lookup.CreatedDate = DateTime.Now;
            _lookupService.Save(lookup);
            return Json(lookup);
        }

        /*Modal Update*/
        public ActionResult ModalUpdateMasterLookup(int id)
        {
            var data = _lookupService.Get(id);
            return View(data);
        }

        [HttpPost]
        public ActionResult PostUpdateMasterLookup(Lookup lookup)
        {

            Lookup data = _lookupService.Get(lookup.ID);
            data.Tipe = lookup.Tipe;
            data.Nama = lookup.Nama;
            data.Nilai = lookup.Nilai;
            data.IsActive = lookup.IsActive;
            data.UpdatedBy = Session["username"] as string;
            data.UpdatedDate = DateTime.Now;



            _lookupService.Save(data);

            return Json(data);
        }

        [HttpPost]
        public ActionResult PostDeleteMasterLookup(int id)
        {
            var data = _lookupService.Get(id);
            data.IsDeleted = true;
            data.UpdatedBy = Session["username"] as string;
            data.UpdatedDate = DateTime.Now;

            _lookupService.Save(data);
            return Json(data);
        }



        /*Modal Detail*/
        public ActionResult ModalDetailMasterLookup(int id)
        {
            var data = _lookupService.Get(id);
            return View(data);
        }

    }
}