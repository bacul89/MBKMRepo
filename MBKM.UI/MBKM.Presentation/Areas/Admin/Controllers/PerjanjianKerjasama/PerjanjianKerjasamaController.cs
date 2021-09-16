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
            IEnumerable<VMLookup> listModel = _lookupService.getLookupByTipe("JenisPertukaran");
            ViewBag.listModel = new SelectList(listModel, "Nilai", "Nama");
            IEnumerable<VMLookup> listKerjasama = _lookupService.getLookupByTipe("JenisKerjasama");
            ViewBag.listKerjasama = new SelectList(listKerjasama, "Nilai", "Nama");
            Debug.WriteLine("akses index");
            return View();
        }
        public void savePerjanjian(MBKM.Entities.Models.MBKM.PerjanjianKerjasama perjanjianKerjasama) 
        {
            perjanjianKerjasama.NoPerjanjian = perjanjianKerjasama.NoPerjanjian;
            perjanjianKerjasama.TanggalAkhir = perjanjianKerjasama.TanggalAkhir;
            perjanjianKerjasama.TanggalMulai = perjanjianKerjasama.TanggalMulai;
            perjanjianKerjasama.IsDeleted = false;
            perjanjianKerjasama.IsActive = true;
            perjanjianKerjasama.NamaInstansi = perjanjianKerjasama.NamaInstansi;
            perjanjianKerjasama.UpdatedDate = DateTime.Now;
            perjanjianKerjasama.CreatedDate = DateTime.Now;
            //perjanjianKerjasama.UniversitasID = 0;
            _perjanjianKerjasamaService.Save(perjanjianKerjasama);
        }
    }
}