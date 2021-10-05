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

        public DaftarSeluruhMahasiswaController(IDaftarAllMahasiswaService mahasiswaService)
        {
            _mahasiswaService = mahasiswaService;
        }

        public ActionResult Index()
        {
            Session["username"] = "Smitty Werben Jeger Man Jensen";
            return View();
        }
        [HttpPost]
        public JsonResult GetList(DataTableAjaxPostModel model)
        {
            VMListDaftarAllMahasiswa vMListCPL = _mahasiswaService.GetListDaftarAllMahasiswa(model);
            return Json(new
            {
                // this is what datatables wants sending back
                draw = model.draw,
                recordsTotal = vMListCPL.TotalCount,
                recordsFiltered = vMListCPL.TotalFilterCount,
                data = vMListCPL.gridDatas
            });
        }
       
      
        public ActionResult ModalDetailMhs(int id)
        {
            var model = _mahasiswaService.Get(id);

            return View("ModalDetailMhs", model);
        }
    }
}