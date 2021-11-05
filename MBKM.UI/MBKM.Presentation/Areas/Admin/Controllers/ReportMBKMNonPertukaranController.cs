using MBKM.Entities.ViewModel;
using MBKM.Services.MBKMServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MBKM.Presentation.Areas.Admin.Controllers
{
    public class ReportMBKMNonPertukaranController : Controller
    {
        private IPendaftaranMataKuliahService _pendaftaranMataKuliahService;
        private IMahasiswaService _mahasiswaService;
        private IInformasiPertukaranService _informasiPertukaranService;
        private IFeedbackMatkulService _feedbackMatkulService;
        private IJadwalKuliahService _jadwalKuliahService;
        private IJadwalUjianMBKMService _jadwalUjianMBKMService;

        public ReportMBKMNonPertukaranController(IPendaftaranMataKuliahService pendaftaranMataKuliahService, IMahasiswaService mahasiswaService, IInformasiPertukaranService informasiPertukaranService, IFeedbackMatkulService feedbackMatkulService, IJadwalKuliahService jadwalKuliahService, IJadwalUjianMBKMService jadwalUjianMBKMService)
        {
            _pendaftaranMataKuliahService = pendaftaranMataKuliahService;
            _mahasiswaService = mahasiswaService;
            _informasiPertukaranService = informasiPertukaranService;
            _feedbackMatkulService = feedbackMatkulService;
            _jadwalKuliahService = jadwalKuliahService;
            _jadwalUjianMBKMService = jadwalUjianMBKMService;
        }


        // GET: Admin/ReportMBKMNonPertukaran
        public ActionResult Index()
        {
            IEnumerable<VMSemester> data = _jadwalUjianMBKMService.getAllSemester();
            ViewData["semester"] = data;
            return View();
        }

        public ActionResult DataTable(int strm)
        {
            var dataMahasiswa = _pendaftaranMataKuliahService.GetListPendaftaranNonPertukaran(strm);
            var dataSemester = _feedbackMatkulService.GetSemesterByStrm(strm.ToString());
            
            List<String[]> final = new List<String[]>();
            foreach(var d in dataMahasiswa)
            {
                var tglFinal = "";

                if (d.InformasiPertukaran.TanggalSK.ToString() != null)
                {
                    var tgl = d.InformasiPertukaran.TanggalSK.ToString().Split(' ');
                    tglFinal = tgl[0];
                }
                else
                {
                    tglFinal = "-";
                }

                final.Add(new String[]{
                    dataSemester.Nama,
                    d.JadwalKuliahs.JenjangStudi,
                    d.mahasiswas.NIM,
                    d.mahasiswas.Nama,
                    d.JadwalKuliahs.NamaProdi,
                    d.InformasiPertukaran.JenisKerjasama,
                    d.InformasiPertukaran.JudulAktivitas,
                    d.InformasiPertukaran.LokasiTugas,
                    d.InformasiPertukaran.NoSK,
                    tglFinal,
                    d.JadwalKuliahs.KodeMataKuliah,
                    d.JadwalKuliahs.NamaMataKuliah,
                    d.JadwalKuliahs.SKS,
                    d.NilaiKuliah.NilaiTotal.ToString(),
                    d.NilaiKuliah.Grade
                });
            }

            return Json(final);
        }
    }
}