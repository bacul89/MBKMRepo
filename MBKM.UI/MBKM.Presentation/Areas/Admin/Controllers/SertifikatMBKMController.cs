using MBKM.Entities.ViewModel;
using MBKM.Services.MBKMServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MBKM.Presentation.Areas.Admin.Controllers
{
    public class SertifikatMBKMController : Controller
    {
        private IJadwalUjianMBKMService _jadwalUjianMBKMService;
        private IPendaftaranMataKuliahService _pendaftaranMataKuliahService;
        private IFeedbackMatkulService _feedbackMatkulService;
        private IMahasiswaService _mahasiswaService;

        public SertifikatMBKMController(IJadwalUjianMBKMService jadwalUjianMBKMService, IPendaftaranMataKuliahService pendaftaranMataKuliahService, IFeedbackMatkulService feedbackMatkulService, IMahasiswaService mahasiswaService)
        {
            _jadwalUjianMBKMService = jadwalUjianMBKMService;
            _pendaftaranMataKuliahService = pendaftaranMataKuliahService;
            _feedbackMatkulService = feedbackMatkulService;
            _mahasiswaService = mahasiswaService;
        }



        // GET: Admin/SertifikatMBKM
        public ActionResult Index()
        {
            IEnumerable<VMSemester> data = _jadwalUjianMBKMService.getAllSemester();
            ViewData["semester"] = data;
            return View();
        }

        [HttpPost]
        public ActionResult GetDataTable(int strm)
        {
            var data = _feedbackMatkulService.Find(x => x.JadwalKuliahs.STRM == strm).GroupBy(z => new {z.MahasiswaID, z.StatusFeedBack}).Select(s => new {MahasiswaID = s.Key.MahasiswaID, Status = s.Key.StatusFeedBack}).ToList();
            var data2 = _feedbackMatkulService.Find(x => x.JadwalKuliahs.STRM == strm)
                .GroupBy(z => new { z.MahasiswaID,z.Mahasiswas})
                .Select(s => new { MahasiswaID = s.Key.MahasiswaID, Mahasiswas = s.Key.Mahasiswas}).ToList();

            List<String[]> final = new List<String[]>();
            var DescSemester = _feedbackMatkulService.GetSemesterByStrm(strm.ToString());
            foreach (var d in data2)
            {
                var dataCheck = data.Where(x => x.MahasiswaID == d.MahasiswaID && x.Status == false).Count();
                if (dataCheck != 0)
                {
                    final.Add(new String[]{
                        d.MahasiswaID.ToString(),
                        DescSemester.Nama,
                        d.Mahasiswas.JenjangStudi,
                        d.Mahasiswas.NamaUniversitas,
                        d.Mahasiswas.NIM,
                        d.Mahasiswas.Nama,
                        d.Mahasiswas.NoKerjasama,
                        "Belum Feedback",
                        "Sudah Bayar"
                    });
                }
                else
                {
                    final.Add(new String[]{
                        d.MahasiswaID.ToString(),
                        DescSemester.Nama,
                        d.Mahasiswas.JenjangStudi,
                        d.Mahasiswas.NamaUniversitas,
                        d.Mahasiswas.NIM,
                        d.Mahasiswas.Nama,
                        d.Mahasiswas.NoKerjasama,
                        "Sudah Feedback",
                        "Sudah Bayar"
                    });
                }
            }
            return Json(final);
        }


        public ActionResult GetFile(int id)
        {
            var tmpMahasiswa = _mahasiswaService.Get(id);
            ViewData["namaMahasiswa"] = tmpMahasiswa.Nama; 
            ViewData["nim"] = tmpMahasiswa.NIM;


            var data = _pendaftaranMataKuliahService.Find(x => x.MahasiswaID == id).ToList();
            ViewData["jumlahMatkul"] = data.Count();
            var jumlahSks = 0;
            foreach(var d in data)
            {
                jumlahSks = jumlahSks + int.Parse(d.JadwalKuliahs.SKS);
            }

            ViewData["jumlahSks"] = jumlahSks;

            return View();

        }
    }
}