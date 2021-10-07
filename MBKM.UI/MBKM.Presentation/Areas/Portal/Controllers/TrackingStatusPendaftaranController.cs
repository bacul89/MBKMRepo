using MBKM.Common.Helpers;
using MBKM.Entities.Models.MBKM;
using MBKM.Presentation.models;
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
            if (data.StatusPendaftaran.Contains("MENUNGGU"))
            {
                ViewData["disabled"] = "true";
            }
            else
            {
                ViewData["disabled"] = "";
            }

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
            if (data.CPLMatakuliahID != null)
            {
                IList<CPLMatakuliah> capaianAsal = _cPLMatakuliahService.Find(x => x.IDMataKUliah == data.CPLMatakuliahs.IDMataKUliah).ToList();
                ViewData["capaianAsal"] = capaianAsal;
                ViewData["countCPAsal"] = capaianAsal.Count();

            }

            if (data.PendaftaranMataKuliahs.StatusPendaftaran.Contains("MAHASISWA"))
            {
                ViewData["disabled"] = "true";
            }
            else
            {
                ViewData["disabled"] = "";
            }

            ViewData["capaianTujuan"] = capaianTujuan;
            ViewData["countCPTujuan"] = capaianTujuan.Count();
            return View(data);
        }

        [HttpPost]
        public JsonResult PostApprovalAccepted(int id)
        {
            try
            {
                CPLMKPendaftaran TmpApproval = _cPLMKPendaftaranService.Get(id);
                PendaftaranMataKuliah pendaftaran = _pendaftaranMataKuliahService.Get(TmpApproval.PendaftaranMataKuliahID);
                pendaftaran.StatusPendaftaran = "ACCEPTED BY MAHASISWA";
                pendaftaran.UpdatedDate = DateTime.Now;
                try
                {
                    _pendaftaranMataKuliahService.Save(pendaftaran);
                }
                catch (Exception e)
                {
                    return Json(new ServiceResponse { status = 300, message = "Terjadi Kesalahan Saat Proses Accepted" });
                }
                return Json(new ServiceResponse { status = 200, message = "Done" });
            }
            catch (Exception e)
            {
                return Json(new ServiceResponse { status = 500, message = "Error" });
            }
        }

        [HttpPost]
        public JsonResult PostApprovalRejected(int id)
        {
            try
            {
                CPLMKPendaftaran TmpApproval = _cPLMKPendaftaranService.Get(id);
                PendaftaranMataKuliah pendaftaran = _pendaftaranMataKuliahService.Get(TmpApproval.PendaftaranMataKuliahID);
                pendaftaran.StatusPendaftaran = "REJECTED BY MAHASISWA";
                pendaftaran.UpdatedDate = DateTime.Now;
                try
                {
                    _pendaftaranMataKuliahService.Save(pendaftaran);
                }
                catch (Exception e)
                {
                    return Json(new ServiceResponse { status = 300, message = "Terjadi Kesalahan Saat Proses Accepted" });
                }
                return Json(new ServiceResponse { status = 200, message = "Done" });
            }
            catch (Exception e)
            {
                return Json(new ServiceResponse { status = 500, message = "Error" });
            }
        }
    }
}