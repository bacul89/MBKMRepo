﻿using MBKM.Entities.Models.MBKM;
using MBKM.Presentation.Helper;
using MBKM.Services.MBKMServices;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MBKM.Presentation.Areas.Portal.Controllers
{
    [MBKMAuthorize]
    public class SummaryPresensiController : Controller
    {
        // GET: Portal/SummaryPresensi
        private IMahasiswaService _mahasiswaService;
        private IPendaftaranMataKuliahService _pendaftaranMataKuliahService;
        private IAbsensiService _absensiService;
        public SummaryPresensiController(IAbsensiService absensiService, IMahasiswaService mahasiswaService, IPendaftaranMataKuliahService pendaftaranMataKuliahService)
        {
            _mahasiswaService = mahasiswaService;
            _pendaftaranMataKuliahService = pendaftaranMataKuliahService;
            _absensiService = absensiService;
        }
        public ActionResult Index()
        {
            var mahasiswa = GetMahasiswaByEmail(Session["email"] as string);
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
        public ActionResult GetJadwalKuliah(int strm)
        {
            var mahasiswa = GetMahasiswaByEmail(Session["email"] as string);
            List<PendaftaranMataKuliah> pmks = new List<PendaftaranMataKuliah>();
            if (strm != 0)
            {
                pmks = _pendaftaranMataKuliahService.Find(pmk => pmk.MahasiswaID == mahasiswa.ID && pmk.StatusPendaftaran == "ACCEPTED BY MAHASISWA" && pmk.JadwalKuliahs.STRM == strm).ToList();
            }
            else
            {
                pmks = _pendaftaranMataKuliahService.Find(pmk => pmk.MahasiswaID == mahasiswa.ID && pmk.StatusPendaftaran == "ACCEPTED BY MAHASISWA").ToList();
            }
            return new ContentResult { Content = JsonConvert.SerializeObject(pmks), ContentType = "application/json" };
        }
        public ActionResult GetSumAbsensi(int jadwalKuliahId)
        {
            var mahasiswa = GetMahasiswaByEmail(Session["email"] as string);
            List<Absensi> absensis = _absensiService.Find(a => a.JadwalKuliahID == jadwalKuliahId).OrderBy(a => a.TanggalAbsen).ToList();
            return new ContentResult { Content = JsonConvert.SerializeObject(absensis), ContentType = "application/json" };
        }
        public ActionResult DetailSummaryPresensiKelas(int id)
        {
            return View(id);
        }
        public Mahasiswa GetMahasiswaByEmail(string email)
        {
            return _mahasiswaService.Find(m => m.Email == email).FirstOrDefault();
        }
    }
}