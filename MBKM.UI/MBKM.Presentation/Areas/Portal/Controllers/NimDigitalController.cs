using MBKM.Entities.Models.MBKM;
using MBKM.Presentation.models;
using MBKM.Services;
using MBKM.Services.MBKMServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MBKM.Presentation.Areas.Portal.Controllers
{
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

            Session["nama"] = "Hallo";
            Session["email"] = "pangestianin@gmail.com";
            return View();
        }


        /*        public JsonResult RunSession(Mahasiswa mahasiswa)
                {
                    *//*String text = "Hello";
                    return View();
                    string emailText = "abiyogakhusus@gmail.com";
                    Session["email"] = emailText;*//*
                    string email = Session["email"] as string;
                    return Json(GetMahasiswaByEmail(email), JsonRequestBehavior.AllowGet);




                }
        */

        public ActionResult GetNim()
        {

            /*var data = _mahasiswaService.Find(x => x.Email == email).FirstOrDefault();*/
            /*var data = _mahasiswaService.Find(m => m.Email == email).First();*/
            /*var data = _mahasiswaService.GetAll();*/
            string email = Session["email"] as string;

            Console.WriteLine(email);
            var ma = _mahasiswaService.Find(x => x.Email == email).First();
            /*string files = ;*/
            var attachment = _attachmentService.Find(x => x.MahasiswaID == ma.ID && x.FileType == "FotoDiri").First();

            return Json(new {
                Nama = ma.Nama,
                NIM = ma.NIM,
                Prodi = ma.ProdiAsal,
                PhotoProfile = attachment.FileName,
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