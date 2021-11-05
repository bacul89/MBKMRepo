﻿using MBKM.Entities.Models.MBKM;
using MBKM.Entities.ViewModel;
using MBKM.Presentation.Helper;
using MBKM.Services;
using MBKM.Services.MBKMServices;
using Rotativa;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MBKM.Presentation.Areas.Admin.Controllers
{
    [MBKMAuthorize]
    public class SertifikatMBKMController : Controller
    {
        private IJadwalUjianMBKMService _jadwalUjianMBKMService;
        private IPendaftaranMataKuliahService _pendaftaranMataKuliahService;
        private IFeedbackMatkulService _feedbackMatkulService;
        private IMahasiswaService _mahasiswaService;
        private ILookupService _lookupService;
        private ICPLMatakuliahService _cPLMatakuliahService;
        private INilaiKuliahService _nilaiKuliahService;

        public SertifikatMBKMController(IJadwalUjianMBKMService jadwalUjianMBKMService, IPendaftaranMataKuliahService pendaftaranMataKuliahService, IFeedbackMatkulService feedbackMatkulService, IMahasiswaService mahasiswaService, ILookupService lookupService, ICPLMatakuliahService cPLMatakuliahService, INilaiKuliahService nilaiKuliahService)
        {
            _jadwalUjianMBKMService = jadwalUjianMBKMService;
            _pendaftaranMataKuliahService = pendaftaranMataKuliahService;
            _feedbackMatkulService = feedbackMatkulService;
            _mahasiswaService = mahasiswaService;
            _lookupService = lookupService;
            _cPLMatakuliahService = cPLMatakuliahService;
            _nilaiKuliahService = nilaiKuliahService;
        }



        // GET: Admin/SertifikatMBKM
        public ActionResult Index()
        {
            IEnumerable<VMSemester> data = _jadwalUjianMBKMService.getAllSemester();
            ViewData["semester"] = data;
            return View();
        }

        [HttpPost]
        public ActionResult GetDataTable(int strm)
        {
            var data = _feedbackMatkulService.Find(x => x.JadwalKuliahs.STRM == strm).GroupBy(z => new {z.MahasiswaID, z.StatusFeedBack}).Select(s => new {MahasiswaID = s.Key.MahasiswaID, Status = s.Key.StatusFeedBack}).ToList();
            var data2 = _feedbackMatkulService.Find(x => x.JadwalKuliahs.STRM == strm)
                .GroupBy(z => new { z.MahasiswaID,z.Mahasiswas})
                .Select(s => new { MahasiswaID = s.Key.MahasiswaID, Mahasiswas = s.Key.Mahasiswas}).ToList();

            List<String[]> final = new List<String[]>();
            var DescSemester = _feedbackMatkulService.GetSemesterByStrm(strm.ToString());
            foreach (var d in data2)
            {
                var dataCheck = data.Where(x => x.MahasiswaID == d.MahasiswaID && x.Status == false).Count();
                if (dataCheck != 0)
                {
                    if (d.Mahasiswas.FlagBayar)
                    {
                        final.Add(new String[]{
                            d.MahasiswaID.ToString(),
                            DescSemester.Nama,
                            d.Mahasiswas.JenjangStudi,
                            d.Mahasiswas.NamaUniversitas,
                            d.Mahasiswas.NIM,
                            d.Mahasiswas.Nama,
                            d.Mahasiswas.NoKerjasama,
                            "Belum Feedback",
                            "Sudah Bayar"
                        });
                    }
                    else
                    {
                        final.Add(new String[]{
                            d.MahasiswaID.ToString(),
                            DescSemester.Nama,
                            d.Mahasiswas.JenjangStudi,
                            d.Mahasiswas.NamaUniversitas,
                            d.Mahasiswas.NIM,
                            d.Mahasiswas.Nama,
                            d.Mahasiswas.NoKerjasama,
                            "Belum Feedback",
                            "Belum Bayar"
                        });
                    }
                }
                else
                {
                    if (d.Mahasiswas.FlagBayar)
                    {
                        final.Add(new String[]{
                            d.MahasiswaID.ToString(),
                            DescSemester.Nama,
                            d.Mahasiswas.JenjangStudi,
                            d.Mahasiswas.NamaUniversitas,
                            d.Mahasiswas.NIM,
                            d.Mahasiswas.Nama,
                            d.Mahasiswas.NoKerjasama,
                            "Sudah Feedback",
                            "Sudah Bayar"
                        });
                    }
                    else
                    {
                        final.Add(new String[]{
                            d.MahasiswaID.ToString(),
                            DescSemester.Nama,
                            d.Mahasiswas.JenjangStudi,
                            d.Mahasiswas.NamaUniversitas,
                            d.Mahasiswas.NIM,
                            d.Mahasiswas.Nama,
                            d.Mahasiswas.NoKerjasama,
                            "Sudah Feedback",
                            "Belum Bayar"
                        });
                    }
                }
            }
            return Json(final);
        }


        public ActionResult GetFile(int id)
        {
            var tmpMahasiswa = _mahasiswaService.Get(id);
            ViewData["namaMahasiswa"] = tmpMahasiswa.Nama; 
            ViewData["nim"] = tmpMahasiswa.NIM;


            var data = _pendaftaranMataKuliahService.Find(x => x.MahasiswaID == id).ToList();

            var dataSemester = _feedbackMatkulService.GetSemesterByStrm(data.First().JadwalKuliahs.STRM.ToString());
            var semesterSekarang = dataSemester.Nama.Split(' ');
            ViewData["TahunSemester"] = semesterSekarang.Last();
            var strm = data.First().JadwalKuliahs.STRM.ToString().Substring(data.First().JadwalKuliahs.STRM.ToString().Length - 2); ;
            if(strm == "10")
            {
                ViewData["jenisSemester"] = "Ganjil";
            }else if(strm == "20")
            {
                ViewData["jenisSemester"] = "Genap";
            }
            else if (strm == "30")
            {
                ViewData["jenisSemester"] = "Pendek";
            }


            ViewData["jumlahMatkul"] = data.Count();
            var jumlahSks = 0;
            foreach(var d in data)
            {
                var dataSplit = d.JadwalKuliahs.SKS.Split('.');
                 jumlahSks = jumlahSks + Convert.ToInt16(dataSplit[0]);
            }

            ViewData["jumlahSks"] = jumlahSks;
            ViewData["DateNow"] = DateTime.Now.ToString("dd MMMM yyyy");

            var dataWarek = _lookupService.Find(x => x.Tipe == "WAREKAkademi").First();
            ViewData["Warek"] = dataWarek.Nilai;
            List<CapaiaMatakuliahSertifkatDTO> final = new List<CapaiaMatakuliahSertifkatDTO>();

            var dataNilaiMahasiswa = _nilaiKuliahService.Find(x => x.MahasiswaID == id).ToList();

/*            var dataPendaftaran = _pendaftaranMataKuliahService.Find(x => x.MahasiswaID == id).ToList();
*/            foreach(var d in dataNilaiMahasiswa)
            {
                IList<CPLMatakuliah> tempId = _cPLMatakuliahService.Find(x => x.IDMataKUliah == d.JadwalKuliahs.MataKuliahID).ToList();
                IList<CPLMatakuliah> capaianTujuan = tempId.Where(x => int.Parse(x.MasterCapaianPembelajarans.ProdiID) == d.JadwalKuliahs.ProdiID).ToList();
                final.Add(new CapaiaMatakuliahSertifkatDTO
                {
                    namaMatkul = d.JadwalKuliahs.NamaMataKuliah,
                    angka = d.NilaiTotal.ToString(),
                    huruf = d.Grade,
                    kompetensi = capaianTujuan
                });
            }
            ViewData["dataGrid"] = final;


            return /*View();*/

                new ViewAsPdf("GetFile")
                {
                    PageOrientation = Rotativa.Options.Orientation.Landscape,
                };

        }
    }
}