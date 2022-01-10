using MBKM.Common.Helpers;
using MBKM.Entities.Models.MBKM;
using MBKM.Entities.ViewModel;
using MBKM.Presentation.models;
using MBKM.Services;
using MBKM.Services.MBKMServices;
using Newtonsoft.Json;
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
        private IPerjanjianKerjasamaService _perjanjianKerjasamaService;
        private ILookupService _lookupService;
        private IJadwalKuliahService _jadwalKuliahService;
        private IPendaftaranMataKuliahService _pendaftaranMataKuliahService;
        private IAttachmentService _attachmentService;

        public HomeController(IMahasiswaService mahasiswaService, IPerjanjianKerjasamaService perjanjianKerjasamaService, ILookupService lookupService, IJadwalKuliahService jadwalKuliahService, IPendaftaranMataKuliahService pendaftaranMataKuliahService, IAttachmentService attachmentService)
        {
            _mahasiswaService = mahasiswaService;
            _perjanjianKerjasamaService = perjanjianKerjasamaService;
            _lookupService = lookupService;
            _jadwalKuliahService = jadwalKuliahService;
            _pendaftaranMataKuliahService = pendaftaranMataKuliahService;
            _attachmentService = attachmentService;
        }

        // GET: Portal/Home 
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CheckEmailResetPassword(String email)
        {
            Mahasiswa mahasiswa = _mahasiswaService.Find(x => x.Email == email).FirstOrDefault();
            if (mahasiswa == null)
            {
                return Json(new ServiceResponse { status = 500, message = "Email Tidak Terdaftar!" });
            }
            Random generator = new Random();
            string token = generator.Next(0, 1000000).ToString("D6");
            while (GetMahasiswaByToken(token) != null)
            {
                token = generator.Next(0, 1000000).ToString("D6");
            }

            mahasiswa.Token = token;
            mahasiswa.UpdatedDate = DateTime.Now;

            if (SendEmailReset(email, token))
            {
                _mahasiswaService.Save(mahasiswa);
                return Json(new ServiceResponse { status = 200, message = "Check Email Untuk Melakukan Reset Password" });
            }
            return Json(new ServiceResponse { status = 500, message = "Email tidak terkirim!" });

            
        }

        [HttpPost]
        public ActionResult CheckTokenPassword(string token)
        {
            Mahasiswa mahasiswa = GetMahasiswaByToken(token);
            if (mahasiswa != null)
            {
                return Json(new ServiceResponse { status = 200, message = "Masukkan Password!" });
            }
            else
            {
                return Json(new ServiceResponse { status = 500, message = "Token invalid!" });
            }
        }


        [HttpPost]
        public ActionResult InputNewPassword(string token, string password)
        {
            Mahasiswa mahasiswa = GetMahasiswaByToken(token);
            if (mahasiswa != null)
            {
                mahasiswa.IsActive = true;
                mahasiswa.Token = null;
                mahasiswa.Password = HashPasswordService.HashPassword(password);
                _mahasiswaService.Save(mahasiswa);
                return Json(new ServiceResponse { status = 200, message = "Password Telah Diperbaharui, silahkan login!" });
            }
            else
            {
                return Json(new ServiceResponse { status = 500, message = "Tolong Ulangi Akses URL Email" });
            }
        }

        public ActionResult ResetPassword()
        {
            TempData["alertMessage"] = "ResetPassword";
            return RedirectToAction("Index", "Home");
        }

        public bool SendEmailReset(string email, string token)
        {
            MailHelper emailHelper = new MailHelper();
            string domain = ConfigurationManager.AppSettings["Domain"];
            string url = this.Url.Action("ResetPassword", "Home", null);
            string subject = "Reset Password ";
            string body = "Here is your reset code <br><br> " + token + " <br><br> Please enter link below to reset your password <br> <a href='" + domain + url + "'>here</a><br>";
            List<string> tos = new List<string>();
            tos.Add(email);
            return emailHelper.SendMail(subject, body, ConfigurationManager.AppSettings["EmailFrom"], tos, null, null, true);
        }

        public ActionResult RegisterExternal(Mahasiswa mahasiswa)
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
                    mahasiswa.Token = token;
                    mahasiswa.IsActive = false;
                    mahasiswa.IsDeleted = false;
                    mahasiswa.Telepon = mahasiswa.NoHp;
                    mahasiswa.CreatedDate = DateTime.Now;
                    mahasiswa.UpdatedDate = DateTime.Now;
                    mahasiswa.StatusVerifikasi = "DAFTAR";
                    mahasiswa.Password = HashPasswordService.HashPassword(mahasiswa.Password);
                    if (SendEmail(mahasiswa.Email, token))
                    {
                        _mahasiswaService.Save(mahasiswa);
                        return Json(new ServiceResponse { status = 200, message = "Pendaftaran mahasiswa berhasil, tolong cek email dan konfirmasi akunmu!" });
                    } 
                    return Json(new ServiceResponse { status = 500, message = "Email tidak terkirim!" });
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
        public ActionResult GetNamaInstansi(int Skip, int Length, string Search)
        {
            List<string> instansis = new List<string>();
            List<VMLookupNoKerjasama> pks = new List<VMLookupNoKerjasama>();
            foreach (var item in _perjanjianKerjasamaService.getNamaInstansi(Skip, Length, Search))
            {
                if (!instansis.Contains(item.NamaInstansi))
                {
                    instansis.Add(item.NamaInstansi);
                    pks.Add(item);
                }
            }
            return new ContentResult { Content = JsonConvert.SerializeObject(pks), ContentType = "application/json" };
        }
        public ActionResult GetNoKerjasama(int Skip, int Length, string Search, string NamaInstansi)
        {
            return new ContentResult { Content = JsonConvert.SerializeObject(_perjanjianKerjasamaService.getNoKerjasama(Skip, Length, Search, NamaInstansi)), ContentType = "application/json" };
        }
        public ActionResult getLookupByTipe(string tipe)
        {
            return new ContentResult { Content = JsonConvert.SerializeObject(_lookupService.getLookupByTipe(tipe)), ContentType = "application/json" };
        }
        public ActionResult VerifyPage()
        {
            TempData["alertMessage"] = "Verify";
            return RedirectToAction("Index", "Home");
        }
        public ActionResult GetSemesterAll3(string jenjangStudi, string search)
        {
            var result = _pendaftaranMataKuliahService.GetSemesterAll3(jenjangStudi, search);
            return new ContentResult { Content = JsonConvert.SerializeObject(result), ContentType = "application/json" };
        }
        public ActionResult VerifyAccount(string token)
        {
            Mahasiswa mahasiswa = GetMahasiswaByToken(token);
            if (mahasiswa != null)
            {
                mahasiswa.IsActive = true;
                mahasiswa.Token = null;
                _mahasiswaService.Save(mahasiswa);
                return Json(new ServiceResponse { status = 200, message = "Akun telah berhasil diaktivasi, silahkan login!" });
            } else
            {
                return Json(new ServiceResponse { status = 400, message = "Token invalid!" });
            }
        }
        public ActionResult LoginInternal(string username, string password)
        {
            var a = _mahasiswaService.getLoginInternal(username, password);
            if (a == null)
            {
                TempData["alertMessage"] = "Username atau password anda salah!";
                return RedirectToAction("Index", "Home");

            }
            else
            {
                Mahasiswa mahasiswa = GetMahasiswaByNim(a.NIM);
                if (mahasiswa == null)
                {
                    Mahasiswa mahasiswa2 = new Mahasiswa();
                    mahasiswa2.Nama = a.Nama;
                    mahasiswa2.Email = a.Email;
                    mahasiswa2.NoHp = a.Phone;
                    mahasiswa2.Telepon = a.Phone;
                    mahasiswa2.Alamat = a.Alamat;
                    mahasiswa2.Agama = a.Agama;
                    mahasiswa2.Password = hp(a.PasswordData);
                    mahasiswa2.NamaUniversitas = "UNIKA Atma Jaya";
                    mahasiswa2.TanggalLahir = a.TanggalLahir;
                    mahasiswa2.TempatLahir = "Jakarta";
                    mahasiswa2.Gender = a.Gender;
                    mahasiswa2.ProdiAsal = a.Prodi;
                    mahasiswa2.ProdiAsalID = a.ProdiIDAsal;
                    mahasiswa2.NIM = a.NIM;
                    mahasiswa2.NIMAsal = a.NIM;
                    mahasiswa2.JenjangStudi = a.JenjangStudi;
                    mahasiswa2.StatusVerifikasi = "AKTIF";
                    mahasiswa2.CreatedDate = DateTime.Now;
                    mahasiswa2.UpdatedDate = DateTime.Now;
                    mahasiswa2.IsActive = true;
                    mahasiswa2.IsDeleted = false;
                    mahasiswa2.Semester = ((int) a.Semester) + "";
                    _mahasiswaService.Save(mahasiswa2);
                    Session["prodiIDAsal"] = a.ProdiIDAsal;
                    Session["prodiAsal"] = a.Prodi;
                    PopulateSession(true, mahasiswa2.Email, mahasiswa2.ID + "", mahasiswa2.Semester + "", mahasiswa2.Nama);
                } else
                {
                    mahasiswa.Email = a.Email;
                    mahasiswa.Semester = ((int)a.Semester) + "";
                    mahasiswa.Password = hp(a.PasswordData);
                    _mahasiswaService.Save(mahasiswa);
                    Session["prodiIDAsal"] = a.ProdiIDAsal;
                    Session["prodiAsal"] = a.Prodi;
                    PopulateSession(true, mahasiswa.Email, mahasiswa.ID + "", mahasiswa.Semester + "", mahasiswa.Nama);
                }
                Session["isInternal"] = true;
                return RedirectToAction("Index", "DataDiri");
            }
        }
        public ActionResult LoginExternal(string username, string password)
        {
            Mahasiswa res = _mahasiswaService.Find(m => m.Email == username).FirstOrDefault();
            if (res == null || !HashPasswordService.ValidatePassword(password, res.Password))
            {
                TempData["alertMessage"] = "Username atau password anda salah!";
                return RedirectToAction("Index", "Home");
            }
            else if (!res.IsActive)
            {
                TempData["alertMessage"] = "Silahkan aktivasi akun anda terlebih dahulu!";
                return RedirectToAction("Index", "Home");
            } 
            PopulateSession(true, res.Email, res.ID + "", res.Semester + "", res.Nama);
            Session["isInternal"] = false;
            return RedirectToAction("Index", "DataDiri");
        }
        public ActionResult Logout()
        {
            Session["prodiIDAsal"] = null;
            Session["prodiAsal"] = null;
            PopulateSession(false, null, null, null, null);
            return RedirectToAction("Index", "Home");
        }
        public Mahasiswa GetMahasiswaByEmail(string email)
        {
            return _mahasiswaService.Find(m => m.Email == email && !m.IsDeleted).FirstOrDefault();
        }
        public Mahasiswa GetMahasiswaByToken(string token)
        {
            return _mahasiswaService.Find(m => m.Token == token && !m.IsDeleted).FirstOrDefault();
        }
        public Mahasiswa GetMahasiswaByNim(string nim)
        {
            return _mahasiswaService.Find(m => m.NIM == nim && !m.IsDeleted).FirstOrDefault();
        }
        public bool SendEmail(string email, string token)
        {
            MailHelper emailHelper = new MailHelper();
            string domain = ConfigurationManager.AppSettings["Domain"];
            string url = this.Url.Action("VerifyPage", "Home", null);
            string subject = "Verify your email";
            string body = "Thanks for Registering your account.<br> Here is your token <br><br> " + token + " <br><br>Please verify your email by entering your token in the link <br> <a href='" + domain + url + "'>here</a><br>";
            List<string> tos = new List<string>();
            tos.Add(email);
            return emailHelper.SendMail(subject, body, ConfigurationManager.AppSettings["EmailFrom"], tos, null, null, true);
        }
        public void PopulateSession(bool isLogin, string email, string id, string semester, string nama)
        {
            Session["isLogin"] = isLogin;
            Session["emailMahasiswa"] = email;
            Session["idMahasiswa"] = id;
            Session["semesterMahasiswa"] = semester;
            Session["nama"] = nama;
        }
        public string hp(string password)
        {
            return HashPasswordService.HashPassword(password);
        }

        public ActionResult DownloadFileUserGuide(string id)
        { 

            string path = ConfigurationManager.AppSettings["PathAttachmentMahasiswa"].ToString();
            if(id == "y")
            {
                string fullName = Server.MapPath(path + "/UG-Internal.pdf");
                byte[] fileBytes = GetFile(fullName);
                return File(
                fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, "User Guide Mahasiswa Internal.pdf");
            }
            else
            {
                string fullName = Server.MapPath(path + "/UG-Eksternal.pdf");
                byte[] fileBytes = GetFile(fullName);
                return File(
                fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, "User Guide Mahasiswa Eksternal.pdf");

            }
            
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