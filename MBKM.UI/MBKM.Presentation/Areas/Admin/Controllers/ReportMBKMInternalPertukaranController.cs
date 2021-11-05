using MBKM.Entities.ViewModel;
using MBKM.Services.MBKMServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MBKM.Presentation.Areas.Admin.Controllers
{

    public class ReportMBKMInternalPertukaranController : Controller
    {
        private IPendaftaranMataKuliahService _pendaftaranMataKuliahService;
        private IMahasiswaService _mahasiswaService;
        private IInformasiPertukaranService _informasiPertukaranService;
        private IFeedbackMatkulService _feedbackMatkulService;
        private IJadwalKuliahService _jadwalKuliahService;
        private IJadwalUjianMBKMService _jadwalUjianMBKMService;

        public ReportMBKMInternalPertukaranController(IPendaftaranMataKuliahService pendaftaranMataKuliahService, IMahasiswaService mahasiswaService, IInformasiPertukaranService informasiPertukaranService, IFeedbackMatkulService feedbackMatkulService, IJadwalKuliahService jadwalKuliahService, IJadwalUjianMBKMService jadwalUjianMBKMService)
        {
            _pendaftaranMataKuliahService = pendaftaranMataKuliahService;
            _mahasiswaService = mahasiswaService;
            _informasiPertukaranService = informasiPertukaranService;
            _feedbackMatkulService = feedbackMatkulService;
            _jadwalKuliahService = jadwalKuliahService;
            _jadwalUjianMBKMService = jadwalUjianMBKMService;
        }

        // GET: Admin/ReportMBKMInternalPertukaran
        public ActionResult Index()
        {
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
                    d.JadwalKuliahs.KodeMataKuliah,
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