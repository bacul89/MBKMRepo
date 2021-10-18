using MBKM.Common.Helpers;
using MBKM.Entities.Models.MBKM;
using MBKM.Entities.ViewModel;
using MBKM.Presentation.Helper;
using MBKM.Services;
using MBKM.Services.MBKMServices;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MBKM.Presentation.Areas.Admin.Controllers
{
    [MBKMAuthorize]
    public class LinkFasilitasAdminController : Controller
    {
        // GET: Admin/LinkFasilitasAdmin
        private ILinkFasilitasService _linkFasilitasService;
        private ICPLMatakuliahService _cplMatakuliah;
        private ILookupService _lookupService;
        private IJadwalKuliahService _jkService;
        private IMasterCapaianPembelajaranService _mcpService;
        public LinkFasilitasAdminController(ICPLMatakuliahService cplMatakuliah, ILookupService lookupService, 
            IJadwalKuliahService jkService, IMasterCapaianPembelajaranService mcpService,
            ILinkFasilitasService linkFasilitasService)
        {
            _cplMatakuliah = cplMatakuliah;
            _jkService = jkService;
            _lookupService = lookupService;
            _linkFasilitasService = linkFasilitasService;
            _mcpService = mcpService;
        }
        public ActionResult Index()
        {
            var listSection = _linkFasilitasService.getSection();
            ViewData["listSection"] = listSection;
            return View();
        }
        public JsonResult GetSection()
        {
            return Json(_linkFasilitasService.getSection(), JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult GetMataKuliah(int skip, int take, string searchBy, string idProdi, string idFakultas)
        {

            var final = _linkFasilitasService.GetMatkul(skip, take, searchBy, idProdi, idFakultas);
            List<object> data = new List<object>();
            foreach (var p in final)
            {
                var q = new
                {
                    id = p.CRSE_ID,
                    text = p.DESCR,
                    kode = p.Expr1
                };
                data.Add(q);
            }

            return Json(data);
        }





        /* Lookup --<> */
        public ActionResult getLookupByTipe(string tipe)
        {
            return Json(_lookupService.getLookupByTipe(tipe), JsonRequestBehavior.AllowGet);
        }


        /* Attribute Kuliah --<> */
        public ActionResult GetFakultas(string search, string JenjangStudi)
        {

            return Json(_mcpService.GetFakultas(JenjangStudi, search), JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetProdiByFakultas(string JenjangStudi, string idFakultas, string search)
        {
            return Json(_mcpService.GetProdiByFakultas(JenjangStudi, idFakultas, search), JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetLokasiByProdi(string JenjangStudi, string namaProdi, string search)
        {
            return Json(_mcpService.GetLokasiByProdi(JenjangStudi, namaProdi, search), JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetMataKuliahByProdi(string namaProdi, string lokasi)
        {
            //return new ContentResult { Content = JsonConvert.SerializeObject(_jkService.Find(jk => jk.NamaProdi == namaProdi && jk.Lokasi == lokasi && jk.NamaMataKuliah == search).ToList()), ContentType = "application/json" };
            List<JadwalKuliah> jks = new List<JadwalKuliah>();
            List<string> jadwalKuliahs = new List<string>();
            foreach (var item in _jkService.Find(jk => jk.NamaProdi == namaProdi && jk.Lokasi == lokasi).ToList())
            {
                if (!jadwalKuliahs.Contains(item.NamaMataKuliah))
                {
                    jks.Add(item);
                    jadwalKuliahs.Add(item.NamaMataKuliah);
                }
            }
            return new ContentResult { Content = JsonConvert.SerializeObject(jks), ContentType = "application/json" };

        }
        public ActionResult GetMataKuliahByID(string MataKuliahID)
        {
            return new ContentResult { Content = JsonConvert.SerializeObject(_jkService.Find(jk => jk.MataKuliahID == MataKuliahID).ToList()), ContentType = "application/json" };
        }
        public JsonResult SearchList(DataTableAjaxPostModel model, string idProdi, string lokasi, string idFakultas, string jenjangStudi, string idMatakuliah,string seksi)
        {
            VMLinkFasilitas vMLinkFasilitas = _linkFasilitasService.SearchListJadwalKuliah(model, idProdi, lokasi, idFakultas, jenjangStudi, idMatakuliah,seksi);
            return Json(new
            {
                // this is what datatables wants sending back
                draw = model.draw,
                recordsTotal = vMLinkFasilitas.TotalCount,
                recordsFiltered = vMLinkFasilitas.TotalFilterCount,
                data = vMLinkFasilitas.gridDatas
            });
        }

        public ActionResult ModalUpdateLinkFasilitas(int id)
        {
           
            var data = _linkFasilitasService.Get(id);
            return View(data);
        }
    }
}