using MBKM.Common.Helpers;
using MBKM.Entities.Models;
using MBKM.Entities.ViewModel;
using MBKM.Services;
using MBKM.Services.MBKMServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MBKM.Presentation.Areas.Admin.Controllers
{
    public class ManageJadwalUjianController : Controller
    {

        private ILookupService _lookupService;
        private IMasterCapaianPembelajaranService _masterCapaianPembelajaranService;
        private IMahasiswaService _mahasiswaService;
        private IJadwalUjianMBKMService _jadwalUjianMBKMService;

        public ManageJadwalUjianController(ILookupService lookupService, IMasterCapaianPembelajaranService masterCapaianPembelajaranService, IMahasiswaService mahasiswaService, IJadwalUjianMBKMService jadwalUjianMBKMService)
        {
            _lookupService = lookupService;
            _masterCapaianPembelajaranService = masterCapaianPembelajaranService;
            _mahasiswaService = mahasiswaService;
            _jadwalUjianMBKMService = jadwalUjianMBKMService;
        }



        // GET: Admin/ManageJadwalUjian
        public ActionResult Index()
        {
            IEnumerable<VMLookup> tempJenjang =   _lookupService.getLookupByTipe("JenjangStudi");
            IEnumerable<VMLookup> tempJenisUjian =   _lookupService.getLookupByTipe("JenisUjian");
            ViewData["Jenjang"] = tempJenjang;
            ViewData["JenisUjian"] = tempJenisUjian;
            return View();
        }


        public ActionResult DetailManageUjian()
        {
            return View();
        }

        [HttpPost]
        public JsonResult getLookupByTipe(string tipe)
        {
            return Json(_lookupService.getLookupByTipe(tipe));
        }

        [HttpPost]
        public ActionResult GetFakultas(string search, string JenjangStudi)
        {

            return Json(_masterCapaianPembelajaranService.GetFakultas(JenjangStudi, search));
        }

        [HttpPost]
        public ActionResult GetSemester(string JenjangStudi)
        {
            return Json(_mahasiswaService.GetDataSemester(JenjangStudi));
        }

        [HttpPost]
        public ActionResult GetDataTable(DataTableAjaxPostModel model, string jenjangStudi, string fakultas, string jenisUjian, string tahunSemester)
        {
            VMListJadwalUjian data = _jadwalUjianMBKMService.GetListManageUjian(model, jenjangStudi, fakultas, jenisUjian, tahunSemester);
            return Json(
                new
                {
                    draw = model.draw,
                    recordsTotal = data.TotalCount,
                    recordsFiltered = data.TotalFilterCount,
                    data = data.gridDatas
                }
            );
        }
    }
}