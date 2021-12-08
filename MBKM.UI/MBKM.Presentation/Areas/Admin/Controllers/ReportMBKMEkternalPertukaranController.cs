using MBKM.Entities.ViewModel;
using MBKM.Services.MBKMServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using Newtonsoft.Json;
using Rotativa.Options;
using Rotativa;
using OfficeOpenXml;
using System.IO;
using OfficeOpenXml.Style;
using System.Drawing;
using OfficeOpenXml.Drawing;
using MBKM.Presentation.Helper;

namespace MBKM.Presentation.Areas.Admin.Controllers
{
    [MBKMAuthorize]
    public class ReportMBKMEkternalPertukaranController : Controller
    {
        private IPendaftaranMataKuliahService _pendaftaranMataKuliahService;
        private IMahasiswaService _mahasiswaService;
        private IInformasiPertukaranService _informasiPertukaranService;
        private IFeedbackMatkulService _feedbackMatkulService;
        private IJadwalKuliahService _jadwalKuliahService;
        private IJadwalUjianMBKMService _jadwalUjianMBKMService;
        private IJadwalKuliahMahasiswaService _jkMhsService;

        public ReportMBKMEkternalPertukaranController(IPendaftaranMataKuliahService pendaftaranMataKuliahService, IMahasiswaService mahasiswaService, IInformasiPertukaranService informasiPertukaranService, IFeedbackMatkulService feedbackMatkulService, IJadwalKuliahService jadwalKuliahService, IJadwalUjianMBKMService jadwalUjianMBKMService, IJadwalKuliahMahasiswaService jkMahasiswaService)
        {
            _pendaftaranMataKuliahService = pendaftaranMataKuliahService;
            _mahasiswaService = mahasiswaService;
            _informasiPertukaranService = informasiPertukaranService;
            _feedbackMatkulService = feedbackMatkulService;
            _jadwalKuliahService = jadwalKuliahService;
            _jadwalUjianMBKMService = jadwalUjianMBKMService;
            _jkMhsService = jkMahasiswaService ;
    }

        // GET: Admin/ReportMBKMExternalPertukaran
        public ActionResult Index()
        {

            /*int currentMonth = DateTime.Now.Month;
            var currentYear = DateTime.Now.Year.ToString();
            var beforeYear = DateTime.
            Console.WriteLine(currentMonth);
            Console.WriteLine(currentYear);

            if (currentMonth > 8)
            {

            }*/
            /*
            VMSemester model = new VMSemester();
            IEnumerable<VMSemester> data = _jadwalUjianMBKMService.getAllSemester().ToList();
            foreach (VMSemester x in data)
            {
                string Name = x.Nama;
                string LastNine = Name.Substring(Math.Max(0, Name.Length - 9));
                if (LastNine == )
                {

                }
            }*/

            //model = 
            //ViewData["semester"] = data;
            //return View();

            //Session["username"] = "Smitty Werben Jeger Man Jensen";
            VMSemester model = _jkMhsService.getOngoingSemester("S1");
            return View(model);



        }

