using System;
using System.Collections.Generic;
using MBKM.Common.Interfaces.RepoInterfaces.MBKMRepoInterfaces;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MBKM.Services;
using MBKM.Services.MBKMServices;
using MBKM.Entities.ViewModel;
using MBKM.Presentation.models;
using MBKM.Entities.Models;
using MBKM.Repository.Repositories;

using MBKM.Presentation.Helper;
using MBKM.Common.Helpers;
using MBKM.Entities.Models.MBKM;
using Newtonsoft.Json;
using Rotativa;
using Rotativa.Options;
using System.IO;
using OfficeOpenXml;

namespace MBKM.Presentation.Areas.Admin.Controllers
{

    [MBKMAuthorize]
    public class DaftarMatkulController : Controller
    {
        private IJadwalKuliahService _jadwalKuliahService;
        private ILookupService _lookupService;
        private IMasterCapaianPembelajaranService _mcpService;
        private IAbsensiService _absensiService;

        public DaftarMatkulController(IAbsensiService absensiService, IJadwalKuliahService jadwalKuliahService, ILookupService lookupService, IMasterCapaianPembelajaranService mcpService)
        {
            _jadwalKuliahService = jadwalKuliahService;
            _lookupService = lookupService;            
            _mcpService = mcpService;
            _absensiService = absensiService;
        }
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GetSemesterAll2()
        {
            var result = _jadwalKuliahService.GetSemesterAll2();
            return new ContentResult { Content = JsonConvert.SerializeObject(result), ContentType = "application/json" };
        }
        public ActionResult GetDaftarMatkul(int strm, string jenjangStudi, string fakultas, string prodi, string lokasi)
        {
            var result = new List<JadwalKuliah>();
            List<JadwalKuliah> MVJadwal = new List<JadwalKuliah>();
            List<string> mapJadwal = new List<string>();
            if (lokasi != null && lokasi.Length != 0)
            {
                result = _jadwalKuliahService.Find(_ => _.STRM == strm && _.JenjangStudi == jenjangStudi && _.NamaFakultas == fakultas && _.NamaProdi == prodi &&  _.Lokasi == lokasi && _.FlagOpen).ToList();
            } else
            {
                result = _jadwalKuliahService.Find(_ => _.STRM == strm && _.JenjangStudi == jenjangStudi && _.NamaFakultas == fakultas && _.NamaProdi == prodi && _.FlagOpen).ToList();
            }
            foreach (var item in result)
            {
                if (!mapJadwal.Contains(item.DosenID.ToString()) || !mapJadwal.Contains(item.ClassSection) || !mapJadwal.Contains(item.NamaMataKuliah) || !mapJadwal.Contains(item.TglAkhirKuliah.ToString()) || !mapJadwal.Contains(item.TglAwalKuliah.ToString()) || !mapJadwal.Contains(item.Lokasi))
                {
                    MVJadwal.Add(item);
                    mapJadwal.Add(item.TglAkhirKuliah.ToString());
                    mapJadwal.Add(item.TglAwalKuliah.ToString());
                    mapJadwal.Add(item.DosenID.ToString());
                    mapJadwal.Add(item.NamaMataKuliah);
                    mapJadwal.Add(item.ClassSection);
                    mapJadwal.Add(item.Lokasi);
                }
            }
            //return new ContentResult { Content = JsonConvert.SerializeObject(result), ContentType = "application/json" };
            return new ContentResult { Content = JsonConvert.SerializeObject(MVJadwal), ContentType = "application/json" };
        }
        public ActionResult ExportPDF(int strm, string jenjangStudi, string fakultas, string prodi, string lokasi, string tahunSemester)
        {
            TempData["tahunSemester"] = tahunSemester;
            var result = new List<JadwalKuliah>();
            if (lokasi != null && lokasi.Length != 0)
            {
                result = _jadwalKuliahService.Find(_ => _.STRM == strm && _.JenjangStudi == jenjangStudi && _.NamaFakultas == fakultas && _.NamaProdi == prodi && _.Lokasi == lokasi && _.FlagOpen).ToList();
            }
            else
            {
                result = _jadwalKuliahService.Find(_ => _.STRM == strm && _.JenjangStudi == jenjangStudi && _.NamaFakultas == fakultas && _.NamaProdi == prodi && _.FlagOpen).ToList();
            }
            return new ViewAsPdf("PdfDaftarMatkul", result)
            {
                FileName = "Report Daftar Mata Kuliah.pdf",
                PageSize = Size.A4,
                PageOrientation = Orientation.Landscape,
                PageMargins = new Margins(10, 3, 20, 3)

            };
        }
        public ActionResult PdfDaftarMatkul(int strm, string jenjangStudi, string fakultas, string prodi, string lokasi, string tahunSemester)
        {
            TempData["tahunSemester"] = tahunSemester;
            var result = new List<JadwalKuliah>();
            if (lokasi != null && lokasi.Length != 0)
            {
                result = _jadwalKuliahService.Find(_ => _.STRM == strm && _.JenjangStudi == jenjangStudi && _.NamaFakultas == fakultas && _.NamaProdi == prodi && _.Lokasi == lokasi && _.FlagOpen).ToList();
            }
            else
            {
                result = _jadwalKuliahService.Find(_ => _.STRM == strm && _.JenjangStudi == jenjangStudi && _.NamaFakultas == fakultas && _.NamaProdi == prodi && _.FlagOpen).ToList();
            }
            return View(result);
        }
        public ActionResult ExportExcel(int strm, string jenjangStudi, string fakultas, string prodi, string lokasi, string tahunSemester)
        {
            var fileDownloadName = "Daftar Mata Kuliah.xlsx";
            var contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

            var result = new List<JadwalKuliah>();
            if (lokasi != null && lokasi.Length != 0)
            {
                result = _jadwalKuliahService.Find(_ => _.STRM == strm && _.JenjangStudi == jenjangStudi && _.NamaFakultas == fakultas && _.NamaProdi == prodi && _.Lokasi == lokasi && _.FlagOpen).ToList();
            }
            else
            {
                result = _jadwalKuliahService.Find(_ => _.STRM == strm && _.JenjangStudi == jenjangStudi && _.NamaFakultas == fakultas && _.NamaProdi == prodi && _.FlagOpen).ToList();
            }

            ExcelPackage package = new ExcelPackage();
            var ws = package.Workbook.Worksheets.Add("DAFTAR MATA KULIAH");
            ws.Cells["A1"].Value = "No.";
            ws.Cells["B1"].Value = "Tahun Semester";
            ws.Cells["C1"].Value = "Jenjang Studi";
            ws.Cells["D1"].Value = "Fakultas";
            ws.Cells["E1"].Value = "Program Studi";
            ws.Cells["F1"].Value = "Kode Mata Kuliah";
            ws.Cells["G1"].Value = "Nama Mata Kuliah";
            ws.Cells["H1"].Value = "SKS";
            ws.Cells["I1"].Value = "Seksi";
            ws.Cells["J1"].Value = "Lokasi";
            ws.Cells["K1"].Value = "Nama Dosen";


            for (int i = 0; i < result.Count; i++)
            {
                var tmp = result[i];
                ws.Cells["A" + (i + 2)].Value = (i+1);
                ws.Cells["B" + (i + 2)].Value = tahunSemester;
                ws.Cells["C" + (i + 2)].Value = jenjangStudi;
                ws.Cells["D" + (i + 2)].Value = fakultas;
                ws.Cells["E" + (i + 2)].Value = prodi;
                ws.Cells["F" + (i + 2)].Value = tmp.KodeMataKuliah;
                ws.Cells["G" + (i + 2)].Value = tmp.NamaMataKuliah;
                ws.Cells["H" + (i + 2)].Value = tmp.SKS;
                ws.Cells["I" + (i + 2)].Value = tmp.ClassSection;
                ws.Cells["J" + (i + 2)].Value = tmp.Lokasi;
                ws.Cells["K" + (i + 2)].Value = tmp.NamaDosen;
            }

            ws.Cells[ws.Dimension.Address].AutoFitColumns();

            var fileStream = new MemoryStream();
            package.SaveAs(fileStream);
            fileStream.Position = 0;

            var fsr = new FileStreamResult(fileStream, contentType);
            fsr.FileDownloadName = fileDownloadName;

            return fsr;
        }
        public ActionResult getLookupByTipe(string tipe)
        {
            return new ContentResult { Content = JsonConvert.SerializeObject(_lookupService.getLookupByTipe(tipe)), ContentType = "application/json" };
        }
        public ActionResult GetFakultasByJenjangStudi(string search, string jenjangStudi)
        {
            return new ContentResult { Content = JsonConvert.SerializeObject(_absensiService.GetFakultasByJenjangStudi(search, jenjangStudi)), ContentType = "application/json" };
        }
        public ActionResult GetProdiByFakultas(string search, string jenjangStudi, string fakultas)
        {
            return new ContentResult { Content = JsonConvert.SerializeObject(_absensiService.GetProdiByFakultas(search, jenjangStudi, fakultas)), ContentType = "application/json" };
        }
        public ActionResult GetLokasiByProdi(string search, string jenjangStudi, string prodi)
        {
            return new ContentResult { Content = JsonConvert.SerializeObject(_absensiService.GetLokasiByProdi(search, jenjangStudi, prodi)), ContentType = "application/json" };
        }
    }
}