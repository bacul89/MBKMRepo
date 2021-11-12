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

                var checkNilai = _nilaiKuliahService.GetNilaiDiakui(d.JadwalKuliahs.JenjangStudi, d.JadwalKuliahs.STRM.ToString(), d.MatkulIDAsal.ToString().PadLeft(6, '0'), d.MatkulKodeAsal, d.mahasiswas.ID.ToString());
/*                var checkNilai = _nilaiKuliahService.GetNilaiDiakui("S1","2110", "001320", "FHK 214", "2015050251");
*/              if(checkNilai == null)
                {
                    final.Add(new String[]{
                        dataSemester.Nama,
                        d.JadwalKuliahs.JenjangStudi,
                        d.mahasiswas.NIM,
                        d.mahasiswas.Nama,
                        d.JadwalKuliahs.NamaProdi,
                        d.JadwalKuliahs.KodeMataKuliah,
                        d.JadwalKuliahs.NamaMataKuliah,
                        d.JadwalKuliahs.SKS,
                        d.NilaiKuliah.Grade,
                        d.MatkulKodeAsal,
                        d.MatkulAsal,
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
                        d.JadwalKuliahs.KodeMataKuliah,
                        d.JadwalKuliahs.NamaMataKuliah,
                        d.JadwalKuliahs.SKS,
                        d.NilaiKuliah.Grade,
                        d.MatkulKodeAsal,
                        d.MatkulAsal,
                        checkNilai.NilaiDiakui,
                        checkNilai.BobotDiakui,
                    });
                }

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
                var checkNilai = _nilaiKuliahService.GetNilaiDiakui(d.JadwalKuliahs.JenjangStudi, d.JadwalKuliahs.STRM.ToString(), d.MatkulIDAsal.ToString().PadLeft(6, '0'), d.MatkulKodeAsal, d.mahasiswas.ID.ToString());
                /*                var checkNilai = _nilaiKuliahService.GetNilaiDiakui("S1","2110", "001320", "FHK 214", "2015050251");
                */
                if (checkNilai == null)
                {
                    final.Add(new String[]{
                        dataSemester.Nama,
                        d.JadwalKuliahs.JenjangStudi,
                        d.mahasiswas.NIM,
                        d.mahasiswas.Nama,
                        d.JadwalKuliahs.NamaProdi,
                        d.JadwalKuliahs.KodeMataKuliah,
                        d.JadwalKuliahs.NamaMataKuliah,
                        d.JadwalKuliahs.SKS,
                        d.NilaiKuliah.Grade,
                        d.MatkulKodeAsal,
                        d.MatkulAsal,
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
                        d.JadwalKuliahs.KodeMataKuliah,
                        d.JadwalKuliahs.NamaMataKuliah,
                        d.JadwalKuliahs.SKS,
                        d.NilaiKuliah.Grade,
                        d.MatkulKodeAsal,
                        d.MatkulAsal,
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
            return new ViewAsPdf("GetFilePdf");
        }


        public ActionResult GetFileExcel(int id)
        {
            var dataMahasiswa = _pendaftaranMataKuliahService.GetListPendaftaranInternalPertukaran(id);
            var dataSemester = _feedbackMatkulService.GetSemesterByStrm(id.ToString());

            List<String[]> final = new List<String[]>();
            foreach (var d in dataMahasiswa)
            {
                var checkNilai = _nilaiKuliahService.GetNilaiDiakui(d.JadwalKuliahs.JenjangStudi, d.JadwalKuliahs.STRM.ToString(), d.MatkulIDAsal.ToString().PadLeft(6, '0'), d.MatkulKodeAsal, d.mahasiswas.ID.ToString());
                /*                var checkNilai = _nilaiKuliahService.GetNilaiDiakui("S1","2110", "001320", "FHK 214", "2015050251");
                */
                if (checkNilai == null)
                {
                    final.Add(new String[]{
                        dataSemester.Nama,
                        d.JadwalKuliahs.JenjangStudi,
                        d.mahasiswas.NIM,
                        d.mahasiswas.Nama,
                        d.JadwalKuliahs.NamaProdi,
                        d.JadwalKuliahs.KodeMataKuliah,
                        d.JadwalKuliahs.NamaMataKuliah,
                        d.JadwalKuliahs.SKS,
                        d.NilaiKuliah.Grade,
                        d.MatkulKodeAsal,
                        d.MatkulAsal,
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
                        d.JadwalKuliahs.KodeMataKuliah,
                        d.JadwalKuliahs.NamaMataKuliah,
                        d.JadwalKuliahs.SKS,
                        d.NilaiKuliah.Grade,
                        d.MatkulKodeAsal,
                        d.MatkulAsal,
                        checkNilai.NilaiDiakui,
                        checkNilai.BobotDiakui,
                    });
                }
            }

            ExcelPackage package = new ExcelPackage();

            var ws = package.Workbook.Worksheets.Add("REPORT MAHASISWA INTERNAL PERTUKARAN MBKM");


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
            ws.Cells["F1:M1"].Merge = true;
            ws.Cells["F1"].Value = "Nilai Transfer";
            ws.Cells["F2"].Value = "Kode Mata Kuliah Asal";
            ws.Cells["G2"].Value = "Nama Mata Kuliah Asal";
            ws.Cells["H2"].Value = "SKS";
            ws.Cells["I2"].Value = "Nilai Huruf Asal";
            ws.Cells["J2"].Value = "Kode Mata Kuliah Diakui";
            ws.Cells["K2"].Value = "Nama Mata Kuliah Diakui";
            ws.Cells["L2"].Value = "Nilai Huruf Matakuliah Diakui";
            ws.Cells["M2"].Value = "Bobot Huruf";
            ws.Cells["A1:M2"].AutoFitColumns();
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
                ws.Cells[ws.Dimension.Address].AutoFitColumns();
            }

            ws.Cells["A1:M2"].Style.Border.Top.Style = ExcelBorderStyle.Hair;
            ws.Cells["A1:M2"].Style.Border.Right.Style = ExcelBorderStyle.Hair;
            ws.Cells["A1:M2"].Style.Border.Bottom.Style = ExcelBorderStyle.Hair;
            ws.Cells["A1:M2"].Style.Border.Left.Style = ExcelBorderStyle.Hair;

            ws.Cells["A:M"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            ws.Cells["A1:M2"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            ws.Cells["A1:M2"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
            ws.Cells["A1:M2"].Style.Fill.PatternType = ExcelFillStyle.Solid;
            ws.Cells["A1:M2"].Style.Fill.BackgroundColor.SetColor(Color.LightGray);


            var fileDownloadName = "Report Nilai Mahasiswa Internal Pertukaran MBKM.xlsx";
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