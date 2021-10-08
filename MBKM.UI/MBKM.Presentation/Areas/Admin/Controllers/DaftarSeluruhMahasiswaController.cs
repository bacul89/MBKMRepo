using MBKM.Common.Helpers;
using MBKM.Entities.Models.MBKM;
using MBKM.Entities.ViewModel;
using MBKM.Presentation.Helper;
using MBKM.Presentation.models;
using MBKM.Services;
using MBKM.Services.MBKMServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MBKM.Presentation.Areas.Admin.Controllers
{
    [MBKMAuthorize]
    public class DaftarSeluruhMahasiswaController : Controller
    {
        // GET: Admin/DaftarSeluruhMahasiswa
        private ILookupService _lookupService;
        private IDaftarAllMahasiswaService _mahasiswaService;
        private IPerjanjianKerjasamaService _perjanjianKerjasamaService;
        //
        public DaftarSeluruhMahasiswaController(ILookupService lookupService,IDaftarAllMahasiswaService mahasiswaService, IPerjanjianKerjasamaService perjanjianKerjasamaService)
        {
            _mahasiswaService = mahasiswaService;
            _lookupService = lookupService;
            _perjanjianKerjasamaService = perjanjianKerjasamaService;
        }

        public ActionResult Index()
        {
            
            return View();
        }
        public ActionResult GetNoKerjasama(int Skip, int Length, string Search, string NamaInstansi)
        {
            return Json(_perjanjianKerjasamaService.getNoKerjasama(Skip, Length, Search, NamaInstansi), JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetBiaya(string NoKerjaSama)
        {
            return Json(_perjanjianKerjasamaService.getBiaya(NoKerjaSama), JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult GetList(DataTableAjaxPostModel model)
        {
            VMListDaftarAllMahasiswa vMListAllMhs = _mahasiswaService.GetListDaftarAllMahasiswa(model);
            return Json(new
            {
                // this is what datatables wants sending back
                draw = model.draw,
                recordsTotal = vMListAllMhs.TotalCount,
                recordsFiltered = vMListAllMhs.TotalFilterCount,
                data = vMListAllMhs.gridDatas
            });
        }
        public ActionResult getLookupByTipe(string tipe)
        {
            return Json(_lookupService.getLookupByTipe(tipe), JsonRequestBehavior.AllowGet);
        }


        public ActionResult ModalDetailMhs(int id)
        {
            var model = _mahasiswaService.Get(id);

            return View("ModalDetailMhs", model);
        }
        [HttpPost]
        public ActionResult UpdateKJ(Mahasiswa mhs)
        {
            Mahasiswa data = _mahasiswaService.Get(mhs.ID);
            data.NoKerjasama = mhs.NoKerjasama;
            data.BiayaKuliah = mhs.BiayaKuliah;
            data.StatusKerjasama = mhs.StatusKerjasama;
            data.UpdatedDate = DateTime.Now;
            data.UpdatedBy = Session["username"] as string;




            _mahasiswaService.Save(data);

            //return Json(data);
            return Json(new ServiceResponse { status = 200, message = "Kerjasama Berhasil Di Ubah" });
        }
    }
}