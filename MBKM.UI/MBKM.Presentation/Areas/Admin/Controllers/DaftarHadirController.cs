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

        public DaftarHadirController(IMahasiswaService mahasiswaService ,IAbsensiService absensiService,ILinkFasilitasService linkFasilitasService,ICPLMatakuliahService cplMatakuliah, ILookupService lookupService, IJadwalKuliahService jkService, IJadwalUjianMBKMDetailService juService, IMasterCapaianPembelajaranService mcpService)
        {
            _cplMatakuliah = cplMatakuliah;
            _lookupService = lookupService;
            _jkService = jkService;
            _juService = juService;
            _mcpService = mcpService;
            _linkFasilitasService = linkFasilitasService;
            _absensiService = absensiService;
            _mahasiswaService = mahasiswaService;
        }

        public ActionResult Index()
        {
            //IEnumerable<VMSemester> data = _jadwalUjianMBKMService.getAllSemester();
            //ViewData["semester"] = data;
            var listSection = _linkFasilitasService.getSection();
            ViewData["listSection"] = listSection;
            return View(_absensiService.GetTahunSemester());
        }
        public ActionResult DHK(int id)
        {
            var jks = _jkService.Find(jk => jk.ID == id).FirstOrDefault();
            var sem = _absensiService.GetSemesterBySTRM(jks.STRM);
            List<Absensi> abs = _absensiService.Find(x => x.JadwalKuliahID == id).ToList();
            //List<Mahasiswa> mhs = _mahasiswaService.Find(x => x.ID == abs.).ToList();
            var list = abs.Select(x => new VMDHK()
            {
                Nama = x.Mahasiswas.Nama,
                StudentID = x.Mahasiswas.NIM
            }) ;
            ViewData["mahasiswas"] = list;
            //var abs = _absensiService.GetSemesterBySTRM(jks.STRM);
            ViewData["semester"] = sem;
            ViewData["prodi"] = jks.NamaProdi;
            ViewData["kodeMK"] = jks.KodeMataKuliah;
            ViewData["namaMK"] = jks.NamaMataKuliah;
            ViewData["sks"] = jks.SKS;
            ViewData["dosen"] = jks.NamaDosen;
            //kanan
            ViewData["seksi"] = jks.ClassSection;
            //ViewData["komponen"] = jks.;
            ViewData["hari"] = jks.Hari;
            ViewData["jam"] = ""+jks.JamMasuk+"-"+jks.JamSelesai;
            ViewData["ruang"] = jks.RuangKelas;
            return View();
        }
        


        public JsonResult SearchList(DataTableAjaxPostModel model, string idProdi, string lokasi, string idFakultas, string jenjangStudi, string strm)
        {
            VMListJadwalUjian vmList = _juService.SearchListJadwalUjian(model, idProdi, lokasi, idFakultas, jenjangStudi, strm);
            return Json(new
            {
                // this is what datatables wants sending back
                draw = model.draw,
                recordsTotal = vmList.TotalCount,
                recordsFiltered = vmList.TotalFilterCount,
                data = vmList.gridDatas
            });
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
    }
}