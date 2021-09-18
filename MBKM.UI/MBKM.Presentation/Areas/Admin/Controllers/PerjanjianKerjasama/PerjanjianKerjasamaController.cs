using MBKM.Entities.ViewModel;
using MBKM.Services;
using MBKM.Services.MBKMServices;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MBKM.Entities.Models.MBKM;
using MBKM.Common.Helpers;

namespace MBKM.Presentation.Areas.Admin.Controllers.PerjanjianKerjasama
{
    public class PerjanjianKerjasamaController : Controller
    {
        private IMenuService _menuService;
        private ILookupService _lookupService;
        private IPerjanjianKerjasamaService _perjanjianKerjasamaService;
        public PerjanjianKerjasamaController(IMenuService menuService,
            ILookupService lookupService, IPerjanjianKerjasamaService perjanjianKerjasamaService)
        {
            _menuService = menuService;
            _lookupService = lookupService;
            _perjanjianKerjasamaService = perjanjianKerjasamaService;
        }
        // GET: Admin/PerjanjianKerjasama
        public ActionResult Index()
        {
            //IEnumerable<VMLookup> listModel = _lookupService.getLookupByTipe("JenisPertukaran");
            //ViewBag.listModel = new SelectList(listModel, "Nilai", "Nama");
            //IEnumerable<VMLookup> listKerjasama = _lookupService.getLookupByTipe("JenisKerjasama");
            //ViewBag.listKerjasama = new SelectList(listKerjasama, "Nilai", "Nama");
            Debug.WriteLine("akses index");
            return View();
        }

        [HttpPost]
        public JsonResult GetListPKGrid(DataTableAjaxPostModel model)
        {
            VMListPerjanjianKerjasama data = _perjanjianKerjasamaService.getListPKGrid(model);
            return Json(new
            {
                draw = model.draw,
                recordsTotal = data.TotalCount,
                recordsFiltered = data.TotalFilterCount,
                data = data.gridDatas
            }
            );
        }
        /*View Modal Created*/
        public ActionResult ModalCreatePerjanjianKerjasama()
        {
            IEnumerable<VMLookup> listModel = _lookupService.getLookupByTipe("JenisPertukaran");
            ViewBag.listModel = new SelectList(listModel, "Nilai", "Nama");
            IEnumerable<VMLookup> listKerjasama = _lookupService.getLookupByTipe("JenisKerjasama");
            ViewBag.listKerjasama = new SelectList(listKerjasama, "Nama", "Nama");
            return View("ModalCreatePerjanjianKerjasama");
        }
        /*Post Modal Created*/
        //[HttpPost]
        //public ActionResult PostDataKerjasama(MBKM.Entities.Models.MBKM.PerjanjianKerjasama perjanjianKerjasama)
        //{
        //    _perjanjianKerjasamaService.Save(perjanjianKerjasama);
        //    return Json(perjanjianKerjasama);
        //}

        /*Modal Detail*/
        public ActionResult ModalDetailKerjasama(int id)
        {
            var data = _perjanjianKerjasamaService.Get(id);
            return View(data);
        }
        public void SavePerjanjian(MBKM.Entities.Models.MBKM.PerjanjianKerjasama perjanjianKerjasama)
        {
            perjanjianKerjasama.NoPerjanjian = perjanjianKerjasama.NoPerjanjian;
            perjanjianKerjasama.TanggalMulai = perjanjianKerjasama.TanggalMulai;
            perjanjianKerjasama.TanggalAkhir = perjanjianKerjasama.TanggalAkhir;
            perjanjianKerjasama.CreatedBy = perjanjianKerjasama.CreatedBy;
            perjanjianKerjasama.CreatedDate = DateTime.Now;
            perjanjianKerjasama.UpdatedBy = perjanjianKerjasama.UpdatedBy;
            perjanjianKerjasama.UpdatedDate = perjanjianKerjasama.UpdatedDate;
            perjanjianKerjasama.IsActive = true;
            perjanjianKerjasama.IsDeleted = false;
            perjanjianKerjasama.NamaInstansi = perjanjianKerjasama.NamaInstansi;
            perjanjianKerjasama.NamaUnit = perjanjianKerjasama.NamaUnit;
            perjanjianKerjasama.JenisPertukaran = perjanjianKerjasama.JenisPertukaran;
            perjanjianKerjasama.JenisKerjasama = perjanjianKerjasama.JenisKerjasama;
            perjanjianKerjasama.BiayaKuliah = perjanjianKerjasama.BiayaKuliah;
            perjanjianKerjasama.Instansi = perjanjianKerjasama.Instansi;
            //perjanjianKerjasama.UniversitasID = 0;
            _perjanjianKerjasamaService.Save(perjanjianKerjasama);
        }

        /*Modal Update*/
        public ActionResult ModalUpdateKerjasama(int id)
        {
            IEnumerable<VMLookup> listModel = _lookupService.getLookupByTipe("JenisPertukaran");
            ViewBag.listModel = new SelectList(listModel, "Nilai", "Nama");
            IEnumerable<VMLookup> listKerjasama = _lookupService.getLookupByTipe("JenisKerjasama");
            ViewBag.listKerjasama = new SelectList(listKerjasama, "Nama", "Nama");
            var data = _perjanjianKerjasamaService.Get(id);
            return View(data);
        }
        [HttpPost]
        public ActionResult UpdateKerjasama(MBKM.Entities.Models.MBKM.PerjanjianKerjasama perjanjianKerjasama)
        {
            MBKM.Entities.Models.MBKM.PerjanjianKerjasama data = _perjanjianKerjasamaService.Get(perjanjianKerjasama.ID);
            data.NoPerjanjian = perjanjianKerjasama.NoPerjanjian;
            data.TanggalMulai = perjanjianKerjasama.TanggalMulai;
            data.TanggalAkhir = perjanjianKerjasama.TanggalAkhir;
            //perjanjianKerjasama.CreatedBy = perjanjianKerjasama.CreatedBy;
            data.CreatedDate = DateTime.Now;
            data.UpdatedBy = perjanjianKerjasama.UpdatedBy;
            data.UpdatedDate = perjanjianKerjasama.UpdatedDate;
            data.IsActive = true;
            data.IsDeleted = false;
            data.NamaInstansi = perjanjianKerjasama.NamaInstansi;
            data.NamaUnit = perjanjianKerjasama.NamaUnit;
            data.JenisPertukaran = perjanjianKerjasama.JenisPertukaran;
            data.JenisKerjasama = perjanjianKerjasama.JenisKerjasama;
            data.BiayaKuliah = perjanjianKerjasama.BiayaKuliah;
            data.Instansi = perjanjianKerjasama.Instansi;
            //perjanjianKerjasama.UniversitasID = 0;
            _perjanjianKerjasamaService.Save(data);

            return Json(data);

        }
    }
}