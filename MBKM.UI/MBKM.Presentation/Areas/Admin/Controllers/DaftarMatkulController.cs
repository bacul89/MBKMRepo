using System;
using System.Collections.Generic;
using MBKM.Common.Interfaces.RepoInterfaces.MBKMRepoInterfaces;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MBKM.Services;
using MBKM.Services.MBKMServices;
using MBKM.Entities.ViewModel;
using MBKM.Presentation.models;
using MBKM.Entities.Models;
using MBKM.Repository.Repositories;

using MBKM.Presentation.Helper;
using MBKM.Common.Helpers;
using MBKM.Entities.Models.MBKM;
using Newtonsoft.Json;

namespace MBKM.Presentation.Areas.Admin.Controllers
{

    [MBKMAuthorize]
    public class DaftarMatkulController : Controller
    {
        private IJadwalKuliahService _jadwalKuliahService;
        private ILookupService _lookupService;
        private IMasterCapaianPembelajaranService _mcpService;

        public DaftarMatkulController(IJadwalKuliahService jadwalKuliahService, ILookupService lookupService, IMasterCapaianPembelajaranService mcpService)
        {
            _jadwalKuliahService = jadwalKuliahService;
            _lookupService = lookupService;            
            _mcpService = mcpService;
        }
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GetSemesterAll2()
        {
            var result = _jadwalKuliahService.GetSemesterAll2();
            return new ContentResult { Content = JsonConvert.SerializeObject(result), ContentType = "application/json" };
        }
        public ActionResult GetDaftarMatkul(int strm, string jenjangStudi, string fakultas, string prodi, string lokasi)
        {
            var result = new List<JadwalKuliah>();
            if (lokasi != null && lokasi.Length != 0)
            {
                result = _jadwalKuliahService.Find(_ => _.STRM == strm && _.JenjangStudi == jenjangStudi && _.NamaFakultas == fakultas && _.NamaProdi == prodi &&  _.Lokasi == lokasi && _.FlagOpen).ToList();
            } else
            {
                result = _jadwalKuliahService.Find(_ => _.STRM == strm && _.JenjangStudi == jenjangStudi && _.NamaFakultas == fakultas && _.NamaProdi == prodi && _.FlagOpen).ToList();
            }
            return new ContentResult { Content = JsonConvert.SerializeObject(result), ContentType = "application/json" };
        }
    }
}