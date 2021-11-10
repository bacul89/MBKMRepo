using MBKM.Services.MBKMServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MBKM.Entities.ViewModel;
using MBKM.Presentation.models;
using Rotativa;
using Rotativa.Options;

using MBKM.Presentation.Helper;
using MBKM.Common.Helpers;
using MBKM.Entities.Models.MBKM;
using Newtonsoft.Json;
using MBKM.Services;

namespace MBKM.Presentation.Areas.Portal.Controllers
{

    [MBKMAuthorize]
    public class TranskripMahasiswaController : Controller
    {



        private INilaiKuliahService _transkripService;
        private IJadwalKuliahMahasiswaService _jkMhsService;
        private IMahasiswaService _mahasiswaService;
        private ILookupService _lookupService;
        private IPendaftaranMataKuliahService _pendaftaranMataKuliahService;

        public TranskripMahasiswaController(INilaiKuliahService transkripService, IJadwalKuliahMahasiswaService jkMhsService, IMahasiswaService mahasiswaService, ILookupService lookupService, IPendaftaranMataKuliahService pendaftaranMKService)
        {
            
            _transkripService = transkripService;
            _mahasiswaService = mahasiswaService;
            _jkMhsService = jkMhsService;
            _lookupService = lookupService;
            _pendaftaranMataKuliahService = pendaftaranMKService;
        }



        // GET: Portal/Transkrip
        public ActionResult Index()
        {
            //Session["nama"] = "Smitty Swagger Werben Jeger Man Jensen";
            //Session["email"] = "sabangsasabana@gmail.com";
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
                    FlagCetak = item.FlagCetak,
                    KodeMataKuliah = item.JadwalKuliahs.KodeMataKuliah,
                    NamaMataKuliah = item.NamaMatakuliah,
                    SKS = item.JadwalKuliahs.SKS,
                    //Nilai = item.Nilai,
                    Grade = item.Grade,
                    STRM = item.JadwalKuliahs.STRM,
                    MataKuilahID = item.JadwalKuliahs.MataKuliahID,

                    NamaMataKuliahEN = GetMatkulEn(item.JadwalKuliahs.KodeMataKuliah, Int32.Parse(item.JadwalKuliahs.MataKuliahID), item.JadwalKuliahs.STRM)
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
                        data.UpdatedBy = Session["nama"] as string;

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
                return Json(new ServiceResponse { status = 500, message = "Cetak Gagal Silakhan Hubungi BAA!!!" });
            }

        }

        public ActionResult PrintTranskrip()
        {
            Session["email"] = "sabangsasabana@gmail.com";
            string email = Session["email"] as string;

            Mahasiswa model = GetMahasiswaByEmail(email);
            return View("PrintTranskrip", model);

        }


        public ActionResult ExportPDF()
        {

            Session["email"] = "sabangsasabana@gmail.com";
            string email = Session["email"] as string;
            Mahasiswa model = GetMahasiswaByEmail(email);
            return new ViewAsPdf("ExportPDF", model)
            {
                FileName = "ReportShift.pdf",
                PageSize = Size.A4,
                PageOrientation = Orientation.Landscape,
                //CustomSwitches = footer,
                PageMargins = new Margins(10, 3, 20, 3)

            };




            /*return new ActionAsPdf("Index")
            {
                FileName = Server.MapPath("~/Content/Relato.pdf"),
                PageOrientation = Rotativa.Options.Orientation.Landscape,
                PageSize = Rotativa.Options.Size.A4
            };*/
        }


        public ActionResult ExportNew()
        {

            Session["email"] = "sabangsasabana@gmail.com";
            string email = Session["email"] as string;
            Mahasiswa model = GetMahasiswaByEmail(email);
            return new ViewAsPdf("PrintTranskrip", model)
            {
                FileName = "ReportShift.pdf",
                PageSize = Size.A4,
                PageOrientation = Orientation.Landscape,
                //CustomSwitches = footer,
                PageMargins = new Margins(10, 3, 20, 3)

            };
        }


        /*public async Task<IActionResult> Tes(){
                    var pdf = new Rotativa.ViewAsPdf("Tes")
                    {
                        FileName = "C:\\Test.pdf",
                        PageSize = Rotativa.Options.Size.A4,
                        PageOrientation = Rotativa.Options.Orientation.Portrait,
                        PageHeight = 20,
                    };

                    var byteArray = await pdf.BuildFile(ControllerContext);
                    return File(byteArray, "application/pdf");
        }*/


        //---<> Sertifikat
        [HttpPost]
        public ActionResult UpdateStatusSertifikat()
        {
            string email = Session["email"] as string;
            Mahasiswa mahasiswa = GetMahasiswaByEmail(email);

            try
            {


                var dataMahasiswa = _pendaftaranMataKuliahService.Find(x => x.MahasiswaID == mahasiswa.ID).ToList();
                foreach (var item in dataMahasiswa)
                {

                    if (item.FlagSertifikat == false)
                    {

                        long id = item.ID;
                        var data = _pendaftaranMataKuliahService.Get(id);
                        data.FlagSertifikat = true;
                        data.UpdatedBy = Session["nama"] as string;

                        _pendaftaranMataKuliahService.Save(data);
                        
                    }
                    else
                    {
                        return Json(new ServiceResponse { status = 500, message = "Cetak Gagal Silakhan Hubungi BAA!!!" });
                    }
                }

                return Json(new ServiceResponse { status = 200, message = "Cetak Berhasil..." });


                /*var dataList = _transkripService.Find(x => x.MahasiswaID == idMahasiswa).ToList();
                foreach (var item in dataList)
                {

                    if (item.FlagCetak == false)
                    {
                        long id = item.ID;
                        var data = _transkripService.Get(id);
                        data.FlagCetak = true;
                        data.UpdatedBy = Session["nama"] as string;

                        _transkripService.Save(data);
                    }
                    else
                    {
                        return Json(new ServiceResponse { status = 500, message = "Cetak Gagal Silakhan Hubungi BAA!!!" });
                    }

                }*/

                /*return Json(new ServiceResponse { status = 200, message = "Cetak Berhasil..." });*/
            }
            catch (Exception e)
            {
                return Json(new ServiceResponse { status = 500, message = "Cetak Gagal Silakhan Hubungi BAA!!!" });
            }
        }

        [HttpPost]
        public ActionResult CheckStatusSertifikat()
        {

            string email = Session["email"] as string;

            Mahasiswa mahasiswa = GetMahasiswaByEmail(email);
            try
            {
                var dataMahasiswa = _pendaftaranMataKuliahService.Find(x => x.MahasiswaID == mahasiswa.ID).First();
                /*foreach (var item in dataList)
                {*/
                /*var data= Json(new {});
                if (dataMahasiswa.FlagSertifikat != false)
                {

                    //return Json(new ServiceResponse { status = 200, message = "Cetak Berhasil...", data="{status:true}" });
                    data = Json(new { status = dataMahasiswa.FlagSertifikat });
                }
                else
                {
                    data = Json(new { status = 0 });
                }*/
                return Json(new ServiceResponse { status = 200, message = "Cetak Gagal Silakhan Hubungi BAA!!!", data = dataMahasiswa.FlagSertifikat });

            }
            catch (Exception e)
            {
                return Json(new ServiceResponse { status = 500, message = "Cetak Gagal Silakhan Hubungi BAA!!!" });
            }

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