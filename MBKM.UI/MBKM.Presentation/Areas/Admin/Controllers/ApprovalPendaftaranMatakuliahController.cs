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

        public ApprovalPendaftaranMatakuliahController(IPendaftaranMataKuliahService pendaftaranMataKuliahService)
        {
            _pendaftaranMataKuliahService = pendaftaranMataKuliahService;
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

        /*public JsonResult GetCobaCoba()
        {
            var return 
        }*/
    }
}