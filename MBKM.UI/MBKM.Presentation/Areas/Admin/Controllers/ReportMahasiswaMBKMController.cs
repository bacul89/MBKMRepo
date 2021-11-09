using MBKM.Entities.ViewModel;
using MBKM.Services.MBKMServices;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using Rotativa;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using MBKM.Presentation.Helper;
using System.Web;
using System.Web.Mvc;

namespace MBKM.Presentation.Areas.Admin.Controllers
{
    [MBKMAuthorize]
    public class ReportMahasiswaMBKMController : Controller
    {
        private IPendaftaranMataKuliahService _pendaftaranMataKuliahService;
        private IMahasiswaService _mahasiswaService;
        private IInformasiPertukaranService _informasiPertukaranService;
        private IFeedbackMatkulService _feedbackMatkulService;
        private IJadwalKuliahService _jadwalKuliahService;
        private IJadwalUjianMBKMService _jadwalUjianMBKMService;

        public ReportMahasiswaMBKMController(IPendaftaranMataKuliahService pendaftaranMataKuliahService, IMahasiswaService mahasiswaService, IInformasiPertukaranService informasiPertukaranService, IFeedbackMatkulService feedbackMatkulService, IJadwalKuliahService jadwalKuliahService, IJadwalUjianMBKMService jadwalUjianMBKMService)
        {
            _pendaftaranMataKuliahService = pendaftaranMataKuliahService;
            _mahasiswaService = mahasiswaService;
            _informasiPertukaranService = informasiPertukaranService;
            _feedbackMatkulService = feedbackMatkulService;
            _jadwalKuliahService = jadwalKuliahService;
            _jadwalUjianMBKMService = jadwalUjianMBKMService;
        }



        // GET: Admin/ReportMahasiswaMBKM
        public ActionResult Index()
        {
            IEnumerable<VMSemester> data = _jadwalUjianMBKMService.getAllSemester();
            ViewData["semester"] = data;
            ViewData["firstSemester"] = _mahasiswaService.GetDataSemester(null).First().Nilai;
            return View();
        }

        [HttpPost]
        public ActionResult TableData(int strm)
        {
            var dataProdi =  _mahasiswaService.GetAllDataProdi();
            var dataMahasiswa = _pendaftaranMataKuliahService.GetListPendaftaranAndInformasiPertukaran(strm);
            var dataSemester = _feedbackMatkulService.GetSemesterByStrm(strm.ToString());


            List<String[]> final = new List<String[]>();
           
            foreach (var d in dataProdi)
            {
                int prodiId = int.Parse(d.IDProdi);
                var datapertama = _jadwalKuliahService.Find(x => x.ProdiID == prodiId).FirstOrDefault();
                if (datapertama != null)
                {
                    var countEksternal = dataMahasiswa.Where(x => x.JadwalKuliahs.ProdiID == int.Parse(d.IDProdi) && x.mahasiswas.NIM != x.mahasiswas.NIMAsal).Count();
                    var internalLintasProdi = dataMahasiswa.Where(x => x.JadwalKuliahs.ProdiID == int.Parse(d.IDProdi) && x.InformasiPertukaran.JenisKerjasama.ToLower() == "internal" && !x.InformasiPertukaran.JenisPertukaran.ToLower().Contains("non")).Count();
                    var internalKeLuar = dataMahasiswa.Where(x => x.JadwalKuliahs.ProdiID == int.Parse(d.IDProdi) && x.InformasiPertukaran.JenisKerjasama.ToLower().Contains("luar") && !x.InformasiPertukaran.JenisPertukaran.ToLower().Contains("non")).Count();
                    var internalNonPertukaran = dataMahasiswa.Where(x => x.JadwalKuliahs.ProdiID == int.Parse(d.IDProdi) && x.InformasiPertukaran.JenisPertukaran.ToLower().Contains("non")).Count();
                    
                    final.Add(new String[]{
                        dataSemester.Nama,
                        datapertama.JenjangStudi,
                        datapertama.NamaFakultas,
                        datapertama.NamaProdi,
                        datapertama.Lokasi,
                        internalLintasProdi.ToString(),
                        internalKeLuar.ToString(),
                        countEksternal.ToString(),
                        internalNonPertukaran.ToString()
                    });
                }
            }
            return Json(final);
        }

