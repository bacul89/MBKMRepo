using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MBKM.Presentation.Helper;
using System.Web.Mvc;
using MBKM.Services.MBKMServices;
using MBKM.Common.Helpers;
using MBKM.Entities.ViewModel;

namespace MBKM.Presentation.Areas.Admin.Controllers
{
    /*[MBKMAuthorize]*/
    public class ApprovalPendaftaranMatakuliahController : Controller
    {

        IPendaftaranMataKuliahService _pendaftaranMataKuliahService;
        ICPLMKPendaftaranService _cPLMKPendaftaranService;

        public ApprovalPendaftaranMatakuliahController(IPendaftaranMataKuliahService pendaftaranMataKuliahService, ICPLMKPendaftaranService cPLMKPendaftaranService)
        {
            _pendaftaranMataKuliahService = pendaftaranMataKuliahService;
            _cPLMKPendaftaranService = cPLMKPendaftaranService;
        }

        // GET: Admin/ApprovalPendaftaranMatakuliah
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult DetailCPL()
        {
            return View();
        }
        
        [HttpPost]
        public JsonResult GetAllApprovalPMK(DataTableAjaxPostModel model)
        {
            var data = _pendaftaranMataKuliahService.GetPendaftaranMahasiswaDataTable(model);

            return Json(new
                {
                    draw = model.draw,
                    recordsTotal = data.TotalCount,
                    recordsFiltered = data.TotalFilterCount,
                    data = data.gridDatas
                }
                );
        }

        [HttpPost]
        public JsonResult GetCobaCoba()
        {

            var data = _cPLMKPendaftaranService.GetAll();
            return Json(data);
        }
    }
}