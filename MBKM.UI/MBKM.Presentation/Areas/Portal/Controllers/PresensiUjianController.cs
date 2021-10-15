using MBKM.Entities.Models.MBKM;
using MBKM.Presentation.Helper;
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
    [MBKMAuthorize]
    public class PresensiUjianController : Controller
    {
        private IMahasiswaService _mahasiswaService;
        private IJadwalUjianMBKMDetailService _jadwalUjianMBKMDetailService;
        private IAbsensiService _absensiService;
        public PresensiUjianController(IAbsensiService absensiService, IJadwalUjianMBKMDetailService jadwalUjianMBKMDetailService, IMahasiswaService mahasiswaService, ILookupService lookupService, IAttachmentService attachmentService, IPerjanjianKerjasamaService perjanjianKerjasamaService)
        {
            _mahasiswaService = mahasiswaService;
            _jadwalUjianMBKMDetailService = jadwalUjianMBKMDetailService;
            _absensiService = absensiService;
        }
        public ActionResult Index()
        {
            var mahasiswa = GetMahasiswaByEmail(Session["email"] as string);
            var list = _jadwalUjianMBKMDetailService.Find(_ => _.MahasiswaID == mahasiswa.ID && _.IsActive && !_.IsDeleted);
            Dictionary<string, string> result = new Dictionary<string, string>();
            foreach (var item in list)
            {
                result.Add(item.JadwalUjianMBKMs.STRM + "", _absensiService.GetSemesterBySTRM(int.Parse(item.JadwalUjianMBKMs.STRM)));
            }
            return View(result);
        }
        public ActionResult GetPresensiUjian(int strm)
        {
            var mahasiswa = GetMahasiswaByEmail(Session["email"] as string);
            var result = _jadwalUjianMBKMDetailService.Find(_ => _.MahasiswaID == mahasiswa.ID && _.IsActive && !_.IsDeleted).ToList();
            if (strm != 0)
            {
                result = _jadwalUjianMBKMDetailService.Find(_ => _.MahasiswaID == mahasiswa.ID && _.IsActive && !_.IsDeleted && int.Parse(_.JadwalUjianMBKMs.STRM) == strm ).ToList();
            }
            return new ContentResult { Content = JsonConvert.SerializeObject(result), ContentType = "application/json" };
        }
        public Mahasiswa GetMahasiswaByEmail(string email)
        {
            return _mahasiswaService.Find(m => m.Email == email).FirstOrDefault();
        }
    }
}