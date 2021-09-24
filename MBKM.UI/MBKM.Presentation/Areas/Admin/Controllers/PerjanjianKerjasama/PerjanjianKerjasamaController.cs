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
using MBKM.Presentation.Helper;

namespace MBKM.Presentation.Areas.Admin.Controllers.PerjanjianKerjasama
{
    [MBKMAuthorize]
    public class PerjanjianKerjasamaController : Controller
    {
        private IMenuService _menuService;
        private ILookupService _lookupService;
        private IPerjanjianKerjasamaService _perjanjianKerjasamaService;
        private IAttachmentPerjanjianKerjasamaService _attachmentPerjanjianKerjasamaService;
        public PerjanjianKerjasamaController(IMenuService menuService,
            ILookupService lookupService, IPerjanjianKerjasamaService perjanjianKerjasamaService, IAttachmentPerjanjianKerjasamaService attachmentPerjanjianKerjasamaService)
        {
            _menuService = menuService;
            _lookupService = lookupService;
            _perjanjianKerjasamaService = perjanjianKerjasamaService;
            _attachmentPerjanjianKerjasamaService = attachmentPerjanjianKerjasamaService;
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
            var file = _perjanjianKerjasamaService.Get(id).AttachmentPerjanjianKerjasamas.Select(x =>
            new AttachmentPerjanjianKerjasama
            {
                FileName = x.FileName,
                ID = x.ID
            });
            ViewData["listFile"] = file;
            return View(data);
        }
        [HttpPost]
        public ActionResult SavePerjanjian(HttpPostedFileBase[] file, MBKM.Entities.Models.MBKM.PerjanjianKerjasama perjanjianKerjasama)
        {
            if (ModelState.IsValid)
            {
                if (file != null)
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
                            var path = Path.Combine(Server.MapPath("~/Upload/"), attch.FileName);
                            files.SaveAs(path);
                        }
                    }
                    perjanjianKerjasama.AttachmentPerjanjianKerjasamas = attachments;
                    perjanjianKerjasama.IsActive = true;
                    perjanjianKerjasama.CreatedBy = Session["username"] as string;
                    perjanjianKerjasama.UpdatedBy = Session["username"] as string;
                    _perjanjianKerjasamaService.Save(perjanjianKerjasama);

                }
                else
                {
                    perjanjianKerjasama.CreatedBy = Session["username"] as string;
                    perjanjianKerjasama.UpdatedBy = Session["username"] as string;
                    perjanjianKerjasama.IsActive = true;
                    _perjanjianKerjasamaService.Save(perjanjianKerjasama);
                }
            }
            return Json(perjanjianKerjasama);
                 
        }


        /*Modal Update*/
        public ActionResult ModalUpdateKerjasama(int id)
        {

            var listPerjanjian = _lookupService.getLookupByTipe("JenisPertukaran");
            ViewData["listPerjanjian"] = listPerjanjian;
            var listKerjasama = _lookupService.getLookupByTipe("JenisKerjasama");
            ViewData["listKerjasama"] = listKerjasama;
            var data = _perjanjianKerjasamaService.Get(id);
            var file = _perjanjianKerjasamaService.Get(id).AttachmentPerjanjianKerjasamas.Select(x =>
            new AttachmentPerjanjianKerjasama
            {
                FileName = x.FileName,
                ID = x.ID
            });
            ViewData["listFile"] = file;
            return View(data);
        }
        [HttpPost]
        public ActionResult UpdateKerjasama(HttpPostedFileBase[] file,MBKM.Entities.Models.MBKM.PerjanjianKerjasama perjanjianKerjasama)
        {
            MBKM.Entities.Models.MBKM.PerjanjianKerjasama data = _perjanjianKerjasamaService.Get(perjanjianKerjasama.ID);
            if (ModelState.IsValid) 
            {
                if (file != null)
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
                            var path = Path.Combine(Server.MapPath("~/Upload/"), attch.FileName);
                            files.SaveAs(path);
                        }
                    }
                    data.AttachmentPerjanjianKerjasamas = attachments;
                    data.NoPerjanjian = perjanjianKerjasama.NoPerjanjian;
                    data.TanggalMulai = perjanjianKerjasama.TanggalMulai;
                    data.TanggalAkhir = perjanjianKerjasama.TanggalAkhir;
                    //perjanjianKerjasama.CreatedBy = perjanjianKerjasama.CreatedBy;
                    data.CreatedDate = DateTime.Now;
                    data.UpdatedBy = Session["username"] as string;
                    data.UpdatedDate = perjanjianKerjasama.UpdatedDate;
                    data.IsActive = true;
                    data.IsDeleted = false;
                    data.NamaInstansi = perjanjianKerjasama.NamaInstansi;
                    data.NamaUnit = perjanjianKerjasama.NamaUnit;
                    data.JenisPertukaran = perjanjianKerjasama.JenisPertukaran;
                    data.JenisKerjasama = perjanjianKerjasama.JenisKerjasama;
                    data.BiayaKuliah = perjanjianKerjasama.BiayaKuliah;
                    data.Instansi = perjanjianKerjasama.Instansi;
                    data.IsActive = true;
                    _perjanjianKerjasamaService.Save(data);
                }
                else {
                    data.NoPerjanjian = perjanjianKerjasama.NoPerjanjian;
                    data.TanggalMulai = perjanjianKerjasama.TanggalMulai;
                    data.TanggalAkhir = perjanjianKerjasama.TanggalAkhir;
                    //perjanjianKerjasama.CreatedBy = perjanjianKerjasama.CreatedBy;
                    data.CreatedDate = DateTime.Now;
                    data.UpdatedBy = Session["username"] as string;
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
                }
            }
            return Json(data);

        }

        public ActionResult DownloadFile(int id)
        {
            var data = _attachmentPerjanjianKerjasamaService.Get(id);
            string path = "~/Upload/";
            string fullName = Server.MapPath(path + data.FileName);

            byte[] fileBytes = GetFile(fullName);
            return File(
                fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, data.FileName);
        }
        byte[] GetFile(string s)
        {
            System.IO.FileStream fs = System.IO.File.OpenRead(s);
            byte[] data = new byte[fs.Length];
            int br = fs.Read(data, 0, data.Length);
            if (br != fs.Length)
                throw new System.IO.IOException(s);
            return data;
        }
    }
}