using MBKM.Entities.Models.MBKM;
using MBKM.Entities.ViewModel;
using MBKM.Presentation.Helper;
using MBKM.Presentation.models;
using MBKM.Services;
using MBKM.Services.MBKMServices;
using Rotativa;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MBKM.Presentation.Areas.Portal.Controllers
{

    [MBKMAuthorize]
    public class SertifikatMbkmController : Controller
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

        public SertifikatMbkmController(IJadwalUjianMBKMService jadwalUjianMBKMService, IPendaftaranMataKuliahService pendaftaranMataKuliahService, IFeedbackMatkulService feedbackMatkulService, IMahasiswaService mahasiswaService, ILookupService lookupService, ICPLMatakuliahService cPLMatakuliahService, INilaiKuliahService nilaiKuliahService, IInformasiPertukaranService informasiPertukaranService, ICPLMKPendaftaranService cPLMKPendaftaranService)
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


        // GET: Portal/SertifikatMbkm
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult GetFile()
        {

            string email = Session["emailMahasiswa"] as string;

            var tmpMahasiswa = _mahasiswaService.Find(x => x.Email == email).First();
            double id = tmpMahasiswa.ID;
            ViewData["namaMahasiswa"] = tmpMahasiswa.Nama;
            ViewData["nim"] = tmpMahasiswa.NIM;

            var dataMahasiswa = _pendaftaranMataKuliahService.Find(z => z.MahasiswaID == id).First();
            if ((tmpMahasiswa.NIM == tmpMahasiswa.NIMAsal ||
                tmpMahasiswa.NIM != tmpMahasiswa.NIMAsal) && dataMahasiswa.FlagSertifikat == false)

            {

                /* new */

                /* var tmpMahasiswa = _mahasiswaService.Get(id);
                 ViewData["namaMahasiswa"] = tmpMahasiswa.Nama;
                 ViewData["nim"] = tmpMahasiswa.NIM;*/


                var data = _pendaftaranMataKuliahService.Find(x => x.MahasiswaID == id).ToList();

                var dataSemester = _feedbackMatkulService.GetSemesterByStrm(data.First().JadwalKuliahs.STRM.ToString());
                var semesterSekarang = dataSemester.Nama.Split(' ');
                ViewData["TahunSemester"] = semesterSekarang.Last();
                var strm = data.First().JadwalKuliahs.STRM.ToString().Substring(data.First().JadwalKuliahs.STRM.ToString().Length - 2); ;
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


                ViewData["jumlahMatkul"] = data.Count();
                var jumlahSks = 0;
                foreach (var d in data)
                {
                    var dataSplit = d.JadwalKuliahs.SKS.Split('.');
                    jumlahSks = jumlahSks + Convert.ToInt16(dataSplit[0]);
                }

                ViewData["jumlahSks"] = jumlahSks;
                ViewData["DateNow"] = DateTime.Now.ToString("dd MMMM yyyy");

                var dataWarek = _lookupService.Find(x => x.Tipe == "WAREKAkademi").FirstOrDefault();
                ViewData["Warek"] = dataWarek.Nilai;
                List<CapaiaMatakuliahSertifkatDTO> final = new List<CapaiaMatakuliahSertifkatDTO>();

                var dataNilaiMahasiswa = _nilaiKuliahService.Find(x => x.MahasiswaID == id).ToList();

                var PendaftaranMatakuliah = _pendaftaranMataKuliahService.Find(x => x.MahasiswaID == id && x.StatusPendaftaran.ToLower().Contains("accept")).ToList();
                var NilaiMahasiswa = _nilaiKuliahService.Find(x => x.MahasiswaID == id).ToList();

                var CheckNilaiAvailable = NilaiMahasiswa.Count();

                if (tmpMahasiswa.NIM == tmpMahasiswa.NIMAsal)
                {
                    var CheckInformasiPertukaran = _informasiPertukaranService.Find(x => x.MahasiswaID == tmpMahasiswa.ID).FirstOrDefault();
                    if (CheckInformasiPertukaran != null)
                    {
                        if (CheckInformasiPertukaran.JenisKerjasama.ToLower().Contains("ke luar") && !CheckInformasiPertukaran.JenisPertukaran.ToLower().Contains("non"))
                        {
                            foreach (var d in PendaftaranMatakuliah)
                            {
                                IList<CPLMatakuliah> tempId = _cPLMatakuliahService.Find(x => x.IDMataKUliah == d.JadwalKuliahs.MataKuliahID).ToList();
                                IList<CPLMatakuliah> capaianTujuan = tempId.Where(x => int.Parse(x.MasterCapaianPembelajarans.ProdiID) == d.JadwalKuliahs.ProdiID).ToList();
                                var nilaiFinal = _nilaiKuliahService.GetNilaiDiakui(d.JadwalKuliahs.JenjangStudi, d.JadwalKuliahs.STRM.ToString(), d.JadwalKuliahs.MataKuliahID, d.JadwalKuliahs.KodeMataKuliah, d.mahasiswas.NIM.ToString(), d.JadwalKuliahs.ClassSection);

                                if (nilaiFinal == null)
                                {
                                    final.Add(new CapaiaMatakuliahSertifkatDTO
                                    {
                                        kodeMatakuliah = d.MatkulKodeAsal,
                                        namaMatkul = d.MatkulAsal,
                                        angka = "-",
                                        huruf = "-",
                                        kompetensi = capaianTujuan,
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
                                        kompetensi = capaianTujuan,
                                        CPLAsal = _cPLMKPendaftaranService.Find(c => c.PendaftaranMataKuliahID == d.ID).FirstOrDefault().CPLAsal
                                    });
                                }

                            }
                        }
                        else
                        {
                            foreach (var d in PendaftaranMatakuliah)
                            {
                                IList<CPLMatakuliah> tempId = _cPLMatakuliahService.Find(x => x.IDMataKUliah == d.JadwalKuliahs.MataKuliahID).ToList();
                                IList<CPLMatakuliah> capaianTujuan = tempId.Where(x => int.Parse(x.MasterCapaianPembelajarans.ProdiID) == d.JadwalKuliahs.ProdiID).ToList();
                                var nilaiFinal = _nilaiKuliahService.GetNilaiDiakui(d.JadwalKuliahs.JenjangStudi, d.JadwalKuliahs.STRM.ToString(), d.JadwalKuliahs.MataKuliahID, d.JadwalKuliahs.KodeMataKuliah, d.mahasiswas.NIM.ToString(), d.JadwalKuliahs.ClassSection);

                                if (nilaiFinal == null)
                                {
                                    final.Add(new CapaiaMatakuliahSertifkatDTO
                                    {
                                        kodeMatakuliah = d.JadwalKuliahs.KodeMataKuliah,
                                        namaMatkul = d.JadwalKuliahs.NamaMataKuliah,
                                        angka = "-",
                                        huruf = "-",
                                        kompetensi = capaianTujuan
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
                                        kompetensi = capaianTujuan
                                    });
                                }

                            }
                        }
                    }
                    else
                    {
                        foreach (var d in PendaftaranMatakuliah)
                        {
                            IList<CPLMatakuliah> tempId = _cPLMatakuliahService.Find(x => x.IDMataKUliah == d.JadwalKuliahs.MataKuliahID).ToList();
                            IList<CPLMatakuliah> capaianTujuan = tempId.Where(x => int.Parse(x.MasterCapaianPembelajarans.ProdiID) == d.JadwalKuliahs.ProdiID).ToList();
                            var nilaiFinal = _nilaiKuliahService.GetNilaiDiakui(d.JadwalKuliahs.JenjangStudi, d.JadwalKuliahs.STRM.ToString(), d.JadwalKuliahs.MataKuliahID, d.JadwalKuliahs.KodeMataKuliah, d.mahasiswas.NIM.ToString(), d.JadwalKuliahs.ClassSection);

                            if (nilaiFinal == null)
                            {
                                final.Add(new CapaiaMatakuliahSertifkatDTO
                                {
                                    kodeMatakuliah = d.JadwalKuliahs.KodeMataKuliah,
                                    namaMatkul = d.JadwalKuliahs.NamaMataKuliah,
                                    angka = "-",
                                    huruf = "-",
                                    kompetensi = capaianTujuan
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
                                    kompetensi = capaianTujuan
                                });
                            }

                        }
                    }
                }
                else
                {
                    foreach (var d in PendaftaranMatakuliah)
                    {

                        IList<CPLMatakuliah> tempId = _cPLMatakuliahService.Find(x => x.IDMataKUliah == d.JadwalKuliahs.MataKuliahID).ToList();
                        IList<CPLMatakuliah> capaianTujuan = tempId.Where(x => int.Parse(x.MasterCapaianPembelajarans.ProdiID) == d.JadwalKuliahs.ProdiID).ToList();
                        if (NilaiMahasiswa == null)
                        {
                            final.Add(new CapaiaMatakuliahSertifkatDTO
                            {
                                kodeMatakuliah = d.JadwalKuliahs.KodeMataKuliah,
                                namaMatkul = d.JadwalKuliahs.NamaMataKuliah,
                                angka = "-",
                                huruf = "-",
                                kompetensi = capaianTujuan
                            });
                        }
                        else
                        {
                            var nilaiAngkaFinal = NilaiMahasiswa.Find(x => x.JadwalKuliahID == d.JadwalKuliahID);
                            if (nilaiAngkaFinal == null)
                            {
                                final.Add(new CapaiaMatakuliahSertifkatDTO
                                {
                                    kodeMatakuliah = d.JadwalKuliahs.KodeMataKuliah,
                                    namaMatkul = d.JadwalKuliahs.NamaMataKuliah,
                                    angka = "-",
                                    huruf = "-",
                                    kompetensi = capaianTujuan
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
                                    kompetensi = capaianTujuan
                                });
                            }
                        }

                    }
                }

                ViewData["dataGrid"] = final;
                var dataPendaftaran = _pendaftaranMataKuliahService.Find(x => x.MahasiswaID == id).ToList();




                return /*View();*/

                new ViewAsPdf("GetFile")
                {
                    PageOrientation = Rotativa.Options.Orientation.Landscape,
                    FileName = tmpMahasiswa.NIM + " - " + tmpMahasiswa.Nama + " - Sertifikat.pdf",
                };

            }
            else
            {
                return Json(new ServiceResponse { status = 200, message = "Cetak Gagal Silakhan Hubungi BAA!!!", data = dataMahasiswa.FlagSertifikat }, JsonRequestBehavior.AllowGet);
            }

        }
    }

}