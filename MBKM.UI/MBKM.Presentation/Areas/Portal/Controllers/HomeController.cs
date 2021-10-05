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
            return View();
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
                    _mahasiswaService.Save(mahasiswa);
                    if (SendEmail(mahasiswa.Email, token))
                    {
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
            return Json(pks, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetNoKerjasama(int Skip, int Length, string Search, string NamaInstansi)
        {
            return Json(_perjanjianKerjasamaService.getNoKerjasama(Skip, Length, Search, NamaInstansi), JsonRequestBehavior.AllowGet);
        }
        public ActionResult getLookupByTipe(string tipe)
        {
            return Json(_lookupService.getLookupByTipe(tipe), JsonRequestBehavior.AllowGet);
        }
        public ActionResult VerifyPage(string token)
        {
            Mahasiswa mahasiswa = GetMahasiswaByToken(token);
            if (mahasiswa != null)
            {
                mahasiswa.IsActive = true;
                mahasiswa.Token = null;
                _mahasiswaService.Save(mahasiswa);
                TempData["alertMessage"] = "Akun telah berhasil diaktivasi, silahkan login!";
            } else
            {
                TempData["alertMessage"] = "Token invalid!";
            }
            return RedirectToAction("Index", "Home");
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
                    mahasiswa.NIMAsal = a.NIM;
                    mahasiswa.StatusVerifikasi = "AKTIF";

                    _mahasiswaService.Save(mahasiswa);
                }
                PopulateSession(true, mahasiswa.Email, mahasiswa.Nama);
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
            PopulateSession(true, res.Email, res.Nama);
            return RedirectToAction("Index", "DataDiri");
        }
        public ActionResult Logout()
        {
            PopulateSession(false, null, null);
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
            string body = "Thanks for Registering your account.<br> please verify your email by clicking the link <br> <a href='" + domain + url + "?token=" + token + "'>verify</a>";
            List<string> tos = new List<string>();
            tos.Add(email);
            return emailHelper.SendMail(subject, body, ConfigurationManager.AppSettings["EmailFrom"], tos, null, null, true);
        }
        public void PopulateSession(bool isLogin, string email, string nama)
        {
            Session["isLogin"] = isLogin;
            Session["email"] = email;
            Session["nama"] = nama;
        }
        public string hp(string password)
        {
            return HashPasswordService.HashPassword(password);
        }
    }
}