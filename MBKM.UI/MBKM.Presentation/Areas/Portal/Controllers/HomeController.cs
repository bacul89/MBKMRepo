using MBKM.Common.Helpers;
using MBKM.Entities.Models.MBKM;
using MBKM.Entities.ViewModel;
using MBKM.Presentation.models;
using MBKM.Services;
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
        private IPerjanjianKerjasamaService _perjanjianKerjasamaService;
        private ILookupService _lookupService;
        public HomeController(IMahasiswaService mahasiswaService, IPerjanjianKerjasamaService perjanjianKerjasamaService, ILookupService lookupService)
        {
            _mahasiswaService = mahasiswaService;
            _perjanjianKerjasamaService = perjanjianKerjasamaService;
            _lookupService = lookupService;
        }
        // GET: Portal/Home
        public ActionResult Index()
        {
            try
            {
                bool isLogin = (bool)Session["isLogin"];
                if (isLogin)
                {
                    return RedirectToAction("Index", "DataDiri");
                }
            }
            catch (Exception)
            {
            }
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
                    mahasiswa.Token = token;
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
        public JsonResult GetNamaInstansi(int Skip, int Length, string Search)
        {
            return Json(_perjanjianKerjasamaService.getNamaInstansi(Skip, Length, Search), JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetNoKerjasama(int Skip, int Length, string Search, string NamaInstansi)
        {
            return Json(_perjanjianKerjasamaService.getNoKerjasama(Skip, Length, Search, NamaInstansi), JsonRequestBehavior.AllowGet);
        }
        public ActionResult VerifyPage(string token)
        {
            Mahasiswa mahasiswa = GetMahasiswaByToken(token);
            if (mahasiswa != null)
            {
                mahasiswa.IsActive = true;
                _mahasiswaService.Save(mahasiswa);
                TempData["alertMessage"] = "Akun telah berhasil diaktivasi, silahkan login!";
            } else
            {
                TempData["alertMessage"] = "Token invalid!";
            }
            return RedirectToAction("Index", "Home");
        }
        public ActionResult Login(string username, string password)
        {
            if (!username.Contains("@"))
            {
                var a = _mahasiswaService.getLoginInternal(username, password);
                if (a == null)
                {
                    TempData["alertMessage"] = "Username atau password anda salah!";
                    return RedirectToAction("Index", "Home");

                } else
                {
                    Mahasiswa mahasiswa = new Mahasiswa();
                    if (GetMahasiswaByNim(a.NIM) == null)
                    {
                        mahasiswa.Nama = a.Nama;
                        mahasiswa.Email = a.Email;
                        mahasiswa.NoHp = a.Phone;
                        mahasiswa.Telepon = a.Phone;
                        mahasiswa.Alamat = a.Alamat;
                        mahasiswa.Agama = a.Agama;
                        mahasiswa.Password = a.PasswordData;
                        mahasiswa.CreatedDate = DateTime.Now;
                        mahasiswa.UpdatedDate = DateTime.Now;
                        mahasiswa.IsActive = true;
                        mahasiswa.IsDeleted = false;
                        mahasiswa.TanggalLahir = a.TanggalLahir;
                        mahasiswa.TempatLahir = "Jakarta";
                        mahasiswa.Gender = a.Gender;
                        mahasiswa.ProdiAsal = a.Prodi;
                        mahasiswa.NIM = a.NIM;

                        _mahasiswaService.Save(mahasiswa);
                    }
                    PopulateSession(true, mahasiswa.Email, mahasiswa.Nama);
                    return RedirectToAction("Index", "DataDiri");
                }
            } else
            {
                Mahasiswa res = _mahasiswaService.Find(m => m.Email == username && m.Password == password).FirstOrDefault();
                if (res == null)
                {
                    TempData["alertMessage"] = "Username atau password anda salah!";
                    return RedirectToAction("Index", "Home");
                }
                else if (!res.IsActive)
                {
                    TempData["alertMessage"] = "Silahkan aktivasi akun anda terlebih dahulu!";
                    return RedirectToAction("Index", "Home");
                }
                PopulateSession(true, res.Email, res.Nama);
                return RedirectToAction("Index", "DataDiri");
            }
        }
        public ActionResult Logout()
        {
            PopulateSession(false, null, null);
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
        public Mahasiswa GetMahasiswaByNim(string nim)
        {
            return _mahasiswaService.Find(m => m.NIM == nim).FirstOrDefault();
        }
        public IEnumerable<VMLookup> getLookupByTipe(string tipe)
        {
            return _lookupService.getLookupByTipe(tipe);
        }

        public void SendEmail(string email, string token)
        {
            string url = this.Url.Action("VerifyPage", "Home", null);
            GMailer mailer = new GMailer();
            mailer.ToEmail = email;
            mailer.Subject = "Verify your email";
            mailer.Body = "Thanks for Registering your account.<br> please verify your email by clicking the link <br> <a href='http://localhost:10776" + url + "?token=" + token + "'>verify</a>";
            mailer.IsHtml = true;
            mailer.Send();
        }
        public void PopulateSession(bool isLogin, string email, string nama)
        {
            Session["isLogin"] = isLogin;
            Session["email"] = email;
            Session["nama"] = nama;
        }
    }
}