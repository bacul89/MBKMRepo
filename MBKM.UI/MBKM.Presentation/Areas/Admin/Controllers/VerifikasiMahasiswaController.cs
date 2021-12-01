using MBKM.Common.Helpers;
using MBKM.Entities.Models.MBKM;
using MBKM.Entities.ViewModel;
using MBKM.Services;
using MBKM.Services.MBKMServices;
using System;
using System.Collections.Generic;
using System.Configuration;
using MBKM.Presentation.Helper;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MBKM.Presentation.models;
using MBKM.Entities.Models;
using Newtonsoft.Json;
using System.Data.Entity.Validation;

namespace MBKM.Presentation.Areas.Admin.Controllers
{
    [MBKMAuthorize]
    public class VerifikasiMahasiswaController : Controller
    {
        private IMahasiswaService _mahasiswaService;
        private IEmailTemplateService _emailTemplateService;
        private ILookupService _lookupService;
        private IPerjanjianKerjasamaService _perjanjianKerjasama;
        private IAttachmentService _attachmentService;

        public VerifikasiMahasiswaController(IMahasiswaService mahasiswaService, IEmailTemplateService emailTemplateService, ILookupService lookupService, IPerjanjianKerjasamaService perjanjianKerjasama, IAttachmentService attachmentService)
        {
            _mahasiswaService = mahasiswaService;
            _emailTemplateService = emailTemplateService;
            _lookupService = lookupService;
            _perjanjianKerjasama = perjanjianKerjasama;
            _attachmentService = attachmentService;
        }




        // GET: Admin/VerifikasiMahasiswa
        
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult GetAllMahasiswa(DataTableAjaxPostModel model)
        {
            //var getAll = _mahasiswaService.getMahasiswasNotYetVer("ubm", "");
            VMListMahasiswa data = _mahasiswaService.getMahasiswasNotYetVer(model);
            return Json(
                new
                {
                    draw = model.draw,
                    recordsTotal = data.TotalCount,
                    recordsFiltered = data.TotalFilterCount,
                    data = data.gridDatas
                }
            );
        }

        public ActionResult IndexDetailMahasiswa(int id)
        {
            var ApproverRole = _lookupService.getLookupByTipe("ApproverRole");
            var StatusKerjasama = _lookupService.getLookupByTipe("StatusKerjasama");

            ViewData["Approver"] = ApproverRole;
            ViewData["StatusKerjasama"] = StatusKerjasama;
            ViewBag.terima = StatusVerifikasi.Terima;
            ViewBag.tolak = StatusVerifikasi.Tolak;
            ViewBag.lunas = true;
            ViewBag.belum = false;
            var data = _mahasiswaService.Get(id);
            return View(data);
        }

        [HttpPost]
        public JsonResult GetAllAttachment(int id)
        {
            var data = _mahasiswaService.Get(id).Attachments.Select(x => new Attachment { 
                FileName = x.FileName,
                FileType = x.FileType,
                ID = x.ID
            });
            return Json(data);
        }

        [HttpPost]
        public JsonResult PostDataUpdate(Mahasiswa _mahasiswa)
        {
            var data = _mahasiswaService.Get(_mahasiswa.ID);

            data.Approval = _mahasiswa.Approval;
            data.BiayaKuliah = _mahasiswa.BiayaKuliah;
            data.Catatan = _mahasiswa.Catatan;
            data.NoKerjasama = _mahasiswa.NoKerjasama;
            data.StatusKerjasama = _mahasiswa.StatusKerjasama;
            data.StatusVerifikasi = _mahasiswa.StatusVerifikasi;
            data.FlagBayar = _mahasiswa.FlagBayar;
            data.UpdatedBy = HttpContext.Session["username"].ToString();
            data.UpdatedDate = DateTime.Now;
            try
            {
                // Your code...
                // Could also be before try if you know the exception occurs in SaveChanges
                _mahasiswaService.Save(data);
            }
            catch (DbEntityValidationException e)
            {
                return Json(new ServiceResponse { status = 300, message = "Terjadi Kesalahan saat Melakukan Save Data" });

            }

            try
            {
                if (_mahasiswa.StatusVerifikasi == "AKTIF")
                {
                    SendEmail(data.Email, "VerifikasiAktif");
                }
                else if (_mahasiswa.StatusVerifikasi == "DITOLAK")
                {
                    SendEmail(data.Email, "VerifikasiDitolak");
                }

            }catch(Exception e)
            {
                return Json(new ServiceResponse { status = 500, message = "Terjadi Kesalahan Dalam Melakukan Pengiriman Email Verifikasi" });
            }
            /*            _mahasiswaService.Save(data);*//**/


            return Json(new ServiceResponse { status = 200, message = "Done" });
        }

        [HttpPost]
        public JsonResult GetAllNoKerjasama(int length, int skip, string search, string instansi)
        {
           
            var final = _perjanjianKerjasama.getNoKerjasama(skip,length, search, instansi);
            List<object> data = new List<object>();
            foreach (var p in final)
            {
                var q = new{
                    id = p.ID,
                    text = p.NoKerjasama
                };
                data.Add(q);
            }

            return Json(data);
        }

        [HttpPost]
        public JsonResult GetDataBiaya(int id)
        {
            var data = _perjanjianKerjasama.Get(id).BiayaKuliah;
            return Json(data);
        }

        public ActionResult DownloadFile(int id)
        {
            var data = _attachmentService.Get(id);
            string path = ConfigurationManager.AppSettings["PathAttachmentMahasiswa"].ToString();
            string fullName = Server.MapPath( path + "/"+ data.mahasiswas.ID + "/" + data.FileName);

            byte[] fileBytes = GetFile(fullName);
            return File(
                fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet,data.mahasiswas.NIMAsal + "_" + data.mahasiswas.Nama + "_" + data.FileName);
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

        public void SendEmail(string email, string status)
        {
            MailHelper emailHelper = new MailHelper();
            if(status == "VerifikasiAktif")
            {
                var data = _emailTemplateService.Find(x => x.TipeMail == "VerifikasiAktif" && x.IsActive == true).First();
                List<string> tos = new List<string>();
                tos.Add(email);
                emailHelper.SendMail(data.SubjectMail, data.BodyMail, ConfigurationManager.AppSettings["EmailFrom"], tos, null, null, true);
            }
            else if(status == "VerifikasiDitolak")
            {
                var data = _emailTemplateService.Find(x => x.TipeMail == "VerifikasiDitolak" && x.IsActive == true).First();
                List<string> tos = new List<string>();
                tos.Add(email);
                emailHelper.SendMail(data.SubjectMail, data.BodyMail, ConfigurationManager.AppSettings["EmailFrom"], tos, null, null, true);
            }
        }
    }
}