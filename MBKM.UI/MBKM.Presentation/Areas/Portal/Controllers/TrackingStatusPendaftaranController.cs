using MBKM.Common.Helpers;
using MBKM.Services.MBKMServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MBKM.Presentation.Areas.Portal.Controllers
{
    public class TrackingStatusPendaftaranController : Controller
    {
        private IPendaftaranMataKuliahService _pendaftaranMataKuliahService;

        public TrackingStatusPendaftaranController(IPendaftaranMataKuliahService pendaftaranMataKuliahService)
        {
            _pendaftaranMataKuliahService = pendaftaranMataKuliahService;
        }

        // GET: Portal/TrackingStatusPendaftaran
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult GetPendaftaranMakul(DataTableAjaxPostModel model, string emailMahasiswa)
        {
            var data = _pendaftaranMataKuliahService.GetPendaftaranMahasiswaDataTableByMahasiswa(model,emailMahasiswa);

            return Json(new
                {
                    draw = model.draw,
                    recordsTotal = data.TotalCount,
                    recordsFiltered = data.TotalFilterCount,
                    data = data.gridDatas
                }
            );
        }
    }
}