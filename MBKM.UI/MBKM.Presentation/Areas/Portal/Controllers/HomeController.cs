using MBKM.Common.Helpers;
using MBKM.Entities.Models.MBKM;
using MBKM.Presentation.models;
using MBKM.Services.MBKMServices;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MBKM.Presentation.Areas.Portal.Controllers
{
    public class HomeController : Controller
    {
        private IMahasiswaService _mahasiswaService;
        public HomeController(IMahasiswaService mahasiswaService)
        {
            _mahasiswaService = mahasiswaService;
        }
        // GET: Portal/Home
        public ActionResult Index()
        {
            //var a = _mahasiswaService.getLoginInternal("11998000648", "126019");
            

            SendEmail("ridhokurniawan8@gmail.com", "aaaaaaa");
            return View();
        }
        public JsonResult RegisterExternal(Mahasiswa mahasiswa)
        {
            try
            {
                if (GetMahasiswaByEmail(mahasiswa.Email) == null)
                {
                    Random generator = new Random();
                    string token = generator.Next(0, 1000000).ToString("D6");
                    while (GetMahasiswaByToken(token) != null)
                    {
                        token = generator.Next(0, 1000000).ToString("D6");
                    }
                    mahasiswa.IsActive = false;
                    mahasiswa.IsDeleted = false;
                    mahasiswa.Telepon = mahasiswa.NoHp;
                    mahasiswa.CreatedDate = DateTime.Now;
                    mahasiswa.UpdatedDate = DateTime.Now;
                    _mahasiswaService.Save(mahasiswa);
                    SendEmail(mahasiswa.Email, token);
                    return Json(new ServiceResponse { status = 200, message = "Pendaftaran mahasiswa berhasil, tolong cek email dan konfirmasi akunmu!" });
                }
                else
                {
                    return Json(new ServiceResponse { status = 400, message = "Email sudah digunakan!" });
                }
            }
            catch (Exception e)
            {
                return Json(new ServiceResponse { status = 500, message = e.Message });
            }
        }
        public ActionResult VerifyPage(string token)
        {
            
            return RedirectToAction("Index", "Home");
        }

        public Mahasiswa GetMahasiswaByEmail(string email)
        {
            return _mahasiswaService.Find(m => m.Email == email).FirstOrDefault();
        }
        public Mahasiswa GetMahasiswaByToken(string token)
        {
            return _mahasiswaService.Find(m => m.Token == token).FirstOrDefault();
        }
        public void SendEmail(string email, string token)
        {
            string url = this.Url.Action("VerifyPage", "Home", null);
            //GMailer mailer = new GMailer();
            //mailer.ToEmail = email;
            //mailer.Subject = "Verify your email";
            //mailer.Body = "Thanks for Registering your account.<br> please verify your email by clicking the link <br> <a href='http://localhost:10776" + url + "'>verify</a>";
            //mailer.IsHtml = true;
            //mailer.Send();
            List<string> mailDest = new List<string>();
            mailDest.Add(email);
            string Subject = "Verify your email";
            string MailBody = "Thanks for Registering your account.<br> please verify your email by clicking the link <br> <a href='http://localhost:10776" + url + "'>verify</a>";
            MailHelper oMailHelper = new MailHelper(ConfigurationManager.AppSettings["SMTPServer"], int.Parse(ConfigurationManager.AppSettings["SMTPPort"]));
            oMailHelper.SendMail(Subject, MailBody, "sendercvonline@gmail.com", mailDest, null, null, true);
        }
    }
}