        public ActionResult DataTable(int strm)
        {

            IEnumerable<VMReportMahasiswaEksternal> dataMahasiswa = _pendaftaranMataKuliahService.GetListPendaftaranEksternalPertukaran(strm);
            //var dataMahasiswa = _pendaftaranMataKuliahService.GetListPendaftaranEksternalPertukaran(strm);
            var dataMahasiswaWitoutNilai = _pendaftaranMataKuliahService.GetListPendaftaranEksternalPertukaranWithoutNilai(strm);
            var dataSemester = _feedbackMatkulService.GetSemesterByStrm(strm.ToString());

            List<String[]> final = new List<String[]>();
            foreach (var d in dataMahasiswa)
            {
                final.Add(new String[]{
                    dataSemester.Nama,
                    d.JadwalKuliahs.JenjangStudi,
                    d.mahasiswas.NamaUniversitas,
                    d.mahasiswas.NIM,
                    d.mahasiswas.Nama,

                    d.JadwalKuliahs.KodeMataKuliah,
                    d.JadwalKuliahs.NamaMataKuliah,
                    d.JadwalKuliahs.ClassSection,
                    d.NilaiKuliah.NilaiTotal.ToString(),
                    d.NilaiKuliah.Grade,
                    d.JadwalKuliahs.Hari.ToString(),
                    d.JadwalKuliahs.JamMasuk,
                    d.JadwalKuliahs.JamSelesai,
                    d.JadwalKuliahs.TglAwalKuliah.ToString(),
                    d.JadwalKuliahs.TglAkhirKuliah.ToString(),
                    d.JadwalKuliahs.NamaDosen,
                    d.JadwalKuliahs.NamaProdi,
                    d.JadwalKuliahs.DosenID.ToString(),
                    /*d.mahasiswas.ID.ToString(),
                    d.mahasiswas.NIM,
                    d.mahasiswas.NIMAsal,
                    d.JadwalKuliahs.ID.ToString(),
                    d.ID.ToString(),
                    d.ID.ToString()*/
                });
            }

            var row = 0;
            foreach (var e in dataMahasiswaWitoutNilai)
            {
                row = row + 1;
                bool check = isAvailable(dataMahasiswa, e.mahasiswas.ID, e.JadwalKuliahs.ID);
                
                if (check == false)
                {
                    final.Add(new String[]{
                            dataSemester.Nama,
                             e.JadwalKuliahs.JenjangStudi,
                            e.mahasiswas.NamaUniversitas,
                            e.mahasiswas.NIM,
                            e.mahasiswas.Nama,
                            e.JadwalKuliahs.KodeMataKuliah,
                            e.JadwalKuliahs.NamaMataKuliah,
                            e.JadwalKuliahs.ClassSection,
                            "-",
                            "-",
                            e.JadwalKuliahs.Hari.ToString(),
                            e.JadwalKuliahs.JamMasuk,
                            e.JadwalKuliahs.JamSelesai,
                            e.JadwalKuliahs.TglAwalKuliah.ToString(),
                            e.JadwalKuliahs.TglAkhirKuliah.ToString(),
                            e.JadwalKuliahs.NamaDosen,
                            e.JadwalKuliahs.NamaProdi,
                            e.JadwalKuliahs.DosenID.ToString(),
                            /*e.mahasiswas.ID.ToString(),
                            e.mahasiswas.NIM,
                            e.mahasiswas.NIMAsal,
                            e.JadwalKuliahs.ID.ToString(),
                            e.ID.ToString(),
                            e.ID.ToString()*/
                        });
                }
            }


            return Json(final);
        }

        private bool isAvailable(IEnumerable<VMReportMahasiswaEksternal> data, long MhsID, long JadwalID)
        {

            //var data = _pendaftaranMataKuliahService.GetListPendaftaranEksternalPertukaran(strm);
            foreach (var d in data)
            {
                var test = d.mahasiswas.ID + " = " + MhsID + ", " + d.JadwalKuliahs.ID + " = " + JadwalID;

                Console.WriteLine(test);
                if (d.mahasiswas.ID == MhsID && d.JadwalKuliahs.ID == JadwalID)
                {
                    return true;
                }
            }
            return false;

        }


        [HttpPost]
        public ActionResult GetSemesterAll(int skip, int take, string search)
        {
            //return Json(, JsonRequestBehavior.AllowGet);



            var final = _jadwalKuliahService.GetSemesterAll(skip, take, search);
            List<object> data = new List<object>();
            foreach (var p in final)
            {
                var q = new
                {
                    Nama = p.Nama,
                    ID = p.ID
                };
                data.Add(q);
            }

            return Json(data);
        }

        [HttpPost]
        public ActionResult GetSemesterAll2()
        {
            var result = _jadwalKuliahService.GetSemesterAll2();
            return new ContentResult { Content = JsonConvert.SerializeObject(result), ContentType = "application/json" };
        }



        /*        public ActionResult ExportExcel()
                {
                    using (ExcelPackage pck = new ExcelPackage("ExportPDF"))
                    {
                        ExcelWorksheet ws = pck.Workbook.Worksheets.Add("Accounts");
                        ws.Cells["A1"].LoadFromDataTable(DataTable, true);
                        pck.Save();
                    }

                }*/

