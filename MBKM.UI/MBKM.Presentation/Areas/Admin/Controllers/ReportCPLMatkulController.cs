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
using Rotativa.Options;
using Rotativa;
using OfficeOpenXml;
using System.IO;

namespace MBKM.Presentation.Areas.Admin.Controllers
{

    [MBKMAuthorize]
    public class ReportCPLMatkulController : Controller
    {
        private IJadwalKuliahService _jadwalKuliahService;
        private ICPLMatakuliahService _cplMatakuliahService;

        public ReportCPLMatkulController(ICPLMatakuliahService cplMatakuliahService, IJadwalKuliahService jkService, ILookupService lookupService, IMasterCapaianPembelajaranService mcpService)
        {
            _jadwalKuliahService = jkService;
            _cplMatakuliahService = cplMatakuliahService;
        }
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GetMatkulByProdi(string jenjangStudi, string prodi, string search)
        {
            var result = _jadwalKuliahService
                .Find(_ => _.JenjangStudi == jenjangStudi && _.NamaProdi == prodi && _.FlagOpen && (_.KodeMataKuliah + " - " + _.NamaMataKuliah).Contains(search))
                .Select(_ => new VMLookup
                {
                    Nama = _.KodeMataKuliah + " - " + _.NamaMataKuliah
                }).Distinct().ToList();
            var cek = new List<string>();
            var final = new List<VMLookup>();
            foreach (var item in result)
            {
                if (!cek.Contains(item.Nama))
                {
                    cek.Add(item.Nama);
                    final.Add(item);
                }
            }
            return new ContentResult { Content = JsonConvert.SerializeObject(final), ContentType = "application/json" };
        }
        public ActionResult GetCPLMatkul(string jenjangStudi, string fakultas, string prodi, string matkul)
        {
            var result = new List<CPLMatakuliah>();
            if (matkul != null && matkul.Length != 0)
            {
                result = _cplMatakuliahService
                .Find(_ => _.MasterCapaianPembelajarans.JenjangStudi == jenjangStudi && _.MasterCapaianPembelajarans.NamaProdi == prodi && _.MasterCapaianPembelajarans.NamaFakultas == fakultas && _.KodeMataKuliah + " - " + _.NamaMataKuliah == matkul).ToList();
            } else
            {
                result = _cplMatakuliahService
                .Find(_ => _.MasterCapaianPembelajarans.JenjangStudi == jenjangStudi && _.MasterCapaianPembelajarans.NamaProdi == prodi && _.MasterCapaianPembelajarans.NamaFakultas == fakultas).ToList();
            }
            result = result.OrderBy(_ => _.KodeMataKuliah).ToList();
            return new ContentResult { Content = JsonConvert.SerializeObject(result), ContentType = "application/json" };
        }
        public ActionResult ExportPDF(string jenjangStudi, string fakultas, string prodi, string matkul)
        {
            var model = new List<CPLMatakuliah>();
            if (matkul != null && matkul.Length != 0)
            {
                model = _cplMatakuliahService
                .Find(_ => _.MasterCapaianPembelajarans.JenjangStudi == jenjangStudi && _.MasterCapaianPembelajarans.NamaProdi == prodi && _.MasterCapaianPembelajarans.NamaFakultas == fakultas && _.KodeMataKuliah + " - " + _.NamaMataKuliah == matkul).ToList();
            }
            else
            {
                model = _cplMatakuliahService
                .Find(_ => _.MasterCapaianPembelajarans.JenjangStudi == jenjangStudi && _.MasterCapaianPembelajarans.NamaProdi == prodi && _.MasterCapaianPembelajarans.NamaFakultas == fakultas).ToList();
            }
            return new ViewAsPdf("PdfCPLMatkul", model)
            {
                FileName = "Report CPL Mata Kuliah " + matkul + ".pdf",
                PageSize = Size.A4,
                PageOrientation = Orientation.Landscape,
                PageMargins = new Margins(10, 3, 20, 3)

            };
        }
        public ActionResult ExportExcel(string jenjangStudi, string fakultas, string prodi, string matkul)
        {
            var fileDownloadName = "Capaian Pembelajaran Mata Kuliah.xlsx";
            var contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

            var result = new List<CPLMatakuliah>();
            if (matkul != null && matkul.Length != 0)
            {
                result = _cplMatakuliahService
                .Find(_ => _.MasterCapaianPembelajarans.JenjangStudi == jenjangStudi && _.MasterCapaianPembelajarans.NamaProdi == prodi && _.MasterCapaianPembelajarans.NamaFakultas == fakultas && _.KodeMataKuliah + " - " + _.NamaMataKuliah == matkul).ToList();
            }
            else
            {
                result = _cplMatakuliahService
                .Find(_ => _.MasterCapaianPembelajarans.JenjangStudi == jenjangStudi && _.MasterCapaianPembelajarans.NamaProdi == prodi && _.MasterCapaianPembelajarans.NamaFakultas == fakultas).ToList();
            }

            ExcelPackage package = new ExcelPackage();
            var ws = package.Workbook.Worksheets.Add("CPL MATA KULIAH");
            ws.Cells["A1"].Value = "No.";
            ws.Cells["B1"].Value = "Jenjang Studi";
            ws.Cells["C1"].Value = "Fakultas";
            ws.Cells["D1"].Value = "Program Studi";
            ws.Cells["E1"].Value = "Kode Mata Kuliah";
            ws.Cells["F1"].Value = "Nama Mata Kuliah";
            ws.Cells["G1"].Value = "Kelompok";
            ws.Cells["H1"].Value = "Capaian Pembelajaran";
            ws.Cells["I1"].Value = "Kode";


            for (int i = 0; i < result.Count; i++)
            {
                var tmp = result[i];
                ws.Cells["A" + (i + 2)].Value = (i + 1);
                ws.Cells["B" + (i + 2)].Value = jenjangStudi;
                ws.Cells["C" + (i + 2)].Value = fakultas;
                ws.Cells["D" + (i + 2)].Value = prodi;
                ws.Cells["E" + (i + 2)].Value = tmp.KodeMataKuliah;
                ws.Cells["F" + (i + 2)].Value = tmp.NamaMataKuliah;
                ws.Cells["G" + (i + 2)].Value = tmp.Kelompok;
                ws.Cells["H" + (i + 2)].Value = tmp.MasterCapaianPembelajarans.Capaian;
                ws.Cells["I" + (i + 2)].Value = tmp.MasterCapaianPembelajarans.Kode;
            }

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