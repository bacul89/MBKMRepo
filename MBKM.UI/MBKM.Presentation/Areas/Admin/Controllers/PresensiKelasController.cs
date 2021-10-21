using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MBKM.Entities.Models;
using MBKM.Repository.Repositories;
using MBKM.Services;
using MBKM.Services.MBKMServices;
using MBKM.Presentation.Helper;
using Newtonsoft.Json;
using MBKM.Entities.ViewModel;
using MBKM.Presentation.models;

namespace MBKM.Presentation.Areas.Admin.Controllers
{
    [MBKMAuthorize]
    public class PresensiKelasController : Controller
    {
        private ILookupService _lookupService;
        private IAbsensiService _absensiService;
        private IPendaftaranMataKuliahService _pendaftaranMataKuliahService;
        private IJadwalKuliahService _jadwalKuliahService;

        public PresensiKelasController(IJadwalKuliahService jadwalKuliahService, IPendaftaranMataKuliahService pendaftaranMataKuliahService, IAbsensiService absensiService, ILookupService lookupService)
        {
            _lookupService = lookupService;
            _pendaftaranMataKuliahService = pendaftaranMataKuliahService;
            _absensiService = absensiService;
            _jadwalKuliahService  = jadwalKuliahService;
        }
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult DetailPresensi(Int64 idJadwal, string tanggalAbsen)
        {
            var result = _jadwalKuliahService.Get(idJadwal);
            var model = new VMPresensi();
            model.TanggalAbsen2 = tanggalAbsen;
            model.JamMasuk2 = result.JamMasuk;
            model.JamKeluar2 = result.JamSelesai;
            model.KodeMataKuliah = result.KodeMataKuliah;
            model.NamaMataKuliah = result.NamaMataKuliah;
            model.Seksi = result.ClassSection;
            model.IDJadwalKuliah = result.ID;
            model.RuangKelas = result.RuangKelas;
            model.NamaDosen = result.NamaDosen;
            return View(model);
        }
        public ActionResult getLookupByTipe(string tipe)
        {
            return new ContentResult { Content = JsonConvert.SerializeObject(_lookupService.getLookupByTipe(tipe)), ContentType = "application/json" };
        }
        public ActionResult GetFakultasByJenjangStudi(string search, string jenjangStudi)
        {
            return new ContentResult { Content = JsonConvert.SerializeObject(_absensiService.GetFakultasByJenjangStudi(search, jenjangStudi)), ContentType = "application/json" };
        }
        public ActionResult GetLokasiByFakultas(string search, string jenjangStudi, string fakultas)
        {
            return new ContentResult { Content = JsonConvert.SerializeObject(_absensiService.GetLokasiByFakultas(search, jenjangStudi, fakultas)), ContentType = "application/json" };
        }
        public ActionResult GetProdiByLokasi(string search, string jenjangStudi, string lokasi)
        {
            return new ContentResult { Content = JsonConvert.SerializeObject(_absensiService.GetProdiByLokasi(search, jenjangStudi, lokasi)), ContentType = "application/json" };
        }
        public ActionResult GetMatkulByProdi(string search, string jenjangStudi, string prodi)
        {
            return new ContentResult { Content = JsonConvert.SerializeObject(_absensiService.GetMatkulByProdi(search, jenjangStudi, prodi)), ContentType = "application/json" };
        }
        public ActionResult GetSeksiByMatkul(string search, string jenjangStudi, string matkul)
        {
            return new ContentResult { Content = JsonConvert.SerializeObject(_absensiService.GetSeksiByMatkul(search, jenjangStudi, matkul)), ContentType = "application/json" };
        }
        public ActionResult GetPresensiMahasiswa(Int64 idJadwal)
        {
            var now = DateTime.Now;
            TimeSpan ts = new TimeSpan(0, 0, 0);
            now = now.Date + ts;
            return new ContentResult { Content = JsonConvert.SerializeObject(_absensiService.Find(_ => _.JadwalKuliahID == idJadwal && _.TanggalAbsen == now).ToList()), ContentType = "application/json" };
        }
        public ActionResult GetPresensi(string jenjangStudi, string fakultas, string lokasi, string prodi, string matkul, string seksi)
        {
            var strm = getOngoingSemester(jenjangStudi);
            var result = _absensiService.GetPresensi((int) strm.ID, jenjangStudi, fakultas, lokasi, prodi, matkul, seksi);
            return new ContentResult { Content = JsonConvert.SerializeObject(result), ContentType = "application/json" };
        }
        public VMSemester getOngoingSemester(string jenjangStudi)
        {
            return _pendaftaranMataKuliahService.getOngoingSemester(jenjangStudi);
        }
        public ActionResult SubmitCheckDosen(IEnumerable<int> idAbsensis)
        {
            try
            {
                foreach (var id in idAbsensis)
                {
                    var absensi = _absensiService.Get(id);
                    absensi.CheckDosen = true;
                    absensi.UpdatedDate = DateTime.Now;
                    _absensiService.Save(absensi);
                }
                return Json(new ServiceResponse { status = 200, message = "DATA BERHASIL TERSIMPAN!" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new ServiceResponse { status = 500, message = e.Message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}