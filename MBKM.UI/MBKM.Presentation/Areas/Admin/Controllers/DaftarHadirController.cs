using MBKM.Common.Helpers;
using MBKM.Entities.Models.MBKM;
using MBKM.Entities.ViewModel;
using MBKM.Presentation.Helper;
using MBKM.Services;
using MBKM.Services.MBKMServices;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MBKM.Presentation.Areas.Admin.Controllers
{
    [MBKMAuthorize]
    public class DaftarHadirController : Controller
    {
        // GET: Admin/DaftarHadir
        private ICPLMatakuliahService _cplMatakuliah;
        private ILinkFasilitasService _linkFasilitasService;
        private ILookupService _lookupService;
        private IJadwalKuliahService _jkService;
        private IJadwalUjianMBKMDetailService _juService;
        private IMasterCapaianPembelajaranService _mcpService;
        private IAbsensiService _absensiService;
        private IMahasiswaService _mahasiswaService;
        private IFeedbackMatkulService _feedbackMatkulService;
        private IPendaftaranMataKuliahService _pendaftaranMKService;
        private IJadwalKuliahMahasiswaService _jkMhsService;

        public DaftarHadirController(IFeedbackMatkulService feedbackMatkulService, IPendaftaranMataKuliahService pendaftaranMKService,IMahasiswaService mahasiswaService, IJadwalKuliahMahasiswaService jkMahasiswaService, IAbsensiService absensiService,ILinkFasilitasService linkFasilitasService,ICPLMatakuliahService cplMatakuliah, ILookupService lookupService, IJadwalKuliahService jkService, IJadwalUjianMBKMDetailService juService, IMasterCapaianPembelajaranService mcpService)
        {
            _cplMatakuliah = cplMatakuliah;
            _lookupService = lookupService;
            _jkService = jkService;
            _juService = juService;
            _mcpService = mcpService;
            _linkFasilitasService = linkFasilitasService;
            _absensiService = absensiService;
            _mahasiswaService = mahasiswaService;
            _pendaftaranMKService = pendaftaranMKService;
            _feedbackMatkulService = feedbackMatkulService;
            _jkMhsService = jkMahasiswaService;
        }

        public ActionResult Index()
        {
            //IEnumerable<VMSemester> data = _jadwalUjianMBKMService.getAllSemester();
            //ViewData["semester"] = data;
            var listSection = _linkFasilitasService.getSection();
            ViewData["listSection"] = listSection;

            VMSemester semester = _jkMhsService.getOngoingSemester("S1");

            ViewData["KodeSemester"] = semester.ID;
            ViewData["NamaSemester"] = semester.Nama;

            var RoleLogin = Session["RoleName"].ToString();
            ViewData["role"] = RoleLogin;
            if (RoleLogin == "Admin Fakultas")
            {
                ViewData["KodeFakultas"] = Session["KodeFakultas"].ToString();
                ViewData["NamaFakultas"] = Session["NamaFakultas"].ToString();
            }
            //else if (RoleLogin == "Kepala Program Studi")
            //{
            //    var prodiID = Session["KodeProdi"].ToString();
            //    var tempProdiID = Convert.ToInt64(prodiID);
            //    //var getJenjang = _jkService.Find(x => x.ProdiID == tempProdiID).FirstOrDefault();
            //    //var jenjangs = getJenjang.JenjangStudi;
            //    //ViewData["jenjangs"] = jenjangs;
            //    ViewData["KodeFakultas"] = Session["KodeFakultas"].ToString();
            //    ViewData["NamaFakultas"] = Session["NamaFakultas"].ToString();
            //    ViewData["KodeProdi"] = prodiID;
            //    ViewData["NamaProdi"] = Session["NamaProdi"].ToString();
            //}
            //if (Session["KodeProdi"].ToString() != "")
            //{
            //    ViewData["KodeProdi"] = Session["KodeProdi"].ToString();
            //    ViewData["NamaProdi"] = Session["NamaProdi"].ToString();
            //}

            return View();
        }
        public ActionResult GetInformasiKampusByProdi()
        {
            var kodeProdi = Session["KodeProdi"] as string;
            var result = _pendaftaranMKService.GetInformasiKampusByIdProdi(kodeProdi);
            return new ContentResult { Content = JsonConvert.SerializeObject(result), ContentType = "application/json" };
        }
        public ActionResult DHK(int id)
        {
            var jks = _jkService.Find(jk => jk.ID == id).FirstOrDefault();
            var listRuangan = _jkService.Find(jk => jk.ClassSection == jks.ClassSection && jk.KodeMataKuliah == jks.KodeMataKuliah && jk.JenjangStudi == jks.JenjangStudi && jk.STRM == jks.STRM && jk.FlagOpen == true).ToList();
            var tempRuang = listRuangan.Select(x => x.RuangKelas).ToList();
            List<string> listDistinctRuang = new List<string>();
            foreach (var item in tempRuang)
            {
                if (!listDistinctRuang.Contains(item))
                {
                    listDistinctRuang.Add(item);
                    //mapJadwal.Add(item.ClassSection);
                }

            }

            var listR = listDistinctRuang.Select(x => new VMListRuangan()
            { 
                Nama = x
            }).Distinct();
            var sem = _absensiService.GetSemesterBySTRM(jks.STRM).ToLower().Replace("odd semester", "SEMESTER GANJIL").Replace("even semester", "SEMESTER GENAP").Replace("short semester", "SEMESTER ANTARA");
            var sem2 = _absensiService.GetSemesterBySTRM(jks.STRM);
            List<PendaftaranMataKuliah> pmk = _pendaftaranMKService.Find(x => x.JadwalKuliahID == id && x.StatusPendaftaran.ToLower().Contains("accepted") && (x.mahasiswas.NIM != x.mahasiswas.NIMAsal && x.mahasiswas.NIM != null && x.mahasiswas.NIMAsal != null)).ToList();
            var list = pmk.Select(x => new VMDHK()
            {
                Nama = x.mahasiswas.Nama,
                StudentID = x.mahasiswas.NIM
            });
            
            ViewData["mahasiswas"] = list;
            ViewData["semester"] = sem;
            ViewData["semester2"] = sem2;
            ViewData["prodi"] = jks.NamaProdi;
            ViewData["kodeMK"] = jks.KodeMataKuliah;
            ViewData["namaMK"] = jks.NamaMataKuliah;
            ViewData["sks"] = (int) float.Parse(jks.SKS);
            ViewData["dosen"] = jks.NamaDosen;
            ViewData["seksi"] = jks.ClassSection;
            ViewData["hari"] = getHari(jks.Hari);
            ViewData["jam"] = jks.JamMasuk + " - " + jks.JamSelesai;
            //ViewData["ruang"] = jks.RuangKelas;
            ViewData["ruang"] = listR;
            ViewData["dosens"] = _feedbackMatkulService.GetDosenMakulPertemuans(jks.KodeMataKuliah, jks.ClassSection, jks.STRM.ToString(), jks.FakultasID.ToString());
            ViewData["komponen"] = _absensiService.GetKomponenDHK(id);
            return View();
        }
        public string getHari(string day)
        {
            Dictionary<string, string> days = new Dictionary<string, string>();
            days.Add("monday", "SENIN");
            days.Add("tuesday", "SELASA");
            days.Add("wednesday", "RABU");
            days.Add("thursday", "KAMIS");
            days.Add("friday", "JUMAT");
            days.Add("saturday", "SABTU");
            days.Add("sunday", "MINGGU");
            if (!days.ContainsKey(day.ToLower()))
            {
                return day.ToUpper();
            }
            return days[day.ToLower()];
        }
        [HttpPost]
        public ActionResult SearchList(int strm, string jenjangStudi, string fakultas, string prodi, string lokasi, string matkul, string seksi)
        {
            
            var result = new List<JadwalKuliah>();
            var gege = new List<JadwalKuliah>();
            List<long> listJadwalKuliah = new List<long>();
            if (seksi == null || seksi.Length == 0)
            {
                var asd = _jkService.Find(_ => _.STRM == strm && _.JenjangStudi == jenjangStudi && _.NamaFakultas == fakultas && _.NamaProdi == prodi && _.Lokasi == lokasi && _.KodeMataKuliah + " - " + _.NamaMataKuliah == matkul && _.FlagOpen == true).ToList();
                //var countMahasiswa = _pendaftaranMKService.Find(_=>_.ID == asd.jad)
                var abc = asd.Select(x => x.ID).ToList();
                
                int countMahasiswaAcceptedExtern = 0;
                foreach (var d in abc)
                {
                    var tempJadwal = _pendaftaranMKService.Find(_=> _.JadwalKuliahID == d);
                    var countMahasiswaID = tempJadwal.Select(x => x.MahasiswaID).ToList();
                    foreach (var m in countMahasiswaID)
                    {
                        var countMahasiswa = _pendaftaranMKService.Find(x => x.MahasiswaID == m && x.JadwalKuliahID == d && x.StatusPendaftaran.ToLower().Contains("accepted") && (x.mahasiswas.NIM != x.mahasiswas.NIMAsal && x.mahasiswas.NIM != null && x.mahasiswas.NIMAsal != null)).ToList();
                        
                        countMahasiswaAcceptedExtern = countMahasiswa.Count();
                        if (countMahasiswaAcceptedExtern >= 1)
                        {

                            listJadwalKuliah.Add(d);
                        }
                    }
                    
                    
                    countMahasiswaAcceptedExtern=0;

                    //Console.WriteLine()
                }
                //result = _jkService.Find(_ => _.STRM == strm && _.JenjangStudi == jenjangStudi && _.NamaFakultas == fakultas && _.NamaProdi == prodi && _.Lokasi == lokasi && _.KodeMataKuliah + " - " + _.NamaMataKuliah == matkul && _.FlagOpen == true).ToList();
                result = _jkService.Find(_ => _.STRM == strm && _.JenjangStudi == jenjangStudi && _.NamaFakultas == fakultas && _.NamaProdi == prodi && _.Lokasi == lokasi && _.KodeMataKuliah + " - " + _.NamaMataKuliah == matkul && _.FlagOpen == true).ToList();
                 gege = result.Where(x => listJadwalKuliah.Contains(x.ID)).ToList();
            } else
            {
                var asd = _jkService.Find(_ => _.STRM == strm && _.JenjangStudi == jenjangStudi && _.NamaFakultas == fakultas && _.NamaProdi == prodi && _.Lokasi == lokasi && _.KodeMataKuliah + " - " + _.NamaMataKuliah == matkul && _.FlagOpen == true && _.ClassSection == seksi).ToList();
                //var countMahasiswa = _pendaftaranMKService.Find(_=>_.ID == asd.jad)
                var abc = asd.Select(x => x.ID).ToList();
                
                int countMahasiswaAcceptedExtern = 0;
                foreach (var d in abc)
                {
                    var tempJadwal = _pendaftaranMKService.Find(_ => _.JadwalKuliahID == d);
                    var countMahasiswaID = tempJadwal.Select(x => x.MahasiswaID).ToList();
                    foreach (var m in countMahasiswaID)
                    {
                        var countMahasiswa = _pendaftaranMKService.Find(x => x.MahasiswaID == m && x.JadwalKuliahID == d && x.StatusPendaftaran.ToLower().Contains("accepted") && (x.mahasiswas.NIM != x.mahasiswas.NIMAsal && x.mahasiswas.NIM != null && x.mahasiswas.NIMAsal != null)).ToList();
                        
                        countMahasiswaAcceptedExtern = countMahasiswa.Count();
                        if (countMahasiswaAcceptedExtern >= 1)
                        {

                            listJadwalKuliah.Add(d);
                        }
                    }

                    
                    countMahasiswaAcceptedExtern = 0;

                    //Console.WriteLine()
                }
                //result = _jkService.Find(_ => _.STRM == strm && _.JenjangStudi == jenjangStudi && _.NamaFakultas == fakultas && _.NamaProdi == prodi && _.Lokasi == lokasi && _.KodeMataKuliah + " - " + _.NamaMataKuliah == matkul && _.FlagOpen == true).ToList();
                result = _jkService.Find(_ => _.STRM == strm && _.JenjangStudi == jenjangStudi && _.NamaFakultas == fakultas && _.NamaProdi == prodi && _.Lokasi == lokasi && _.KodeMataKuliah + " - " + _.NamaMataKuliah == matkul && _.FlagOpen == true && _.ClassSection == seksi).ToList();
                gege = result.Where(x => listJadwalKuliah.Contains(x.ID)).ToList();
                // result = _jkService.Find(_ => _.STRM == strm && _.JenjangStudi == jenjangStudi && _.NamaFakultas == fakultas && _.NamaProdi == prodi && _.Lokasi == lokasi && _.KodeMataKuliah + " - " + _.NamaMataKuliah == matkul && _.ClassSection == seksi && _.FlagOpen == true).ToList();

            }
            return new ContentResult { Content = JsonConvert.SerializeObject(gege), ContentType = "application/json" };
        }
        public ActionResult GetMatkulByLokasi(string search, int strm, string jenjangStudi, string prodi, string lokasi)
        {
            var cek = _jkService.Find(_ => _.STRM == strm && _.JenjangStudi == jenjangStudi && _.NamaProdi == prodi && _.Lokasi == lokasi).GroupBy(x => x.KodeMataKuliah)
                .Select(x => x.First()).ToList();
            var result = new List<VMLookup>();
            foreach (var item in cek)
            {
                var tmp = new VMLookup();
                tmp.Nama = item.KodeMataKuliah + " - " + item.NamaMataKuliah;
                result.Add(tmp);
            }
            return new ContentResult { Content = JsonConvert.SerializeObject(result), ContentType = "application/json" };
        }
        /* Lookup --<> */
        public ActionResult getLookupByTipe(string tipe)
        {
            return Json(_lookupService.getLookupByTipe(tipe), JsonRequestBehavior.AllowGet);
        }
        /* Attribute Kuliah --<> */
        public ActionResult GetFakultas(string search, string JenjangStudi)
        {

            return Json(_mcpService.GetFakultas(JenjangStudi, search), JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetProdiLocByFakultas(string JenjangStudi, string idFakultas, string search)
        {
            return Json(_cplMatakuliah.GetProdiLocByFakultas(JenjangStudi, idFakultas, search), JsonRequestBehavior.AllowGet);
        }
        //public ActionResult GetProdiByFakultas(string JenjangStudi, string idFakultas, string search)
        //{
        //    return Json(_mcpService.GetProdiByFakultas(JenjangStudi, idFakultas, search), JsonRequestBehavior.AllowGet);
        //}
        //public ActionResult GetLokasiByProdi(string JenjangStudi, string namaProdi, string search)
        //{
        //    return Json(_mcpService.GetLokasiByProdi(JenjangStudi, namaProdi, search), JsonRequestBehavior.AllowGet);
        //}

        public ActionResult GetMataKuliahByProdi(string namaProdi, string lokasi)
        {
            //return new ContentResult { Content = JsonConvert.SerializeObject(_jkService.Find(jk => jk.NamaProdi == namaProdi && jk.Lokasi == lokasi && jk.NamaMataKuliah == search).ToList()), ContentType = "application/json" };
            List<JadwalKuliah> jks = new List<JadwalKuliah>();
            List<string> jadwalKuliahs = new List<string>();
            foreach (var item in _jkService.Find(jk => jk.NamaProdi == namaProdi && jk.Lokasi == lokasi).ToList())
            {
                if (!jadwalKuliahs.Contains(item.NamaMataKuliah))
                {
                    jks.Add(item);
                    jadwalKuliahs.Add(item.NamaMataKuliah);
                }
            }
            return new ContentResult { Content = JsonConvert.SerializeObject(jks), ContentType = "application/json" };

        }

        public ActionResult GetMataKuliahByID(string MataKuliahID)
        {
            return new ContentResult { Content = JsonConvert.SerializeObject(_jkService.Find(jk => jk.MataKuliahID == MataKuliahID).ToList()), ContentType = "application/json" };
        }


        [HttpPost]
        public ActionResult GetMataKuliah(int skip, int take, string searchBy, string idProdi, string idFakultas)
        {

            /*            string searchBy = "BAHASA INDONESIA";
                        int skip = 1;
                        int take = 10;


                        string idProdi = "0101";
                        string idFakultas = "0001";*/


            /*            int pageNumber = skip;
                        int pageSize = length;
                        string search = "";*/
            //string email = Session["email"] as string;
            //var result = GetMa = takuliah(email);

            /*"PageNumber":10,
            "PageSize":10,
            "Search":"",
            "ProdiID":"0001",
            "FakultasID":"0101"*/
            //return Json(_cplMatakuliah.GetMatkul(skip, take, searchBy, idProdi, idFakultas), JsonRequestBehavior.AllowGet);
            var final = _cplMatakuliah.GetMatkul(skip, take, searchBy, idProdi, idFakultas);
            List<object> data = new List<object>();
            foreach (var p in final)
            {
                var q = new
                {
                    id = p.CRSE_ID,
                    text = p.DESCR,
                    kode = p.Expr1
                };
                data.Add(q);
            }

            return Json(data);
        }




        [HttpPost]
        public ActionResult GetSemesterAll(int skip, int take, string search)
        {

            var final = _jkService.GetSemesterAll(skip, take, search);
            List<object> data = new List<object>();
            foreach (var p in final)
            {
                var q = new
                {
                    Nama = p.Nama,
                    ID = p.ID
                };
                data.Add(q);
            }

            return Json(data);
        }

        [HttpPost]
        public ActionResult GetSection()
        {

            var final = _juService.getSection();
            List<object> data = new List<object>();
            foreach (var p in final)
            {
                var q = new
                {
                    Nama = p.Nama,
                    //ID = p.ID
                };
                data.Add(q);
            }

            return Json(data);
        }
        //consume controller sendiri
        public ActionResult GetSemesterAll2()
        {
            var result = _jkService.GetSemesterAll2();
            return new ContentResult { Content = JsonConvert.SerializeObject(result), ContentType = "application/json" };
        }
        public ActionResult GetProdiByFakultas(string search, string jenjangStudi, string fakultas)
        {
            return new ContentResult { Content = JsonConvert.SerializeObject(_absensiService.GetProdiByFakultas(search, jenjangStudi, fakultas)), ContentType = "application/json" };
        }
        
        [HttpPost]
        public ActionResult GetMataKuliahFlag(string searchBy, string idProdi, string lokasi, string idFakultas, string jenjangStudi, string strm)
        {
            List<JadwalKuliah> MVJadwal = new List<JadwalKuliah>();
            List<string> mapJadwal = new List<string>();

            int idProdiInt = Int32.Parse(idProdi);
            int idFakultasInt = Int32.Parse(idFakultas);
            int strmInt = Int32.Parse(strm);

            if (String.IsNullOrEmpty(searchBy))
            {
                foreach (var item in _jkService.Find(dataMap =>
                    dataMap.ProdiID == idProdiInt &&
                    dataMap.FakultasID == idFakultasInt &&
                    dataMap.Lokasi == lokasi &&
                    dataMap.JenjangStudi == jenjangStudi &&
                    dataMap.STRM == strmInt &&
                    dataMap.FlagOpen == true

                ).ToList())
                {
                    if (!mapJadwal.Contains(item.NamaMataKuliah) && !mapJadwal.Contains(item.KodeMataKuliah))
                    {
                        MVJadwal.Add(item);
                        mapJadwal.Add(item.NamaMataKuliah);
                    }
                }
            }
            else
            {
                foreach (var item in _jkService.Find(dataMap =>
                    dataMap.ProdiID == idProdiInt &&
                    dataMap.FakultasID == idFakultasInt &&
                    dataMap.Lokasi == lokasi &&
                    dataMap.JenjangStudi == jenjangStudi &&
                    dataMap.STRM == strmInt &&
                    dataMap.FlagOpen == true &&
                    dataMap.NamaMataKuliah.Contains(searchBy) &&
                    dataMap.NamaMataKuliah.Contains(searchBy) ||
                    dataMap.KodeMataKuliah.Contains(searchBy)

                ).ToList())
                {
                    if (!mapJadwal.Contains(item.NamaMataKuliah) && !mapJadwal.Contains(item.KodeMataKuliah))
                    {
                        MVJadwal.Add(item);
                        mapJadwal.Add(item.NamaMataKuliah);
                    }
                }
            }




            return new ContentResult { Content = JsonConvert.SerializeObject(MVJadwal), ContentType = "application/json" };
        }

    
    //public ActionResult GetMataKuliahFlag(int skip, int take, string searchBy, string idProdi, string lokasi, string idFakultas, string jenjangStudi, string strm)
    //{


    //    /*List<JadwalKuliah> MVJadwal = new List<JadwalKuliah>();
    //    List<string> mapJadwal = new List<string>();*/

    //    /*int idProdiInt = Int32.Parse(idProdi);
    //    int idFakultasInt = Int32.Parse(idFakultas);
    //    int strmInt = Int32.Parse(strm);*/

    //    List<JadwalKuliah> final = _jkService.GetMatkulFlag(skip, take, searchBy, idProdi, lokasi, idFakultas, jenjangStudi, strm).ToList();

    //    /*foreach (var item in final)
    //    {
    //        if (!mapJadwal.Contains(item.NamaMataKuliah))
    //        {
    //            MVJadwal.Add(item);
    //        }
    //    }*/
    //    return new ContentResult { Content = JsonConvert.SerializeObject(final), ContentType = "application/json" };
    //}
    public ActionResult GetLokasiByProdi(string JenjangStudi, string namaProdi, string search)
        {
            return Json(_mcpService.GetLokasiByProdi(JenjangStudi, namaProdi, search), JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetSeksiByMatkul(string search, string jenjangStudi, string matkul, string lokasi)
        {
            return new ContentResult { Content = JsonConvert.SerializeObject(_absensiService.GetSeksiByMatkul(search, jenjangStudi, matkul, lokasi)), ContentType = "application/json" };
        }
    }
}