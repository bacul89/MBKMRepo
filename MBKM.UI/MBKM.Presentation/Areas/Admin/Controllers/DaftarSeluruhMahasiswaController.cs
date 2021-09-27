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
        private readonly IMahasiswaService _mahasiswaService;

        public DaftarSeluruhMahasiswaController(IMahasiswaService mahasiswaService)
        {
            _mahasiswaService = mahasiswaService;
        }

        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public JsonResult GetList(DataTableAjaxPostModel model)
        {
            VMListMahasiswa vMListMahasiswa = _mahasiswaService.getMahasiswasNotYetVer(model);
            return Json(new
            {
                // this is what datatables wants sending back
                draw = model.draw,
                recordsTotal = vMListMahasiswa.TotalCount,
                recordsFiltered = vMListMahasiswa.TotalFilterCount,
                data = vMListMahasiswa.gridDatas
            });
        }
    }
}