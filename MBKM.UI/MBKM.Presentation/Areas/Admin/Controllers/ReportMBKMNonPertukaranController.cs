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
using System.Web;
using MBKM.Presentation.Helper;
using System.Web.Mvc;
using OfficeOpenXml.Drawing;

namespace MBKM.Presentation.Areas.Admin.Controllers
{
    [MBKMAuthorize]
    public class ReportMBKMNonPertukaranController : Controller
    {
        private IPendaftaranMataKuliahService _pendaftaranMataKuliahService;
        private IMahasiswaService _mahasiswaService;
        private IInformasiPertukaranService _informasiPertukaranService;
        private IFeedbackMatkulService _feedbackMatkulService;
        private IJadwalKuliahService _jadwalKuliahService;
        private IJadwalUjianMBKMService _jadwalUjianMBKMService;

        public ReportMBKMNonPertukaranController(IPendaftaranMataKuliahService pendaftaranMataKuliahService, IMahasiswaService mahasiswaService, IInformasiPertukaranService informasiPertukaranService, IFeedbackMatkulService feedbackMatkulService, IJadwalKuliahService jadwalKuliahService, IJadwalUjianMBKMService jadwalUjianMBKMService)
        {
            _pendaftaranMataKuliahService = pendaftaranMataKuliahService;
            _mahasiswaService = mahasiswaService;
            _informasiPertukaranService = informasiPertukaranService;
            _feedbackMatkulService = feedbackMatkulService;
            _jadwalKuliahService = jadwalKuliahService;
            _jadwalUjianMBKMService = jadwalUjianMBKMService;
        }


        // GET: Admin/ReportMBKMNonPertukaran
        public ActionResult Index()
        {
            IEnumerable<VMSemester> data = _jadwalUjianMBKMService.getAllSemester();
            ViewData["firstSemester"] = _mahasiswaService.GetDataSemester(null).First().Nilai;
            ViewData["semester"] = data;
            return View();
        }

        public ActionResult DataTable(int strm)
        {
            var dataMahasiswa = _pendaftaranMataKuliahService.GetListPendaftaranNonPertukaran(strm);
            var dataSemester = _feedbackMatkulService.GetSemesterByStrm(strm.ToString());
            
            List<String[]> final = new List<String[]>();
            foreach(var d in dataMahasiswa)
            {
                var tglFinal = "";

                if (d.InformasiPertukaran.TanggalSK.ToString() != null)
                {
                    var tgl = d.InformasiPertukaran.TanggalSK.ToString().Split(' ');
                    tglFinal = tgl[0];
                }
                else
                {
                    tglFinal = "-";
                }

                final.Add(new String[]{
                    dataSemester.Nama,
                    d.JadwalKuliahs.JenjangStudi,
                    d.mahasiswas.NIM,
                    d.mahasiswas.Nama,
                    d.JadwalKuliahs.NamaProdi,
                    d.InformasiPertukaran.JenisKerjasama,
                    d.InformasiPertukaran.JudulAktivitas,
                    d.InformasiPertukaran.LokasiTugas,
                    d.InformasiPertukaran.NoSK,
                    tglFinal,
                    d.JadwalKuliahs.KodeMataKuliah,
                    d.JadwalKuliahs.NamaMataKuliah,
                    d.JadwalKuliahs.SKS,
                    d.NilaiKuliah.NilaiTotal.ToString(),
                    d.NilaiKuliah.Grade
                });
            }

            return Json(final);
        }


