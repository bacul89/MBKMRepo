using MBKM.Common.Helpers;
using MBKM.Entities.Models.MBKM;
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
        private ICPLMKPendaftaranService _cPLMKPendaftaranService;
        private ICPLMatakuliahService _cPLMatakuliahService;

        public TrackingStatusPendaftaranController(IPendaftaranMataKuliahService pendaftaranMataKuliahService, ICPLMKPendaftaranService cPLMKPendaftaranService, ICPLMatakuliahService cPLMatakuliahService)
        {
            _pendaftaranMataKuliahService = pendaftaranMataKuliahService;
            _cPLMKPendaftaranService = cPLMKPendaftaranService;
            _cPLMatakuliahService = cPLMatakuliahService;
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

        [HttpPost]
        public ActionResult _getIndexDetailView(int id)
        {
            var data = _pendaftaranMataKuliahService.Get(id);
            return View(data);
        }

        public ActionResult IndexDetailPendaftaran(int id)
        {
            var data = _cPLMKPendaftaranService.Find(x => x.IsDeleted == false && x.PendaftaranMataKuliahID == id).First();
            if (data.PendaftaranMataKuliahs.mahasiswas.NIM != data.PendaftaranMataKuliahs.mahasiswas.NIMAsal)
            {
                ViewData["jenisProgram"] = "Pertukaran";
                ViewData["jenisKegiatan"] = "Eksternal";
            }
            else if (data.PendaftaranMataKuliahs.mahasiswas.NoKerjasama != null)
            {
                ViewData["jenisProgram"] = "Pertukaran";
                ViewData["jenisKegiatan"] = "Internal";
            }
            else if (data.PendaftaranMataKuliahs.mahasiswas.NoKerjasama == null)
            {
                ViewData["jenisProgram"] = "Non-Pertukaran";
                ViewData["jenisKegiatan"] = "Internal";
            }
            var IDMakul = data.PendaftaranMataKuliahs.JadwalKuliahs.MataKuliahID;
            IList<CPLMatakuliah> capaianTujuan = _cPLMatakuliahService.Find(x => x.IDMataKUliah == data.PendaftaranMataKuliahs.JadwalKuliahs.MataKuliahID).ToList();
            ViewData["capaianTujuan"] = capaianTujuan;
            ViewData["countCPTujuan"] = capaianTujuan.Count();
            return View(data);
        }
    }
}