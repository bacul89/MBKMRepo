using MBKM.Common.Helpers;
using MBKM.Entities.Models.MBKM;
using MBKM.Entities.ViewModel;
using MBKM.Services;
using MBKM.Services.MBKMServices;
using System;
using System.Collections.Generic;
using System.Linq;
using MBKM.Presentation.Helper;
using System.Web;
using System.Web.Mvc;

namespace MBKM.Presentation.Areas.Admin.Controllers
{
    [MBKMAuthorize]
    public class JadwalUjianController : Controller
    {

        private ILookupService _lookupService;
        private IMasterCapaianPembelajaranService _masterCapaianPembelajaranService;
        private IMahasiswaService _mahasiswaService;
        private IJadwalUjianMBKMService _jadwalUjianMBKMService;
        private IPendaftaranMataKuliahService _pendaftaranMataKuliahService;
        private IJadwalUjianMBKMDetailService _jadwalUjianMBKMDetailService;

        public JadwalUjianController(ILookupService lookupService, IMasterCapaianPembelajaranService masterCapaianPembelajaranService, IMahasiswaService mahasiswaService, IJadwalUjianMBKMService jadwalUjianMBKMService, IPendaftaranMataKuliahService pendaftaranMataKuliahService, IJadwalUjianMBKMDetailService jadwalUjianMBKMDetailService)
        {
            _lookupService = lookupService;
            _masterCapaianPembelajaranService = masterCapaianPembelajaranService;
            _mahasiswaService = mahasiswaService;
            _jadwalUjianMBKMService = jadwalUjianMBKMService;
            _pendaftaranMataKuliahService = pendaftaranMataKuliahService;
            _jadwalUjianMBKMDetailService = jadwalUjianMBKMDetailService;
        }





        // GET: Admin/JadwalUjian
        public ActionResult Index()
        {
            IEnumerable<VMLookup> tempJenjang = _lookupService.getLookupByTipe("JenjangStudi");
            IEnumerable<VMLookup> tempJenisUjian = _lookupService.getLookupByTipe("JenisUjian");
            ViewData["Jenjang"] = tempJenjang;
            ViewData["JenisUjian"] = tempJenisUjian;
            return View();
        }

        [HttpPost]
        public JsonResult getLookupByTipe(string tipe)
        {
            return Json(_lookupService.getLookupByTipe(tipe));
        }

        [HttpPost]
        public ActionResult GetSemester(string JenjangStudi)
        {
            return Json(_mahasiswaService.GetDataSemester(JenjangStudi));
        }

        [HttpPost]
        public ActionResult GetDataTable(DataTableAjaxPostModel model, string jenjangStudi, string fakultas, string jenisUjian, string tahunSemester)
        {
            VMListJadwalUjian data = _jadwalUjianMBKMService.GetListJadwalUjian(model, jenjangStudi, fakultas, jenisUjian, tahunSemester);
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


        [HttpPost]
        public ActionResult GetFakultas(string search, string JenjangStudi)
        {

            return Json(_masterCapaianPembelajaranService.GetFakultas(JenjangStudi, search));
        }

        public ActionResult DetailJadwalUjian(int id)
        {
            int idd = id;
            var data = _jadwalUjianMBKMService.Get(idd);

            IEnumerable<JadwalUjianMBKMDetail> tempJadwalUjian = _jadwalUjianMBKMDetailService.Find(x => x.JadwalUjianMBKMID == idd).ToList();
            ViewData["dataMahasiswa"] = tempJadwalUjian;
            return View(data);
        }

        [HttpPost]
        public ActionResult GetDataTableMahasiswa(int dataID)
        {
            IEnumerable<JadwalUjianMBKMDetail> tempDataMahasiswa = _jadwalUjianMBKMDetailService.Find(x => x.JadwalUjianMBKMID == dataID).ToList();
          
            List<String[]> final = new List<String[]>();

            foreach (var dt in tempDataMahasiswa)
            {
                var ddd = dt.MahasiswaID.ToString();

                final.Add(new String[]
                {
                    ddd,
                    dt.Mahasiswas.NIM,
                    dt.Mahasiswas.Nama,
                });
                

            }
            return Json(final);
        }
    }
}