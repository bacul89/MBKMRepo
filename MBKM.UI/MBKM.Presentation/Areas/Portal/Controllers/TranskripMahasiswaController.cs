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

namespace MBKM.Presentation.Areas.Portal.Controllers
{
    public class TranskripMahasiswaController : Controller
    {



        private INilaiKuliahService _transkripService;
        private IJadwalKuliahMahasiswaService _jkMhsService;
        private IMahasiswaService _mahasiswaService;

        public TranskripMahasiswaController(INilaiKuliahService transkripService, IJadwalKuliahMahasiswaService jkMhsService, IMahasiswaService mahasiswaService)
        {
            
            _transkripService = transkripService;
            _mahasiswaService = mahasiswaService;
            _jkMhsService = jkMhsService;
        }



        // GET: Portal/Transkrip
        public ActionResult Index()
        {
            Session["nama"] = "Smitty Swagger Werben Jeger Man Jensen";
            Session["email"] = "sabangsasabana@gmail.com";
            //var mahasiswa = GetMahasiswaByEmail(Session["email"] as string);
            string email = Session["email"] as string;

            Mahasiswa model = GetMahasiswaByEmail(email);
            return View(model);
        }

        [HttpPost]
        public ActionResult getTranskrip()
        {
            string email = Session["email"] as string;

            List<NilaiKuliah> MVTranskrip = new List<NilaiKuliah>();
            List<string> mapTranskrip = new List<string>();


            Mahasiswa mahasiswa = GetMahasiswaByEmail(email);
            var transkrip = _transkripService.Find(m => m.MahasiswaID == mahasiswa.ID && m.IsActive == true && m.IsDeleted == false).ToList();
            //return View(model);



            List<object> data = new List<object>();

            //Console.WriteLine(getNilaiMahasiswa);

            foreach (var item in transkrip)
            {
                var row = new
                {
                    ID = item.ID,
                    //mhs = item.Mahasiswas,
                    //flagCetak = item.FlagCetak,
                    KodeMataKuliah = item.JadwalKuliahs.KodeMataKuliah,
                    NamaMataKuliah = item.NamaMatakuliah,
                    SKS = item.JadwalKuliahs.SKS,
                    //Nilai = item.Nilai,
                    Grade = item.Grade
                };
                data.Add(row);
            }

            /*            foreach (var item in transkrip)
                        {
                            if (!mapTranskrip.Contains(item.NamaMatakuliah))
                            {
                                MVTranskrip.Add(item);
                                mapTranskrip.Add(item.JadwalKuliahs.KodeMataKuliah);
                            }
                        }*/
            return new ContentResult { Content = JsonConvert.SerializeObject(data), ContentType = "application/json" };
        }

        public Mahasiswa GetMahasiswaByEmail(string email)
        {
            return _mahasiswaService.Find(m => m.Email == email).FirstOrDefault();
        }

        [HttpPost]
        public ActionResult UpdateStatus(int idMahasiswa)
        {

            try
            {
                var dataList = _transkripService.Find(x => x.MahasiswaID == idMahasiswa).ToList();
                foreach (var item in dataList)
                {

                    if (item.FlagCetak == false)
                    {
                        long id = item.ID;
                        var data = _transkripService.Get(id);
                        data.FlagCetak = true;
                        data.UpdatedBy = Session["name"] as string;

                        _transkripService.Save(data);
                    }
                    else
                    {
                        return Json(new ServiceResponse { status = 500, message = "Cetak Gagal Silakhan Hubungi BAA!!!" });
                    }

                }

                return Json(new ServiceResponse { status = 200, message = "Cetak Berhasil..." });
            }
            catch (Exception e)
            {
                return Json(new ServiceResponse { status = 500, message = "Cetak Silakhan Hubungi BAA!!!" });
            }

        }




    }
}