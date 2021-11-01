using MBKM.Services.MBKMServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MBKM.Entities.ViewModel;
using MBKM.Presentation.models;

using MBKM.Presentation.Helper;
using MBKM.Common.Helpers;
using MBKM.Entities.Models.MBKM;
using Newtonsoft.Json;
using MBKM.Services;

namespace MBKM.Presentation.Areas.Admin.Controllers
{

    [MBKMAuthorize]

    public class TranskripController : Controller
    {

        private INilaiKuliahService _transkripService;
        private IMahasiswaService _mahasiswaService;
        private ILookupService _lookupService;

        public TranskripController(INilaiKuliahService transkripService, IJadwalKuliahMahasiswaService jkMhsService, IMahasiswaService mahasiswaService, ILookupService lookupService)
        {

            _transkripService = transkripService;
            _mahasiswaService = mahasiswaService;
            _lookupService = lookupService;
        }





        // GET: Admin/Transkrip
        public ActionResult Index()
        {
            //Session["username"] = "Smitty Werben Jeger Man Jensen";
            return View();
        }

        public ActionResult GetTranskripList()
        {

            VMListNilaiKuliah getNilaiMahasiswa = _transkripService.GetNilaiMahasiswa();
            //var getNilaiMahasiswa = _transkripService.Find(m => m.IsActive == true && m.IsDeleted == false).OrderByDescending(x => x.MahasiswaID).Take(1);
            //var transkrip = _transkripService.Find(m => m.IsActive == true && m.IsDeleted == false).ToList();
            //return View(model);

            //List<NilaiKuliah> MVTranskrip = new List<NilaiKuliah>();
            //List<string> mapTranskrip = new List<string>();
            List<object> data = new List<object>();

            //Console.WriteLine(getNilaiMahasiswa);

            /*foreach (var item in getNilaiMahasiswa)
            {
                var q = new
                {
                    ID = item.MahasiswaID,
                    //mhs = item.Mahasiswas,
                    flagCetak = item.FlagCetak,
                    jks = item.JadwalKuliahs,
                    Name = item.Mahasiswas.Nama,
                    NIM = item.Mahasiswas.NIM,
                    NamaUniversitas = item.Mahasiswas.NamaUniversitas,
                    JenjangStudi = item.Mahasiswas.JenjangStudi,
                    NoKerjasama = item.Mahasiswas.NoKerjasama
                };
                data.Add(q);
            }*/

            /*foreach (var item in transkrip)
            {
                if (!mapTranskrip.Contains(item.NamaMatakuliah))
                {
                    MVTranskrip.Add(item);
                    mapTranskrip.Add(item.Mahasiswas.Nama);
                    mapTranskrip.Add(item.Mahasiswas.ID.ToString());
                    mapTranskrip.Add(item.Mahasiswas.NIM);
                    mapTranskrip.Add(item.Mahasiswas.NamaUniversitas);
                    mapTranskrip.Add(item.Mahasiswas.JenjangStudi);
                    mapTranskrip.Add(item.Mahasiswas.NoKerjasama);
                    mapTranskrip.Add(item.FlagCetak.ToString());

                }
            }*/


            return new ContentResult { Content = JsonConvert.SerializeObject(getNilaiMahasiswa.gridDatas), ContentType = "application/json" };
        }

        [HttpPost]
        public ActionResult UpdateStatus(int idMahasiswa)
        {

            //var id = 0;
            try
            {
                var dataList = _transkripService.Find(x => x.MahasiswaID == idMahasiswa).ToList();
                foreach (var item in dataList)
                {
                    long id = item.ID;
                    var data = _transkripService.Get(id);
                    data.FlagCetak = false;
                    data.UpdatedBy = Session["username"] as string;

                    _transkripService.Save(data);
                }

                

                return Json(new ServiceResponse { status = 200, message = "Cetak Status Berhasil Di Update" });
            }
            catch (Exception e)
            {
                return Json(new ServiceResponse { status = 500, message = "Cetak Status Gagal Di Update!!!" });
            }

        }


        [HttpPost]
        public ActionResult getTranskripByIdMahasiswa(int id)
        {

            List<NilaiKuliah> MVTranskrip = new List<NilaiKuliah>();
            List<string> mapTranskrip = new List<string>();           

            var transkrip = _transkripService.Find(m => m.MahasiswaID == id && m.IsActive == true && m.IsDeleted == false).ToList();

            List<object> data = new List<object>();

            //Console.WriteLine(getNilaiMahasiswa);

            foreach (var item in transkrip)
            {

                var row = new
                {
                    ID = item.ID,
                    FlagCetak = item.FlagCetak,
                    KodeMataKuliah = item.JadwalKuliahs.KodeMataKuliah,
                    NamaMataKuliah = item.NamaMatakuliah,
                    SKS = item.JadwalKuliahs.SKS,
                    Grade = item.Grade,
                    STRM = item.JadwalKuliahs.STRM,
                    MataKuilahID = item.JadwalKuliahs.MataKuliahID,

                    //mahasiswa
                    NIM = item.Mahasiswas.NIM,
                    Nama = item.Mahasiswas.Nama,
                    NamaUniversitas = item.Mahasiswas.NamaUniversitas,
                    JenjangStudi = item.Mahasiswas.JenjangStudi,
                    TempatLahir = item.Mahasiswas.TempatLahir,
                    TanggalLahir = item.Mahasiswas.TanggalLahir,

                    NamaMataKuliahEN = GetMatkulEn(item.JadwalKuliahs.KodeMataKuliah, Int32.Parse(item.JadwalKuliahs.MataKuliahID), item.JadwalKuliahs.STRM)
                };
                data.Add(row);
            }
            return new ContentResult { Content = JsonConvert.SerializeObject(data), ContentType = "application/json" };
        }



        [HttpPost]
        public ActionResult GetMahasiswaById(int id)
        {
            var mahasiswa = _mahasiswaService.Find(m => m.ID == id).FirstOrDefault();
            return new ContentResult { Content = JsonConvert.SerializeObject(mahasiswa), ContentType = "application/json" };
        }


        //-- SP
        private string GetMatkulEn(string KodeMataKuliah, int MataKuliahID, int STRM)
        {

            var GetMatkulEn = _transkripService.GetMatkulEn(KodeMataKuliah, MataKuliahID, STRM);
            string final = "";
            foreach (var item in GetMatkulEn)
            {
                final = item.COURSE_TITLE_LONG;
            }
            return final;
        }




        //--- lookup
        public ActionResult getLookupByTipe(string tipe)
        {
            return Json(_lookupService.getLookupByTipe(tipe), JsonRequestBehavior.AllowGet);
        }

    }
}