﻿using MBKM.Entities.Models.MBKM;
using MBKM.Presentation.Helper;
using MBKM.Presentation.models;
using MBKM.Services;
using MBKM.Services.MBKMServices;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MBKM.Presentation.Areas.Admin.Controllers
{
    [MBKMAuthorize]
    public class RegisMhsByAdminController : Controller
    {
        // GET: Admin/RegisMhsByAdmin
        private IMahasiswaService _mahasiswaService;
        private IPerjanjianKerjasamaService _perjanjianKerjasamaService;
        private ILookupService _lookupService;

        public RegisMhsByAdminController(IMahasiswaService mahasiswaService, IPerjanjianKerjasamaService perjanjianKerjasamaService, ILookupService lookupService)
        {
            _perjanjianKerjasamaService = perjanjianKerjasamaService;
            _mahasiswaService = mahasiswaService;
            _lookupService = lookupService;
        }

        public ActionResult Index()
        {
            return View();
        }
        public JsonResult GetNamaInstansi(int Skip, int Length, string Search)
        {
            return Json(_perjanjianKerjasamaService.getNamaInstansi(Skip, Length, Search), JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetNoKerjasama(int Skip, int Length, string Search, string NamaInstansi)
        {
            return Json(_perjanjianKerjasamaService.getNoKerjasama(Skip, Length, Search, NamaInstansi), JsonRequestBehavior.AllowGet);
        }
        public JsonResult getLookupByTipe(string tipe)
        {
            return Json(_lookupService.getLookupByTipe(tipe), JsonRequestBehavior.AllowGet);
        }
        public Mahasiswa GetMahasiswaByEmail(string email)
        {
            return _mahasiswaService.Find(m => m.Email == email).FirstOrDefault();
        }
        public Mahasiswa GetMahasiswaByToken(string token)
        {
            return _mahasiswaService.Find(m => m.Token == token).FirstOrDefault();
        }
        public ActionResult VerifyPage(string token)
        {
            Mahasiswa mahasiswa = GetMahasiswaByToken(token);
            if (mahasiswa != null)
            {
                mahasiswa.IsActive = true;
                _mahasiswaService.Save(mahasiswa);
                TempData["alertMessage"] = "Akun telah berhasil diaktivasi, silahkan login!";
            }
            else
            {
                TempData["alertMessage"] = "Token invalid!";
            }
            return RedirectToAction("Index", "Home",new { area = "Portal" });
        }
        public void SendEmail(string email, string token)
        {
            string domain = ConfigurationManager.AppSettings["Domain"];
            string url = this.Url.Action("VerifyPage", "RegisMhsByAdmin", null);
            GMailer mailer = new GMailer();
            mailer.ToEmail = email;
            mailer.Subject = "Verify your email";
            mailer.Body = "Thanks for Registering your account.<br> please verify your email by clicking the link <br> <a href='" + domain + url + "?token=" + token + "'>verify</a>";
            //mailer.Body = "Thanks for Registering your account.<br> please verify your email by clicking the link <br> <a href='http://localhost:10776" + url + "?token=" + token + "'>verify</a>";
            mailer.IsHtml = true;
            mailer.Send();
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
                    mahasiswa.StatusVerifikasi = "DAFTAR";
                    mahasiswa.Password = HashPasswordService.HashPassword(mahasiswa.Password);
                    mahasiswa.CreatedBy = "Admin";
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
    }
}