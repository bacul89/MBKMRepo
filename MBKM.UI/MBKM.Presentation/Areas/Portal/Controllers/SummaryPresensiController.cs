using MBKM.Entities.Models.MBKM;
using MBKM.Presentation.Helper;
using MBKM.Presentation.Models;
using MBKM.Services.MBKMServices;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MBKM.Presentation.Areas.Portal.Controllers
{
    [MBKMAuthorize]
    public class SummaryPresensiController : Controller
    {
        // GET: Portal/SummaryPresensi
        private IMahasiswaService _mahasiswaService;
        private IPendaftaranMataKuliahService _pendaftaranMataKuliahService;
        private IAbsensiService _absensiService;
        private IJadwalKuliahService _jdwlService;
        public SummaryPresensiController(IAbsensiService absensiService, IMahasiswaService mahasiswaService, IPendaftaranMataKuliahService pendaftaranMataKuliahService, IJadwalKuliahService jdwlService)
        {
            _mahasiswaService = mahasiswaService;
            _pendaftaranMataKuliahService = pendaftaranMataKuliahService;
            _absensiService = absensiService;
            _jdwlService = jdwlService;
        }
        public ActionResult Index()
        {
            var mahasiswa = GetMahasiswaByEmail(Session["email"] as string);
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
                Lokasi = x.JadwalKuliahs.Lokasi,
                NamaFakultas = x.JadwalKuliahs.NamaFakultas,
                NamaProdi = x.JadwalKuliahs.NamaProdi,
                NamaMataKuliah = x.JadwalKuliahs.NamaMataKuliah,
                KodeMataKuliah = x.JadwalKuliahs.KodeMataKuliah,
                SKS = x.JadwalKuliahs.SKS,
                NamaDosen = x.JadwalKuliahs.NamaDosen,
                STRM = x.JadwalKuliahs.STRM.ToString(),
                Presetanse = GetPresentase(mahasiswa.ID, x.JadwalKuliahID),
                JadwalKuliahID = x.JadwalKuliahID,
                ID = x.ID
            });
            return new ContentResult { Content = JsonConvert.SerializeObject(list), ContentType = "application/json" };
        }
        public ActionResult GetSumAbsensi(int jadwalKuliahId)
        {
            var mahasiswa = GetMahasiswaByEmail(Session["email"] as string);
            List<Absensi> absensis = _absensiService.Find(a => a.JadwalKuliahID == jadwalKuliahId && a.MahasiswaID== mahasiswa.ID).OrderBy(a => a.TanggalAbsen).ToList();
            return new ContentResult { Content = JsonConvert.SerializeObject(absensis), ContentType = "application/json" };
        }
        public ActionResult DetailSummaryPresensiKelas(int id)
        {
            return View(id);
        }
        public Mahasiswa GetMahasiswaByEmail(string email)
        {
            return _mahasiswaService.Find(m => m.Email == email).FirstOrDefault();
        }
        public ActionResult GetJadwalKuliahById(int id)
        {
            var result = _pendaftaranMataKuliahService.Get(id);
            return new ContentResult { Content = JsonConvert.SerializeObject(result), ContentType = "application/json" };
        }
        public string GetPresentase(Int64 MahasiswaID, Int64 JadwalKuliahID)
        {
            int CountData = _absensiService.Find(x => x.MahasiswaID == MahasiswaID && x.JadwalKuliahID == JadwalKuliahID).Count();
            int CountDataPresent = _absensiService.Find(x => x.MahasiswaID == MahasiswaID && x.JadwalKuliahID == JadwalKuliahID
                                    && x.Present == true).Count();
            double persen = 0;
            if (CountData > 0)
            {
                persen = (CountDataPresent * 100)/ CountData;
            }
            return persen.ToString() + "%";

        }
        //public ActionResult PrintDetailgagal(int id)
        //{
        //    var report = new Rotativa.ActionAsPdf("DetailSummaryPresensiKelas", new { id = id });
        //    return report;
        //}
        public ActionResult BAP(int id)
        {
            //var report = new Rotativa.ActionAsPdf("Index");
            //dynamic mymodel = new ExpandoObject();
            string email = Session["email"] as string;
            Mahasiswa mahasiswa = GetMahasiswaByEmail(email);
            Absensi absensi = _absensiService.Find(a => a.ID == id).FirstOrDefault();
            ViewData["nama"] = mahasiswa.Nama;
            ViewData["nim"] = mahasiswa.NIM;
            ViewData["univ"] = mahasiswa.NamaUniversitas;
            ViewData["dosen"] = absensi.NamaDosen;
            ViewData["present"] = absensi.Present;
            var jdwlID = absensi.JadwalKuliahID;
            JadwalKuliah jdwl = _jdwlService.Find(j => j.ID == jdwlID).FirstOrDefault();
            ViewData["prodi"] = jdwl.NamaProdi;
            ViewData["namaMK"] = jdwl.NamaMataKuliah;
            ViewData["kodeMK"] = jdwl.KodeMataKuliah;
            ViewData["seksi"] = jdwl.ClassSection;
            ViewData["tanggal"] = absensi.TanggalAbsen;
            return View(id);
        }
        
        [AllowAnonymous]
        public ActionResult PrintDetail(int id)
        {
            string email = Session["email"] as string;
            Mahasiswa mahasiswa = GetMahasiswaByEmail(email);
            Absensi absensi = _absensiService.Find(a => a.ID == id).FirstOrDefault();
            ViewData["nama"] = mahasiswa.Nama;
            ViewData["nim"] = mahasiswa.NIM;
            ViewData["univ"] = mahasiswa.NamaUniversitas;
            ViewData["dosen"] = absensi.NamaDosen;
            ViewData["present"] = absensi.Present;
            var jdwlID = absensi.JadwalKuliahID;
            JadwalKuliah jdwl = _jdwlService.Find(j => j.ID == jdwlID).FirstOrDefault();
            ViewData["prodi"] = jdwl.NamaProdi;
            ViewData["namaMK"] = jdwl.NamaMataKuliah;
            ViewData["kodeMK"] = jdwl.KodeMataKuliah;
            ViewData["seksi"] = jdwl.ClassSection;
            ViewData["tanggal"] = absensi.TanggalAbsen;

            var tgl = absensi.TanggalAbsen.ToString("dd/MM/yyyy");
            
            var report = new Rotativa.ViewAsPdf("BAP")
            { FileName = tgl +"-"+ jdwl.KodeMataKuliah + "-BAP.pdf" };
            return report;
        }
        //[AllowAnonymous]
        //public ActionResult PrintDetail()
        //{
        //    //var report = new Rotativa.ActionAsPdf("BAP");
        //    //return report;
        //    Dictionary<string, string> cookieCollection = new Dictionary<string, string>();

        //    foreach (var key in Request.Cookies.AllKeys)
        //    {
        //        cookieCollection.Add(key, Request.Cookies.Get(key).Value);
        //    }

        //    return new Rotativa.ActionAsPdf("BAP")
        //    {
        //        FileName = "Name.pdf",
        //        Cookies = cookieCollection
        //    };
        //}

       
    }
}