using MBKM.Common.Helpers;
using MBKM.Entities.Models;
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
        public PenilaianController(IJadwalKuliahService jadwalKuliahService, IPendaftaranMataKuliahService pendaftaranMataKuliahService)
        {
            _pendaftaranMataKuliahService = pendaftaranMataKuliahService;
            _jadwalKuliahService = jadwalKuliahService;
        }
        // GET: Admin/UserManage
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GetKelas(string jenjangStudi, string fakultas, string lokasi, string prodi, string matkul, string seksi)
        {
            var strm = getOngoingSemester(jenjangStudi);
            var result = _jadwalKuliahService.Find(_ => _.JenjangStudi == jenjangStudi && _.STRM == strm.ID && _.NamaFakultas == fakultas && _.Lokasi == lokasi && _.NamaProdi == prodi && _.KodeMataKuliah + " - " + _.NamaMataKuliah == matkul && _.ClassSection == seksi).ToList();
            return new ContentResult { Content = JsonConvert.SerializeObject(result), ContentType = "application/json" };
        }
        public ActionResult DetailPenilaian(int idMatkul)
        {
            return View(idMatkul);
        }
        public VMSemester getOngoingSemester(string jenjangStudi)
        {
            return _pendaftaranMataKuliahService.getOngoingSemester(jenjangStudi);
        }
    }
}