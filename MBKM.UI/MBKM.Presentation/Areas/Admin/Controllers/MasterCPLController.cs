using MBKM.Common.Helpers;
using MBKM.Entities.Models.MBKM;
using MBKM.Entities.ViewModel;
using MBKM.Presentation.Helper;
using MBKM.Presentation.models;
using MBKM.Services;
using MBKM.Services.MBKMServices;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MBKM.Presentation.Areas.Admin.Controllers
{
    [MBKMAuthorize]
    public class MasterCPLController : Controller
    {
        private ILookupService _lookupService;
        private IMasterCapaianPembelajaranService _mcpService;
        private ICPLMatakuliahService _cplMatakuliah;
        public MasterCPLController(ICPLMatakuliahService cplMatakuliah, ILookupService lookupService, IMasterCapaianPembelajaranService mcpService)
        {
            _cplMatakuliah = cplMatakuliah;
            _lookupService = lookupService;
            _mcpService = mcpService;
        }

        // GET: Admin/MasterCPL
        public ActionResult Index()
        {
            IEnumerable<VMLookup> tempJenjang = _lookupService.getLookupByTipe("JenjangStudi");
            ViewData["Jenjang"] = tempJenjang;
            return View();
        }

        [HttpPost]
        public JsonResult GetList(DataTableAjaxPostModel model, string prodi, string jenjang, string fakultas)
        {
            /*var tprodi = prodi.ToString().PadLeft(4, '0');
            var tfakultas = fakultas.ToString().PadLeft(4, '0');*/
            VMListMasterCPL vMListCPL = _mcpService.GetListMasterCPL(model, prodi, jenjang, fakultas);
            return Json(new
            {
                // this is what datatables wants sending back
                draw = model.draw,
                recordsTotal = vMListCPL.TotalCount,
                recordsFiltered = vMListCPL.TotalFilterCount,
                data = vMListCPL.gridDatas
            });
        }

        [HttpPost]
        public ActionResult PostDataCPL(MasterCapaianPembelajaran model)
        {

            try
            {
                model.CreatedDate = DateTime.Now;
                model.UpdatedDate = DateTime.Now;
                model.IsDeleted = false;
                model.IsActive = model.IsActive;
                model.CreatedBy = Session["username"] as string;
                _mcpService.Save(model);
                return Json(new ServiceResponse { status = 200, message = "Pendaftaran CPL Berhasil!" });
            }
            catch (Exception e)
            {
                return Json(new ServiceResponse { status = 500, message = e.Message });
            }

        }
        [HttpPost]
        public ActionResult PostUpdateCPL(MasterCapaianPembelajaran cpl)
        {
            MasterCapaianPembelajaran data = _mcpService.Get(cpl.ID);
            data.Kelompok = cpl.Kelompok;
            data.Kode = cpl.Kode;
            data.Capaian = cpl.Capaian;
            data.IsActive = cpl.IsActive;
            data.UpdatedBy = Session["username"] as string;
            _mcpService.Save(data);
            return Json(new ServiceResponse { status = 200, message = "Update CPL Berhasil!" });
        }
        [HttpPost]
        public ActionResult PostDeleteCPL(int id)
        {
            var data = _mcpService.Get(id);
            data.IsDeleted = true;
            data.UpdatedBy = Session["username"] as string;

            _mcpService.Save(data);
            return Json(new ServiceResponse { status = 200, message = "Master CPL Dihapus!" });
        }

        [HttpPost]
        public ActionResult GetProdi(string idFakultas, string search, string jenjang)
        {
            var prodi = _mcpService.GetProdiByFakultas(jenjang, idFakultas, search);
            return Json(prodi);
        }

        public ActionResult ModalDetailMasterCpl(int id)
        {
            var data = _mcpService.Get(id);
            return new ContentResult { Content = JsonConvert.SerializeObject(data), ContentType = "application/json" };
        }
        public ActionResult ModalEditMasterCpl(int id)
        {
            var data = _mcpService.Get(id);
            return new ContentResult { Content = JsonConvert.SerializeObject(data), ContentType = "application/json" };
        }
      
        public ActionResult GetFakultas(string search, string jenjang)
        {
            return Json(_mcpService.GetFakultas(jenjang, search), JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetProdiByFakultas(string idFakultas, string search, string jenjang)
        {
            return Json(_mcpService.GetProdiByFakultas(jenjang, idFakultas, search), JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetLokasiByProdi(string namaProdi, string search, string jenjang)
        {
            return Json(_mcpService.GetLokasiByProdi(jenjang, namaProdi, search), JsonRequestBehavior.AllowGet);
        }
        public ActionResult getLookupByTipe(string tipe)
        {
            return Json(_lookupService.getLookupByTipe(tipe), JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetProdiLocByFakultas(string JenjangStudi, string idFakultas, string search)
        {
            return Json(_cplMatakuliah.GetProdiLocByFakultas(JenjangStudi, idFakultas, search), JsonRequestBehavior.AllowGet);
        }
    }
}