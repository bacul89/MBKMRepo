using MBKM.Entities.ViewModel;
using MBKM.Services;
using MBKM.Services.MBKMServices;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MBKM.Entities.Models.MBKM;
using MBKM.Common.Helpers;
using System.IO;
using MBKM.Presentation.models;

namespace MBKM.Presentation.Areas.Admin.Controllers.PerjanjianKerjasama
{
    public class PerjanjianKerjasamaController : Controller
    {
        private IMenuService _menuService;
        private ILookupService _lookupService;
        private IPerjanjianKerjasamaService _perjanjianKerjasamaService;
        public PerjanjianKerjasamaController(IMenuService menuService,
            ILookupService lookupService, IPerjanjianKerjasamaService perjanjianKerjasamaService)
        {
            _menuService = menuService;
            _lookupService = lookupService;
            _perjanjianKerjasamaService = perjanjianKerjasamaService;
        }
        // GET: Admin/PerjanjianKerjasama
        public ActionResult Index()
        {
            Debug.WriteLine("akses index");
            return View();
        }

        [HttpPost]
        public JsonResult GetListPKGrid(DataTableAjaxPostModel model)
        {
            VMListPerjanjianKerjasama data = _perjanjianKerjasamaService.getListPKGrid(model);
            return Json(new
            {
                draw = model.draw,
                recordsTotal = data.TotalCount,
                recordsFiltered = data.TotalFilterCount,
                data = data.gridDatas
            }
            );
        }
        /*View Modal Created*/
        public ActionResult ModalCreatePerjanjianKerjasama()
        {
            var listPerjanjian = _lookupService.getLookupByTipe("JenisPertukaran");
            ViewData["listPerjanjian"] = listPerjanjian;
            var listKerjasama = _lookupService.getLookupByTipe("JenisKerjasama");
            ViewData["listKerjasama"] = listKerjasama;
            return View("ModalCreatePerjanjianKerjasama");
        }
        /*Post Modal Created*/
        //[HttpPost]
        //public ActionResult PostDataKerjasama(MBKM.Entities.Models.MBKM.PerjanjianKerjasama perjanjianKerjasama)
        //{
        //    _perjanjianKerjasamaService.Save(perjanjianKerjasama);
        //    return Json(perjanjianKerjasama);
        //}

        /*Modal Detail*/
        public ActionResult ModalDetailKerjasama(int id)
        {
            var data = _perjanjianKerjasamaService.Get(id);
            return View(data);
        }

        public JsonResult SavePerjanjian(HttpPostedFileBase[] file, MBKM.Entities.Models.MBKM.PerjanjianKerjasama perjanjianKerjasama)
        {
            try
            {


                if (ModelState.IsValid)
                {
                    List<AttachmentPerjanjianKerjasama> attachments = new List<AttachmentPerjanjianKerjasama>();
                    for (int i = 0; i < file.Length; i++)
                    {
                        var files = file[i];
                        if (files != null && files.ContentLength > 0)
                        {
                            var fileName = Path.GetFileName(files.FileName);
                            AttachmentPerjanjianKerjasama attch = new AttachmentPerjanjianKerjasama()
                            {
                                FileName = fileName,
                                FileExt = Path.GetExtension(fileName),
                                FileSze = files.ContentLength,
                                IsActive = true,
                                IsDeleted = false
                            };
                            attachments.Add(attch);
                            var path = Path.Combine(Server.MapPath("~/Upload/"), attch.FileName + attch.FileExt);
                            files.SaveAs(path);
                        }
                    }
                    perjanjianKerjasama.AttachmentPerjanjianKerjasamas = attachments;
                    perjanjianKerjasama.NoPerjanjian = perjanjianKerjasama.NoPerjanjian;
                    perjanjianKerjasama.TanggalMulai = perjanjianKerjasama.TanggalMulai;
                    perjanjianKerjasama.TanggalAkhir = perjanjianKerjasama.TanggalAkhir;
                    perjanjianKerjasama.CreatedBy = perjanjianKerjasama.CreatedBy;
                    perjanjianKerjasama.CreatedDate = DateTime.Now;
                    perjanjianKerjasama.UpdatedBy = perjanjianKerjasama.UpdatedBy;
                    perjanjianKerjasama.UpdatedDate = perjanjianKerjasama.UpdatedDate;
                    perjanjianKerjasama.IsActive = true;
                    perjanjianKerjasama.IsDeleted = false;
                    perjanjianKerjasama.NamaInstansi = perjanjianKerjasama.NamaInstansi;
                    perjanjianKerjasama.NamaUnit = perjanjianKerjasama.NamaUnit;
                    perjanjianKerjasama.JenisPertukaran = perjanjianKerjasama.JenisPertukaran;
                    perjanjianKerjasama.JenisKerjasama = perjanjianKerjasama.JenisKerjasama;
                    perjanjianKerjasama.BiayaKuliah = perjanjianKerjasama.BiayaKuliah;
                    perjanjianKerjasama.Instansi = perjanjianKerjasama.Instansi;
                    //perjanjianKerjasama.UniversitasID = 0;
                    _perjanjianKerjasamaService.Save(perjanjianKerjasama);
                    return Json(new ServiceResponse { status = 200, message = "Data diri berhasil diupdate!" });
                }
                return Json(new ServiceResponse { status = 500, message = "Not Valid Model" });
            }
            catch (Exception e)
            {
                return Json(new ServiceResponse { status = 500, message = e.Message });
            }
            //return Json(new ServiceResponse { status = 500, message = e.Message });
        }


        /*Modal Update*/
        public ActionResult ModalUpdateKerjasama(int id)
        {

            var listPerjanjian = _lookupService.getLookupByTipe("JenisPertukaran");
            ViewData["listPerjanjian"] = listPerjanjian;
            var listKerjasama = _lookupService.getLookupByTipe("JenisKerjasama");
            ViewData["listKerjasama"] = listKerjasama;
            var data = _perjanjianKerjasamaService.Get(id);
            return View(data);
        }
        [HttpPost]
        public ActionResult UpdateKerjasama(MBKM.Entities.Models.MBKM.PerjanjianKerjasama perjanjianKerjasama)
        {
            MBKM.Entities.Models.MBKM.PerjanjianKerjasama data = _perjanjianKerjasamaService.Get(perjanjianKerjasama.ID);
            data.NoPerjanjian = perjanjianKerjasama.NoPerjanjian;
            data.TanggalMulai = perjanjianKerjasama.TanggalMulai;
            data.TanggalAkhir = perjanjianKerjasama.TanggalAkhir;
            //perjanjianKerjasama.CreatedBy = perjanjianKerjasama.CreatedBy;
            data.CreatedDate = DateTime.Now;
            data.UpdatedBy = perjanjianKerjasama.UpdatedBy;
            data.UpdatedDate = perjanjianKerjasama.UpdatedDate;
            data.IsActive = true;
            data.IsDeleted = false;
            data.NamaInstansi = perjanjianKerjasama.NamaInstansi;
            data.NamaUnit = perjanjianKerjasama.NamaUnit;
            data.JenisPertukaran = perjanjianKerjasama.JenisPertukaran;
            data.JenisKerjasama = perjanjianKerjasama.JenisKerjasama;
            data.BiayaKuliah = perjanjianKerjasama.BiayaKuliah;
            data.Instansi = perjanjianKerjasama.Instansi;
            //perjanjianKerjasama.UniversitasID = 0;
            _perjanjianKerjasamaService.Save(data);

            return Json(data);

        }
    }
}