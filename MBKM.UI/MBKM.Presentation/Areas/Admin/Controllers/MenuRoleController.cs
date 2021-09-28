using MBKM.Common.Helpers;
using MBKM.Entities.ViewModel;
using MBKM.Presentation.Helper;
using MBKM.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MBKM.Presentation.Areas.Admin.Controllers
{
    [MBKMAuthorize]
    public class MenuRoleController : Controller
    {
        private Services.IMenuRoleService _menuRoleService;
        public MenuRoleController(IMenuRoleService menuRoleService)
        {
            _menuRoleService = menuRoleService;
        }
        // GET: Admin/MenuRole
        public ActionResult Index()
        {
            return View();
        }

        //get data table
        [HttpPost]
        public ActionResult GetDataMenuRole(DataTableAjaxPostModel model)
        {
            VMListMenuRole vMListMenuRole = _menuRoleService.getListMRGrid(model);
            return Json(new
            {
                // this is what datatables wants sending back
                draw = model.draw,
                recordsTotal = vMListMenuRole.TotalCount,
                recordsFiltered = vMListMenuRole.TotalFilterCount,
                data = vMListMenuRole.gridDatas
            });
        }
    }
}