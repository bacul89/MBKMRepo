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
    public class PresensiKelasController : Controller
    {
        private IMahasiswaService _mahasiswaService;
        private IPendaftaranMataKuliahService _pendaftaranMataKuliahService;
        private IAbsensiService _absensiService;
        public PresensiKelasController(IAbsensiService absensiService, IMahasiswaService mahasiswaService, IPendaftaranMataKuliahService pendaftaranMataKuliahService)
        {
            _mahasiswaService = mahasiswaService;
            _pendaftaranMataKuliahService = pendaftaranMataKuliahService;
            _absensiService = absensiService;
        }
        public ActionResult Index()
        {
            var mahasiswa = GetMahasiswaById(long.Parse(Session["idMahasiswa"] as string));
            var list = _pendaftaranMataKuliahService.Find(pmk => pmk.MahasiswaID == mahasiswa.ID && pmk.StatusPendaftaran == "ACCEPTED BY MAHASISWA").ToList();
            Dictionary<string, string> result = new Dictionary<string, string>();
            foreach (var item in list)
            {
                if (!result.ContainsKey(item.JadwalKuliahs.STRM + ""))
                {
                    result.Add(item.JadwalKuliahs.STRM + "", _absensiService.GetSemesterBySTRM(item.JadwalKuliahs.STRM));
                }
            }
            return View(result);
        }
        public ActionResult DetailPresensiKelas(int id)
        {
            return View(id);
        }
        public ActionResult SubmitAbsensi(int idAbsensi)
        {
            try
            {
                var absensis = _absensiService.Get(idAbsensi);
                absensis.Present = true;
                absensis.UpdatedBy = Session["nama"] as string;
                absensis.UpdatedDate = DateTime.Now;
                _absensiService.Save(absensis);
                return Json(new ServiceResponse { status = 200, message = "TERIMA KASIH SUDAH MENGISI DAFTAR HADIR!" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new ServiceResponse { status = 500, message = e.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult GetAbsensi(int jadwalKuliahId)
        {
            var mahasiswa = GetMahasiswaById(long.Parse(Session["idMahasiswa"] as string));
            List<Absensi> absensis = _absensiService.Find(a => a.JadwalKuliahID == jadwalKuliahId && a.MahasiswaID == mahasiswa.ID).OrderBy(a => a.TanggalAbsen).ToList();
            return new ContentResult { Content = JsonConvert.SerializeObject(absensis), ContentType = "application/json" };
        }
        public ActionResult GetJadwalKuliah(int strm)
        {
            var mahasiswa = GetMahasiswaById(long.Parse(Session["idMahasiswa"] as string));
            List<PendaftaranMataKuliah> pmks = new List<PendaftaranMataKuliah>();
            if (strm != 0)
            {
                pmks = _pendaftaranMataKuliahService.Find(pmk => pmk.MahasiswaID == mahasiswa.ID && pmk.StatusPendaftaran == "ACCEPTED BY MAHASISWA" && pmk.JadwalKuliahs.STRM == strm).ToList();
            } else
            {
                pmks = _pendaftaranMataKuliahService.Find(pmk => pmk.MahasiswaID == mahasiswa.ID && pmk.StatusPendaftaran == "ACCEPTED BY MAHASISWA").ToList();
            }
            return new ContentResult { Content = JsonConvert.SerializeObject(pmks), ContentType = "application/json" };
        }
        public ActionResult GetJadwalKuliahById(int id)
        {
            var result = _pendaftaranMataKuliahService.Get(id);
            return new ContentResult { Content = JsonConvert.SerializeObject(result), ContentType = "application/json" };
        }
        public Mahasiswa GetMahasiswaById(long id)
        {
            return _mahasiswaService.Find(m => m.ID == id).FirstOrDefault();
        }
    }
}