using MBKM.Entities.Models.MBKM;
using MBKM.Entities.ViewModel;
using MBKM.Presentation.Helper;
using MBKM.Presentation.models;
using MBKM.Services.MBKMServices;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MBKM.Presentation.Areas.Portal.Controllers
{
    [MBKMAuthorize]
    public class FeedBackMatakuliahController : Controller
    {

        private IPendaftaranMataKuliahService _pendaftaranMataKuliahService;
        private IJadwalKuliahService _jadwalKuliahService;
        private IFeedbackMatkulService _feedbackMatkulService;
        private IFeedbackMatkulDetailService _feedbackMatkulDetailService;
        private IMahasiswaService _mahasiswaService;
        private IJadwalUjianMBKMService _jadwalUjianMBKMService;

        public FeedBackMatakuliahController(IPendaftaranMataKuliahService pendaftaranMataKuliahService, IJadwalKuliahService jadwalKuliahService, IFeedbackMatkulService feedbackMatkulService, IFeedbackMatkulDetailService feedbackMatkulDetailService, IMahasiswaService mahasiswaService, IJadwalUjianMBKMService jadwalUjianMBKMService)
        {
            _pendaftaranMataKuliahService = pendaftaranMataKuliahService;
            _jadwalKuliahService = jadwalKuliahService;
            _feedbackMatkulService = feedbackMatkulService;
            _feedbackMatkulDetailService = feedbackMatkulDetailService;
            _mahasiswaService = mahasiswaService;
            _jadwalUjianMBKMService = jadwalUjianMBKMService;
        }

        // GET: Portal/FeedBackMatakuliah
        public ActionResult Index()
        {
            var email = HttpContext.Session["email"].ToString();
            var jenjang = _mahasiswaService.Find(x => x.Email == email).First().JenjangStudi;
            var dataSemester = _mahasiswaService.GetDataSemester(jenjang).First().ID;
            ViewData["firstSemester"] = dataSemester.ToString();
            IEnumerable<VMSemester> data = _jadwalUjianMBKMService.getAllSemester();
            ViewData["semester"] = data;
            return View();
        }

        [HttpPost]
        public ActionResult GetDataTable(int semester)
        {
            var email = HttpContext.Session["email"].ToString();
            var data1 = _pendaftaranMataKuliahService.Find(x => x.mahasiswas.Email == email && x.JadwalKuliahs.STRM == semester).ToList();
            var DescSemester = _feedbackMatkulService.GetSemesterByStrm(data1.First().JadwalKuliahs.STRM.ToString());
            List<String[]> final = new List<String[]>();
           
            foreach (var d in data1)
            {
                var listDosen = _feedbackMatkulService.GetDosenMakulPertemuans(d.JadwalKuliahs.KodeMataKuliah, d.JadwalKuliahs.ClassSection, d.JadwalKuliahs.STRM.ToString(), d.JadwalKuliahs.FakultasID.ToString());
                var checkstatus = "-";
                foreach (var p in listDosen)
                {
                    var checkStatusDosen = _feedbackMatkulService.Find(x => x.JadwalKuliahID == d.JadwalKuliahID && x.DosenID == p.Instructor_id).Count();
                    if(checkStatusDosen == 0)
                    {
                        checkstatus = "ada";
                    }
                }
                var idJadwalKuliah = d.JadwalKuliahID.ToString();
                var strm = d.JadwalKuliahs.STRM.ToString();
                
                if (listDosen.Count() == 0)
                {
                    final.Add(new String[]{
                        idJadwalKuliah,
                        DescSemester.Nama,
                        d.JadwalKuliahs.KodeMataKuliah,
                        d.JadwalKuliahs.NamaMataKuliah,
                        d.JadwalKuliahs.SKS,
                        d.JadwalKuliahs.ClassSection,
                        "Belum"
                    });
                }
                else if (checkstatus == "ada")
                {
                    final.Add(new String[]{
                        idJadwalKuliah,
                        DescSemester.Nama,
                        d.JadwalKuliahs.KodeMataKuliah,
                        d.JadwalKuliahs.NamaMataKuliah,
                        d.JadwalKuliahs.SKS,
                        d.JadwalKuliahs.ClassSection,
                        "Belum"
                    });
                }
                else
                {
                    final.Add(new String[]{
                        idJadwalKuliah,
                        DescSemester.Nama,
                        d.JadwalKuliahs.KodeMataKuliah,
                        d.JadwalKuliahs.NamaMataKuliah,
                        d.JadwalKuliahs.SKS,
                        d.JadwalKuliahs.ClassSection,
                        "Sudah"
                    });
                }
            }

            return Json(final);
        }

        public ActionResult DetailFeedBackMatakuliah(int id)
        {
            var email = HttpContext.Session["email"].ToString();
            var jenjang = _mahasiswaService.Find(x => x.Email == email).First().JenjangStudi;
            var dataSemester = _mahasiswaService.GetDataSemester(jenjang).First().ID;
            IEnumerable<VMPertanyaanFeedback> Pertanyaan = _feedbackMatkulService.GetPertanyaanFeedbacks(jenjang, "2010");
            var DescSemester = _feedbackMatkulService.GetSemesterByStrm("2110");
            ViewData["semester"] = DescSemester.Nama;
            ViewData["pertanyaan"] = Pertanyaan;
            ViewData["jadwalID"] = id;
            IEnumerable<VMJawabanFeedback> Jawaban = _feedbackMatkulService.GetJawabanFeedback(Pertanyaan.First().KodeJawaban);
            ViewData["jawaban"] = Jawaban;
            var data1 = _jadwalKuliahService.Get(id);
            var listDosen = _feedbackMatkulService.GetDosenMakulPertemuans(data1.KodeMataKuliah, data1.ClassSection, data1.STRM.ToString(), data1.FakultasID.ToString());
            var dosenTmp = listDosen.FirstOrDefault().NamaDosen.Split('-');
            ViewData["namaDosen"] = dosenTmp[1];
            ViewData["idDosen"] = listDosen.FirstOrDefault().Instructor_id;
            ViewData["urutan"] = 0;
            List<String> final = new List<String>();
            foreach (var d in listDosen)
            {
                final.Add(d.Instructor_id);
            }
            ViewData["listDosen"] = string.Join(",", final.ToArray());
            return View(data1);
        }

        [HttpPost]
        public ActionResult PostDataAnswer(List<DTOAnswer> answer)
        {
            var email = HttpContext.Session["email"].ToString();
            var mahasiswaID = _mahasiswaService.Find(x => x.Email == email).First().ID;
            FeedbackMatkul dBaru = new FeedbackMatkul();
            dBaru.MahasiswaID = mahasiswaID;
            dBaru.JadwalKuliahID = int.Parse(answer.First().JadwalKuliahID);
            dBaru.KritikSaran = answer.First().kritik;
            dBaru.DosenID = answer.First().dosenID;
            dBaru.NamaDosen = answer.First().namaDosen;
            dBaru.StatusFeedBack = true;
            dBaru.CreatedBy = "MAHASISWA";
            dBaru.CreatedDate = DateTime.Now;
            dBaru.IsActive = true;
            dBaru.IsDeleted = false;
            try
            {
                _feedbackMatkulService.Save(dBaru);
            }
            catch (DbEntityValidationException e)
            {
                return Json(new ServiceResponse { status = 300, message = "Terjadi Kesalahan saat Melakukan Save Data" });
            }

            var FeedBackID = dBaru.ID;
            foreach (var d in answer)
            {
                var dataCollection = d.data.Split('$');
                FeedbackMatkulDetail newAnswer = new FeedbackMatkulDetail();
                newAnswer.FeedbackMatkulID = FeedBackID;
                newAnswer.PertanyaanID = dataCollection[0];
                newAnswer.Pertanyaan = dataCollection[1];
                newAnswer.KategoriPertanyaan = dataCollection[2];
                newAnswer.Nilai = int.Parse(d.nilai);
                newAnswer.IsActive = true;
                newAnswer.IsDeleted = false;
                newAnswer.CreatedBy = "MAHASISWA";
                newAnswer.CreatedDate = DateTime.Now;
                try
                {
                    _feedbackMatkulDetailService.Save(newAnswer);
                }
                catch (DbEntityValidationException e)
                {
                    return Json(new ServiceResponse { status = 300, message = "Terjadi Kesalahan saat Melakukan Save Data" });
                }
                
            }
            return Json(new ServiceResponse { status = 200, message = "Feedback Berhasil Tersimpan" });
        }

        [HttpPost]
        public ActionResult CheckDosenAnswer(string dosenID, int jadwalID)
        {
            var email = HttpContext.Session["email"].ToString();
            var mahasiswaID = _mahasiswaService.Find(x => x.Email == email).First().ID;
            var data = _feedbackMatkulService.Find(x => x.DosenID == dosenID && x.MahasiswaID == mahasiswaID && x.JadwalKuliahID == jadwalID).Count();
            if (data != 0)
            {
                return Json(new ServiceResponse { status = 300, message = "Anda Sudah Melakukan Feedback Untuk Dosen Ini" });
            }
            else
            {
                return Json(new ServiceResponse { status = 200, message = "Anda Sudah Melakukan Feedback Untuk Dosen Ini" });
            }
        }

        [HttpPost]
        public ActionResult NextPageFeedBack(string listDosen, int urutan, int idJadwalKuliah)
        {
            var nextPage = urutan + 1;

            var email = HttpContext.Session["email"].ToString();
            var jenjang = _mahasiswaService.Find(x => x.Email == email).First().JenjangStudi;
            var dataSemester = _mahasiswaService.GetDataSemester(jenjang).First().ID;

            IEnumerable<VMPertanyaanFeedback> Pertanyaan = _feedbackMatkulService.GetPertanyaanFeedbacks(jenjang, "2010");
            var DescSemester = _feedbackMatkulService.GetSemesterByStrm("2110");
            ViewData["semester"] = DescSemester.Nama;
            ViewData["pertanyaan"] = Pertanyaan;
            ViewData["jadwalID"] = idJadwalKuliah;

            IEnumerable<VMJawabanFeedback> Jawaban = _feedbackMatkulService.GetJawabanFeedback(Pertanyaan.First().KodeJawaban);
            ViewData["jawaban"] = Jawaban;

            var data1 = _jadwalKuliahService.Get(idJadwalKuliah);
            var listDosen1 = _feedbackMatkulService.GetDosenMakulPertemuans(data1.KodeMataKuliah, data1.ClassSection, data1.STRM.ToString(), data1.FakultasID.ToString());
            var dataDosen = listDosen.Split(',');
            var idDosenTerpilih = dataDosen[nextPage];
            var dosenTerpilih = listDosen1.Where(x => x.Instructor_id == idDosenTerpilih).First();
            var dosenTmp = dosenTerpilih.NamaDosen.Split('-');
            ViewData["namaDosen"] = dosenTmp[1];
            ViewData["idDosen"] = dosenTerpilih.Instructor_id;
            ViewData["urutan"] = nextPage;
            ViewData["listDosen"] = listDosen;
            return View(data1);
        }

        [HttpPost]
        public ActionResult PreviousPageFeedback(string listDosen, int urutan, int idJadwalKuliah)
        {
            var nextPage = urutan - 1;

            var email = HttpContext.Session["email"].ToString();
            var jenjang = _mahasiswaService.Find(x => x.Email == email).First().JenjangStudi;
            var dataSemester = _mahasiswaService.GetDataSemester(jenjang).First().ID;

            IEnumerable<VMPertanyaanFeedback> Pertanyaan = _feedbackMatkulService.GetPertanyaanFeedbacks(jenjang, "2010");
            var DescSemester = _feedbackMatkulService.GetSemesterByStrm("2110");
            ViewData["semester"] = DescSemester.Nama;
            ViewData["pertanyaan"] = Pertanyaan;
            ViewData["jadwalID"] = idJadwalKuliah;

            IEnumerable<VMJawabanFeedback> Jawaban = _feedbackMatkulService.GetJawabanFeedback(Pertanyaan.First().KodeJawaban);
            ViewData["jawaban"] = Jawaban;

            var data1 = _jadwalKuliahService.Get(idJadwalKuliah);
            var listDosen1 = _feedbackMatkulService.GetDosenMakulPertemuans(data1.KodeMataKuliah, data1.ClassSection, data1.STRM.ToString(), data1.FakultasID.ToString());
            var dataDosen = listDosen.Split(',');
            var idDosenTerpilih = dataDosen[nextPage];
            var dosenTerpilih = listDosen1.Where(x => x.Instructor_id == idDosenTerpilih).First();

            var dosenTmp = dosenTerpilih.NamaDosen.Split('-');
            ViewData["namaDosen"] = dosenTmp[1];
            ViewData["idDosen"] = dosenTerpilih.Instructor_id;
            ViewData["urutan"] = nextPage;
            ViewData["listDosen"] = listDosen;
            return View(data1);
        }

    }
}