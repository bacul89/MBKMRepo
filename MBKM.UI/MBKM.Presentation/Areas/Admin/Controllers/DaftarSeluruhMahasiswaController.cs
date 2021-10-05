using MBKM.Common.Helpers;
using MBKM.Entities.ViewModel;
using MBKM.Services.MBKMServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MBKM.Presentation.Areas.Admin.Controllers
{
    public class DaftarSeluruhMahasiswaController : Controller
    {
        // GET: Admin/DaftarSeluruhMahasiswa
        private readonly IDaftarAllMahasiswaService _mahasiswaService;
        //private IPerjanjianKerjasamaService _perjanjianKerjasamaService;
        //, IPerjanjianKerjasamaService perjanjianKerjasamaService
        public DaftarSeluruhMahasiswaController(IDaftarAllMahasiswaService mahasiswaService)
        {
            _mahasiswaService = mahasiswaService;
            //_perjanjianKerjasamaService = perjanjianKerjasamaService;
        }

        public ActionResult Index()
        {
            Session["username"] = "Smitty Werben Jeger Man Jensen";
            return View();
        }
        //public ActionResult GetNoKerjasama(int Skip, int Length, string Search, string NamaInstansi)
        //{
        //    return Json(_perjanjianKerjasamaService.getNoKerjasama(Skip, Length, Search, NamaInstansi), JsonRequestBehavior.AllowGet);
        //}
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
       
      
        public ActionResult ModalDetailMhs(int id)
        {
            var model = _mahasiswaService.Get(id);

            return View("ModalDetailMhs", model);
        }
    }
}