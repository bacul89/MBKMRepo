using MBKM.Entities.ViewModel;
using MBKM.Services.MBKMServices;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using MBKM.Presentation.Helper;
using System.Web;
using System.Web.Mvc;
using Rotativa;
using MBKM.Entities.Models.MBKM;
using OfficeOpenXml.Drawing;

namespace MBKM.Presentation.Areas.Admin.Controllers
{
    [MBKMAuthorize]
    public class ReportMBKMInternalPertukaranController : Controller
    {
        private IPendaftaranMataKuliahService _pendaftaranMataKuliahService;
        private IMahasiswaService _mahasiswaService;
        private IInformasiPertukaranService _informasiPertukaranService;
        private IFeedbackMatkulService _feedbackMatkulService;
        private IJadwalKuliahService _jadwalKuliahService;
        private IJadwalUjianMBKMService _jadwalUjianMBKMService;
        private INilaiKuliahService _nilaiKuliahService;

        public ReportMBKMInternalPertukaranController(IPendaftaranMataKuliahService pendaftaranMataKuliahService, IMahasiswaService mahasiswaService, IInformasiPertukaranService informasiPertukaranService, IFeedbackMatkulService feedbackMatkulService, IJadwalKuliahService jadwalKuliahService, IJadwalUjianMBKMService jadwalUjianMBKMService, INilaiKuliahService nilaiKuliahService)
        {
            _pendaftaranMataKuliahService = pendaftaranMataKuliahService;
            _mahasiswaService = mahasiswaService;
            _informasiPertukaranService = informasiPertukaranService;
            _feedbackMatkulService = feedbackMatkulService;
            _jadwalKuliahService = jadwalKuliahService;
            _jadwalUjianMBKMService = jadwalUjianMBKMService;
            _nilaiKuliahService = nilaiKuliahService;
        }



        // GET: Admin/ReportMBKMInternalPertukaran
        public ActionResult Index()
        {
            IEnumerable<VMSemester> data = _jadwalUjianMBKMService.getAllSemester();
            ViewData["firstSemester"] = _mahasiswaService.GetDataSemester(null).First().Nilai;
            ViewData["semester"] = data;
            return View();
        }

        public ActionResult DataTable(int strm)
        {

            var dataMahasiswa = _pendaftaranMataKuliahService.GetListPendaftaranInternalPertukaran(strm);
            var dataSemester = _feedbackMatkulService.GetSemesterByStrm(strm.ToString());

            List<String[]> final = new List<String[]>();
            foreach (var d in dataMahasiswa)
            {

                var checkNilaiDiakui = new VMNilaiDiakui();
                var nilaiDiakui = " ";
                var nilaiBobotDiakui = new VMNilaiGrade();
               
                checkNilaiDiakui = _nilaiKuliahService.GetNilaiDiakui(d.JadwalKuliahs.JenjangStudi, d.JadwalKuliahs.STRM.ToString(), d.JadwalKuliahs.MataKuliahID, d.JadwalKuliahs.KodeMataKuliah, d.mahasiswas.NIM.ToString(), d.JadwalKuliahs.ClassSection);
                if (checkNilaiDiakui == null)
                {
                    nilaiDiakui = "-";
                    nilaiBobotDiakui.GRADE_POINTS = "-";
                }
                else
                {
                    nilaiDiakui = checkNilaiDiakui.NilaiDiakui;
                    nilaiBobotDiakui.GRADE_POINTS = checkNilaiDiakui.BobotDiakui;
                }
                
                final.Add(new String[]{
                    dataSemester.Nama,
                    d.JadwalKuliahs.JenjangStudi,
                    d.mahasiswas.NIM,
                    d.mahasiswas.Nama,
                    d.JadwalKuliahs.NamaProdi,
                    d.JadwalKuliahs.KodeMataKuliah,
                    d.JadwalKuliahs.NamaMataKuliah,
                    d.JadwalKuliahs.SKS,
                    nilaiDiakui,
                    d.MatkulKodeAsal,
                    d.MatkulAsal,
                    nilaiDiakui,
                    nilaiBobotDiakui.GRADE_POINTS
                });

            }
            return Json(final);
        }