        /*        public void TestExcel()
                {
                    int strm = 2010;

                    VMReportMahasiswaEksternal data = new VMReportMahasiswaEksternal();
                    List<VMReportMahasiswaEksternal> external = _pendaftaranMataKuliahService.GetListPendaftaranEksternalPertukaran(strm).ToList();
                    *//*List<VMReportMahasiswaEksternal> data = new List<VMReportMahasiswaEksternal>();
                    using (var context = new StudentsEntities())
                    {
                        data = context.Student_details.ToList();

                    }*//*

                    using (ExcelPackage pck = new ExcelPackage("ExportPDF"))
                    {
                        ExcelWorksheet ws = pck.Workbook.Worksheets.Add("Accounts");
                        ws.Cells["A1"].LoadFromCollection(external, true);
                        pck.Save();
                    }


                    *//*ExcelPackage excel = new ExcelPackage();
                    var workSheet = excel.Workbook.Worksheets.Add("Sheet1");
                    workSheet.Cells[1, 1].LoadFromCollection(external, true);*/
        /*            using (var memoryStream = new MemoryStream())
                    {
                        Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                        //here i have set filname as Students.xlsx
                        Response.AddHeader("content-disposition", "attachment;  filename=Students.xlsx");
                        excel.SaveAs(memoryStream);
                        memoryStream.WriteTo(Response.OutputStream);
                        Response.Flush();
                        Response.End();
                    }*//*
                }
        */



