using MBKM.Entities.ViewModel;
using MBKM.Presentation.Helper;
using MBKM.Services;
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
using System.Web.Mvc;

namespace MBKM.Presentation.Areas.Admin.Controllers
{
    [MBKMAuthorize]
    public class ReportInternalKeluarController : Controller
    {
        // GET: Admin/ReportInternalKeluar
        private IJadwalUjianMBKMService _jadwalUjianMBKMService;
        private ILookupService _lookupService;
        private IPendaftaranMataKuliahService _pendaftaranMataKuliahService;
        private IFeedbackMatkulService _feedbackMatkulService;
        private IMahasiswaService _mahasiswaService;
        private INilaiKuliahService _nilaiKuliahService;

        public ReportInternalKeluarController(IJadwalUjianMBKMService jadwalUjianMBKMService, ILookupService lookupService, IPendaftaranMataKuliahService pendaftaranMataKuliahService, IFeedbackMatkulService feedbackMatkulService, IMahasiswaService mahasiswaService, INilaiKuliahService nilaiKuliahService)
        {
            _jadwalUjianMBKMService = jadwalUjianMBKMService;
            _lookupService = lookupService;
            _pendaftaranMataKuliahService = pendaftaranMataKuliahService;
            _feedbackMatkulService = feedbackMatkulService;
            _mahasiswaService = mahasiswaService;
            _nilaiKuliahService = nilaiKuliahService;
        }

        public ActionResult Index()
        {
            IEnumerable<VMSemester> data = _jadwalUjianMBKMService.getAllSemester();
            ViewData["firstSemester"] = _mahasiswaService.GetDataSemester(null).First().Nilai;
            ViewData["semester"] = data;
            return View();
        }
        public ActionResult DataTable(int strm)
        {
            var dataMahasiswa = _pendaftaranMataKuliahService.GetListPendaftaranInternalPertukaranKeluar(strm);
            var dataSemester = _feedbackMatkulService.GetSemesterByStrm(strm.ToString());

            List<String[]> final = new List<String[]>();
            foreach (var d in dataMahasiswa)
            {
                var checkNilai = _nilaiKuliahService.GetNilaiDiakui(d.JadwalKuliahs.JenjangStudi, d.JadwalKuliahs.STRM.ToString(), d.JadwalKuliahs.MataKuliahID.ToString().PadLeft(6, '0'), d.JadwalKuliahs.KodeMataKuliah, d.mahasiswas.ID.ToString());
                if (checkNilai == null)
                {
                    final.Add(new String[]{
                         dataSemester.Nama,
                        d.JadwalKuliahs.JenjangStudi,
                        d.mahasiswas.NIM,
                        d.mahasiswas.Nama,
                        d.JadwalKuliahs.NamaProdi,
                        d.PerjanjianKerjasama.Instansi,
                        d.MatkulAsal,
                        d.JadwalKuliahs.SKS,
                        "-",
                        d.JadwalKuliahs.KodeMataKuliah,
                        d.JadwalKuliahs.NamaMataKuliah,
                        "-",
                        "-",
                    });
                }
                else
                {
                    final.Add(new String[]{
                        dataSemester.Nama,
                        d.JadwalKuliahs.JenjangStudi,
                        d.mahasiswas.NIM,
                        d.mahasiswas.Nama,
                        d.JadwalKuliahs.NamaProdi,
                        d.PerjanjianKerjasama.Instansi,
                        d.MatkulAsal,
                        d.JadwalKuliahs.SKS,
                        "-",
                        d.JadwalKuliahs.KodeMataKuliah,
                        d.JadwalKuliahs.NamaMataKuliah,
                        checkNilai.NilaiDiakui,
                        checkNilai.BobotDiakui,
                    });
                }
            }

            return Json(final);
        }

