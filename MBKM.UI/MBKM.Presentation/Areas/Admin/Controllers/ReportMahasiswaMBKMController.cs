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
using OfficeOpenXml.Drawing;

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

            var semesterSekarang = dataSemester.Nama.Split(' ');
            ViewData["TahunSemester"] = semesterSekarang.Last();
            var strm = id.ToString().Substring(id.ToString().Length - 2); ;
            if (strm == "10")
            {
                ViewData["jenisSemester"] = "Ganjil";
            }
            else if (strm == "20")
            {
                ViewData["jenisSemester"] = "Genap";
            }
            else if (strm == "30")
            {
                ViewData["jenisSemester"] = "Pendek";
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

            double columnWidth = 2;
            ws.Column(1).Width = columnWidth;
            ws.Column(8).Width = columnWidth;
            int RowIndex = 0;
            int ColIndex = 0;

            //Image.FromFile(@"D:\sample.png");
            /*int Width = 320;
            int Height = 200;*/

            var dir = Server.MapPath("~/Asset");
            var path = Path.Combine(dir, "Lambang_Unika_Atma_Jaya.png"); //validate the path for security or use other means to generate the path.
            Image imageView = Image.FromFile(path);

            ExcelPicture pic = ws.Drawings.AddPicture("Logo", imageView);
            pic.SetPosition(RowIndex, 2, ColIndex, 15);
            pic.SetSize(180, 70);


            //ws.Column().AddPicture("Picture_Name", img);


            ws.Cells["A1:B2"].Merge = true;
            ws.Cells["C1:I1"].Merge = true;
            ws.Cells["C2:I2"].Merge = true;
            ws.Cells["C3:I4"].Merge = true;
            //ws.Cells["Q1:Q2"].Merge = true;

            ws.Row(1).Height = 15;
            ws.Row(2).Height = 26;
            ws.Column(1).Width = 17;

            ws.Cells["C2:I3"].Style.Font.Bold = true;
            ws.Cells["C2:I3"].Style.Font.Size = 15;
            /*ws.Cells["B2:I3"].Style.WrapText = true;*/
            ws.Cells["C2:I3"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
            /*ws.Cells["C2:I2"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;*/
            /* ws.Cells["A3:I4"].Style.Font.Bold = true;*/

            /*            ws.Cells["C1"].Value = "UNIVERSITAS KATOLIK INDONESIA";
                        ws.Cells["C2"].Value = "ATMA JAYA";*/
            ws.Cells["C2"].Value = "FORMULIR \nMATRIKS REPORT MAHASISWA MBKM";

            ws.Cells["A6:J6"].Style.Font.Bold = true;
            ws.Cells["A6:J6"].Style.Border.Top.Style = ExcelBorderStyle.Thick;
            ws.Cells["A6:J6"].Style.Border.Bottom.Style = ExcelBorderStyle.Thick;
            ws.Cells["A6:J6"].Style.Border.Left.Style = ExcelBorderStyle.Thick;
            ws.Cells["A6:J6"].Style.Border.Right.Style = ExcelBorderStyle.Thick;
            ws.Cells["A6:J6"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            ws.Cells["A3:I4"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
            ws.Cells["A3:I4"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;


            ws.Cells["A6"].Value = "No.";
            ws.Cells["B6"].Value = "Tahun Semester";
            ws.Cells["C6"].Value = "Jenjang";
            ws.Cells["D6"].Value = "Fakultas";
            ws.Cells["E6"].Value = "Program Studi";
            ws.Cells["F6"].Value = "Lokasi";
            ws.Cells["G6"].Value = "MBKM Pertukaran";
            ws.Cells["H6"].Value = "MBKM  Pertukaran Ke Luar";
            ws.Cells["I6"].Value = "MBKM Eksternal";
            ws.Cells["J6"].Value = "MBKM Non Pertukaran";

            var IndexTotal = 0;

            for (int i = 0; i < final.Count; i++)
            {
                IndexTotal = IndexTotal + 1;
                var tmp = final[i];
                ws.Cells["A:J"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                ws.Cells["A6:J6"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                ws.Cells["A6:J6"].Style.Fill.BackgroundColor.SetColor(Color.LightGray);

                ws.Cells["A" + (i + 7)].Value = (i + 1);
                ws.Cells["B" + (i + 7)].Value = tmp[0];
                ws.Cells["C" + (i + 7)].Value = tmp[1];
                ws.Cells["D" + (i + 7)].Value = tmp[2];
                ws.Cells["E" + (i + 7)].Value = tmp[3];
                ws.Cells["F" + (i + 7)].Value = tmp[4];
                ws.Cells["G" + (i + 7)].Value = tmp[5];
                ws.Cells["H" + (i + 7)].Value = tmp[6];
                ws.Cells["I" + (i + 7)].Value = tmp[7];
                ws.Cells["J" + (i + 7)].Value = tmp[8];
            }

            ws.Cells["A" + (IndexTotal + 7) + ":F" + (IndexTotal + 7)].Merge = true;
            ws.Cells["A" + (IndexTotal + 7)].Value = "TOTAL";
            ws.Cells["G" + (IndexTotal + 7)].Value = totalInternalPertukaran;
            ws.Cells["H" + (IndexTotal + 7)].Value = totalInternalKeluar;
            ws.Cells["I" + (IndexTotal + 7)].Value = totalEksternal;
            ws.Cells["J" + (IndexTotal + 7)].Value = totalInternalNonPertukaran;

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