        public ActionResult GetDataPDF(int id)
        {
            var dataMahasiswa = _pendaftaranMataKuliahService.GetListPendaftaranNonPertukaran(id);
            var dataSemester = _feedbackMatkulService.GetSemesterByStrm(id.ToString());

            List<String[]> final = new List<String[]>();
            foreach (var d in dataMahasiswa)
            {
                var tglFinal = "";

                if (d.InformasiPertukaran.TanggalSK.ToString() != null)
                {
                    var tgl = d.InformasiPertukaran.TanggalSK.ToString().Split(' ');
                    tglFinal = tgl[0];
                }
                else
                {
                    tglFinal = "-";
                }

                final.Add(new String[]{
                    dataSemester.Nama,
                    d.JadwalKuliahs.JenjangStudi,
                    d.mahasiswas.NIM,
                    d.mahasiswas.Nama,
                    d.JadwalKuliahs.NamaProdi,
                    d.InformasiPertukaran.JenisKerjasama,
                    d.InformasiPertukaran.JudulAktivitas,
                    d.InformasiPertukaran.LokasiTugas,
                    d.InformasiPertukaran.NoSK,
                    tglFinal,
                    d.JadwalKuliahs.KodeMataKuliah,
                    d.JadwalKuliahs.NamaMataKuliah,
                    d.JadwalKuliahs.SKS,
                    d.NilaiKuliah.NilaiTotal.ToString(),
                    d.NilaiKuliah.Grade
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

            return /*View();*/
                new ViewAsPdf("getDataPdf")
                {
                    PageOrientation = Rotativa.Options.Orientation.Landscape,
                };
        }


        public ActionResult GetFileExcel(int id)
        {
            var result = new List<VMReportMahasiswaInternal>();
            result = _pendaftaranMataKuliahService.GetListPendaftaranNonPertukaran(id).ToList();
            var dataSemester = _feedbackMatkulService.GetSemesterByStrm(id.ToString());

            ExcelPackage package = new ExcelPackage();
            var ws = package.Workbook.Worksheets.Add("REPORT MAHASISWA NON-PERTUKARAN MBKM");


            double columnWidth = 2;
            ws.Column(1).Width = columnWidth;
            ws.Column(8).Width = columnWidth;
            int RowIndex = 0;
            int ColIndex = 0;

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
            ws.Cells["C2"].Value = "FORMULIR \nMATRIKS REPORT MAHASISWA NON-PERTUKARAN MBKM";

            ws.Cells["A6:P7"].Style.Font.Bold = true;
            ws.Cells["A6:P7"].Style.Border.Top.Style = ExcelBorderStyle.Thick;
            ws.Cells["A6:P7"].Style.Border.Bottom.Style = ExcelBorderStyle.Thick;
            ws.Cells["A6:P7"].Style.Border.Left.Style = ExcelBorderStyle.Thick;
            ws.Cells["A6:P7"].Style.Border.Right.Style = ExcelBorderStyle.Thick;
            ws.Cells["A6:P7"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            ws.Cells["A6:I4"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
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
            ws.Cells["F6"].Value = "Program Studi";
            ws.Cells["G6:G7"].Merge = true;
            ws.Cells["G6"].Value = "Jenis Kegiatan";
            ws.Cells["H6:H7"].Merge = true;
            ws.Cells["H6"].Value = "Judul Aktivitas";
            ws.Cells["I6:I7"].Merge = true;
            ws.Cells["I6"].Value = "Lokasi Aktivitas";
            ws.Cells["J6:J7"].Merge = true;
            ws.Cells["J6"].Value = "No. SK Tugas";
            ws.Cells["K6:K7"].Merge = true;
            ws.Cells["K6"].Value = "Tanggal SK Tugas";
            ws.Cells["L6:P6"].Merge = true;
            ws.Cells["L6"].Value = "Nilai Penyetaraan Kegiatan MBKM";
            ws.Cells["L7"].Value = "Kode Mata Kuliah";
            ws.Cells["M7"].Value = "Nama Mata Kuliah";
            ws.Cells["N7"].Value = "SKS";
            ws.Cells["O7"].Value = "Nilai Angka";
            ws.Cells["P7"].Value = "Nilai Huruf";
            ws.Cells["A6:P7"].AutoFitColumns();
            var IndexTotal = 0;

            for (int i = 0; i < result.Count; i++)
            {
                IndexTotal = IndexTotal + 1;
                var tmp = result[i];

                var tglFinal = "";

                if (tmp.InformasiPertukaran.TanggalSK.ToString() != null)
                {
                    var tgl = tmp.InformasiPertukaran.TanggalSK.ToString().Split(' ');
                    tglFinal = tgl[0];
                }
                else
                {
                    tglFinal = "-";
                }

                ws.Cells["A" + (i + 8)].Value = (i + 1);
                ws.Cells["B" + (i + 8)].Value = dataSemester.Nama;
                ws.Cells["C" + (i + 8)].Value = tmp.JadwalKuliahs.JenjangStudi;
                ws.Cells["D" + (i + 8)].Value = tmp.mahasiswas.NIM;
                ws.Cells["E" + (i + 8)].Value = tmp.mahasiswas.Nama;
                ws.Cells["F" + (i + 8)].Value = tmp.JadwalKuliahs.NamaProdi;
                ws.Cells["G" + (i + 8)].Value = tmp.InformasiPertukaran.JenisKerjasama;
                ws.Cells["H" + (i + 8)].Value = tmp.InformasiPertukaran.JudulAktivitas;
                ws.Cells["I" + (i + 8)].Value = tmp.InformasiPertukaran.LokasiTugas;
                ws.Cells["J" + (i + 8)].Value = tmp.InformasiPertukaran.NoSK;
                ws.Cells["K" + (i + 8)].Value = tglFinal;
                ws.Cells["L" + (i + 8)].Value = tmp.JadwalKuliahs.KodeMataKuliah;
                ws.Cells["M" + (i + 8)].Value = tmp.JadwalKuliahs.NamaMataKuliah;
                ws.Cells["N" + (i + 8)].Value = tmp.JadwalKuliahs.SKS;
                ws.Cells["O" + (i + 8)].Value = tmp.NilaiKuliah.NilaiTotal.ToString();
                ws.Cells["P" + (i + 8)].Value = tmp.NilaiKuliah.Grade;
                ws.Cells[ws.Dimension.Address].AutoFitColumns();
            }

            ws.Cells["A6:P7"].Style.Border.Top.Style = ExcelBorderStyle.Hair;
            ws.Cells["A6:P7"].Style.Border.Right.Style = ExcelBorderStyle.Hair;
            ws.Cells["A6:P7"].Style.Border.Bottom.Style = ExcelBorderStyle.Hair;
            ws.Cells["A6:P7"].Style.Border.Left.Style = ExcelBorderStyle.Hair;

            ws.Cells["A:P"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            ws.Cells["A6:P7"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            ws.Cells["A6:P7"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
            ws.Cells["A6:P7"].Style.Fill.PatternType = ExcelFillStyle.Solid;
            ws.Cells["A6:P7"].Style.Fill.BackgroundColor.SetColor(Color.LightGray);
           

            var fileDownloadName = "Report Nilai Mahasiswa Internal Non Pertukaran MBKM.xlsx";
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