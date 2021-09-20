using MBKM.Common.Helpers;
using MBKM.Entities.Models.MBKM;
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
    public class VerifikasiMahasiswaController : Controller
    {
        private IMahasiswaService _mahasiswaService;
        private IEmailTemplateService _emailTemplateService;
        private ILookupService _lookupService;
        private IPerjanjianKerjasamaService _perjanjianKerjasama;

        public VerifikasiMahasiswaController(IMahasiswaService mahasiswaService, IEmailTemplateService emailTemplateService, ILookupService lookupService, IPerjanjianKerjasamaService perjanjianKerjasama)
        {
            _mahasiswaService = mahasiswaService;
            _emailTemplateService = emailTemplateService;
            _lookupService = lookupService;
            _perjanjianKerjasama = perjanjianKerjasama;
        }


        // GET: Admin/VerifikasiMahasiswa
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult GetAllMahasiswa(DataTableAjaxPostModel model)
        {
            //var getAll = _mahasiswaService.getMahasiswasNotYetVer("ubm", "");
            VMListMahasiswa data = _mahasiswaService.getMahasiswasNotYetVer(model);
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

        public ActionResult IndexDetailMahasiswa(int id)
        {
            var ApproverRole = _lookupService.getLookupByTipe("ApproverRole");
            var StatusKerjasama = _lookupService.getLookupByTipe("StatusKerjasama");
            ViewData["Approver"] = ApproverRole;
            ViewData["StatusKerjasama"] = StatusKerjasama;
            var data = _mahasiswaService.Get(id);
            return View(data);
        }

        [HttpPost]
        public JsonResult GetAllLookUp()
        {
            var data = _mahasiswaService.Get(2);
            return Json(data);
        }

        [HttpPost]
        public JsonResult PostDataUpdate(Mahasiswa _mahasiswa)
        {
            var data = _mahasiswaService.Get(_mahasiswa.ID);

            data.Approval = _mahasiswa.Approval;
            data.BiayaKuliah = _mahasiswa.BiayaKuliah;
            data.Catatan = _mahasiswa.Catatan;
            data.NoKerjasama = _mahasiswa.NoKerjasama;
            data.StatusKerjasama = _mahasiswa.StatusKerjasama;
            data.StatusVerifikasi = _mahasiswa.StatusVerifikasi;

            _mahasiswaService.Save(data);
            return Json(data);
        }

        [HttpPost]
        public JsonResult GetAllNoKerjasama(int length, int skip, string search, string instansi)
        {
           
            var final = _perjanjianKerjasama.getNoKerjasama(skip,length, search, instansi);
            List<object> data = new List<object>();
            foreach (var p in final)
            {
                var q = new{
                    id = p.ID,
                    text = p.NoKerjasama
                };
                data.Add(q);
            }

            return Json(data);
        }

        [HttpPost]
        public JsonResult GetDataBiaya(int id)
        {
            var data = _perjanjianKerjasama.Get(id);
            return Json(data);
        }
    }
}