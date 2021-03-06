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
            var mahasiswa = GetMahasiswaById(long.Parse(Session["idMahasiswa"] as string));
            var date1WeekAhead = DateTime.Today.AddDays(7);
            var date = DateTime.Today;
            var list = _jadwalUjianMBKMDetailService.Find(_ => _.MahasiswaID == mahasiswa.ID && _.IsActive && !_.IsDeleted && date1WeekAhead >= _.JadwalUjianMBKMs.TanggalUjian && date <= _.JadwalUjianMBKMs.TanggalUjian);
            Dictionary<string, string> result = new Dictionary<string, string>();
            foreach (var item in list)
            {
                result.Add(item.JadwalUjianMBKMs.STRM + "", _absensiService.GetSemesterBySTRM(int.Parse(item.JadwalUjianMBKMs.STRM)));
            }
            return View(result);
        }
        public ActionResult GetPresensiUjian(int strm)
        {
            var mahasiswa = GetMahasiswaById(long.Parse(Session["idMahasiswa"] as string));
            var date1WeekAhead = DateTime.Today.AddDays(7);
            var date = DateTime.Today;
            var result = _jadwalUjianMBKMDetailService.Find(_ => _.MahasiswaID == mahasiswa.ID && _.IsActive && !_.IsDeleted && date1WeekAhead >= _.JadwalUjianMBKMs.TanggalUjian && date <= _.JadwalUjianMBKMs.TanggalUjian).ToList();
            if (strm != 0)
            {
                var tmp = strm + "";
                result = _jadwalUjianMBKMDetailService.Find(_ => _.MahasiswaID == mahasiswa.ID && _.IsActive && !_.IsDeleted && _.JadwalUjianMBKMs.STRM == tmp && date1WeekAhead >= _.JadwalUjianMBKMs.TanggalUjian && date <= _.JadwalUjianMBKMs.TanggalUjian).ToList();
            }
            return new ContentResult { Content = JsonConvert.SerializeObject(result), ContentType = "application/json" };
        }
        public ActionResult SubmitAbsensiUjian(int idAbsensiUjian)
        {
            try
            {
                var absensis = _jadwalUjianMBKMDetailService.Get(idAbsensiUjian);
                absensis.Present = true;
                absensis.UpdatedDate = DateTime.Now;
                _jadwalUjianMBKMDetailService.Save(absensis);
                return Json(new ServiceResponse { status = 200, message = "TERIMA KASIH SUDAH MENGISI DAFTAR HADIR!" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new ServiceResponse { status = 500, message = e.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public Mahasiswa GetMahasiswaById(long id)
        {
            return _mahasiswaService.Find(m => m.ID == id).FirstOrDefault();
        }
    }
}