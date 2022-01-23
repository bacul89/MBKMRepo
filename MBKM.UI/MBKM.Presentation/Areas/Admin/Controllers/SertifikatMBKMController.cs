using MBKM.Entities.Models.MBKM;
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
        private IInformasiPertukaranService _informasiPertukaranService;
        private ICPLMKPendaftaranService _cPLMKPendaftaranService;

        public SertifikatMBKMController(IJadwalUjianMBKMService jadwalUjianMBKMService, IPendaftaranMataKuliahService pendaftaranMataKuliahService, IFeedbackMatkulService feedbackMatkulService, IMahasiswaService mahasiswaService, ILookupService lookupService, ICPLMatakuliahService cPLMatakuliahService, INilaiKuliahService nilaiKuliahService, IInformasiPertukaranService informasiPertukaranService, ICPLMKPendaftaranService cPLMKPendaftaranService)
        {
            _jadwalUjianMBKMService = jadwalUjianMBKMService;
            _pendaftaranMataKuliahService = pendaftaranMataKuliahService;
            _feedbackMatkulService = feedbackMatkulService;
            _mahasiswaService = mahasiswaService;
            _lookupService = lookupService;
            _cPLMatakuliahService = cPLMatakuliahService;
            _nilaiKuliahService = nilaiKuliahService;
            _informasiPertukaranService = informasiPertukaranService;
            _cPLMKPendaftaranService = cPLMKPendaftaranService;
        }



        // GET: Admin/SertifikatMBKM
        public ActionResult Index()
        {
            IEnumerable<VMSemester> data = _jadwalUjianMBKMService.getAllSemester();
            ViewData["semester"] = data;
            ViewData["firstSemester"] = _mahasiswaService.GetDataSemester(null).First().Nilai;
            return View();
        }

        [HttpPost]
        public ActionResult GetDataTable(int strm)
        {
            
            var data = _feedbackMatkulService.Find(x => x.JadwalKuliahs.STRM == strm && x.Mahasiswas.NIM != x.Mahasiswas.NIMAsal)
                .GroupBy(z => new {z.MahasiswaID, z.StatusFeedBack, z.Mahasiswas, z.JadwalKuliahID })
                .Select(s => new {MahasiswaID = s.Key.MahasiswaID, Status = s.Key.StatusFeedBack, Mahasiswas = s.Key.Mahasiswas, JadwalID = s.Key.JadwalKuliahID }).ToList();
            
            var data2 = data.GroupBy(z => new { z.MahasiswaID,z.Mahasiswas})
                .Select(s => new { MahasiswaID = s.Key.MahasiswaID, Mahasiswas = s.Key.Mahasiswas}).ToList();

            var data3 = data.GroupBy(z => new { z.MahasiswaID, z.Mahasiswas, z.JadwalID})
                .Select(s => new { MahasiswaID = s.Key.MahasiswaID, Mahasiswas = s.Key.Mahasiswas }).ToList();

            var pendaftaran = _pendaftaranMataKuliahService.Find(x => x.JadwalKuliahs.STRM == strm).ToList();

            List<String[]> final = new List<String[]>();
            var DescSemester = _feedbackMatkulService.GetSemesterByStrm(strm.ToString());
            foreach (var d in data2)
            {
                var dataCheck = data.Where(x => x.MahasiswaID == d.MahasiswaID && x.Status == false).Count();
                var MatkulPendaftaranCheck = pendaftaran.Where(x => x.MahasiswaID == d.MahasiswaID && x.StatusPendaftaran.ToLower().Contains("accepted")).Count();
                var matkulFeedbackCheck = data3.Where(x => x.MahasiswaID == d.MahasiswaID).Count();

                if (dataCheck == 0 && (matkulFeedbackCheck == MatkulPendaftaranCheck))
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
                        d.Mahasiswas.FlagBayar.ToString()
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
                        d.Mahasiswas.FlagBayar.ToString()
                    });
                }
            }

            var MahasiswaINternal = _pendaftaranMataKuliahService.Find(x => x.JadwalKuliahs.STRM == strm && x.mahasiswas.NIM == x.mahasiswas.NIMAsal && x.StatusPendaftaran.ToLower().Contains("accepted"))
                    .GroupBy(z => new { z.MahasiswaID, z.mahasiswas })
                    .Select(s => new { MahasiswaID = s.Key.MahasiswaID, mahasiswas = s.Key.mahasiswas })
                    .ToList();
            foreach (var f in MahasiswaINternal)
            {
                final.Add(new String[]{
                        f.MahasiswaID.ToString(),
                        DescSemester.Nama,
                        f.mahasiswas.JenjangStudi,
                        f.mahasiswas.NamaUniversitas,
                        f.mahasiswas.NIM,
                        f.mahasiswas.Nama,
                        f.mahasiswas.NoKerjasama,
                        "Sudah Feedback",
                        "Sudah Bayar"
                    });
            }
            return Json(final);
        }


        public ActionResult GetFile(int id)
        {
            var tmpMahasiswa = _mahasiswaService.Get(id);
            ViewData["namaMahasiswa"] = tmpMahasiswa.Nama; 
            ViewData["nim"] = tmpMahasiswa.NIM;

            var dataInformasiPertukaran = _informasiPertukaranService.Find(x => x.MahasiswaID == id).FirstOrDefault();
            if (dataInformasiPertukaran != null)
            {
                ViewData["jenisMahasiswa"] = "in";
                if (dataInformasiPertukaran.JenisPertukaran.ToLower().Contains("non"))
                {
                    ViewData["jenisPertukaran"] = "non pertukaran";
                    ViewData["JenisKegiatan"] = dataInformasiPertukaran.JenisKerjasama.ToLower();
                }
                else
                {
                    ViewData["jenisPertukaran"] = "pertukaran";
                    ViewData["JenisKegiatan"] = "mahasiswa";
                }

            }
            else
            {
                ViewData["jenisMahasiswa"] = "ek";
                ViewData["jenisPertukaran"] = "pertukaran";
                ViewData["JenisKegiatan"] = "mahasiswa";
            }

            var data = _pendaftaranMataKuliahService.Find(x => x.MahasiswaID == id && x.StatusPendaftaran.ToLower().Contains("accept")).ToList();

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

            var dataWarek = _lookupService.Find(x => x.Tipe == "WAREKAkademi").FirstOrDefault();
            ViewData["Warek"] = dataWarek.Nilai;
            List<CapaiaMatakuliahSertifkatDTO> final = new List<CapaiaMatakuliahSertifkatDTO>();

            var PendaftaranMatakuliah = _pendaftaranMataKuliahService.Find(x => x.MahasiswaID == id && x.StatusPendaftaran.ToLower().Contains("accept")).ToList();
            var NilaiMahasiswa = _nilaiKuliahService.Find(x => x.MahasiswaID == id).ToList();

            var CheckNilaiAvailable = NilaiMahasiswa.Count();
            
            if (tmpMahasiswa.NIM == tmpMahasiswa.NIMAsal)
            {
                var CheckInformasiPertukaran = _informasiPertukaranService.Find(x => x.MahasiswaID == tmpMahasiswa.ID).FirstOrDefault();
                if(CheckInformasiPertukaran != null)
                {
                    if(CheckInformasiPertukaran.JenisKerjasama.ToLower().Contains("ke luar") && !CheckInformasiPertukaran.JenisPertukaran.ToLower().Contains("non"))
                    {
                        foreach (var d in PendaftaranMatakuliah)
                        {
                            IList<CPLMatakuliah> tempId = _cPLMatakuliahService.Find(x => x.IDMataKUliah == d.JadwalKuliahs.MataKuliahID && x.IsActive == true && x.IsDeleted == false).ToList();
                            /*IList<CPLMatakuliah> capaianTujuan = tempId.Where(x => int.Parse(x.MasterCapaianPembelajarans.ProdiID) == d.JadwalKuliahs.ProdiID).ToList();*/
                            var nilaiFinal = _nilaiKuliahService.GetNilaiDiakui(d.JadwalKuliahs.JenjangStudi, d.JadwalKuliahs.STRM.ToString(), d.JadwalKuliahs.MataKuliahID, d.JadwalKuliahs.KodeMataKuliah, d.mahasiswas.NIM.ToString(), d.JadwalKuliahs.ClassSection);

                            if (nilaiFinal == null)
                            {
                                final.Add(new CapaiaMatakuliahSertifkatDTO
                                {
                                    kodeMatakuliah = d.MatkulKodeAsal,
                                    namaMatkul = d.MatkulAsal,
                                    angka = "-",
                                    huruf = "-",
                                    kompetensi = tempId,
                                    CPLAsal = _cPLMKPendaftaranService.Find(c => c.PendaftaranMataKuliahID == d.ID).FirstOrDefault().CPLAsal
                                });
                            }
                            else
                            {
                                final.Add(new CapaiaMatakuliahSertifkatDTO
                                {
                                    kodeMatakuliah = d.MatkulKodeAsal,
                                    namaMatkul = d.MatkulAsal,
                                    angka = nilaiFinal.NilaiAngkaFinal,
                                    huruf = nilaiFinal.NilaiDiakui,
                                    kompetensi = tempId,
                                    CPLAsal = _cPLMKPendaftaranService.Find(c => c.PendaftaranMataKuliahID == d.ID).FirstOrDefault().CPLAsal
                                });
                            }

                        }
                    }
                    else
                    {
                        foreach (var d in PendaftaranMatakuliah)
                        {
                            IList<CPLMatakuliah> tempId = _cPLMatakuliahService.Find(x => x.IDMataKUliah == d.JadwalKuliahs.MataKuliahID && x.IsActive == true && x.IsDeleted == false).ToList();
/*                            IList<CPLMatakuliah> capaianTujuan = tempId.Where(x => int.Parse(x.MasterCapaianPembelajarans.ProdiID) == d.JadwalKuliahs.ProdiID).ToList();
*/                            var nilaiFinal = _nilaiKuliahService.GetNilaiDiakui(d.JadwalKuliahs.JenjangStudi, d.JadwalKuliahs.STRM.ToString(), d.JadwalKuliahs.MataKuliahID, d.JadwalKuliahs.KodeMataKuliah, d.mahasiswas.NIM.ToString(), d.JadwalKuliahs.ClassSection);

                            if (nilaiFinal == null)
                            {
                                final.Add(new CapaiaMatakuliahSertifkatDTO
                                {
                                    kodeMatakuliah = d.JadwalKuliahs.KodeMataKuliah,
                                    namaMatkul = d.JadwalKuliahs.NamaMataKuliah,
                                    angka = "-",
                                    huruf = "-",
                                    kompetensi = tempId
                                });
                            }
                            else
                            {
                                final.Add(new CapaiaMatakuliahSertifkatDTO
                                {
                                    kodeMatakuliah = d.JadwalKuliahs.KodeMataKuliah,
                                    namaMatkul = d.JadwalKuliahs.NamaMataKuliah,
                                    angka = nilaiFinal.NilaiAngkaFinal,
                                    huruf = nilaiFinal.NilaiDiakui,
                                    kompetensi = tempId
                                });
                            }

                        }
                    }
                }
                else
                {
                    foreach (var d in PendaftaranMatakuliah)
                    {
                        IList<CPLMatakuliah> tempId = _cPLMatakuliahService.Find(x => x.IDMataKUliah == d.JadwalKuliahs.MataKuliahID && x.IsActive == true && x.IsDeleted == false).ToList();
/*                        IList<CPLMatakuliah> capaianTujuan = tempId.Where(x => int.Parse(x.MasterCapaianPembelajarans.ProdiID) == d.JadwalKuliahs.ProdiID).ToList();
*/                        var nilaiFinal = _nilaiKuliahService.GetNilaiDiakui(d.JadwalKuliahs.JenjangStudi, d.JadwalKuliahs.STRM.ToString(), d.JadwalKuliahs.MataKuliahID, d.JadwalKuliahs.KodeMataKuliah, d.mahasiswas.NIM.ToString(), d.JadwalKuliahs.ClassSection);

                        if (nilaiFinal == null)
                        {
                            final.Add(new CapaiaMatakuliahSertifkatDTO
                            {
                                kodeMatakuliah = d.JadwalKuliahs.KodeMataKuliah,
                                namaMatkul = d.JadwalKuliahs.NamaMataKuliah,
                                angka = "-",
                                huruf = "-",
                                kompetensi = tempId
                            });
                        }
                        else
                        {
                            final.Add(new CapaiaMatakuliahSertifkatDTO
                            {
                                kodeMatakuliah = d.JadwalKuliahs.KodeMataKuliah,
                                namaMatkul = d.JadwalKuliahs.NamaMataKuliah,
                                angka = nilaiFinal.NilaiAngkaFinal,
                                huruf = nilaiFinal.NilaiDiakui,
                                kompetensi = tempId
                            });
                        }

                    }
                }
            }
            else
            {
                foreach (var d in PendaftaranMatakuliah)
                {
                    
                    IList<CPLMatakuliah> tempId = _cPLMatakuliahService.Find(x => x.IDMataKUliah == d.JadwalKuliahs.MataKuliahID && x.IsActive == true && x.IsDeleted == false).ToList();
/*                    IList<CPLMatakuliah> capaianTujuan = tempId.Where(x => int.Parse(x.MasterCapaianPembelajarans.ProdiID) == d.JadwalKuliahs.ProdiID).ToList();
*/                    if (NilaiMahasiswa == null)
                    {
                        final.Add(new CapaiaMatakuliahSertifkatDTO
                        {
                            kodeMatakuliah = d.JadwalKuliahs.KodeMataKuliah,
                            namaMatkul = d.JadwalKuliahs.NamaMataKuliah,
                            angka = "-",
                            huruf = "-",
                            kompetensi = tempId
                        });
                    }
                    else
                    {
                        var nilaiAngkaFinal = NilaiMahasiswa.Find(x => x.JadwalKuliahID == d.JadwalKuliahID);
                        if(nilaiAngkaFinal == null)
                        {
                            final.Add(new CapaiaMatakuliahSertifkatDTO
                            {
                                kodeMatakuliah = d.JadwalKuliahs.KodeMataKuliah,
                                namaMatkul = d.JadwalKuliahs.NamaMataKuliah,
                                angka = "-",
                                huruf = "-",
                                kompetensi = tempId
                            });
                        }
                        else
                        {
                            final.Add(new CapaiaMatakuliahSertifkatDTO
                            {
                                kodeMatakuliah = d.JadwalKuliahs.KodeMataKuliah,
                                namaMatkul = d.JadwalKuliahs.NamaMataKuliah,
                                angka = nilaiAngkaFinal.NilaiTotal.ToString(),
                                huruf = nilaiAngkaFinal.Grade,
                                kompetensi = tempId
                            });
                        }
                    }

                }
            }
                
            ViewData["dataGrid"] = final;
            
            var dataPendaftaran = _pendaftaranMataKuliahService.Find(x => x.MahasiswaID == id).ToList();
            var semesterBerjalan = _mahasiswaService.GetDataSemester(null).First().Nama;
            var fileDownloadName = semesterBerjalan + " - Report Nilai Mahasiswa Internal Pertukaran MBKM.xlsx";
            Response.AppendHeader("Content-Disposition", "inline; filename="+ semesterBerjalan + " - " + tmpMahasiswa.NIM +"_"+ tmpMahasiswa.Nama + " - SERTIFIKAT MBKM.pdf");
            return
                new ViewAsPdf("GetFile")
                {
                    PageOrientation = Rotativa.Options.Orientation.Landscape,
                };

        }
    }
}