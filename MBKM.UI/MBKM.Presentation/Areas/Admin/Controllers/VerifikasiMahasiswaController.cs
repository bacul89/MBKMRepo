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

        public VerifikasiMahasiswaController(IMahasiswaService mahasiswaService, IEmailTemplateService emailTemplateService)
        {
            _mahasiswaService = mahasiswaService;
            _emailTemplateService = emailTemplateService;
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
            var data = _mahasiswaService.Get(id);
            return View(data);
        }
    }
}