        public ActionResult getDataPdf(int id)
        {
            var dataProdi = _mahasiswaService.GetAllDataProdi();
            var dataMahasiswa = _pendaftaranMataKuliahService.GetListPendaftaranAndInformasiPertukaran(id);
            var dataSemester = _feedbackMatkulService.GetSemesterByStrm(id.ToString());

            List<String[]> final = new List<String[]>();

            var totalEksternal = 0;
            var totalInternalPertukaran = 0;
            var totalInternalKeluar = 0;
            var totalInternalNonPertukaran = 0;

            foreach (var d in dataProdi)
            {
                int prodiId = int.Parse(d.IDProdi);
                var datapertama = _jadwalKuliahService.Find(x => x.ProdiID == prodiId).FirstOrDefault();
                if (datapertama != null)
                {
                    var countEksternal = dataMahasiswa.Where(x => x.JadwalKuliahs.ProdiID == int.Parse(d.IDProdi) && x.mahasiswas.NIM != x.mahasiswas.NIMAsal).Count();
                    var internalLintasProdi = dataMahasiswa.Where(x => x.JadwalKuliahs.ProdiID == int.Parse(d.IDProdi) && x.InformasiPertukaran.JenisKerjasama.ToLower() == "internal" && !x.InformasiPertukaran.JenisPertukaran.ToLower().Contains("non")).Count();
                    var internalKeLuar = dataMahasiswa.Where(x => x.JadwalKuliahs.ProdiID == int.Parse(d.IDProdi) && x.InformasiPertukaran.JenisKerjasama.ToLower().Contains("luar") && !x.InformasiPertukaran.JenisPertukaran.ToLower().Contains("non")).Count();
                    var internalNonPertukaran = dataMahasiswa.Where(x => x.JadwalKuliahs.ProdiID == int.Parse(d.IDProdi) && x.InformasiPertukaran.JenisPertukaran.ToLower().Contains("non")).Count();

                    final.Add(new String[]{
                        dataSemester.Nama,
                        datapertama.JenjangStudi,
                        datapertama.NamaFakultas,
                        datapertama.NamaProdi,
                        datapertama.Lokasi,
                        internalLintasProdi.ToString(),
                        internalKeLuar.ToString(),
                        countEksternal.ToString(),
                        internalNonPertukaran.ToString()
                    });

                    totalEksternal = totalEksternal + countEksternal;
                    totalInternalPertukaran = totalInternalPertukaran + internalLintasProdi;
                    totalInternalKeluar = totalInternalKeluar + internalKeLuar;
                    totalInternalNonPertukaran = totalInternalNonPertukaran + internalNonPertukaran;

                }
            }

            ViewData["eksternal"] = totalEksternal;
            ViewData["internal"] = totalInternalPertukaran;
            ViewData["internalKeluar"] = totalInternalKeluar;
            ViewData["nonpertukaran"] = totalInternalNonPertukaran;
            ViewData["datas"] = final;
            return /*View();*/
                 new ViewAsPdf("getDataPdf");
        }



