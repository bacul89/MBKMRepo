using MBKM.Common.Helpers;
using MBKM.Common.Interfaces.RepoInterfaces.MBKMRepoInterfaces;
using MBKM.Entities.Models.MBKM;
using MBKM.Entities.ViewModel;
using MBKM.Presentation.models;
using MBKM.Services;
using MBKM.Services.MBKMServices;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using MBKM.Presentation.Helper;
using Rotativa;
using Rotativa.Options;

namespace MBKM.Presentation.Areas.Admin.Controllers
{
    [MBKMAuthorize]
    public class DaftarHadirUjianController : Controller
    {

        private ICPLMatakuliahService _cplMatakuliah;
        private ILookupService _lookupService;
        private IJadwalKuliahService _jkService;
        private IJadwalUjianMBKMDetailService _juDetailService;
        private IJadwalUjianMBKMService _juService;
        private IMasterCapaianPembelajaranService _mcpService;
        private IFeedbackMatkulService _feedbackMatkulService;
        private IJadwalKuliahMahasiswaService _jkMhsService;
        private IAbsensiService _absensiService;


        public DaftarHadirUjianController(ICPLMatakuliahService cplMatakuliah, ILookupService lookupService, IJadwalKuliahService jkService, IMasterCapaianPembelajaranService mcpService, IJadwalUjianMBKMDetailService juDetailService, IJadwalUjianMBKMService juService, IFeedbackMatkulService feedbackMatkulService, IJadwalKuliahMahasiswaService jkMahasiswaService, IAbsensiService absensiService)
        {
            _cplMatakuliah = cplMatakuliah;
            _lookupService = lookupService;
            _jkService = jkService;
            _juDetailService = juDetailService;
            _juService = juService;
            _mcpService = mcpService;
            _feedbackMatkulService = feedbackMatkulService;
            _jkMhsService = jkMahasiswaService;
            _absensiService = absensiService;
        }



        // GET: Admin/DaftarHadirUjian
        public ActionResult Index()
        {
            //Session["username"] = "Smitty Werben Jeger Man Jensen";
            VMSemester model = _jkMhsService.getOngoingSemester("S1");
            return View(model);
        }


        public JsonResult SearchList(DataTableAjaxPostModel model, string idProdi, string lokasi, string idFakultas, string jenjangStudi, string strm, string idMatakuliah, string seksi)
        {
            VMListJadwalUjian vmList = _juDetailService.GetDHU(model, idProdi, lokasi, idFakultas, jenjangStudi, strm, idMatakuliah, seksi);
            return Json(new
            {                
                draw = model.draw,
                recordsTotal = vmList.TotalCount,
                recordsFiltered = vmList.TotalFilterCount,
                data = vmList.gridDatas
            });
        }

        public ActionResult PrintDHU(int ID)
        {

            VMDHU vmDHU = new VMDHU();            
            var jadwalUjian = _juService.Get(ID);
            List<JadwalUjianMBKMDetail> mahasiswa = _juDetailService.Find(x => x.JadwalUjianMBKMID == ID).ToList();
            var dataSemester = _feedbackMatkulService.GetSemesterByStrm(jadwalUjian.STRM);
            var ujian = _juDetailService.GetAttrubuteDHU(jadwalUjian.ProdiID, jadwalUjian.Lokasi, jadwalUjian.FakultasID, jadwalUjian.JenjangStudi, jadwalUjian.STRM, jadwalUjian.IDMatkul, jadwalUjian.ClassSection).Where(x => x.ID == ID).First();

            var list = mahasiswa.Select(x => new VMDHU()
            {
                Nama = x.Mahasiswas.Nama,
                StudentID = x.Mahasiswas.NIM
            });

            ViewData["ujian"] = JsonConvert.SerializeObject(ujian);
            ViewData["semester"] = dataSemester.Nama;
            ViewData["mahasiswas"] = JsonConvert.SerializeObject(list);
            return View("PrintDHU");
        }

