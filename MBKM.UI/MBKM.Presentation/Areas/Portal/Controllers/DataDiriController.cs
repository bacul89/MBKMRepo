using MBKM.Entities.Models.MBKM;
using MBKM.Presentation.models;
using MBKM.Presentation.Models;
using MBKM.Services;
using MBKM.Services.MBKMServices;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MBKM.Presentation.Areas.Portal.Controllers
{
    public class DataDiriController : Controller
    {
        private IMahasiswaService _mahasiswaService;
        private ILookupService _lookupService;
        private IAttachmentService _attachmentService;
        private IPerjanjianKerjasamaService _perjanjianKerjasamaService;
        public DataDiriController(IMahasiswaService mahasiswaService, ILookupService lookupService, IAttachmentService attachmentService, IPerjanjianKerjasamaService perjanjianKerjasamaService)
        {
            _mahasiswaService = mahasiswaService;
            _lookupService = lookupService;
            _attachmentService = attachmentService;
            _perjanjianKerjasamaService = perjanjianKerjasamaService;
        }
        public ActionResult Index()
        {
            string email = Session["email"] as string;
            Mahasiswa mahasiswa = GetMahasiswaByEmail(email);
            ViewData["status"] = mahasiswa.StatusVerifikasi;
            return View();
        }
        public ActionResult GetDataMahasiswa()
        {
            string email = Session["email"] as string;
            var result = GetMahasiswaByEmail(email);
            return new ContentResult { Content = JsonConvert.SerializeObject(result), ContentType = "application/json" }; 
        }
        public ActionResult getLookupByValue(string tipe, string value)
        {
            return Json(_lookupService.Find(l => l.Nilai == value && l.Tipe == tipe).FirstOrDefault(), JsonRequestBehavior.AllowGet); ;
        }
        public Mahasiswa GetMahasiswaByEmail(string email)
        {
            return _mahasiswaService.Find(m => m.Email == email).FirstOrDefault();
        }
        public ActionResult GetPerjanjianKerjasama(string noPerjanjian)
        {
            return new ContentResult { Content = JsonConvert.SerializeObject(_perjanjianKerjasamaService.Find(pk => pk.NoPerjanjian == noPerjanjian).FirstOrDefault()), ContentType = "application/json" }; 
        }
        public void UploadFilePendukung(FilePendukung filePendukung, Int64 id)
        {
            if (filePendukung.fotoDiri != null)
            {
                UploadAttachment(id, filePendukung.fotoDiri, "FotoDiri");
            }
            if (filePendukung.fotoKTP != null)
            {
                UploadAttachment(id, filePendukung.fotoKTP, "KTP");
            }
            if (filePendukung.fotoKIM != null)
            {
                UploadAttachment(id, filePendukung.fotoKIM, "FotoKIM");
            }
            if (filePendukung.transkripNilai != null)
            {
                UploadAttachment(id, filePendukung.transkripNilai, "TranskripNilai");
            }
            if (filePendukung.suratKeterangan != null)
            {
                UploadAttachment(id, filePendukung.suratKeterangan, "SuratKeterangan");
            }
        }
        public void UploadAttachment(Int64 id, HttpPostedFileBase file, string tipe)
        {
            string folder = ConfigurationManager.AppSettings["PathAttachmentMahasiswa"];
            Attachment attachment = new Attachment();
            attachment.CreatedDate = DateTime.Now;
            attachment.UpdatedDate = DateTime.Now;
            attachment.IsActive = true;
            attachment.IsDeleted = false;
            attachment.MahasiswaID = id;
            attachment.FileType = tipe;
            attachment.FileName = id + "_" + tipe + Path.GetExtension(file.FileName);
            attachment.FileExt = Path.GetExtension(file.FileName);
            attachment.FileSze = file.ContentLength;

            var path = Path.Combine(Server.MapPath(folder + id + "/"), id + "_" + tipe + attachment.FileExt);
            file.SaveAs(path);

            _attachmentService.Save(attachment);
        }
        public ActionResult UpdateDataDiri(Mahasiswa mahasiswa, FilePendukung filePendukung)
        {
            try
            {
                Mahasiswa res = _mahasiswaService.Find(m => m.ID == mahasiswa.ID).FirstOrDefault();
                res.Nama = mahasiswa.Nama;
                res.Gender = mahasiswa.Gender;
                res.TempatLahir = mahasiswa.TempatLahir;
                res.TanggalLahir = mahasiswa.TanggalLahir;
                res.WargaNegara = mahasiswa.WargaNegara;
                res.NoKTP = mahasiswa.NoKTP;
                res.Email = mahasiswa.Email;
                res.NoHp = mahasiswa.NoHp;
                res.Telepon = mahasiswa.Telepon;
                res.Alamat = mahasiswa.Alamat;
                res.NamaDarurat = mahasiswa.NamaDarurat;
                res.HubunganDarurat = mahasiswa.HubunganDarurat;
                res.NoHPDarurat = mahasiswa.NoHPDarurat;
                res.TeleponDarurat = mahasiswa.TeleponDarurat;
                res.EmailDarurat = mahasiswa.EmailDarurat;
                res.AlamatDarurat = mahasiswa.AlamatDarurat;
                res.BiayaKuliah = mahasiswa.BiayaKuliah;
                res.NamaUniversitas = mahasiswa.NamaUniversitas;
                res.NoKerjasama = mahasiswa.NoKerjasama;
                res.JenjangStudi = mahasiswa.JenjangStudi;
                res.ProdiAsal = mahasiswa.ProdiAsal;
                res.NIMAsal = mahasiswa.NIMAsal;
                res.StatusVerifikasi = "MENUNGGU VERIFIKASI";
                res.UpdatedDate = DateTime.Now;
                UploadFilePendukung(filePendukung, res.ID);
                _mahasiswaService.Save(res);
                return Json(new ServiceResponse { status = 200, message = "Data diri berhasil diupdate!" });
            }
            catch (Exception e)
            {
                return Json(new ServiceResponse { status = 500, message = e.Message });
            }
        }
    }
}