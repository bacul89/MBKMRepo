using MBKM.Common.Helpers;
using MBKM.Entities.Models;
using MBKM.Entities.Models.MBKM;
using MBKM.Entities.ViewModel;
using MBKM.Presentation.Helper;
using MBKM.Presentation.models;
using MBKM.Services;
using MBKM.Services.MBKMServices;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MBKM.Presentation.Areas.Admin.Controllers
{
    [MBKMAuthorize]
    public class PenilaianController : Controller
    {
        private IPendaftaranMataKuliahService _pendaftaranMataKuliahService;
        private IJadwalKuliahService _jadwalKuliahService;
        private IAbsensiService _absensiService;
        private INilaiKuliahService _nilaiKuliahService;
        public PenilaianController(INilaiKuliahService nilaiKuliahService, IAbsensiService absensiService, IJadwalKuliahService jadwalKuliahService, IPendaftaranMataKuliahService pendaftaranMataKuliahService)
        {
            _pendaftaranMataKuliahService = pendaftaranMataKuliahService;
            _jadwalKuliahService = jadwalKuliahService;
            _absensiService = absensiService;
            _nilaiKuliahService = nilaiKuliahService;
        }
        // GET: Admin/UserManage
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult DetailPenilaian(int idMatkul)
        {
            var model = _jadwalKuliahService.Get(idMatkul);
            return View(model);
        }
        public ActionResult GetKelas(string jenjangStudi, string fakultas, string lokasi, string prodi, string matkul, string seksi)
        {
            var result = _jadwalKuliahService.Find(_ => _.JenjangStudi == jenjangStudi && _.NamaFakultas == fakultas && _.Lokasi == lokasi && _.NamaProdi == prodi && _.KodeMataKuliah + " - " + _.NamaMataKuliah == matkul && _.ClassSection == seksi).ToList();
            return new ContentResult { Content = JsonConvert.SerializeObject(result), ContentType = "application/json" };
        }
        public ActionResult GetBobot(string idMatkul)
        {
            var result = _nilaiKuliahService.GetBobot(idMatkul);
            return new ContentResult { Content = JsonConvert.SerializeObject(result), ContentType = "application/json" };
        }
        public ActionResult GetSubBobot(string idMatkul)
        {
            var result = _nilaiKuliahService.GetBobot(idMatkul);
            return new ContentResult { Content = JsonConvert.SerializeObject(result), ContentType = "application/json" };
        }
        public ActionResult GetSemester(int strm)
        {
            var result = _absensiService.GetSemesterBySTRM(strm);
            return new ContentResult { Content = JsonConvert.SerializeObject(result), ContentType = "application/json" };
        }
        public ActionResult GetNilai(IEnumerable<int> ids, int idJadwalKuliah)
        {
            var result = new List<NilaiKuliah>();
            if (ids == null) return null;
            foreach (var id in ids)
            {
                var tmp = _nilaiKuliahService.Find(_ => _.MahasiswaID == id && _.JadwalKuliahID == idJadwalKuliah).FirstOrDefault();
                if (tmp != null) result.Add(tmp);
            }
            return new ContentResult { Content = JsonConvert.SerializeObject(result), ContentType = "application/json" };
        }
        public ActionResult GetMahasiswa(int idJadwalKuliah)
        {
            var result = _pendaftaranMataKuliahService.Find(_ => _.JadwalKuliahID == idJadwalKuliah && _.StatusPendaftaran == "ACCEPTED BY MAHASISWA");
            return new ContentResult { Content = JsonConvert.SerializeObject(result), ContentType = "application/json" };
        }
        public VMSemester getOngoingSemester(string jenjangStudi)
        {
            return _pendaftaranMataKuliahService.getOngoingSemester(jenjangStudi);
        }
    }
}