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
    public class DaftarHadirUjianController : Controller
    {

        private ICPLMatakuliahService _cplMatakuliah;
        private ILookupService _lookupService;
        private IJadwalKuliahService _jkService;
        private IJadwalUjianMBKMDetailService _juDetailService;
        private IJadwalUjianMBKMService _juService;
        private IMasterCapaianPembelajaranService _mcpService;
        private IFeedbackMatkulService _feedbackMatkulService;


        public DaftarHadirUjianController(ICPLMatakuliahService cplMatakuliah, ILookupService lookupService, IJadwalKuliahService jkService, IMasterCapaianPembelajaranService mcpService, IJadwalUjianMBKMDetailService juDetailService, IJadwalUjianMBKMService juService, IFeedbackMatkulService feedbackMatkulService)
        {
            _cplMatakuliah = cplMatakuliah;
            _lookupService = lookupService;
            _jkService = jkService;
            _juDetailService = juDetailService;
            _juService = juService;
            _mcpService = mcpService;
            _feedbackMatkulService = feedbackMatkulService;
        }



        // GET: Admin/DaftarHadirUjian
        public ActionResult Index()
        {
            Session["username"] = "Smitty Werben Jeger Man Jensen";
            return View();
        }


        public JsonResult SearchList(DataTableAjaxPostModel model, string idProdi, string lokasi, string idFakultas, string jenjangStudi, string strm, string idMatakuliah, string seksi)
        {
            VMListJadwalUjian vmList = _juDetailService.SearchListJadwalUjian(model, idProdi, lokasi, idFakultas, jenjangStudi, strm, idMatakuliah, seksi);
            return Json(new
            {
                // this is what datatables wants sending back
                draw = model.draw,
                recordsTotal = vmList.TotalCount,
                recordsFiltered = vmList.TotalFilterCount,
                data = vmList.gridDatas
            });
        }

        public ActionResult PrintDHU(int ID)
        {

            //int ID = 5187;
            VMDHU vmDHU = new VMDHU();
            
            var jadwalUjian = _juService.Get(ID);
            List<JadwalUjianMBKMDetail> mahasiswa = _juDetailService.Find(x => x.JadwalUjianMBKMID == ID).ToList();
            var dosen = _juDetailService.GetDosen(jadwalUjian.ClassSection, jadwalUjian.KodeMatkul, jadwalUjian.STRM, jadwalUjian.FakultasID);
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

            //List <VMMahasiswas> mahasiswas = new List<VMMahasiswas>();
            /*foreach (var p in mahasiswa)
            {
                //p.JadwalUjianMBKMs.;

                var q = new
                {
                    Nama = p.Mahasiswas.Nama,
                    NIM = p.Mahasiswas.NIM,
                    NamaUniversitas = p.Mahasiswas.NamaUniversitas,
                    NoKerjasama = p.Mahasiswas.NoKerjasama

                    //ID = p.ID
                };
                mahasiswas.Add(q);
            }*/

            ViewData["ujian"] = JsonConvert.SerializeObject(jadwalUjian);
            ViewData["semester"] = dataSemester.Nama;
            ViewData["mahasiswas"] = list;
            ViewData["mahasiswaLenght"] = list.Count();
            ViewData["dosen"] = JsonConvert.SerializeObject(dosen);
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
        }

        public ActionResult viewDHU(int ID)
        {
            //int ID = 5187;
            VMDHU vmDHU = new VMDHU();
            
            var jadwalUjian = _juService.Get(ID);
            List<JadwalUjianMBKMDetail> mahasiswa = _juDetailService.Find(x => x.JadwalUjianMBKMID == ID).ToList();
            var dosen = _juDetailService.GetDosen(jadwalUjian.ClassSection, jadwalUjian.KodeMatkul, jadwalUjian.STRM, jadwalUjian.FakultasID);
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

            //List <VMMahasiswas> mahasiswas = new List<VMMahasiswas>();
            /*foreach (var p in mahasiswa)
            {
                //p.JadwalUjianMBKMs.;

                var q = new
                {
                    Nama = p.Mahasiswas.Nama,
                    NIM = p.Mahasiswas.NIM,
                    NamaUniversitas = p.Mahasiswas.NamaUniversitas,
                    NoKerjasama = p.Mahasiswas.NoKerjasama

                    //ID = p.ID
                };
                mahasiswas.Add(q);
            }*/

            ViewData["ujian"] = JsonConvert.SerializeObject(jadwalUjian);
            ViewData["semester"] = dataSemester.Nama;
            ViewData["mahasiswaLenght"] = list.Count();
            ViewData["mahasiswas"] = list;
            ViewData["dosen"] = JsonConvert.SerializeObject(dosen);
            ViewData["jadwal"] = JsonConvert.SerializeObject(jadwal);
            return View("PrintDHU");
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
}