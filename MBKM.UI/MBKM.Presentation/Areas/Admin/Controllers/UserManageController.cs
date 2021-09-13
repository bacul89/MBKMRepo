using MBKM.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MBKM.Presentation.Areas.Admin.Controllers
{
    public class UserManageController : Controller
    {
        private readonly IUserService _userService;
        public UserManageController(IUserService userService)
        {
            _userService = userService;
        }
        // GET: Admin/UserManage
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Get()
        {
            //var obj = _financeTangkiService.SearchFinanceTangki(requestModel.Start, requestModel.Length, TanggalDari, TanggalSampai, PICKeuanganId, ShiftId, TangkiId, CargoId);

            //var data = obj.Item1.Select(fP => new
            //{
            //    Id = fP.Id,
            //    Tanggal = fP.Tanggal,
            //    Shift = fP.Shift.Description,
            //    PICKeuangan = fP.PICKeuangan.Nama,
            //    NomorTangki = fP.Tangki.Description,
            //    NamaCargo = fP.Cargo.Description,
            //    Quantity = fP.Qty,
            //});
            return Json(null);
            //return Json(new DataTablesResponse(requestModel.Draw, data.ToList(), obj.Item3, obj.Item2), JsonRequestBehavior.AllowGet);
        }

    }
}