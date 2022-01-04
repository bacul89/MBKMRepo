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
        private IFeedbackMatkulService _feedbackMatkulService;
        private IInformasiPertukaranService _informasiPertukaranService;

        public TranskripMahasiswaController(INilaiKuliahService transkripService, IJadwalKuliahMahasiswaService jkMhsService, IMahasiswaService mahasiswaService, ILookupService lookupService, IPendaftaranMataKuliahService pendaftaranMKService, IFeedbackMatkulService feedbackMatkulService, IInformasiPertukaranService informasiPertukaranService)
        {
            
            _transkripService = transkripService;
            _mahasiswaService = mahasiswaService;
            _jkMhsService = jkMhsService;
            _lookupService = lookupService;
            _pendaftaranMataKuliahService = pendaftaranMKService;
            _feedbackMatkulService = feedbackMatkulService;
            _informasiPertukaranService = informasiPertukaranService;
        }



        // GET: Portal/Transkrip
        public ActionResult Index()
        {
            //Session["nama"] = "Smitty Swagger Werben Jeger Man Jensen";
            //Session["email"] = "sabangsasabana@gmail.com";
            //var mahasiswa = GetMahasiswaByEmail(Session["email"] as string);
            string email = Session["emailMahasiswa"] as string;

            Mahasiswa model = GetMahasiswaByEmail(email);
            return View(model);
        }

        [HttpPost]
        public ActionResult getTranskrip()
        {
            string email = Session["emailMahasiswa"] as string;

            List<NilaiKuliah> MVTranskrip = new List<NilaiKuliah>();
            List<string> mapTranskrip = new List<string>();


            Mahasiswa mahasiswa = GetMahasiswaByEmail(email);
            //
            //String strmString;
            //int semesterID;

           
            List<object> data = new List<object>();
            var transkrip = _transkripService.Find(m => m.MahasiswaID == mahasiswa.ID && m.IsActive == true && m.IsDeleted == false).ToList();





            string strmString ="";
            int temp = 0;
            foreach (var item in transkrip)
            {
                temp++;
                if (temp == 1)
                {
                    strmString = item.JadwalKuliahs.STRM.ToString();
                }

                VMNilaiBobot  Nilai = _transkripService.GetBobotNilai(item.NilaiTotal);               
                
                //return View(model);
                var row = new
                {
                    
                    ID = item.ID,
                    //mhs = item.Mahasiswas,
                    FlagCetak = item.FlagCetak,
                    KodeMataKuliah = item.JadwalKuliahs.KodeMataKuliah,
                    NamaMataKuliah = item.NamaMatakuliah,
                    SKS = item.JadwalKuliahs.SKS,
                    NilaiTotal = item.NilaiTotal,
                    Nilai = Nilai.GRADE_POINTS,
                    Grade = Nilai.DESCR,
                    STRM = item.JadwalKuliahs.STRM,
                    MataKuilahID = item.JadwalKuliahs.MataKuliahID,

                    NamaMataKuliahEN = GetMatkulEn(item.JadwalKuliahs.KodeMataKuliah, Int32.Parse(item.JadwalKuliahs.MataKuliahID), item.JadwalKuliahs.STRM)
                };
                data.Add(row);
            }


            if (strmString == "")
            {
                VMSemester semester = _jkMhsService.getOngoingSemester(mahasiswa.JenjangStudi);
                strmString = semester.ID.ToString();
            }


            //Int32 strm  = strmString.parse();
            int strmInt = Int32.Parse(strmString);
            var strmName = _feedbackMatkulService.GetSemesterByStrm(strmInt.ToString());

            var cry = false;
            try
            {
                var infoPertukaran = _informasiPertukaranService.Find(m => m.MahasiswaID == mahasiswa.ID && m.STRM == strmInt).First();
                cry = true;
            }
            catch
            {
                cry = false;
            }



            var result = new
            {                
                pertukaran = cry,
                transkrip = data,
                mahasiswaBirthday = mahasiswa.TanggalLahir,
                semester = strmName.Nama,
                strm = strmInt,
                mahasiswaName = mahasiswa.Nama,
                mahasiswaNIM = mahasiswa.NIM
            };




            return new ContentResult { Content = JsonConvert.SerializeObject(result), ContentType = "application/json" };
        }

        public Mahasiswa GetMahasiswaByEmail(string email)
        {
            return _mahasiswaService.Find(m => m.Email == email).FirstOrDefault();
        }

        [HttpPost]
        public ActionResult UpdateStatus(int idMahasiswa, string strmStr)
        {
            try
            {
                string email = Session["emailMahasiswa"] as string;
                Mahasiswa mahasiswa = GetMahasiswaByEmail(email);
                //VMSemester semester = _jkMhsService.getOngoingSemester(mahasiswa.JenjangStudi);

                //var strmString = semester.ID;
                int strm = Int32.Parse(strmStr.ToString());

                var data = _feedbackMatkulService.Find(x => x.JadwalKuliahs.STRM == strm).Where(x => x.MahasiswaID == mahasiswa.ID)
                    .GroupBy(z => new { z.MahasiswaID, z.StatusFeedBack, z.Mahasiswas, z.JadwalKuliahID })
                    .Select(s => new { MahasiswaID = s.Key.MahasiswaID, Status = s.Key.StatusFeedBack, Mahasiswas = s.Key.Mahasiswas, JadwalID = s.Key.JadwalKuliahID }).ToList();

                var data2 = data.GroupBy(z => new { z.MahasiswaID, z.Mahasiswas })
                    .Select(s => new { MahasiswaID = s.Key.MahasiswaID, Mahasiswas = s.Key.Mahasiswas }).ToList();

                var data3 = data.GroupBy(z => new { z.MahasiswaID, z.Mahasiswas, z.JadwalID })
                    .Select(s => new { MahasiswaID = s.Key.MahasiswaID, Mahasiswas = s.Key.Mahasiswas }).ToList();

                var pendaftaran = _pendaftaranMataKuliahService.Find(x => x.JadwalKuliahs.STRM == strm && x.MahasiswaID == mahasiswa.ID && x.StatusPendaftaran.ToLower().Contains("accepted")).ToList();

                List<String[]> final = new List<String[]>();
                var DescSemester = _feedbackMatkulService.GetSemesterByStrm(strmStr.ToString());
                foreach (var d in data2)
                {
                    var dataCheck = data.Where(x => x.MahasiswaID == d.MahasiswaID && x.Status == false).Count();
                    var MatkulPendaftaranCheck = pendaftaran.Where(x => x.MahasiswaID == d.MahasiswaID).Count();
                    var matkulFeedbackCheck = data3.Where(x => x.MahasiswaID == d.MahasiswaID).Count();

                    if (dataCheck == 0 && (matkulFeedbackCheck == MatkulPendaftaranCheck))
                    {
                        var dataList = _transkripService.Find(x => x.MahasiswaID == idMahasiswa).ToList();
                        foreach (var item in dataList)
                        {

                            if (item.FlagCetak == false)
                            {
                                long id = item.ID;
                                var dataSave = _transkripService.Get(id);
                                dataSave.FlagCetak = true;
                                dataSave.UpdatedBy = Session["nama"] as string;

                                _transkripService.Save(dataSave);
                            }
                            else
                            {
                                return Json(new ServiceResponse { status = 500, message = "Cetak Gagal Silakhan Hubungi BAA!!!" });
                            }

                        }


                    }
                    else
                    {
                        return Json(new ServiceResponse { status = 500, message = "Silahkan Mengisi Feedback Terlebih Dahulu!!!" });
                    }
                }


                return Json(new ServiceResponse { status = 200, message = "Cetak Berhasil..." });
            }
            catch (Exception e)
            {
                return Json(new ServiceResponse { status = 500, message = "Cetak Gagal Silakhan Hubungi BAA!!!" });
            }

        }

        /*public ActionResult PrintTranskrip()
        {
            Session["email"] = "sabangsasabana@gmail.com";
            string email = Session["emailMahasiswa"] as string;

            Mahasiswa model = GetMahasiswaByEmail(email);
            return View("PrintTranskrip", model);

        }*/

        //---<> Sertifikat
        [HttpPost]
        public ActionResult UpdateStatusSertifikat()
        {
            string email = Session["emailMahasiswa"] as string;
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

            string email = Session["emailMahasiswa"] as string;

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

        [HttpPost]
        public ActionResult CheckStatusFeedback(string strmStr)
        {
            string email = Session["emailMahasiswa"] as string;
            Mahasiswa mahasiswa = GetMahasiswaByEmail(email);
            //VMSemester semester = _jkMhsService.getOngoingSemester(mahasiswa.JenjangStudi);



            //var strmStr = semester.ID;
            int strm = Int32.Parse(strmStr.ToString());

            var data = _feedbackMatkulService.Find(x => x.JadwalKuliahs.STRM == strm && x.MahasiswaID == mahasiswa.ID && x.IsDeleted == false)
                .GroupBy(z => new { z.MahasiswaID, z.StatusFeedBack, z.Mahasiswas, z.JadwalKuliahID })
                .Select(s => new { MahasiswaID = s.Key.MahasiswaID, Status = s.Key.StatusFeedBack, Mahasiswas = s.Key.Mahasiswas, JadwalID = s.Key.JadwalKuliahID }).ToList();

            var data2 = data.GroupBy(z => new { z.MahasiswaID, z.Mahasiswas })
                .Select(s => new { MahasiswaID = s.Key.MahasiswaID, Mahasiswas = s.Key.Mahasiswas }).ToList();

            var data3 = data.GroupBy(z => new { z.MahasiswaID, z.Mahasiswas, z.JadwalID })
                .Select(s => new { MahasiswaID = s.Key.MahasiswaID, Mahasiswas = s.Key.Mahasiswas }).ToList();

            var pendaftaran = _pendaftaranMataKuliahService.Find(x => x.JadwalKuliahs.STRM == strm && x.MahasiswaID == mahasiswa.ID && x.StatusPendaftaran.ToLower().Contains("accepted") && x.IsDeleted == false).ToList();

            List<String[]> final = new List<String[]>();
            /*foreach (var d in pendaftaran)
            {*/
            //var dataCheck = data.Where(x => x.MahasiswaID == d.MahasiswaID && x.Status == false).Count();
            //var MatkulPendaftaranCheck = pendaftaran.Where(x => x.MahasiswaID == d.MahasiswaID).Count();
            //var matkulFeedbackCheck = data3.Where(x => x.MahasiswaID == d.MahasiswaID).Count();

            if (mahasiswa.NIM != mahasiswa.NIMAsal)
            {
                foreach (var p in pendaftaran)
                {

                    int index = 0;
                    long temp = 0;
                    foreach (var e in data)
                    {
                        index++;
                        if (e.JadwalID == p.JadwalKuliahID)
                        {

                            final.Add(new String[]{
                                p.MahasiswaID.ToString(),
                                p.JadwalKuliahs.NamaMataKuliah,
                                p.JadwalKuliahs.KodeMataKuliah,
                                p.JadwalKuliahs.MataKuliahID,
                                "Sudah Feedback",
                                mahasiswa.FlagBayar.ToString(),
                                p.JadwalKuliahID.ToString(),
                                GetMatkulEn(p.JadwalKuliahs.KodeMataKuliah, Int32.Parse(p.JadwalKuliahs.MataKuliahID), strm)
                                //e.Status.ToString(),
                            });

                            temp = p.JadwalKuliahID;
                        }

                        if (data.Count() == index && p.JadwalKuliahID != temp)
                        {
                            final.Add(new String[]{
                                    p.MahasiswaID.ToString(),
                                    p.JadwalKuliahs.NamaMataKuliah,
                                    p.JadwalKuliahs.KodeMataKuliah,
                                    p.JadwalKuliahs.MataKuliahID,
                                    "Belum Feedback",
                                    mahasiswa.FlagBayar.ToString(),
                                    p.JadwalKuliahID.ToString(),
                                    GetMatkulEn(p.JadwalKuliahs.KodeMataKuliah, Int32.Parse(p.JadwalKuliahs.MataKuliahID), strm)
                                    //e.Status.ToString(),
                                });
                            temp = p.JadwalKuliahID;
                        }


                        /*else
                        {
                            final.Add(new String[]{
                                p.MahasiswaID.ToString(),
                                p.JadwalKuliahs.NamaMataKuliah,
                                p.JadwalKuliahs.KodeMataKuliah,
                                p.JadwalKuliahs.MataKuliahID,
                                "Belum Feedback",
                                mahasiswa.FlagBayar.ToString(),
                            });
                        }*/
                        Console.WriteLine(final);
                        Console.WriteLine(final);
                    }
                }
            }
            else
            {
                foreach (var p in pendaftaran)
                {


                            final.Add(new String[]{
                                    p.MahasiswaID.ToString(),
                                    p.JadwalKuliahs.NamaMataKuliah,
                                    p.JadwalKuliahs.KodeMataKuliah,
                                    p.JadwalKuliahs.MataKuliahID,
                                    "Belum Feedback",
                                    mahasiswa.FlagBayar.ToString(),
                                    p.JadwalKuliahID.ToString(),
                                    GetMatkulEn(p.JadwalKuliahs.KodeMataKuliah, Int32.Parse(p.JadwalKuliahs.MataKuliahID), strm)
                                    //e.Status.ToString(),
                                });


                        /*else
                        {
                            final.Add(new String[]{
                                p.MahasiswaID.ToString(),
                                p.JadwalKuliahs.NamaMataKuliah,
                                p.JadwalKuliahs.KodeMataKuliah,
                                p.JadwalKuliahs.MataKuliahID,
                                "Belum Feedback",
                                mahasiswa.FlagBayar.ToString(),
                            });
                        }*/
                        Console.WriteLine(final);
                        Console.WriteLine(final);
                    
                }
            }

            /*}*/
            return Json(final);
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