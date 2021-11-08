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

namespace MBKM.Presentation.Areas.Admin.Controllers
{
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

            Session["username"] = "Smitty Werben Jeger Man Jensen";
            VMSemester model = _jkMhsService.getOngoingSemester("S1");
            return View(model);



        }

        public ActionResult DataTable(int strm)
        {
            var dataMahasiswa = _pendaftaranMataKuliahService.GetListPendaftaranEksternalPertukaran(strm);
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
                    d.JadwalKuliahs.DosenID.ToString()
                });
            }

            return Json(final);
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
            var fileDownloadName = "Report Pertukaran Eksternal.xlsx";
            var contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

            var result = new List<VMReportMahasiswaEksternal>();
            var dataSemester = _feedbackMatkulService.GetSemesterByStrm(strm.ToString());

            result = _pendaftaranMataKuliahService.GetListPendaftaranEksternalPertukaran(strm).ToList();

            /*if (matkul != null && matkul.Length != 0)
            {
                result = _cplMatakuliahService
                .Find(_ => _.MasterCapaianPembelajarans.JenjangStudi == jenjangStudi && _.MasterCapaianPembelajarans.NamaProdi == prodi && _.MasterCapaianPembelajarans.NamaFakultas == fakultas && _.KodeMataKuliah + " - " + _.NamaMataKuliah == matkul).ToList();
            }
            else
            {
                result = _cplMatakuliahService
                .Find(_ => _.MasterCapaianPembelajarans.JenjangStudi == jenjangStudi && _.MasterCapaianPembelajarans.NamaProdi == prodi && _.MasterCapaianPembelajarans.NamaFakultas == fakultas).ToList();
            }*/

            ExcelPackage package = new ExcelPackage();
            var ws = package.Workbook.Worksheets.Add("Report Pertukaran Eksternal");
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


            for (int i = 0; i < result.Count; i++)
            {
                var tmp = result[i];
                ws.Cells["A" + (i + 2)].Value = (i + 1);
                ws.Cells["B" + (i + 2)].Value = dataSemester;
                ws.Cells["C" + (i + 2)].Value = tmp.mahasiswas.JenjangStudi;
                ws.Cells["D" + (i + 2)].Value = tmp.mahasiswas.NamaUniversitas;
                ws.Cells["E" + (i + 2)].Value = tmp.mahasiswas.NIM;
                ws.Cells["F" + (i + 2)].Value = tmp.mahasiswas.Nama;
                ws.Cells["G" + (i + 2)].Value = tmp.JadwalKuliahs.KodeMataKuliah;
                ws.Cells["H" + (i + 2)].Value = tmp.JadwalKuliahs.NamaMataKuliah;
                ws.Cells["I" + (i + 2)].Value = tmp.JadwalKuliahs.ClassSection;
                ws.Cells["J" + (i + 2)].Value = tmp.NilaiKuliah.NilaiTotal;
                ws.Cells["K" + (i + 2)].Value = tmp.NilaiKuliah.Grade;
                ws.Cells["L" + (i + 2)].Value = tmp.JadwalKuliahs.Hari;
                ws.Cells["M" + (i + 2)].Value = tmp.JadwalKuliahs.JamMasuk;
                ws.Cells["N" + (i + 2)].Value = tmp.JadwalKuliahs.JamSelesai;
                ws.Cells["O" + (i + 2)].Value = tmp.JadwalKuliahs.TglAwalKuliah;
                ws.Cells["P" + (i + 2)].Value = tmp.JadwalKuliahs.TglAkhirKuliah;
                ws.Cells["Q" + (i + 2)].Value = tmp.JadwalKuliahs.DosenID+" - "+tmp.JadwalKuliahs.NamaDosen;
                ws.Cells["R" + (i + 2)].Value = tmp.JadwalKuliahs.NamaProdi;
            }

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
                FileName = "ReportPertukaranExternal.pdf",
                PageSize = Rotativa.Options.Size.A4,
                PageOrientation = Orientation.Portrait,
                //CustomSwitches = footer,
                PageMargins = new Margins(10, 3, 20, 3)

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