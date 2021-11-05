using MBKM.Entities.ViewModel;
using MBKM.Services.MBKMServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MBKM.Presentation.Areas.Admin.Controllers
{
    public class ReportMahasiswaMBKMController : Controller
    {
        private IPendaftaranMataKuliahService _pendaftaranMataKuliahService;
        private IMahasiswaService _mahasiswaService;
        private IInformasiPertukaranService _informasiPertukaranService;
        private IFeedbackMatkulService _feedbackMatkulService;
        private IJadwalKuliahService _jadwalKuliahService;
        private IJadwalUjianMBKMService _jadwalUjianMBKMService;

        public ReportMahasiswaMBKMController(IPendaftaranMataKuliahService pendaftaranMataKuliahService, IMahasiswaService mahasiswaService, IInformasiPertukaranService informasiPertukaranService, IFeedbackMatkulService feedbackMatkulService, IJadwalKuliahService jadwalKuliahService, IJadwalUjianMBKMService jadwalUjianMBKMService)
        {
            _pendaftaranMataKuliahService = pendaftaranMataKuliahService;
            _mahasiswaService = mahasiswaService;
            _informasiPertukaranService = informasiPertukaranService;
            _feedbackMatkulService = feedbackMatkulService;
            _jadwalKuliahService = jadwalKuliahService;
            _jadwalUjianMBKMService = jadwalUjianMBKMService;
        }



        // GET: Admin/ReportMahasiswaMBKM
        public ActionResult Index()
        {
            IEnumerable<VMSemester> data = _jadwalUjianMBKMService.getAllSemester();
            ViewData["semester"] = data;
            return View();
        }

        [HttpPost]
        public ActionResult TableData()
        {
            int strm = 2110;
            var dataProdi =  _mahasiswaService.GetAllDataProdi();
            var dataMahasiswa = _pendaftaranMataKuliahService.GetListPendaftaranAndInformasiPertukaran(strm);
            var dataSemester = _feedbackMatkulService.GetSemesterByStrm(strm.ToString());


            List<String[]> final = new List<String[]>();

            foreach (var d in dataProdi)
            {
                int prodiId = int.Parse(d.IDProdi);
                var datapertama = _jadwalKuliahService.Find(x => x.ProdiID == prodiId).FirstOrDefault();
                if (datapertama != null)
                {
                    var countEksternal = dataMahasiswa.Where(x => x.JadwalKuliahs.ProdiID == int.Parse(d.IDProdi) && x.mahasiswas.NIM != x.mahasiswas.NIMAsal).Count();
                    var internalLintasProdi = dataMahasiswa.Where(x => x.JadwalKuliahs.ProdiID == int.Parse(d.IDProdi) && x.InformasiPertukaran.JenisKerjasama.ToLower() == "internal" && !x.InformasiPertukaran.JenisPertukaran.ToLower().Contains("non")).Count();
                    var internalKeLuar = dataMahasiswa.Where(x => x.JadwalKuliahs.ProdiID == int.Parse(d.IDProdi) && x.InformasiPertukaran.JenisKerjasama.ToLower().Contains("luar") && !x.InformasiPertukaran.JenisPertukaran.ToLower().Contains("non")).Count();
                    var internalNonPertukaran = dataMahasiswa.Where(x => x.JadwalKuliahs.ProdiID == int.Parse(d.IDProdi) && x.InformasiPertukaran.JenisPertukaran.ToLower().Contains("non")).Count();

                    final.Add(new String[]{
                        dataSemester.Nama,
                        datapertama.JenjangStudi,
                        datapertama.NamaFakultas,
                        datapertama.NamaProdi,
                        datapertama.Lokasi,
                        internalLintasProdi.ToString(),
                        internalKeLuar.ToString(),
                        countEksternal.ToString(),
                        internalNonPertukaran.ToString()
                    });
                }


            }
            return Json(final);
        }
    }
}