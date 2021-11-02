using MBKM.Entities.Models.MBKM;
using MBKM.Entities.ViewModel;
using MBKM.Presentation.Helper;
using MBKM.Services.MBKMServices;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MBKM.Presentation.Areas.Portal.Controllers
{
    [MBKMAuthorize]
    public class KHSController : Controller
    {
        private INilaiKuliahService _transkripService;
        private IJadwalKuliahMahasiswaService _jkMhsService;
        private IMahasiswaService _mahasiswaService;
        private IPendaftaranMataKuliahService _pendaftaranMataKuliahService;
        private IAbsensiService _absensiService;
        private INilaiKuliahService _nilaiKuliahService;

        public KHSController(INilaiKuliahService transkripService, IJadwalKuliahMahasiswaService jkMhsService, IMahasiswaService mahasiswaService, IPendaftaranMataKuliahService pendaftaranMataKuliahService,IAbsensiService absensiService, INilaiKuliahService nilaiKuliahService)
        {
            _transkripService = transkripService;
            _jkMhsService = jkMhsService;
            _mahasiswaService = mahasiswaService;
            _pendaftaranMataKuliahService = pendaftaranMataKuliahService;
            _absensiService = absensiService;
            _nilaiKuliahService = nilaiKuliahService;
        }

        // GET: Portal/KHS
        public ActionResult Index()
        {
            var mahasiswa = GetMahasiswaByEmail(Session["email"] as string);
            ViewData["nama"] = mahasiswa.Nama;
            ViewData["nim"] = mahasiswa.NIM;
            ViewData["univ"] = mahasiswa.NamaUniversitas;

            var list = _pendaftaranMataKuliahService.Find(pmk => pmk.MahasiswaID == mahasiswa.ID && pmk.StatusPendaftaran == "ACCEPTED BY MAHASISWA").ToList();
            Dictionary<string, string> result = new Dictionary<string, string>();
            foreach (var item in list)
            {
                if (!result.ContainsKey(item.JadwalKuliahs.STRM + ""))
                {
                    result.Add(item.JadwalKuliahs.STRM + "", _absensiService.GetSemesterBySTRM(item.JadwalKuliahs.STRM));
                }
            }
            return View(result);
        }
        public ActionResult GetJadwalKuliah(int strm)
        {
            var mahasiswa = GetMahasiswaByEmail(Session["email"] as string);
            List<PendaftaranMataKuliah> pmks = new List<PendaftaranMataKuliah>();
            //List<NilaiKuliah> nilaiKuliahs = new List<NilaiKuliah>();
            //nilaiKuliahs = _nilaiKuliahService.Find(nilaiKuliah => nilaiKuliah.MahasiswaID == 98).ToList();

            if (strm != 0)
            {
                pmks = _pendaftaranMataKuliahService.Find(pmk => pmk.MahasiswaID == mahasiswa.ID && pmk.StatusPendaftaran == "ACCEPTED BY MAHASISWA" && pmk.JadwalKuliahs.STRM == strm).ToList();
            }
            else
            {
                pmks = _pendaftaranMataKuliahService.Find(pmk => pmk.MahasiswaID == mahasiswa.ID && pmk.StatusPendaftaran == "ACCEPTED BY MAHASISWA").ToList();
            }
            var list = pmks.Select(x => new
            {
                Seksi = x.JadwalKuliahs.ClassSection,
                NamaMataKuliah = x.JadwalKuliahs.NamaMataKuliah,
                KodeMataKuliah = x.JadwalKuliahs.KodeMataKuliah,
                SKS = x.JadwalKuliahs.SKS,
                NamaDosen = x.JadwalKuliahs.NamaDosen,
                STRM = x.JadwalKuliahs.STRM.ToString(),
                Presetanse = //x.JadwalKuliahID,
                             GetPresentase(mahasiswa.ID, x.JadwalKuliahID),
                JadwalKuliahID = x.JadwalKuliahID,
                Hasil = GetHasil(mahasiswa.ID, x.JadwalKuliahID, x.JadwalKuliahs.SKS),
                ID = x.ID
            });
            return new ContentResult { Content = JsonConvert.SerializeObject(list), ContentType = "application/json" };
        }
        public string GetPresentase(Int64 MahasiswaID, Int64 JadwalKuliahID)
        {
           
            var CountData2 = _nilaiKuliahService.Find(x => x.MahasiswaID == MahasiswaID && x.JadwalKuliahID == JadwalKuliahID).FirstOrDefault() ;
            var n = CountData2.Grade;
           
            return n.ToString();

        }
        public string GetHasil(Int64 MahasiswaID, Int64 JadwalKuliahID, string sks)
        {

            var CountData2 = _nilaiKuliahService.Find(x => x.MahasiswaID == MahasiswaID && x.JadwalKuliahID == JadwalKuliahID).FirstOrDefault();
            var n = CountData2.Grade;
            if (n == "A")
            {
                var hasil = Convert.ToInt64(sks.Substring(0, sks.IndexOf('.') > 0 ? sks.IndexOf('.') : sks.Length)) * 4.00;
                var gege = hasil.ToString();
                var ccd = gege.Replace(',', '.');
                return ccd;
            }
            if (n == "A-")
            {
                var hasil = Convert.ToInt64(sks.Substring(0, sks.IndexOf('.') > 0 ? sks.IndexOf('.') : sks.Length)) * 3.70;
                var gege = hasil.ToString();
                var ccd = gege.Replace(',', '.');
                return ccd;
            }
            if (n == "B+")
            {
                var hasil = Convert.ToInt64(sks.Substring(0, sks.IndexOf('.') > 0 ? sks.IndexOf('.') : sks.Length)) * 3.30;
                var gege = hasil.ToString();
                var ccd = gege.Replace(',', '.');
                return ccd;
            }
            if (n == "B")
            {
                var hasil = Convert.ToInt64(sks.Substring(0, sks.IndexOf('.') > 0 ? sks.IndexOf('.') : sks.Length)) * 3.00;
                var gege = hasil.ToString();
                var ccd = gege.Replace(',', '.');
                return ccd;
            }
            if (n == "B-")
            {
                var hasil = Convert.ToInt64(sks.Substring(0, sks.IndexOf('.') > 0 ? sks.IndexOf('.') : sks.Length)) * 2.70;
                var gege = hasil.ToString();
                var ccd = gege.Replace(',', '.');
                return ccd;
            }
            if (n == "C+")
            {
                var hasil = Convert.ToInt64(sks.Substring(0, sks.IndexOf('.') > 0 ? sks.IndexOf('.') : sks.Length)) * 2.30;
                var gege = hasil.ToString();
                var ccd = gege.Replace(',', '.');
                return ccd;
            }
            if (n == "C")
            {
                var hasil = Convert.ToInt64(sks.Substring(0, sks.IndexOf('.') > 0 ? sks.IndexOf('.') : sks.Length)) * 2.00;
                var gege = hasil.ToString();
                var ccd = gege.Replace(',', '.');
                return ccd;
            }
            if (n == "D")
            {
                var hasil = Convert.ToInt64(sks.Substring(0, sks.IndexOf('.') > 0 ? sks.IndexOf('.') : sks.Length)) * 1.00;
                var gege = hasil.ToString();
                var ccd = gege.Replace(',', '.');
                return ccd;
            }
            var cek = Convert.ToInt64(sks.Substring(0, sks.IndexOf('.') > 0 ? sks.IndexOf('.') : sks.Length));
            var E = 0;
            return E.ToString();

        }
        public ActionResult KHS(string strmT, int strmID)
        {

            var mahasiswa = GetMahasiswaByEmail(Session["email"] as string);
            ViewData["nama"] = mahasiswa.Nama;
            ViewData["nim"] = mahasiswa.NIM;
            ViewData["univ"] = mahasiswa.NamaUniversitas;
            ViewData["semester"] = strmT;
            //var mahasiswa = GetMahasiswaByEmail(Session["email"] as string);
            List<PendaftaranMataKuliah> pmks = new List<PendaftaranMataKuliah>();
            //List<NilaiKuliah> nilaiKuliahs = new List<NilaiKuliah>();
            //nilaiKuliahs = _nilaiKuliahService.Find(nilaiKuliah => nilaiKuliah.MahasiswaID == 98).ToList();

            if (strmID != 0)
            {
                pmks = _pendaftaranMataKuliahService.Find(pmk => pmk.MahasiswaID == mahasiswa.ID && pmk.StatusPendaftaran == "ACCEPTED BY MAHASISWA" && pmk.JadwalKuliahs.STRM == strmID).ToList();
            }
            else
            {
                pmks = _pendaftaranMataKuliahService.Find(pmk => pmk.MahasiswaID == mahasiswa.ID && pmk.StatusPendaftaran == "ACCEPTED BY MAHASISWA").ToList();
            }
            var list = pmks.Select(x => new VMKHS()
            {
                Seksi = x.JadwalKuliahs.ClassSection,
                //NamaMataKuliah = x.JadwalKuliahs.NamaMataKuliah,
                NamaMatakuliah = x.JadwalKuliahs.NamaMataKuliah,
                KodeMataKuliah = x.JadwalKuliahs.KodeMataKuliah,
                SKS = x.JadwalKuliahs.SKS,
                //NamaDosen = x.JadwalKuliahs.NamaDosen,
                //STRM = x.JadwalKuliahs.STRM.ToString(),
                Nilai = //x.JadwalKuliahID,
                             GetPresentase(mahasiswa.ID, x.JadwalKuliahID),
                //JadwalKuliahID = x.JadwalKuliahID,
                Hasil = GetHasil(mahasiswa.ID, x.JadwalKuliahID, x.JadwalKuliahs.SKS)
                //ID = x.ID
            });
            //List<VMKHS> khsmodel = new List<VMKHS>();
            //VMKHS khsModel = list;
            //ViewBag.pmk1 = list;
            ViewData["EmployeeList2"] = list;

            return View();
        }
        [AllowAnonymous]
        public ActionResult PrintKHS(string strmT, int strmID)
        {
            var mahasiswa = GetMahasiswaByEmail(Session["email"] as string);
            ViewData["nama"] = mahasiswa.Nama;
            ViewData["nim"] = mahasiswa.NIM;
            ViewData["univ"] = mahasiswa.NamaUniversitas;
            ViewData["semester"] = strmT;
            //var mahasiswa = GetMahasiswaByEmail(Session["email"] as string);
            List<PendaftaranMataKuliah> pmks = new List<PendaftaranMataKuliah>();
            //List<NilaiKuliah> nilaiKuliahs = new List<NilaiKuliah>();
            //nilaiKuliahs = _nilaiKuliahService.Find(nilaiKuliah => nilaiKuliah.MahasiswaID == 98).ToList();

            if (strmID != 0)
            {
                pmks = _pendaftaranMataKuliahService.Find(pmk => pmk.MahasiswaID == mahasiswa.ID && pmk.StatusPendaftaran == "ACCEPTED BY MAHASISWA" && pmk.JadwalKuliahs.STRM == strmID).ToList();
            }
            else
            {
                pmks = _pendaftaranMataKuliahService.Find(pmk => pmk.MahasiswaID == mahasiswa.ID && pmk.StatusPendaftaran == "ACCEPTED BY MAHASISWA").ToList();
            }
            var list = pmks.Select(x => new VMKHS()
            {
                Seksi = x.JadwalKuliahs.ClassSection,
                //NamaMataKuliah = x.JadwalKuliahs.NamaMataKuliah,
                NamaMatakuliah = x.JadwalKuliahs.NamaMataKuliah,
                KodeMataKuliah = x.JadwalKuliahs.KodeMataKuliah,
                SKS = x.JadwalKuliahs.SKS,
                //NamaDosen = x.JadwalKuliahs.NamaDosen,
                //STRM = x.JadwalKuliahs.STRM.ToString(),
                Nilai = //x.JadwalKuliahID,
                             GetPresentase(mahasiswa.ID, x.JadwalKuliahID),
                //JadwalKuliahID = x.JadwalKuliahID,
                Hasil = GetHasil(mahasiswa.ID, x.JadwalKuliahID, x.JadwalKuliahs.SKS)
                //ID = x.ID
            });
            //List<VMKHS> khsmodel = new List<VMKHS>();
            //VMKHS khsModel = list;
            //ViewBag.pmk1 = list;
            ViewData["EmployeeList2"] = list;

            var report = new Rotativa.ViewAsPdf("KHS")
            { FileName = "-KHS.pdf" };
            return report;
        }
        public Mahasiswa GetMahasiswaByEmail(string email)
        {
            return _mahasiswaService.Find(m => m.Email == email).FirstOrDefault();
        }
    }
}