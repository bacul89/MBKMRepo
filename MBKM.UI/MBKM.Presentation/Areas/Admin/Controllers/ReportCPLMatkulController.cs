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
    public class ReportCPLMatkulController : Controller
    {
        private IJadwalKuliahService _jadwalKuliahService;
        private ICPLMatakuliahService _cplMatakuliahService;

        public ReportCPLMatkulController(ICPLMatakuliahService cplMatakuliahService, IJadwalKuliahService jkService, ILookupService lookupService, IMasterCapaianPembelajaranService mcpService)
        {
            _jadwalKuliahService = jkService;
            _cplMatakuliahService = cplMatakuliahService;
        }
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GetMatkulByProdi(string jenjangStudi, string prodi, string search)
        {
            var result = _jadwalKuliahService
                .Find(_ => _.JenjangStudi == jenjangStudi && _.NamaProdi == prodi && _.FlagOpen && (_.KodeMataKuliah + " - " + _.NamaMataKuliah).Contains(search))
                .Select(_ => new VMLookup
                {
                    Nama = _.KodeMataKuliah + " - " + _.NamaMataKuliah
                }).Distinct().ToList();
            return new ContentResult { Content = JsonConvert.SerializeObject(result), ContentType = "application/json" };
        }
        public ActionResult GetCPLMatkul(string jenjangStudi, string fakultas, string prodi, string matkul)
        {
            var result = new List<CPLMatakuliah>();
            if (matkul != null && matkul.Length != 0)
            {
                result = _cplMatakuliahService
                .Find(_ => _.MasterCapaianPembelajarans.JenjangStudi == jenjangStudi && _.MasterCapaianPembelajarans.NamaProdi == prodi && _.MasterCapaianPembelajarans.NamaFakultas == fakultas && _.KodeMataKuliah + " - " + _.NamaMataKuliah == matkul).ToList();
            } else
            {
                result = _cplMatakuliahService
                .Find(_ => _.MasterCapaianPembelajarans.JenjangStudi == jenjangStudi && _.MasterCapaianPembelajarans.NamaProdi == prodi && _.MasterCapaianPembelajarans.NamaFakultas == fakultas).ToList();
            }
            return new ContentResult { Content = JsonConvert.SerializeObject(result), ContentType = "application/json" };
        }
    }
}