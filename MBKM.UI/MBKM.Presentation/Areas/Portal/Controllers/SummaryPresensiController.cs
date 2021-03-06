using MBKM.Entities.Models.MBKM;
using MBKM.Entities.ViewModel;
using MBKM.Presentation.Helper;
using MBKM.Presentation.Models;
using MBKM.Services.MBKMServices;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text.RegularExpressions;
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
        private IReportBAPService _reportBAPService;
        public SummaryPresensiController(IAbsensiService absensiService, IMahasiswaService mahasiswaService, IPendaftaranMataKuliahService pendaftaranMataKuliahService, IJadwalKuliahService jdwlService, IReportBAPService reportBAPService)
        {
            _mahasiswaService = mahasiswaService;
            _pendaftaranMataKuliahService = pendaftaranMataKuliahService;
            _absensiService = absensiService;
            _jdwlService = jdwlService;
            _reportBAPService = reportBAPService;
        }
        public ActionResult Index()
        {
            var mahasiswa = GetMahasiswaByEmail(Session["emailMahasiswa"] as string);
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
            var mahasiswa = GetMahasiswaByEmail(Session["emailMahasiswa"] as string);
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
            var mahasiswa = GetMahasiswaByEmail(Session["emailMahasiswa"] as string);
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
                                    && x.CheckDosen == true).Count();
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
        public ActionResult GetReportBAPByAbsensiID(int id)
        {
            var result = _reportBAPService.GetBAPBYAbsenID(id);
            return new ContentResult { Content = JsonConvert.SerializeObject(result), ContentType = "application/json" };
        }
      
        public ActionResult BAP(int id)
        {
            
            //string email = Session["emailMahasiswa"] as string;
            //VMListReportBAP bap = _reportBAPService.Find(a => a.ID == id).FirstOrDefault();
            
            //Mahasiswa mahasiswa = GetMahasiswaByEmail(email);
            //Absensi absensi = _absensiService.Find(a => a.ID == id).FirstOrDefault();
            //ViewData["nama"] = mahasiswa.Nama;
            //ViewData["nim"] = mahasiswa.NIM;
            //ViewData["univ"] = mahasiswa.NamaUniversitas;
            //ViewData["dosen"] = absensi.NamaDosen;
            //ViewData["present"] = absensi.Present;
            //var jdwlID = absensi.JadwalKuliahID;
            //JadwalKuliah jdwl = _jdwlService.Find(j => j.ID == jdwlID).FirstOrDefault();
            //ViewData["prodi"] = jdwl.NamaProdi;
            //ViewData["namaMK"] = jdwl.NamaMataKuliah;
            //ViewData["kodeMK"] = jdwl.KodeMataKuliah;
            //ViewData["seksi"] = jdwl.ClassSection;
            //ViewData["tanggal"] = absensi.TanggalAbsen;
           
            //var nullObj = " ";

            //BATAS coba REPORT BAP SP BY ABSENSI ID
            //IEnumerable<VMListReportBAP> tempJadwalUjian = GetReportBAPByAbsensiID(id);
            VMListReportBAP bap = _reportBAPService.GetBAPBYAbsenID(id).FirstOrDefault();
            //ViewData["platform"] = bap.PLATFORM;
           

            //    if (bap.PLATFORM == null)
            //{
            //    //ViewData["platform"] = nullObj;

            //}
            //else
            //{
            //    ViewData["platform"] = bap.PLATFORM;
            //}
            //ViewData["metode"] = bap.BENTUK;
            //List<VMListReportBAP> bap = new List<VMListReportBAP>()
            //{
            //    bap.
            //};
            //bap = _reportBAPService.GetBAPByAbsensiID(id).ToList();
            //List<VMListReportBAP> bap = _reportBAPService.GetBAPByAbsensiID(id).FirstOrDefault();

            //ViewData["platform"] = bap.;

            return View(bap);
        }
        
        [AllowAnonymous]
        public ActionResult PrintDetail(int id)
        {
            //string email = Session["emailMahasiswa"] as string;
            //Mahasiswa mahasiswa = GetMahasiswaByEmail(email);
            //Absensi absensi = _absensiService.Find(a => a.ID == id).FirstOrDefault();
            //VMListReportBAP model = new VMListReportBAP();
            //ViewData["nama"] = mahasiswa.Nama;
            //ViewData["nim"] = mahasiswa.NIM;
            //ViewData["univ"] = mahasiswa.NamaUniversitas;
            //ViewData["dosen"] = absensi.NamaDosen;
            //ViewData["present"] = absensi.Present;
            //var jdwlID = absensi.JadwalKuliahID;
            //JadwalKuliah jdwl = _jdwlService.Find(j => j.ID == jdwlID).FirstOrDefault();
            //ViewData["prodi"] = jdwl.NamaProdi;
            //ViewData["namaMK"] = jdwl.NamaMataKuliah;
            //ViewData["kodeMK"] = jdwl.KodeMataKuliah;
            //ViewData["seksi"] = jdwl.ClassSection;
            //ViewData["tanggal"] = absensi.TanggalAbsen;
            VMListReportBAP bap = _reportBAPService.GetBAPBYAbsenID(id).FirstOrDefault();
            //var nullObj = " ";
            //ViewData["platform"] = bap.PLATFORM;

            //ViewData["platform"] = bap.PLATFORM;
            //ViewData["metode"] = bap.BENTUK;
            //if (bap.PLATFORM == null)
            //{
            //   // ViewData["platform"] = nullObj;

            //}
            //else
            //{
            //    ViewData["platform"] = bap.PLATFORM;
            //}

            //penamaan pdf
            var tgl = bap.TANGGAL.ToString();
            var tgl1 = Convert.ToDateTime(tgl);
            var formattgl = tgl1.ToString("yyyyMMdd");
            var trim = Regex.Replace(formattgl, @"s", "_");
           // var tgl2 = tgl1.ToString("");
           //var namatrim = Regex.Replace(bap.Nama, @"s", "_");
            var report = new Rotativa.ViewAsPdf("BAP",bap)
            { FileName = trim +"_"+ bap.Nama+"_"+bap.KodeMataKuliah + "-BAP.pdf" };
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

        //public ActionResult BAP(int id)
        //{
        //    //var report = new Rotativa.ActionAsPdf("Index");
        //    //dynamic mymodel = new ExpandoObject();
        //    string email = Session["emailMahasiswa"] as string;
        //    //VMListReportBAP bap = _reportBAPService.Find(a => a.ID == id).FirstOrDefault();
        //    VMListReportBAP model = new VMListReportBAP();
        //    Mahasiswa mahasiswa = GetMahasiswaByEmail(email);
        //    Absensi absensi = _absensiService.Find(a => a.ID == id).FirstOrDefault();
        //    ViewData["nama"] = mahasiswa.Nama;
        //    ViewData["nim"] = mahasiswa.NIM;
        //    ViewData["univ"] = mahasiswa.NamaUniversitas;
        //    ViewData["dosen"] = absensi.NamaDosen;
        //    ViewData["present"] = absensi.Present;
        //    var jdwlID = absensi.JadwalKuliahID;
        //    JadwalKuliah jdwl = _jdwlService.Find(j => j.ID == jdwlID).FirstOrDefault();
        //    ViewData["prodi"] = jdwl.NamaProdi;
        //    ViewData["namaMK"] = jdwl.NamaMataKuliah;
        //    ViewData["kodeMK"] = jdwl.KodeMataKuliah;
        //    ViewData["seksi"] = jdwl.ClassSection;
        //    ViewData["tanggal"] = absensi.TanggalAbsen;
        //    return View(id);
        //}

        //[AllowAnonymous]
        //public ActionResult PrintDetail(int id)
        //{
        //    string email = Session["emailMahasiswa"] as string;
        //    Mahasiswa mahasiswa = GetMahasiswaByEmail(email);
        //    Absensi absensi = _absensiService.Find(a => a.ID == id).FirstOrDefault();
        //    ViewData["nama"] = mahasiswa.Nama;
        //    ViewData["nim"] = mahasiswa.NIM;
        //    ViewData["univ"] = mahasiswa.NamaUniversitas;
        //    ViewData["dosen"] = absensi.NamaDosen;
        //    ViewData["present"] = absensi.Present;
        //    var jdwlID = absensi.JadwalKuliahID;
        //    JadwalKuliah jdwl = _jdwlService.Find(j => j.ID == jdwlID).FirstOrDefault();
        //    ViewData["prodi"] = jdwl.NamaProdi;
        //    ViewData["namaMK"] = jdwl.NamaMataKuliah;
        //    ViewData["kodeMK"] = jdwl.KodeMataKuliah;
        //    ViewData["seksi"] = jdwl.ClassSection;
        //    ViewData["tanggal"] = absensi.TanggalAbsen;

        //    var tgl = absensi.TanggalAbsen.ToString("dd/MM/yyyy");

        //    var report = new Rotativa.ViewAsPdf("BAP")
        //    { FileName = tgl + "-" + jdwl.KodeMataKuliah + "-BAP.pdf" };
        //    return report;
        //}

    }
}