        public ActionResult GetDataPDF(int id)
        {
            var dataMahasiswa = _pendaftaranMataKuliahService.GetListPendaftaranInternalPertukaranKeluar(id);
            var dataSemester = _feedbackMatkulService.GetSemesterByStrm(id.ToString());

            List<String[]> final = new List<String[]>();
            foreach (var d in dataMahasiswa)
            {
                var checkNilai = _nilaiKuliahService.GetNilaiDiakui(d.JadwalKuliahs.JenjangStudi, d.JadwalKuliahs.STRM.ToString(), d.JadwalKuliahs.MataKuliahID.ToString().PadLeft(6, '0'), d.JadwalKuliahs.KodeMataKuliah, d.mahasiswas.ID.ToString());
                if (checkNilai == null)
                {
                    final.Add(new String[]{
                         dataSemester.Nama,
                        d.JadwalKuliahs.JenjangStudi,
                        d.mahasiswas.NIM,
                        d.mahasiswas.Nama,
                        d.JadwalKuliahs.NamaProdi,
                        d.PerjanjianKerjasama.Instansi,
                        d.MatkulAsal,
                        d.JadwalKuliahs.SKS,
                        "-",
                        d.JadwalKuliahs.KodeMataKuliah,
                        d.JadwalKuliahs.NamaMataKuliah,
                        "-",
                        "-",
                    });
                }
                else
                {
                    final.Add(new String[]{
                        dataSemester.Nama,
                        d.JadwalKuliahs.JenjangStudi,
                        d.mahasiswas.NIM,
                        d.mahasiswas.Nama,
                        d.JadwalKuliahs.NamaProdi,
                        d.PerjanjianKerjasama.Instansi,
                        d.MatkulAsal,
                        d.JadwalKuliahs.SKS,
                        "-",
                        d.JadwalKuliahs.KodeMataKuliah,
                        d.JadwalKuliahs.NamaMataKuliah,
                        checkNilai.NilaiDiakui,
                        checkNilai.BobotDiakui,
                    });
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

            ViewData["datas"] = final;

            return /*View();*/
                new ViewAsPdf("getDataPdf")
                {
                    PageOrientation = Rotativa.Options.Orientation.Landscape,
                };
        }

        public ActionResult GetFileExcel(int id)
        {
            var dataMahasiswa = _pendaftaranMataKuliahService.GetListPendaftaranInternalPertukaranKeluar(id);
            var dataSemester = _feedbackMatkulService.GetSemesterByStrm(id.ToString());

            List<String[]> final = new List<String[]>();
            foreach (var d in dataMahasiswa)
            {
                var checkNilai = _nilaiKuliahService.GetNilaiDiakui(d.JadwalKuliahs.JenjangStudi, d.JadwalKuliahs.STRM.ToString(), d.JadwalKuliahs.MataKuliahID.ToString().PadLeft(6, '0'), d.JadwalKuliahs.KodeMataKuliah, d.mahasiswas.ID.ToString());
                if (checkNilai == null)
                {
                    final.Add(new String[]{
                         dataSemester.Nama,
                        d.JadwalKuliahs.JenjangStudi,
                        d.mahasiswas.NIM,
                        d.mahasiswas.Nama,
                        d.JadwalKuliahs.NamaProdi,
                        d.PerjanjianKerjasama.Instansi,
                        d.MatkulAsal,
                        d.JadwalKuliahs.SKS,
                        "-",
                        d.JadwalKuliahs.KodeMataKuliah,
                        d.JadwalKuliahs.NamaMataKuliah,
                        "-",
                        "-",
                    });
                }
                else
                {
                    final.Add(new String[]{
                        dataSemester.Nama,
                        d.JadwalKuliahs.JenjangStudi,
                        d.mahasiswas.NIM,
                        d.mahasiswas.Nama,
                        d.JadwalKuliahs.NamaProdi,
                        d.PerjanjianKerjasama.Instansi,
                        d.MatkulAsal,
                        d.JadwalKuliahs.SKS,
                        "-",
                        d.JadwalKuliahs.KodeMataKuliah,
                        d.JadwalKuliahs.NamaMataKuliah,
                        checkNilai.NilaiDiakui,
                        checkNilai.BobotDiakui,
                    });
                }
            }

            ExcelPackage package = new ExcelPackage();

            var ws = package.Workbook.Worksheets.Add("REPORT MAHASISWA INTERNAL MBKM KELUAR ATMA JAYA");


            ws.Cells["A1:A2"].Merge = true;
            ws.Cells["A1"].Value = "No.";
            ws.Cells["B1:B2"].Merge = true;
            ws.Cells["B1"].Value = "Tahun Semester";
            ws.Cells["C1:C2"].Merge = true;
            ws.Cells["C1"].Value = "Jenjang";
            ws.Cells["D1:D2"].Merge = true;
            ws.Cells["D1"].Value = "NIM";
            ws.Cells["E1:E2"].Merge = true;
            ws.Cells["E1"].Value = "Nama Mahasiswa";
            ws.Cells["F1:F2"].Merge = true;
            ws.Cells["F1"].Value = "Program Studi";
            ws.Cells["G1:O1"].Merge = true;
            ws.Cells["G1"].Value = "Nilai Transfer";
            ws.Cells["G2"].Value = "Kampus Tempat MBKM";
            ws.Cells["H2"].Value = "Nama Mata Kuliah Asal";
            ws.Cells["I2"].Value = "SKS";
            ws.Cells["J2"].Value = "Nilai Huruf asal";
            ws.Cells["K2"].Value = "Kode Mata Kuliah Diakui";
            ws.Cells["L2"].Value = "Nama Mata Kuliah Diakui";
            ws.Cells["M2"].Value = "Nilai Huruf Diakui";
            ws.Cells["N2"].Value = "Bobot Nilai";
            ws.Cells["A1:N2"].AutoFitColumns();
            var IndexTotal = 0;

            for (int i = 0; i < final.Count; i++)
            {
                IndexTotal = IndexTotal + 1;
                var tmp = final[i];
                ws.Cells["A" + (i + 3)].Value = (i + 1);
                ws.Cells["B" + (i + 3)].Value = tmp[0];
                ws.Cells["C" + (i + 3)].Value = tmp[1];
                ws.Cells["D" + (i + 3)].Value = tmp[2];
                ws.Cells["E" + (i + 3)].Value = tmp[3];
                ws.Cells["F" + (i + 3)].Value = tmp[4];
                ws.Cells["G" + (i + 3)].Value = tmp[5];
                ws.Cells["H" + (i + 3)].Value = tmp[6];
                ws.Cells["I" + (i + 3)].Value = tmp[7];
                ws.Cells["J" + (i + 3)].Value = tmp[8];
                ws.Cells["K" + (i + 3)].Value = tmp[9];
                ws.Cells["L" + (i + 3)].Value = tmp[10];
                ws.Cells["M" + (i + 3)].Value = tmp[11];
                ws.Cells["N" + (i + 3)].Value = tmp[12];
                ws.Cells[ws.Dimension.Address].AutoFitColumns();
            }

            ws.Cells["A1:N2"].Style.Border.Top.Style = ExcelBorderStyle.Hair;
            ws.Cells["A1:N2"].Style.Border.Right.Style = ExcelBorderStyle.Hair;
            ws.Cells["A1:N2"].Style.Border.Bottom.Style = ExcelBorderStyle.Hair;
            ws.Cells["A1:N2"].Style.Border.Left.Style = ExcelBorderStyle.Hair;

            ws.Cells["A:N"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            ws.Cells["A1:N2"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            ws.Cells["A1:N2"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
            ws.Cells["A1:N2"].Style.Fill.PatternType = ExcelFillStyle.Solid;
            ws.Cells["A1:N2"].Style.Fill.BackgroundColor.SetColor(Color.LightGray);


            var fileDownloadName = "Report Nilai Mahasiswa MBKM Internal Pertukaran Ke Luar Atma Jaya.xlsx";
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