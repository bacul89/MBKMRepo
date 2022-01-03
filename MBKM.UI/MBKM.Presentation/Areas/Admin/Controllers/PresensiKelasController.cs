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
            return View(_absensiService.GetTahunSemester());
        }
        /*public ActionResult DetailPresensi(Int64 idJadwal, string tanggalAbsen)
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
        }*/
        public ActionResult DetailPresensi(string tanggalAbsen, string kodeMatkul, string section, int strm)
        {
            //string ruangKelas,
            var getJadwal = _jadwalKuliahService.Find(x =>
                x.KodeMataKuliah == kodeMatkul &&
                //x.RuangKelas == ruangKelas &&
                x.ClassSection == section &&
                x.STRM == strm &&
                x.FlagOpen == true
                /*&&
                x.JamMasuk == jamMasuk &&
                x.JamSelesai == jamKeluar*/
            ).FirstOrDefault();

            //DateTime date = DateTime.Parse(tanggalAbsen);

            /*var getJadwal = _absensiService.Find(x =>
                x.JadwalKuliahs.KodeMataKuliah == kodeMatkul &&
                x.JadwalKuliahs.RuangKelas == ruangKelas &&
                x.JadwalKuliahs.ClassSection == section &&
                x.JadwalKuliahs.STRM == strm &&
                x.TanggalAbsen == date

            *//*
            x.JamMasuk == jamMasuk &&
            x.JamSelesai == jamKeluar*//*
            ).FirstOrDefault();*/

            var model = new VMPresensi();
            var result = _jadwalKuliahService.Get(getJadwal.ID);

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

        public ActionResult GetTahunSemester()
        {   
            return new ContentResult { Content = JsonConvert.SerializeObject(_absensiService.GetTahunSemester()), ContentType = "application/json" };
        }
        public ActionResult getLookupByTipe(string tipe)
        {
            return new ContentResult { Content = JsonConvert.SerializeObject(_lookupService.getLookupByTipe(tipe)), ContentType = "application/json" };
        }
        public ActionResult GetFakultasByJenjangStudi(string search, string jenjangStudi)
        {
            return new ContentResult { Content = JsonConvert.SerializeObject(_absensiService.GetFakultasByJenjangStudi(search, jenjangStudi)), ContentType = "application/json" };
        }
        public ActionResult GetProdiByFakultas(string search, string jenjangStudi, string fakultas)
        {
            return new ContentResult { Content = JsonConvert.SerializeObject(_absensiService.GetProdiByFakultas(search, jenjangStudi, fakultas)), ContentType = "application/json" };
        }
        public ActionResult GetLokasiByProdi(string search, string jenjangStudi, string prodi)
        {
            return new ContentResult { Content = JsonConvert.SerializeObject(_absensiService.GetLokasiByProdi(search, jenjangStudi, prodi)), ContentType = "application/json" };
        }
        public ActionResult GetMatkulByLokasi(string search, string jenjangStudi, string prodi, string lokasi)
        {
            var result = _absensiService.GetMatkulByLokasi(search, jenjangStudi, prodi, lokasi);
            return new ContentResult { Content = JsonConvert.SerializeObject(result), ContentType = "application/json" };
        }
        public ActionResult GetSeksiByMatkul(string search, string jenjangStudi, string matkul, string lokasi)
        {
            return new ContentResult { Content = JsonConvert.SerializeObject(_absensiService.GetSeksiByMatkul(search, jenjangStudi, matkul, lokasi)), ContentType = "application/json" };
        }
        public ActionResult GetPresensiMahasiswa(Int64 idJadwal, DateTime tanggalAbsen)
        {
            TimeSpan ts = new TimeSpan(0, 0, 0);
            var date = tanggalAbsen.Date + ts;
            var result = _absensiService.Find(_ => _.JadwalKuliahID == idJadwal && _.TanggalAbsen == date && (_.Mahasiswas.NIM != _.Mahasiswas.NIMAsal && _.Mahasiswas.NIM != null && _.Mahasiswas.NIMAsal != null)).ToList();
            return new ContentResult { Content = JsonConvert.SerializeObject(result), ContentType = "application/json" };
        }

        [HttpPost]
        public ActionResult GetPresensi(int strm, string jenjangStudi, string fakultas, string lokasi, string prodi, string matkul, string seksi)
        {
            var result = _absensiService.GetPresensi(strm, jenjangStudi, fakultas, lokasi, prodi, matkul, seksi);
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
        public ActionResult GetSemesterAll2()
        {
            var result = _jadwalKuliahService.GetSemesterAll2();
            return new ContentResult { Content = JsonConvert.SerializeObject(result), ContentType = "application/json" };
        }
    }
}