        public ActionResult GetFileExcel(int id)
        {
            var fileDownloadName = "Report Nilai Mahasiswa MBKM.xlsx";
            var contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

            var dataProdi = _mahasiswaService.GetAllDataProdi();
            var dataMahasiswa = _pendaftaranMataKuliahService.GetListPendaftaranAndInformasiPertukaran(id);
            var dataSemester = _feedbackMatkulService.GetSemesterByStrm(id.ToString());

            List<String[]> final = new List<String[]>();

            var totalEksternal = 0;
            var totalInternalPertukaran = 0;
            var totalInternalKeluar = 0;
            var totalInternalNonPertukaran = 0;

            foreach (var d in dataProdi)
            {
                int prodiId = int.Parse(d.IDProdi);
                var datapertama = _jadwalKuliahService.Find(x => x.ProdiID == prodiId).FirstOrDefault();
                if (datapertama != null)
                {
                    var countEksternal = dataMahasiswa.Where(x => x.JadwalKuliahs.ProdiID == int.Parse(d.IDProdi) && x.mahasiswas.NIM != x.mahasiswas.NIMAsal).Count();
                    var internalLintasProdi = dataMahasiswa.Where(x => x.JadwalKuliahs.ProdiID == int.Parse(d.IDProdi) && x.InformasiPertukaran.JenisKerjasama.ToLower() == "internal" && !x.InformasiPertukaran.JenisPertukaran.ToLower().Contains("non")).Count();
                    var internalKeLuar = dataMahasiswa.Where(x => x.JadwalKuliahs.ProdiID == int.Parse(d.IDProdi) && x.InformasiPertukaran.JenisKerjasama.ToLower().Contains("luar") && !x.InformasiPertukaran.JenisPertukaran.ToLower().Contains("non")).Count();
                    var internalNonPertukaran = dataMahasiswa.Where(x => x.JadwalKuliahs.ProdiID == int.Parse(d.IDProdi) && x.InformasiPertukaran.JenisPertukaran.ToLower().Contains("non")).Count();

                    final.Add(new String[]{
                        dataSemester.Nama,
                        datapertama.JenjangStudi,
                        datapertama.NamaFakultas,
                        datapertama.NamaProdi,
                        datapertama.Lokasi,
                        internalLintasProdi.ToString(),
                        internalKeLuar.ToString(),
                        countEksternal.ToString(),
                        internalNonPertukaran.ToString()
                    });

                    totalEksternal = totalEksternal + countEksternal;
                    totalInternalPertukaran = totalInternalPertukaran + internalLintasProdi;
                    totalInternalKeluar = totalInternalKeluar + internalKeLuar;
                    totalInternalNonPertukaran = totalInternalNonPertukaran + internalNonPertukaran;

                }
            }
            ExcelPackage package = new ExcelPackage();

            var ws = package.Workbook.Worksheets.Add("REPORT MAHASISWA MBKM");
            ws.Cells["A1"].Value = "No.";
            ws.Cells["B1"].Value = "Tahun Semester";
            ws.Cells["C1"].Value = "Jenjang";
            ws.Cells["D1"].Value = "Fakultas";
            ws.Cells["E1"].Value = "Program Studi";
            ws.Cells["F1"].Value = "Lokasi";
            ws.Cells["G1"].Value = "MBKM Pertukaran";
            ws.Cells["H1"].Value = "MBKM  Pertukaran Ke Luar";
            ws.Cells["I1"].Value = "MBKM Eksternal";
            ws.Cells["J1"].Value = "MBKM Non Pertukaran";

            var IndexTotal = 0;

            for (int i = 0; i < final.Count; i++)
            {
                IndexTotal = IndexTotal + 1;
                var tmp = final[i];
                ws.Cells["A:J"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                ws.Cells["A1:J1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                ws.Cells["A1:J1"].Style.Fill.BackgroundColor.SetColor(Color.LightGray);

                ws.Cells["A" + (i + 2)].Value = (i + 1);
                ws.Cells["B" + (i + 2)].Value = tmp[0];
                ws.Cells["C" + (i + 2)].Value = tmp[1];
                ws.Cells["D" + (i + 2)].Value = tmp[2];
                ws.Cells["E" + (i + 2)].Value = tmp[3];
                ws.Cells["F" + (i + 2)].Value = tmp[4];
                ws.Cells["G" + (i + 2)].Value = tmp[5];
                ws.Cells["H" + (i + 2)].Value = tmp[6];
                ws.Cells["I" + (i + 2)].Value = tmp[7];
                ws.Cells["J" + (i + 2)].Value = tmp[8];
            }

            ws.Cells["A" + (IndexTotal + 2) + ":F" + (IndexTotal + 2)].Merge = true;
            ws.Cells["A" + (IndexTotal + 2)].Value = "TOTAL";
            ws.Cells["G" + (IndexTotal + 2)].Value = totalInternalPertukaran;
            ws.Cells["H" + (IndexTotal + 2)].Value = totalInternalKeluar;
            ws.Cells["I" + (IndexTotal + 2)].Value = totalEksternal;
            ws.Cells["J" + (IndexTotal + 2)].Value = totalInternalNonPertukaran;

            ws.Cells[ws.Dimension.Address].AutoFitColumns();

            var fileStream = new MemoryStream();
            package.SaveAs(fileStream);
            fileStream.Position = 0;

            var fsr = new FileStreamResult(fileStream, contentType);
            fsr.FileDownloadName = fileDownloadName;

            return fsr;
        }
    }
}