        public ActionResult GetFilePdf(int id)
        {
            
            var dataMahasiswa = _pendaftaranMataKuliahService.GetListPendaftaranInternalPertukaran(id);
            var dataSemester = _feedbackMatkulService.GetSemesterByStrm(id.ToString());

            List<String[]> final = new List<String[]>();
            foreach (var d in dataMahasiswa)
            {
                var checkNilaiDiakui = new VMNilaiDiakui();
                var nilaiDiakui = " ";
                var nilaiBobotDiakui = new VMNilaiGrade();
                
                checkNilaiDiakui = _nilaiKuliahService.GetNilaiDiakui(d.JadwalKuliahs.JenjangStudi, d.JadwalKuliahs.STRM.ToString(), d.JadwalKuliahs.MataKuliahID, d.JadwalKuliahs.KodeMataKuliah, d.mahasiswas.NIM.ToString(), d.JadwalKuliahs.ClassSection);
                if (checkNilaiDiakui == null)
                {
                    nilaiDiakui = "-";
                    nilaiBobotDiakui.GRADE_POINTS = "-";
                }
                else
                {
                    nilaiDiakui = checkNilaiDiakui.NilaiDiakui;
                    nilaiBobotDiakui.GRADE_POINTS = checkNilaiDiakui.BobotDiakui;
                }
                
                final.Add(new String[]{
                    dataSemester.Nama,
                    d.JadwalKuliahs.JenjangStudi,
                    d.mahasiswas.NIM,
                    d.mahasiswas.Nama,
                    d.JadwalKuliahs.NamaProdi,
                    d.JadwalKuliahs.KodeMataKuliah,
                    d.JadwalKuliahs.NamaMataKuliah,
                    d.JadwalKuliahs.SKS,
                    nilaiDiakui,
                    d.MatkulKodeAsal,
                    d.MatkulAsal,
                    nilaiDiakui,
                    nilaiBobotDiakui.GRADE_POINTS
                });
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

            ViewData["datas"] = final;
            return new ViewAsPdf("GetFilePdf");
        }


        public ActionResult GetFileExcel(int id)
        {
            var result = new List<VMReportMahasiswaInternal>();
            result = _pendaftaranMataKuliahService.GetListPendaftaranInternalPertukaran(id).ToList();
            IEnumerable<VMReportMahasiswaInternal> dataMahasiswa = _pendaftaranMataKuliahService.GetListPendaftaranInternalPertukaran(id);
            var dataSemester = _feedbackMatkulService.GetSemesterByStrm(id.ToString());            

            ExcelPackage package = new ExcelPackage();
            var ws = package.Workbook.Worksheets.Add("REPORT MAHASISWA INTERNAL PERTUKARAN MBKM");

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
            ws.Cells["C2"].Value = "FORMULIR \nMATRIKS REPORT MAHASISWA INTERNAL PERTUKARAN MBKM";

            ws.Cells["A6:N6"].Style.Font.Bold = true;
            ws.Cells["A6:N6"].Style.Border.Top.Style = ExcelBorderStyle.Thick;
            ws.Cells["A6:N6"].Style.Border.Bottom.Style = ExcelBorderStyle.Thick;
            ws.Cells["A6:N6"].Style.Border.Left.Style = ExcelBorderStyle.Thick;
            ws.Cells["A6:N6"].Style.Border.Right.Style = ExcelBorderStyle.Thick;
            ws.Cells["A6:N6"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            ws.Cells["A3:I4"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
            ws.Cells["A3:I4"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

            ws.Cells["A6:A7"].Merge = true;
            ws.Cells["A6"].Value = "No.";
            ws.Cells["B6:B7"].Merge = true;
            ws.Cells["B6"].Value = "Tahun Semester";
            ws.Cells["C6:C7"].Merge = true;
            ws.Cells["C6"].Value = "Jenjang";
            ws.Cells["D6:D7"].Merge = true;
            ws.Cells["D6"].Value = "NIM";
            ws.Cells["E6:E7"].Merge = true;
            ws.Cells["E6"].Value = "Nama Mahasiswa"; 
            ws.Cells["F6:F7"].Merge = true;
            ws.Cells["F6"].Value = "Nama Prodi";
            ws.Cells["G6:N6"].Merge = true;
            ws.Cells["G6"].Value = "Nilai Transfer";
            ws.Cells["G7"].Value = "Kode Mata Kuliah Asal";
            ws.Cells["H7"].Value = "Nama Mata Kuliah Asal";
            ws.Cells["I7"].Value = "SKS";
            ws.Cells["J7"].Value = "Nilai Huruf Asal";
            ws.Cells["K7"].Value = "Kode Mata Kuliah Diakui";
            ws.Cells["L7"].Value = "Nama Mata Kuliah Diakui";
            ws.Cells["M7"].Value = "Nilai Huruf Matakuliah Diakui";
            ws.Cells["N7"].Value = "Bobot Huruf";
            ws.Cells["A6:N7"].AutoFitColumns();
            var IndexTotal = 0;

            for (int i = 0; i < result.Count; i++)
            {

                IndexTotal = IndexTotal + 1;
                var tmp = result[i];
                var checkNilai2 = new VMNilaiDiakui();
                var nilaiDiakui = " ";
                var nilaiBobotDiakui = new VMNilaiGrade();
                
                checkNilai2 = _nilaiKuliahService.GetNilaiDiakui(tmp.JadwalKuliahs.JenjangStudi, tmp.JadwalKuliahs.STRM.ToString(), tmp.JadwalKuliahs.MataKuliahID, tmp.JadwalKuliahs.KodeMataKuliah, tmp.mahasiswas.NIM.ToString(), tmp.JadwalKuliahs.ClassSection);
                if (checkNilai2 == null)
                {
                    nilaiDiakui = "-";
                    nilaiBobotDiakui.GRADE_POINTS = "-";
                }
                else
                {
                    nilaiDiakui = checkNilai2.NilaiDiakui;
                    nilaiBobotDiakui.GRADE_POINTS = checkNilai2.BobotDiakui;
                }
                


                ws.Cells["A" + (i + 8)].Value = (i + 1);
                ws.Cells["B" + (i + 8)].Value = dataSemester.Nama;
                ws.Cells["C" + (i + 8)].Value = tmp.JadwalKuliahs.JenjangStudi;
                ws.Cells["D" + (i + 8)].Value = tmp.mahasiswas.NIM;
                ws.Cells["E" + (i + 8)].Value = tmp.mahasiswas.Nama;
                ws.Cells["F" + (i + 8)].Value = tmp.JadwalKuliahs.NamaProdi;
                ws.Cells["G" + (i + 8)].Value = tmp.JadwalKuliahs.KodeMataKuliah;
                ws.Cells["H" + (i + 8)].Value = tmp.JadwalKuliahs.NamaMataKuliah;
                ws.Cells["I" + (i + 8)].Value = tmp.JadwalKuliahs.SKS;

                ws.Cells["J" + (i + 8)].Value = nilaiDiakui;
                ws.Cells["K" + (i + 8)].Value = tmp.MatkulKodeAsal;
                ws.Cells["L" + (i + 8)].Value = tmp.MatkulAsal;
                ws.Cells["M" + (i + 8)].Value = nilaiDiakui;
                ws.Cells["N" + (i + 8)].Value = nilaiBobotDiakui.GRADE_POINTS;
                
                
                ws.Cells[ws.Dimension.Address].AutoFitColumns();
            }

            ws.Cells["A6:N7"].Style.Border.Top.Style = ExcelBorderStyle.Hair;
            ws.Cells["A6:N7"].Style.Border.Right.Style = ExcelBorderStyle.Hair;
            ws.Cells["A6:N7"].Style.Border.Bottom.Style = ExcelBorderStyle.Hair;
            ws.Cells["A6:N7"].Style.Border.Left.Style = ExcelBorderStyle.Hair;

            ws.Cells["A:N"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            ws.Cells["A6:N7"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            ws.Cells["A6:N7"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
            ws.Cells["A6:N7"].Style.Fill.PatternType = ExcelFillStyle.Solid;
            ws.Cells["A6:N7"].Style.Fill.BackgroundColor.SetColor(Color.LightGray);

            var semesterBerjalan = _mahasiswaService.GetDataSemester(null).First().Nama;
            var fileDownloadName = semesterBerjalan + " - Report Nilai Mahasiswa Internal Pertukaran MBKM.xlsx";
            var contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            var fileStream = new MemoryStream();
            package.SaveAs(fileStream);
            fileStream.Position = 0;

            var fsr = new FileStreamResult(fileStream, contentType);
            fsr.FileDownloadName = fileDownloadName;

            return fsr;
        }
    }
}