using MBKM.Common.Helpers;
using MBKM.Entities.Models;
using MBKM.Entities.Models.MBKM;
using MBKM.Entities.ViewModel;
using MBKM.Presentation.models;
using MBKM.Services;
using MBKM.Services.MBKMServices;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using MBKM.Presentation.Helper;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MBKM.Presentation.Areas.Admin.Controllers
{
    [MBKMAuthorize]
    public class ManageJadwalUjianController : Controller
    {

        private ILookupService _lookupService;
        private IMasterCapaianPembelajaranService _masterCapaianPembelajaranService;
        private IMahasiswaService _mahasiswaService;
        private IJadwalUjianMBKMService _jadwalUjianMBKMService;
        private IPendaftaranMataKuliahService _pendaftaranMataKuliahService;
        private IJadwalUjianMBKMDetailService _jadwalUjianMBKMDetailService;
        private IJadwalKuliahService _jadwalKuliahService;

        public ManageJadwalUjianController(ILookupService lookupService, IMasterCapaianPembelajaranService masterCapaianPembelajaranService, IMahasiswaService mahasiswaService, IJadwalUjianMBKMService jadwalUjianMBKMService, IPendaftaranMataKuliahService pendaftaranMataKuliahService, IJadwalUjianMBKMDetailService jadwalUjianMBKMDetailService, IJadwalKuliahService jadwalKuliahService)
        {
            _lookupService = lookupService;
            _masterCapaianPembelajaranService = masterCapaianPembelajaranService;
            _mahasiswaService = mahasiswaService;
            _jadwalUjianMBKMService = jadwalUjianMBKMService;
            _pendaftaranMataKuliahService = pendaftaranMataKuliahService;
            _jadwalUjianMBKMDetailService = jadwalUjianMBKMDetailService;
            _jadwalKuliahService = jadwalKuliahService;
        }





        // GET: Admin/ManageJadwalUjian
        public ActionResult Index()
        {
            IEnumerable<VMLookup> tempJenjang =   _lookupService.getLookupByTipe("JenjangStudi");
            IEnumerable<VMLookup> tempJenisUjian =   _lookupService.getLookupByTipe("JenisUjian");
            IEnumerable<VMSemester> data = _jadwalUjianMBKMService.getAllSemester();
            ViewData["semester"] = data;
            ViewData["Jenjang"] = tempJenjang;
            ViewData["JenisUjian"] = tempJenisUjian;
            var d = HttpContext.Session["RoleName"].ToString().ToLower();
            ViewData["role"] = HttpContext.Session["RoleName"].ToString().ToLower();
            var f = _jadwalUjianMBKMService.GetInformasiData(HttpContext.Session["KodeProdi"].ToString().ToLower());
            ViewData["jenjangStudiDefault"] = (f != null ? f.JenjangStudi : "");
            
            return View();
        }


        public ActionResult DetailManageUjian(int id)
        {
            var data = _jadwalUjianMBKMService.Get(id);
            var checkLagi = _jadwalUjianMBKMDetailService.Find(x => x.JadwalUjianMBKMID == id).Count();
            ViewData["countAvailable"] = data.Tersedia - checkLagi;
            IEnumerable<PendaftaranMataKuliah> tempJadwalUjian = _pendaftaranMataKuliahService.Find(x => x.JadwalKuliahs.MataKuliahID == data.IDMatkul).ToList();
            ViewData["dataMahasiswa"] = tempJadwalUjian;
            return View(data);
        }

        [HttpPost]
        public ActionResult GetDataTableMahasiswa(int dataID)
        {
            var data = _jadwalUjianMBKMService.Get(dataID);
            /*var tempProdi = int.Parse(data.ProdiID);*/
            var tempJadwalUjian = _pendaftaranMataKuliahService.Find(x => x.JadwalKuliahs.MataKuliahID == data.IDMatkul && x.JadwalKuliahs.ClassSection == data.ClassSection
            /*&& x.JadwalKuliahs.ProdiID == tempProdi*/
            && x.mahasiswas.NIM != x.mahasiswas.NIMAsal
            && x.StatusPendaftaran.Contains("ACCEPTED")
            
            ).ToList();
            
            List<String[]> final = new List<String[]>();

            foreach(var dt in tempJadwalUjian)
            {
                var check = _jadwalUjianMBKMDetailService.Find(
                    s => s.MahasiswaID == dt.MahasiswaID 
                    && s.JadwalUjianMBKMs.IDMatkul == data.IDMatkul
                    && s.JadwalUjianMBKMs.STRM == data.STRM
                    && s.JadwalUjianMBKMs.TipeUjian == data.TipeUjian
                ).Count();

                if(check == 0)
                {
                    var ddd = dt.MahasiswaID.ToString();

                    final.Add(new String[]
                    {
                        ddd,
                        dt.mahasiswas.NIM,
                        dt.mahasiswas.Nama,
                    });
                }
                
            }
            return Json(final);
        }

        [HttpPost]
        public JsonResult CheckData(int idData, string dataMahasiswa)
        {
            var checkLagi = _jadwalUjianMBKMDetailService.Find(x => x.JadwalUjianMBKMID == idData).Count();
            var available = _jadwalUjianMBKMService.Get(idData).Tersedia;
            string[] tempMahasiswa = dataMahasiswa.Split(',');
            string[] data1 = tempMahasiswa.Distinct().ToArray();


            if(checkLagi >= available)
            {
                return Json(new ServiceResponse { status = 300, message = "Ruangan Ini sudah Penuh" });
            }

            if (data1.Count() > (available -checkLagi))
            {
                var d = data1.Count() - (available - checkLagi);
                return Json(new ServiceResponse { status = 300, message = "kurangi mahasiswa sejumlah " + d });

            }else if (data1.Count() < (available - checkLagi))
            {
                var d = (available - checkLagi) - data1.Count();
                return Json(new ServiceResponse { status = 400, message = "Kursi masih tersedia sejumlah " + d });
            }
            else
            {
                return Json(new ServiceResponse { status = 200, message = "sukses" });
            }
        }

        [HttpPost]
        public JsonResult PostDataMahasiswa(int idData, string dataMahasiswa)
        {
            try
            {
                string[] tempMahasiswa = dataMahasiswa.Split(',');
                string[] data1 = tempMahasiswa.Distinct().ToArray();
                foreach (string dt in data1)
                {
                    JadwalUjianMBKMDetail dataBaru = new JadwalUjianMBKMDetail();
                    dataBaru.JadwalUjianMBKMID = idData;
                    dataBaru.MahasiswaID = int.Parse(dt);
                    dataBaru.CreatedBy = HttpContext.Session["username"].ToString();
                    dataBaru.CreatedDate = DateTime.Now;
                    dataBaru.IsActive = true;
                    dataBaru.IsDeleted = false;
                
                    _jadwalUjianMBKMDetailService.Save(dataBaru);
                    
                }

                return Json(new ServiceResponse { status = 200, message = "sukses" });

            }
            catch (Exception e)
            {
                return Json(new ServiceResponse { status = 500, message = "gagal" });

            }
        }


        [HttpPost]
        public JsonResult getLookupByTipe(string tipe)
        {
            return Json(_lookupService.getLookupByTipe(tipe));
        }

        [HttpPost]
        public ActionResult GetFakultas(string search, string JenjangStudi)
        {

            return Json(_masterCapaianPembelajaranService.GetFakultas(JenjangStudi, search));
        }

        [HttpPost]
        public ActionResult GetSemester(string JenjangStudi)
        {
            return Json(_mahasiswaService.GetDataSemester(JenjangStudi));
        }

        [HttpPost]
        public ActionResult GetDataTable(DataTableAjaxPostModel model, string jenjangStudi, string fakultas, string jenisUjian, string tahunSemester)
        {
            var roleData = HttpContext.Session["RoleName"].ToString().ToLower();
            var namaProdi = HttpContext.Session["NamaProdi"].ToString();
            var dataFakultas = "";
            if (roleData.Contains("program studi"))
            {
                var dataTemp = _jadwalKuliahService.Find(x => x.NamaProdi == namaProdi).FirstOrDefault().FakultasID;
                dataFakultas = dataTemp.ToString();
            }
            else
            {
                dataFakultas = (fakultas != null) ? fakultas : HttpContext.Session["KodeFakultas"].ToString();
            }

            VMListJadwalUjian data = _jadwalUjianMBKMService.GetListManageUjian(model, jenjangStudi, dataFakultas, jenisUjian, tahunSemester);
            return Json(
                new
                {
                    draw = model.draw,
                    recordsTotal = data.TotalCount,
                    recordsFiltered = data.TotalFilterCount,
                    data = data.gridDatas
                }
            );
        }
        public static DateTime UnixTimeStampToDateTime(double unixTimeStamp)
        {
            DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddMilliseconds(unixTimeStamp).ToLocalTime();
            return dtDateTime;
        }

        [HttpPost]
        public ActionResult _getIndexModal(string KodeMatkul, string NamaMatkul, string ClassSection, string TanggalUjian, string JamMulai)
        {
            var sementara = TanggalUjian.Replace("Date", "").Replace("/", "").Replace("(", "").Replace(")", "");
            var tanggalan = UnixTimeStampToDateTime(double.Parse(sementara));
            IEnumerable<JadwalUjianMBKM> data = _jadwalUjianMBKMService.Find(x =>
            x.ClassSection == ClassSection
            && x.JamMulai == JamMulai
            && x.KodeMatkul == KodeMatkul
            && x.NamaMatkul == NamaMatkul
            && DbFunctions.TruncateTime(x.TanggalUjian) == tanggalan
            ).ToList();

            ViewData["dataKelas"] = data;

            return View();
        }

    }
}