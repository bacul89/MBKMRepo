using MBKM.Common.Helpers;
using MBKM.Entities.Models.MBKM;
using MBKM.Presentation.models;
using MBKM.Services.MBKMServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MBKM.Presentation.Helper;

namespace MBKM.Presentation.Areas.Portal.Controllers
{
    [MBKMAuthorize]
    public class TrackingStatusPendaftaranController : Controller
    {
        private IPendaftaranMataKuliahService _pendaftaranMataKuliahService;
        private ICPLMKPendaftaranService _cPLMKPendaftaranService;
        private ICPLMatakuliahService _cPLMatakuliahService;
        private IMahasiswaService _mahasiswaService;
        private IApprovalPendaftaranService _approvalPendaftaranService;

        public TrackingStatusPendaftaranController(IPendaftaranMataKuliahService pendaftaranMataKuliahService, ICPLMKPendaftaranService cPLMKPendaftaranService, ICPLMatakuliahService cPLMatakuliahService, IMahasiswaService mahasiswaService, IApprovalPendaftaranService approvalPendaftaranService)
        {
            _pendaftaranMataKuliahService = pendaftaranMataKuliahService;
            _cPLMKPendaftaranService = cPLMKPendaftaranService;
            _cPLMatakuliahService = cPLMatakuliahService;
            _mahasiswaService = mahasiswaService;
            _approvalPendaftaranService = approvalPendaftaranService;
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

            IList<ApprovalPendaftaran> dataApproval = _approvalPendaftaranService.Find(x => x.PendaftaranMataKuliahID == id).OrderByDescending(x => x.ID).ToList();
            ViewData["lastStatus"] = _approvalPendaftaranService.Find(x => x.PendaftaranMataKuliahID == id).Last().StatusPendaftaran;
            ViewData["status"] = dataApproval;
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
            ViewData["catatanBaru"] = _approvalPendaftaranService.Find(x => x.PendaftaranMataKuliahID == data.PendaftaranMataKuliahID && (x.StatusPendaftaran.Contains("APPROVED") || x.StatusPendaftaran.Contains("REJECTED"))).FirstOrDefault().Catatan;
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
                    var NimBaru = _mahasiswaService.GetNim();
                    
                    if (pendaftaran.mahasiswas.NIM == null || (pendaftaran.mahasiswas.NIM != pendaftaran.mahasiswas.NIMAsal && !pendaftaran.mahasiswas.NIM.Contains("MBKM")))
                    {
                        Mahasiswa tmpMhs = _mahasiswaService.Get(pendaftaran.MahasiswaID);
                        tmpMhs.NIM = NimBaru;
                        _mahasiswaService.Save(tmpMhs);
                        string[] tempNimArray = NimBaru.Split(new string[] { "MBKM" }, StringSplitOptions.None);
                        int CountNim = Int32.Parse(tempNimArray[1]);
                        _mahasiswaService.UpdateNim(CountNim);
                    }

                    ApprovalPendaftaran tempHistoryApproval = new ApprovalPendaftaran();
                    tempHistoryApproval.Approval = "MAHASISWA";
                    tempHistoryApproval.Catatan = "-";
                    tempHistoryApproval.StatusPendaftaran = "ACCEPTED BY MAHASISWA";
                    tempHistoryApproval.IsActive = true;
                    tempHistoryApproval.IsDeleted = false;
                    tempHistoryApproval.PendaftaranMataKuliahID = TmpApproval.PendaftaranMataKuliahID;
                    tempHistoryApproval.CreatedBy = "MAHASISWA";
                    tempHistoryApproval.UpdatedBy = "MAHASISWA";
                    tempHistoryApproval.UpdatedDate = DateTime.Now;
                    tempHistoryApproval.CreatedDate = DateTime.Now;
                    try
                    {
                        _approvalPendaftaranService.Save(tempHistoryApproval);
                    }
                    catch (Exception e)
                    {
                        return Json(new ServiceResponse { status = 300, message = "Error Saat Menyimpan Data Pendaftaran" });
                    }
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
                    var NimBaru = _mahasiswaService.GetNim();

                    ApprovalPendaftaran tempHistoryApproval = new ApprovalPendaftaran();
                    tempHistoryApproval.Approval = "MAHASISWA";
                    tempHistoryApproval.Catatan = "-";
                    tempHistoryApproval.StatusPendaftaran = "REJECTED BY MAHASISWA";
                    tempHistoryApproval.IsActive = true;
                    tempHistoryApproval.IsDeleted = false;
                    tempHistoryApproval.PendaftaranMataKuliahID = TmpApproval.PendaftaranMataKuliahID;
                    tempHistoryApproval.CreatedBy = "MAHASISWA";
                    tempHistoryApproval.UpdatedBy = "MAHASISWA";
                    tempHistoryApproval.UpdatedDate = DateTime.Now;
                    tempHistoryApproval.CreatedDate = DateTime.Now;
                    try
                    {
                        _approvalPendaftaranService.Save(tempHistoryApproval);
                    }
                    catch (Exception e)
                    {
                        return Json(new ServiceResponse { status = 300, message = "Error Saat Menyimpan Data Pendaftaran" });
                    }
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