        public ActionResult ExportExcel(int strm)
        {
            
            var contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

            var result = new List<VMReportMahasiswaEksternal>();
            var dataSemester = _feedbackMatkulService.GetSemesterByStrm(strm.ToString());

            result = _pendaftaranMataKuliahService.GetListPendaftaranEksternalPertukaran(strm).ToList();
            var fileDownloadName = dataSemester.Nama + " - Report Pertukaran Eksternal.xlsx";
           
            ExcelPackage package = new ExcelPackage();
            var ws = package.Workbook.Worksheets.Add("Report Pertukaran Eksternal");

            double columnWidth = 2;
            ws.Column(1).Width = columnWidth;
            ws.Column(8).Width = columnWidth;
            int RowIndex = 0;
            int ColIndex = 0;

            //Image.FromFile(@"D:\sample.png");
            /*int Width = 320;
            int Height = 200;*/
            
            var dir = Server.MapPath("~/Asset");
            var path = Path.Combine(dir, "Report_Lambang_Atma_Jaya.png"); //validate the path for security or use other means to generate the path.
            Image imageView =  Image.FromFile(path);

            ExcelPicture pic = ws.Drawings.AddPicture("Logo", imageView);
            pic.SetPosition(RowIndex, 2, ColIndex, 15);
            pic.SetSize(205, 50);

           
            //ws.Column().AddPicture("Picture_Name", img);


            ws.Cells["A1:D2"].Merge = true;
            /*ws.Cells["B1:I1"].Merge = true;
            ws.Cells["B2:I2"].Merge = true;*/
            ws.Cells["A3:I4"].Merge = true;
            //ws.Cells["Q1:Q2"].Merge = true;

            ws.Row(1).Height = 15;
            ws.Row(2).Height = 26;
            ws.Column(1).Width = 17;

            /*ws.Cells["B1:I1"].Style.Font.Bold = true;
            ws.Cells["B2:I2"].Style.Font.Bold = true;
            ws.Cells["B2:I2"].Style.Font.Size = 25;
            ws.Cells["B2:I2"].Style.WrapText = true;
            ws.Cells["B1:I1"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
            ws.Cells["B2:I2"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;*/
            ws.Cells["A3:I4"].Style.Font.Bold = true;

            ws.Cells["A3"].Value = "FORMULIR \nMATRIKS MATAKULIAH PROGRAM MBKM PERTUKARAN EKSTERNAL";
            /*ws.Cells["B1"].Value = "UNIVERSITAS KATOLIK INDONESIA";
            ws.Cells["B2"].Value = "ATMA JAYA";*/

            ws.Cells["A6:R6"].Style.Font.Bold = true;
            ws.Cells["A6:R6"].Style.Border.Top.Style = ExcelBorderStyle.Thick;
            ws.Cells["A6:R6"].Style.Border.Bottom.Style = ExcelBorderStyle.Thick;
            ws.Cells["A6:R6"].Style.Border.Left.Style = ExcelBorderStyle.Thick;
            ws.Cells["A6:R6"].Style.Border.Right.Style = ExcelBorderStyle.Thick;
            ws.Cells["A6:R6"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            ws.Cells["A3:I4"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
            ws.Cells["A3:I4"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

            /*ws.Cells["Q1:Q2"].Merge = true;
            ws.Cells["Q1"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
            ws.Cells["Q1"].Value = "Kode Dokumen :\nFR-UAJ-02-92/R1";
            ws.Cells["Q3"].Value = "Tanggal Berlaku: ";
            ws.Cells["Q3"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
            ws.Cells["Q4"].Value = "Tanggal Revisi: ";
            ws.Cells["Q4"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;*/


            ws.Cells["A6"].Value = "No.";
            ws.Cells["B6"].Value = "Tahun Semester";
            ws.Cells["C6"].Value = "Jenjang Studi";
            ws.Cells["D6"].Value = "Universitas Asal";
            ws.Cells["E6"].Value = "NIM";
            ws.Cells["F6"].Value = "Nama Mahasiswa";
            ws.Cells["G6"].Value = "Kode Mata Kuliah";
            ws.Cells["H6"].Value = "Nama Mata Kuliah";
            ws.Cells["I6"].Value = "Seksi";
            ws.Cells["J6"].Value = "Nilai Akhir";
            ws.Cells["K6"].Value = "Grade";
            ws.Cells["L6"].Value = "Hari";
            ws.Cells["M6"].Value = "Jam Mulai";
            ws.Cells["N6"].Value = "Jam Selesai";
            ws.Cells["O6"].Value = "Tanggal Mulai";
            ws.Cells["P6"].Value = "Tanggal Selesai";
            ws.Cells["Q6"].Value = "Dosen";
            ws.Cells["R6"].Value = "Program Studi";
            ws.Column(1).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            ws.Column(2).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            ws.Column(3).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            ws.Column(5).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            ws.Column(7).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            ws.Column(9).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            ws.Column(10).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            ws.Column(11).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            ws.Column(12).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            ws.Column(13).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            ws.Column(14).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            ws.Column(15).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            ws.Column(16).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            ws.Column(17).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            ws.Column(18).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            ws.Column(19).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            int i = 0;
            for (i = 0; i < result.Count; i++)
            {
                
                ws.Column(i + 7).Width = 10;
                Console.WriteLine(i + 7);
                ws.Cells["A" + (i + 7) + ":R" + (i + 7)].Style.Border.Top.Style = ExcelBorderStyle.Hair;
                ws.Cells["A" + (i + 7) + ":R" + (i + 7)].Style.Border.Bottom.Style = ExcelBorderStyle.Hair;
                ws.Cells["A" + (i + 7) + ":R" + (i + 7)].Style.Border.Left.Style = ExcelBorderStyle.Hair;
                ws.Cells["A" + (i + 7) + ":R" + (i + 7)].Style.Border.Right.Style = ExcelBorderStyle.Hair;
                var tmp = result[i];
                ws.Cells["A" + (i + 7)].Value = (i + 1);
                ws.Cells["B" + (i + 7)].Value = dataSemester.Nama;
                ws.Cells["C" + (i + 7)].Value = tmp.mahasiswas.JenjangStudi;
                ws.Cells["D" + (i + 7)].Value = tmp.mahasiswas.NamaUniversitas;
                ws.Cells["E" + (i + 7)].Value = tmp.mahasiswas.NIM;
                ws.Cells["F" + (i + 7)].Value = tmp.mahasiswas.Nama;
                ws.Cells["G" + (i + 7)].Value = tmp.JadwalKuliahs.KodeMataKuliah;
                ws.Cells["H" + (i + 7)].Value = tmp.JadwalKuliahs.NamaMataKuliah;
                ws.Cells["I" + (i + 7)].Value = tmp.JadwalKuliahs.ClassSection;
                ws.Cells["J" + (i + 7)].Value = tmp.NilaiKuliah.NilaiTotal;
                ws.Cells["K" + (i + 7)].Value = tmp.NilaiKuliah.Grade;
                ws.Cells["L" + (i + 7)].Value = tmp.JadwalKuliahs.Hari;
                ws.Cells["M" + (i + 7)].Value = tmp.JadwalKuliahs.JamMasuk;
                ws.Cells["N" + (i + 7)].Value = tmp.JadwalKuliahs.JamSelesai;
                ws.Cells["O" + (i + 7)].Value = tmp.JadwalKuliahs.TglAwalKuliah.Date.ToString("dd/MM/yyyy");
                ws.Cells["P" + (i + 7)].Value = tmp.JadwalKuliahs.TglAkhirKuliah.Date.ToString("dd/MM/yyyy");
                ws.Cells["Q" + (i + 7)].Value = tmp.JadwalKuliahs.DosenID+" - "+tmp.JadwalKuliahs.NamaDosen;
                ws.Cells["R" + (i + 7)].Value = tmp.JadwalKuliahs.NamaProdi;

                
            }
            
            //ws.Cell["A7"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            //ws.Cells["A" + 7 + ":R" + (i + 7)].AutoFitColumns();
            //ws.Cells["A" + 7 + ":R" + (i + 7)].Width = 10;
            //ws.Cells["A"+ (0 + 7) + ":R" + (i + 7)].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;




            ws.Cells[ws.Dimension.Address].AutoFitColumns();

            var fileStream = new MemoryStream();
            package.SaveAs(fileStream);
            fileStream.Position = 0;

            var fsr = new FileStreamResult(fileStream, contentType);
            fsr.FileDownloadName = fileDownloadName;

            return fsr;
        }

        public ActionResult ExportPDF(int strm)
        {


            var dataSemester = _feedbackMatkulService.GetSemesterByStrm(strm.ToString());

            VMReportMahasiswaEksternal data = new VMReportMahasiswaEksternal();
            List<VMReportMahasiswaEksternal> external = _pendaftaranMataKuliahService.GetListPendaftaranEksternalPertukaran(strm).ToList();


            var list = external.Select(x => new VMReportMahasiswaEksternal()
            {
                ID = x.ID,
                MatkulIDAsal = x.MatkulIDAsal,
                MatkulKodeAsal = x.MatkulKodeAsal,
                MatkulAsal = x.MatkulAsal,
                Kesenjangan = x.Kesenjangan,
                Nilai = x.Nilai,
                Konversi = x.Konversi,
                Hasil = x.Hasil,
                DosenID = x.DosenID,
                DosenPembimbing = x.DosenPembimbing,
                MahasiswaID = x.MahasiswaID,
                mahasiswas = x.mahasiswas,
                JadwalKuliahID = x.JadwalKuliahID,
                JadwalKuliahs = x.JadwalKuliahs,
                StatusPendaftaran = x.StatusPendaftaran,
                informasiID = x.informasiID,
                InformasiPertukaran = x.InformasiPertukaran,
                NilaiKuliah = x.NilaiKuliah


            });


            ViewData["semester"] = dataSemester.Nama;
            ViewData["mahasiswas"] = list;
            return new ViewAsPdf()
            {
                FileName = dataSemester.Nama+" - ReportPertukaranExternal.pdf",
                PageSize = Rotativa.Options.Size.A4,
                PageOrientation = Orientation.Landscape,
                //CustomSwitches = footer,
                PageMargins = new Margins(5, 3, 5, 3)

            };

        }


        public ActionResult ExportView(int strm)
        {

            
            var dataSemester = _feedbackMatkulService.GetSemesterByStrm(strm.ToString());

            VMReportMahasiswaEksternal data = new VMReportMahasiswaEksternal();
            List<VMReportMahasiswaEksternal> external = _pendaftaranMataKuliahService.GetListPendaftaranEksternalPertukaran(strm).ToList();


            var list = external.Select(x => new VMReportMahasiswaEksternal()
            {
                ID = x.ID,
                MatkulIDAsal = x.MatkulIDAsal,
                MatkulKodeAsal = x.MatkulKodeAsal,
                MatkulAsal = x.MatkulAsal,
                Kesenjangan = x.Kesenjangan,
                Nilai = x.Nilai,
                Konversi = x.Konversi,
                Hasil = x.Hasil,
                DosenID = x.DosenID,
                DosenPembimbing = x.DosenPembimbing,
                MahasiswaID = x.MahasiswaID,
                mahasiswas = x.mahasiswas,
                JadwalKuliahID = x.JadwalKuliahID,
                JadwalKuliahs = x.JadwalKuliahs,
                StatusPendaftaran = x.StatusPendaftaran,
                informasiID = x.informasiID,
                InformasiPertukaran = x.InformasiPertukaran,
                NilaiKuliah = x.NilaiKuliah


            });

            
            ViewData["semester"] = dataSemester.Nama;
            ViewData["mahasiswas"] = list;

            return View("ExportPDF");
 

        }

    }



}