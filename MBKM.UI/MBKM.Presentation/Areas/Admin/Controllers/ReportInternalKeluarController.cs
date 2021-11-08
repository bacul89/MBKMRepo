using MBKM.Entities.ViewModel;
using MBKM.Presentation.Helper;
using MBKM.Services;
using MBKM.Services.MBKMServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MBKM.Presentation.Areas.Admin.Controllers
{
    [MBKMAuthorize]
    public class ReportInternalKeluarController : Controller
    {
        // GET: Admin/ReportInternalKeluar
        private IJadwalUjianMBKMService _jadwalUjianMBKMService;
        private ILookupService _lookupService;
        private IPendaftaranMataKuliahService _pendaftaranMataKuliahService;
        private IFeedbackMatkulService _feedbackMatkulService;

        public ReportInternalKeluarController(IJadwalUjianMBKMService jadwalUjianMBKMService, ILookupService lookupService, IPendaftaranMataKuliahService pendaftaranMataKuliahService, IFeedbackMatkulService feedbackMatkulService)
        {
            _jadwalUjianMBKMService = jadwalUjianMBKMService;
            _pendaftaranMataKuliahService = pendaftaranMataKuliahService;
            _feedbackMatkulService = feedbackMatkulService;
            _lookupService = lookupService;
        }   

        public ActionResult Index()
        {
            ViewData["role"] = HttpContext.Session["RoleName"].ToString();
            IEnumerable<VMLookup> tempJenjang = _lookupService.getLookupByTipe("JenjangStudi");
            ViewData["Jenjang"] = tempJenjang;
            IEnumerable<VMSemester> data = _jadwalUjianMBKMService.getAllSemester();
            ViewData["semester"] = data;
            return View();
        }
        public ActionResult DataTable(int strm)
        {
            var dataMahasiswa = _pendaftaranMataKuliahService.GetListPendaftaranInternalPertukaran(strm);
            var dataSemester = _feedbackMatkulService.GetSemesterByStrm(strm.ToString());

            List<String[]> final = new List<String[]>();
            foreach (var d in dataMahasiswa)
            {
                final.Add(new String[]{
                    dataSemester.Nama,
                    d.JadwalKuliahs.JenjangStudi,
                    d.mahasiswas.NIM,
                    d.mahasiswas.Nama,
                    d.JadwalKuliahs.NamaProdi,
                    d.InformasiPertukaran.LokasiTugas,
                    d.JadwalKuliahs.NamaMataKuliah,
                    d.JadwalKuliahs.SKS,
                    d.NilaiKuliah.Grade,
                    d.MatkulKodeAsal,
                    d.MatkulAsal,
                    "A",
                    "4.00"
                });
            }

            return Json(final);
        }
    }
}