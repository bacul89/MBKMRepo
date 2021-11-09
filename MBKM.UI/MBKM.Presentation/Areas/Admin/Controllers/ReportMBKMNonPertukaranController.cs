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


            ViewData["datas"] = final;

            return /*View();*/
                new ViewAsPdf("getDataPdf")
                {
                    PageOrientation = Rotativa.Options.Orientation.Landscape,
                };
        }


        public ActionResult GetFileExcel(int id)
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

            ExcelPackage package = new ExcelPackage();

            var ws = package.Workbook.Worksheets.Add("REPORT MAHASISWA NON-PERTUKARAN MBKM");


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
            ws.Cells["G1:G2"].Merge = true;
            ws.Cells["G1"].Value = "Jenis Kegiatan";
            ws.Cells["H1:H2"].Merge = true;
            ws.Cells["H1"].Value = "Judul Aktivitas";
            ws.Cells["I1:I2"].Merge = true;
            ws.Cells["I1"].Value = "Lokasi Aktivitas";
            ws.Cells["J1:J2"].Merge = true;
            ws.Cells["J1"].Value = "No. SK Tugas";
            ws.Cells["K1:K2"].Merge = true;
            ws.Cells["K1"].Value = "Tanggal SK Tugas";
            ws.Cells["L1:P1"].Merge = true;
            ws.Cells["L1"].Value = "Nilai Penyetaraan Kegiatan MBKM";
            ws.Cells["L2"].Value = "Kode Mata Kuliah";
            ws.Cells["M2"].Value = "Nama Mata Kuliah";
            ws.Cells["N2"].Value = "SKS";
            ws.Cells["O2"].Value = "Nilai Angka";
            ws.Cells["P2"].Value = "Nilai Huruf";
            ws.Cells["A1:P2"].AutoFitColumns();
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
                ws.Cells["O" + (i + 3)].Value = tmp[13];
                ws.Cells["P" + (i + 3)].Value = tmp[14];
                ws.Cells[ws.Dimension.Address].AutoFitColumns();
            }

            ws.Cells["A1:P2"].Style.Border.Top.Style = ExcelBorderStyle.Hair;
            ws.Cells["A1:P2"].Style.Border.Right.Style = ExcelBorderStyle.Hair;
            ws.Cells["A1:P2"].Style.Border.Bottom.Style = ExcelBorderStyle.Hair;
            ws.Cells["A1:P2"].Style.Border.Left.Style = ExcelBorderStyle.Hair;

            ws.Cells["A:P"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            ws.Cells["A1:P2"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            ws.Cells["A1:P2"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
            ws.Cells["A1:P2"].Style.Fill.PatternType = ExcelFillStyle.Solid;
            ws.Cells["A1:P2"].Style.Fill.BackgroundColor.SetColor(Color.LightGray);
           

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