        [HttpPost]
        public ActionResult GetDHU(int ID)
        {

            VMDHU vmDHU = new VMDHU();
            var jadwalUjian = _juService.Get(ID);
            List<JadwalUjianMBKMDetail> mahasiswa = _juDetailService.Find(x => x.JadwalUjianMBKMID == ID).ToList();
            var dataSemester = _feedbackMatkulService.GetSemesterByStrm(jadwalUjian.STRM);
            var ujian = _juDetailService.GetAttrubuteDHU(jadwalUjian.ProdiID, jadwalUjian.Lokasi, jadwalUjian.FakultasID, jadwalUjian.JenjangStudi, jadwalUjian.STRM, jadwalUjian.IDMatkul, jadwalUjian.ClassSection).Where(x => x.ID == ID).First();

            int strmInt = Int32.Parse(jadwalUjian.STRM);
            long fkaultasInt = Int64.Parse(jadwalUjian.FakultasID);
            long prodiInt = Int64.Parse(jadwalUjian.ProdiID);
            var jadwalKuliah = _jkService.Find(x =>
                                x.ClassSection == jadwalUjian.ClassSection &&
                                x.KodeMataKuliah == jadwalUjian.KodeMatkul &&
                                x.STRM == strmInt &&
                                x.FakultasID == fkaultasInt &&
                                x.ProdiID == prodiInt &&
                                x.Lokasi == jadwalUjian.Lokasi
                         ).First();





            var presensi = _absensiService.Find(x =>
                                x.JadwalKuliahID == jadwalKuliah.ID &&
                                x.JadwalKuliahs.STRM == strmInt
                         ).GroupBy(z => new { z.MahasiswaID, z.Present, z.LockedAbsen, z.CheckDosen })
               .Select(s => new { MahasiswaID = s.Key.MahasiswaID, Present = s.Key.Present, LockedAbsen = s.Key.LockedAbsen, CheckDosen = s.Key.CheckDosen }).ToList();


            int defaultZero = 0;
            var list = mahasiswa.Select(x => new VMDHU()
            {
                Nama = x.Mahasiswas.Nama,
                StudentID = x.Mahasiswas.NIM,
                MahasiswaID = x.Mahasiswas.ID,
                Present = defaultZero,
                Checked = defaultZero,
                Lock = defaultZero

            });;


            
            ViewData["ujian"] = ujian;
            ViewData["semester"] = dataSemester.Nama;
            ViewData["mahasiswas"] = list;
            ViewData["presensi"] = presensi;
            return Json(ViewData, JsonRequestBehavior.AllowGet);
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

        public ActionResult GetProdiByFakultas(string JenjangStudi, string idFakultas, string search)
        {
            return Json(_mcpService.GetProdiByFakultas(JenjangStudi, idFakultas, search), JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetLokasiByProdi(string JenjangStudi, string namaProdi, string search)
        {
            return Json(_mcpService.GetLokasiByProdi(JenjangStudi, namaProdi, search), JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetMataKuliahByProdi(string namaProdi, string lokasi)
        {
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

            var final = _juDetailService.getSection();
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

    }


    //[HttpGet]
    /*public ActionResult Test(string idProdi, string lokasi, string idFakultas, string jenjangStudi, string strm, string idMatakuliah, string seksi)
    {
        List<VMJadwalUjian> vmList = _juDetailService.GetAttrubuteDHU(idProdi, lokasi, idFakultas, jenjangStudi, strm, idMatakuliah, seksi);
        return Json(vmList, JsonRequestBehavior.AllowGet);
    }*/


    /*public ActionResult ViewDHU(int ID)
    {

        //int ID = 5187;
        VMDHU vmDHU = new VMDHU();

        var jadwalUjian = _juService.Get(ID);
        List<JadwalUjianMBKMDetail> mahasiswa = _juDetailService.Find(x => x.JadwalUjianMBKMID == ID).ToList();
        //var dosen = _juDetailService.GetDosen(jadwalUjian.ClassSection, jadwalUjian.KodeMatkul, jadwalUjian.STRM, jadwalUjian.FakultasID);
        var dataSemester = _feedbackMatkulService.GetSemesterByStrm(jadwalUjian.STRM);


        int strmInt = Int32.Parse(jadwalUjian.STRM);
        long fkaultasInt = Int64.Parse(jadwalUjian.FakultasID);
        long prodiInt = Int64.Parse(jadwalUjian.ProdiID);

        var jadwal = _jkService.Find(x =>
                            x.ClassSection == jadwalUjian.ClassSection &&
                            x.KodeMataKuliah == jadwalUjian.KodeMatkul &&
                            x.STRM == strmInt &&
                            x.FakultasID == fkaultasInt &&
                            x.ProdiID == prodiInt &&
                            x.Lokasi == jadwalUjian.Lokasi
                     ).First();

        var list = mahasiswa.Select(x => new VMDHU()
        {
            Nama = x.Mahasiswas.Nama,
            StudentID = x.Mahasiswas.NIM
        });


        ViewData["ujian"] = JsonConvert.SerializeObject(jadwalUjian);
        ViewData["semester"] = dataSemester.Nama;
        ViewData["mahasiswas"] = list;
        ViewData["mahasiswaLenght"] = list.Count();
        //ViewData["dosen"] = JsonConvert.SerializeObject(dosen);
        ViewData["jadwal"] = JsonConvert.SerializeObject(jadwal);
        return new ViewAsPdf("PrintDHU")
        {
            FileName = 
                dataSemester.Nama+"_"+
                jadwalUjian.JenjangStudi + "_" +
                jadwalUjian.NamaFakultas + "_" +
                jadwalUjian.NamaProdi + "_" +
                jadwalUjian.NamaProdi + "_" +
                jadwalUjian.KodeMatkul + "-" + jadwalUjian.NamaMatkul + "_DHU.pdf",
            PageSize = Size.A4,
            PageOrientation = Orientation.Portrait,
            //CustomSwitches = footer,
            PageMargins = new Margins(3, 3, 20, 3)

        };
    }*/

}