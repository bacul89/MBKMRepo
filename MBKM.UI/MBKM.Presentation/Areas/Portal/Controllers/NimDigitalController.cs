﻿using MBKM.Entities.Models.MBKM;
using MBKM.Presentation.models;
using MBKM.Services;
using MBKM.Services.MBKMServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MBKM.Presentation.Helper;

namespace MBKM.Presentation.Areas.Portal.Controllers
{
    [MBKMAuthorize]
    public class NimDigitalController : Controller
    {

        private IMahasiswaService _mahasiswaService;
        private IAttachmentService _attachmentService;

        public NimDigitalController(IMahasiswaService mahasiswaService, IAttachmentService attachmentService)
        {
            _mahasiswaService = mahasiswaService;
            _attachmentService = attachmentService;
        }

        public ActionResult Index()
        {
            //Session["nama"] = "smitty werben man jensen";
            //Session["email"] = "abayyina@gmail.com";
            return View();
        }


        public ActionResult GetNim()
        {

            string email = Session["emailMahasiswa"] as string;

            var ma = _mahasiswaService.Find(x => x.Email == email).First();
            var photoprofile = "none";
            try
            {
                var attachment = _attachmentService.Find(x => x.MahasiswaID == ma.ID && x.FileType == "FotoDiri").First();
                photoprofile = "Upload/" + ma.ID + "/" + attachment.FileName;
            }
            catch (Exception ex)
            {
                photoprofile = "Asset/default-photo-profile.png";
            }
            
            return Json(new {
                Nama = ma.Nama,
                NIM = ma.NIM,
                Prodi = ma.ProdiAsal,
                PhotoProfile = photoprofile,
                NamaUniversitas = ma.NamaUniversitas
            }, JsonRequestBehavior.AllowGet);

        }


        /*        public Mahasiswa GetMahasiswaByEmail(string email)
                {
                    return _mahasiswaService.Find(m => m.Email == email).FirstOrDefault().Attachments.Select(x => new Attachment
                    {
                        ID = x.ID
                    });
                }*/
    }
}