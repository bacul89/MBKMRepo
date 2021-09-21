using MBKM.Entities.Models.MBKM;
using MBKM.Presentation.models;
using MBKM.Presentation.Models;
using MBKM.Services;
using MBKM.Services.MBKMServices;
using System;
using System.Collections.Generic;
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
        public DataDiriController(IMahasiswaService mahasiswaService, ILookupService lookupService, IAttachmentService attachmentService)
        {
            _mahasiswaService = mahasiswaService;
            _lookupService = lookupService;
            _attachmentService = attachmentService;
        }
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult GetDataMahasiswa(Mahasiswa mahasiswa)
        {
            string email = Session["email"] as string;
            return Json(GetMahasiswaByEmail(email), JsonRequestBehavior.AllowGet);
        }
        public JsonResult getLookupByValue(string tipe, string value)
        {
            return Json(_lookupService.Find(l => l.Nilai == value && l.Tipe == tipe).FirstOrDefault(), JsonRequestBehavior.AllowGet); ;
        }
        public Mahasiswa GetMahasiswaByEmail(string email)
        {
            return _mahasiswaService.Find(m => m.Email == email).FirstOrDefault();
        }
        public List<Attachment> UploadFilePendukung(FilePendukung filePendukung, Int64 id)
        {
            List<Attachment> result = new List<Attachment>();
            Attachment attachment = new Attachment();
            attachment.CreatedDate = DateTime.Now;
            attachment.UpdatedDate = DateTime.Now;
            attachment.IsActive = true;
            attachment.IsDeleted = false;
            attachment.MahasiswaID = id;

            if (filePendukung.fotoDiri != null)
            {
                attachment.FileType = "FotoDiri";
                attachment.FileName = id + "_FotoDiri" + Path.GetExtension(filePendukung.fotoDiri.FileName);
                attachment.FileExt = Path.GetExtension(filePendukung.fotoDiri.FileName);
                attachment.FileSze = filePendukung.fotoDiri.ContentLength;

                var path = Path.Combine(Server.MapPath("~/Upload/" + id + "/"), id + "_FotoDiri" + attachment.FileExt);
                bool exists = Directory.Exists(Server.MapPath("~/Upload/" + id));

                if (!exists)
                    Directory.CreateDirectory(Server.MapPath("~/Upload/" + id));
                filePendukung.fotoDiri.SaveAs(path);

                result.Add(attachment);
            }
            if (filePendukung.fotoKTP != null)
            {
                attachment.FileType = "KTP";
                attachment.FileName = id + "_KTP" + Path.GetExtension(filePendukung.fotoKTP.FileName);
                attachment.FileExt = Path.GetExtension(filePendukung.fotoKTP.FileName);
                attachment.FileSze = filePendukung.fotoKTP.ContentLength;

                var path = Path.Combine(Server.MapPath("~/Upload/" + id + "/"), id + "_KTP" + attachment.FileExt);
                filePendukung.fotoKTP.SaveAs(path);


                result.Add(attachment);
            }
            if (filePendukung.fotoKIM != null)
            {
                attachment.FileType = "FotoKIM";
                attachment.FileName = id + "_FotoKIM" + Path.GetExtension(filePendukung.fotoKIM.FileName);
                attachment.FileExt = Path.GetExtension(filePendukung.fotoKIM.FileName);
                attachment.FileSze = filePendukung.fotoKIM.ContentLength;

                var path = Path.Combine(Server.MapPath("~/Upload/" + id + "/"), id + "_FotoKIM" + attachment.FileExt);
                filePendukung.fotoKIM.SaveAs(path);

                result.Add(attachment);
            }
            if (filePendukung.transkripNilai != null)
            {
                attachment.FileType = "TranskripNilai";
                attachment.FileName = id + "_TranskripNilai" + Path.GetExtension(filePendukung.transkripNilai.FileName);
                attachment.FileExt = Path.GetExtension(filePendukung.transkripNilai.FileName);
                attachment.FileSze = filePendukung.transkripNilai.ContentLength;

                var path = Path.Combine(Server.MapPath("~/Upload/" + id + "/"), id + "_TranskripNilai" + attachment.FileExt);
                filePendukung.transkripNilai.SaveAs(path);

                result.Add(attachment);
            }
            if (filePendukung.suratKeterangan != null)
            {
                attachment.FileType = "SuratKeterangan";
                attachment.FileName = id + "_SuratKeterangan" + Path.GetExtension(filePendukung.suratKeterangan.FileName);
                attachment.FileExt = Path.GetExtension(filePendukung.suratKeterangan.FileName);
                attachment.FileSze = filePendukung.suratKeterangan.ContentLength;

                var path = Path.Combine(Server.MapPath("~/Upload/" + id + "/"), id + "_SuratKeterangan" + attachment.FileExt);
                filePendukung.suratKeterangan.SaveAs(path);

                result.Add(attachment);
            }

            return result;
        }
        public JsonResult UpdateDataDiri(Mahasiswa mahasiswa, FilePendukung filePendukung)
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

                res.Attachments = UploadFilePendukung(filePendukung, res.ID);
                res.StatusVerifikasi = "MENUNGGU VERIFIKASI";
                res.UpdatedDate = DateTime.Now;
                _mahasiswaService.Save(res);
                Session["status"] = "MENUNGGU VERIFIKASI";
                return Json(new ServiceResponse { status = 200, message = "Data diri berhasil diupdate!" });
            }
            catch (Exception e)
            {
                return Json(new ServiceResponse { status = 500, message = e.Message });
            }
        }
    }
}