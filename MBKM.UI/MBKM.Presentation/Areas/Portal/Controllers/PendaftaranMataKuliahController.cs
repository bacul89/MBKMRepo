using MBKM.Common.Helpers;
using MBKM.Entities.Models.MBKM;
using MBKM.Entities.ViewModel;
using MBKM.Presentation.Helper;
using MBKM.Presentation.models;
using MBKM.Services;
using MBKM.Services.MBKMServices;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MBKM.Presentation.Areas.Portal.Controllers
{
    [MBKMAuthorize]
    public class PendaftaranMataKuliahController : Controller
    {
        private IPendaftaranMataKuliahService _pmkService;
        private IJadwalKuliahService _jkService;
        private IMahasiswaService _mahasiswaService;
        private IJenisKerjasamaModelService _jenisKerjasamaService;
        private IPerjanjianKerjasamaService _perjanjianKerjasamaService;
        private IInformasiPertukaranService _informasiPertukaranService;
        private IPendaftaranMataKuliahService _pendaftaranMataKuliahService;
        private ICPLMKPendaftaranService _cplmkPendaftaranService;
        private ICPLMatakuliahService _cplMatakuliahService;
        private IApprovalPendaftaranService _approvalPendaftaranService;
        public PendaftaranMataKuliahController(IApprovalPendaftaranService approvalPendaftaranService, ICPLMatakuliahService cplMatakuliahService, ICPLMKPendaftaranService cplmkPendaftaranService, IPendaftaranMataKuliahService pendaftaranMataKuliahService, IInformasiPertukaranService informasiPertukaranService, IPerjanjianKerjasamaService perjanjianKerjasamaService, IJenisKerjasamaModelService jenisKerjasamaService, IPendaftaranMataKuliahService pmkService, IMahasiswaService mahasiswaService, IJadwalKuliahService jkService)
        {
            _pmkService = pmkService;
            _mahasiswaService = mahasiswaService;
            _jkService = jkService;
            _jenisKerjasamaService = jenisKerjasamaService;
            _perjanjianKerjasamaService = perjanjianKerjasamaService;
            _informasiPertukaranService = informasiPertukaranService;
            _pendaftaranMataKuliahService = pendaftaranMataKuliahService;
            _cplmkPendaftaranService = cplmkPendaftaranService;
            _cplMatakuliahService = cplMatakuliahService;
            _approvalPendaftaranService = approvalPendaftaranService;
        }
        public ActionResult Index()
        {
            var mahasiswa = GetMahasiswaByEmail(Session["emailMahasiswa"] as string);
            if (mahasiswa.StatusVerifikasi == "DAFTAR")
            {
                TempData["alertMessage"] = "Tolong lengkapi data diri anda terlebih dahulu!";
                return RedirectToAction("Index", "DataDiri");
            }
            else if (mahasiswa.StatusVerifikasi == "MENUNGGU VERIFIKASI")
            {
                TempData["alertMessage"] = "Tolong tunggu sementara data diri anda diverifikasi!";
                return RedirectToAction("Index", "DataDiri");
            }
            else if (mahasiswa.StatusVerifikasi == "DITOLAK")
            {
                TempData["alertMessage"] = "Lengkapi kembali data diri anda atau hubungi pihak administrator universitas atma jaya!";
                return RedirectToAction("Index", "DataDiri");
            }
            if (mahasiswa.NIM == mahasiswa.NIMAsal)
            {
                return RedirectToAction("Internal");
            }
            return RedirectToAction("Eksternal");
        }
        public ActionResult Internal()
        {
            var mahasiswa = GetMahasiswaByEmail(Session["emailMahasiswa"] as string);
            if (mahasiswa.NIM != mahasiswa.NIMAsal)
            {
                return RedirectToAction("Eksternal");
            }
            var model = _pmkService.getOngoingSemester(mahasiswa.JenjangStudi);
            return View(model);
        }
        public ActionResult Eksternal()
        {
            var mahasiswa = GetMahasiswaByEmail(Session["emailMahasiswa"] as string);
            if (mahasiswa.NIM == mahasiswa.NIMAsal)
            {
                return RedirectToAction("Internal");
            }
            var model = _pmkService.getOngoingSemester(mahasiswa.JenjangStudi);
            return View(model);
        }
        public ActionResult FormPendaftaran(int idMatkul)
        {
            var matkul = GetJadwalKuliah(idMatkul);
            VMPendaftaranJadwalKuliah model = new VMPendaftaranJadwalKuliah(matkul);
            model.ID = idMatkul;
            model.NoKerjasama = GetMahasiswaByEmail(Session["emailMahasiswa"] as string).NoKerjasama;
            return View(model);
        }
        public ActionResult FormPendaftaranInternalKeLuarAtma()
        {
            return View();
        }
        public ActionResult FormPendaftaranInternal(int idMatkul, string jenisKegiatan)
        {
            var matkul = GetJadwalKuliah(idMatkul);
            VMPendaftaranJadwalKuliah model = new VMPendaftaranJadwalKuliah(matkul);
            model.ID = idMatkul;
            if (jenisKegiatan.ToLower().Contains("internal ke luar atma jaya"))
            {
                ViewData["Model"] = model;
                return RedirectToAction("FormPendaftaranInternalKeLuarAtma");
            }
            return View(model);
        }
        public ActionResult GetFakultas(string search)
        {
            string email = Session["emailMahasiswa"] as string;
            var result = GetMahasiswaByEmail(email);
            return new ContentResult { Content = JsonConvert.SerializeObject(_pmkService.GetFakultas(result.JenjangStudi, search)), ContentType = "application/json" };
        }
        public ActionResult GetMataKuliahByProdi(string prodi, string lokasi, int strm)
        {
            List<JadwalKuliah> jks = new List<JadwalKuliah>();
            List<string> jadwalKuliahs = new List<string>();
            foreach (var item in _jkService.Find(jk => jk.NamaProdi == prodi && jk.Lokasi == lokasi && jk.STRM == strm && jk.FlagOpen).ToList())
            {
                if (!jadwalKuliahs.Contains(item.NamaMataKuliah))
                {
                    jks.Add(item);
                    jadwalKuliahs.Add(item.NamaMataKuliah);
                }
            }
            return new ContentResult { Content = JsonConvert.SerializeObject(jks), ContentType = "application/json" };
        }
        public ActionResult GetProdiByFakultas(string idFakultas, string search)
        {
            string email = Session["emailMahasiswa"] as string;
            var result = GetMahasiswaByEmail(email);
            return new ContentResult { Content = JsonConvert.SerializeObject(_pmkService.GetProdiByFakultas(result.JenjangStudi, idFakultas, search)), ContentType = "application/json" };
        }
        public ActionResult GetLokasiByProdi(string namaProdi, string search)
        {
            string email = Session["emailMahasiswa"] as string;
            var result = GetMahasiswaByEmail(email);
            return new ContentResult { Content = JsonConvert.SerializeObject(_pmkService.GetLokasiByProdi(result.JenjangStudi, namaProdi, search)), ContentType = "application/json" };
        }
        public ActionResult GetProgram()
        {
            List<string> programs = new List<string>();
            List<JenisKerjasamaModel> jkm = new List<JenisKerjasamaModel>();
            foreach (var item in _jenisKerjasamaService.GetAll())
            {
                if (!programs.Contains(item.JenisPertukaran))
                {
                    programs.Add(item.JenisPertukaran);
                    jkm.Add(item);
                }
            }
            return new ContentResult { Content = JsonConvert.SerializeObject(jkm), ContentType = "application/json" };
        }
        public ActionResult GetKegiatanByProgram(string program)
        {
            return new ContentResult { Content = JsonConvert.SerializeObject(_jenisKerjasamaService.Find(jk => jk.JenisPertukaran == program).ToList()), ContentType = "application/json" };
        }
        public ActionResult GetInstansiByJenisKerjasama(string idKerjasama)
        {
            List<string> instansis = new List<string>();
            List<VMLookup> pks = new List<VMLookup>();
            var result = _perjanjianKerjasamaService.Find(pk => pk.JenisKerjasama == idKerjasama).ToList();
            foreach (var item in result)
            {
                if (!instansis.Contains(item.NamaInstansi))
                {
                    instansis.Add(item.NamaInstansi);
                    var tes = new VMLookup();
                    tes.Nama = item.NamaInstansi;
                    pks.Add(tes);
                }
            }
            return new ContentResult { Content = JsonConvert.SerializeObject(pks), ContentType = "application/json" };
        }
        public ActionResult GetInstansiByNoKerjasama(string noKerjasama)
        {
            var result = _perjanjianKerjasamaService.Find(pk => pk.NoPerjanjian == noKerjasama).FirstOrDefault();
            return new ContentResult { Content = JsonConvert.SerializeObject(result), ContentType = "application/json" };
        }
        public ActionResult GetNoKerjasamaByInstansi(string instansi, string idKerjasama)
        {
            return new ContentResult { Content = JsonConvert.SerializeObject(_perjanjianKerjasamaService.Find(pk => pk.NamaInstansi == instansi && pk.JenisKerjasama == idKerjasama).ToList()), ContentType = "application/json" };
        }
        public ActionResult GetInformasiPertukaran()
        {
            Int64 id = GetMahasiswaByEmail(Session["emailMahasiswa"] as string).ID;
            return new ContentResult { Content = JsonConvert.SerializeObject(_informasiPertukaranService.Find(ip => ip.MahasiswaID == id).FirstOrDefault()), ContentType = "application/json" };
        }
        public ActionResult GetJenisKerjasama()
        {
            string noKerjasama = GetMahasiswaByEmail(Session["emailMahasiswa"] as string).NoKerjasama;
            int jenisKerjasamaID = int.Parse(_perjanjianKerjasamaService.Find(pk => pk.NoPerjanjian == noKerjasama).FirstOrDefault().JenisKerjasama);
            var jenisKerjasama = _jenisKerjasamaService.Find(jk => jk.ID == jenisKerjasamaID).FirstOrDefault();
            return new ContentResult { Content = JsonConvert.SerializeObject(jenisKerjasama), ContentType = "application/json" };
        }
        public ActionResult GetJenisKerjasamaInternal()
        {
            Int64 id = GetMahasiswaByEmail(Session["emailMahasiswa"] as string).ID;
            var informasiPertukaran = _informasiPertukaranService.Find(ip => ip.MahasiswaID == id).FirstOrDefault();
            return new ContentResult { Content = JsonConvert.SerializeObject(informasiPertukaran), ContentType = "application/json" };
        }
        public ActionResult GetMataKuliahAsal(int idMatkul)
        {
            var result = _jkService.Get(idMatkul);
            string prodiIDAsal = Session["prodiIDAsal"] as string;
            //string prodiIDAsal = "0700";
            List<string> kodeNama = new List<string>();
            List<CPLMatakuliah> cplmks = new List<CPLMatakuliah>();
            var list = _cplMatakuliahService.Find(cplmk => cplmk.MasterCapaianPembelajarans.IsActive && !cplmk.MasterCapaianPembelajarans.IsDeleted && cplmk.IsActive && !cplmk.IsDeleted && cplmk.MasterCapaianPembelajarans.ProdiID == prodiIDAsal).ToList();
            foreach (var item in list)
            {
                if (!kodeNama.Contains(item.KodeMataKuliah + " - " + item.NamaMataKuliah))
                {
                    kodeNama.Add(item.KodeMataKuliah + " - " + item.NamaMataKuliah);
                    cplmks.Add(item);
                }
            }
            return new ContentResult { Content = JsonConvert.SerializeObject(cplmks), ContentType = "application/json" };
        }
        public ActionResult InsertInformasiPertukaran(InformasiPertukaran informasiPertukaran)
        {
            try
            {
                Int64 id = GetMahasiswaByEmail(Session["emailMahasiswa"] as string).ID;
                var ip = _informasiPertukaranService.Find(_ => _.MahasiswaID == id).FirstOrDefault();
                if (ip == null)
                {
                    informasiPertukaran.STRM = (int) _pmkService.getOngoingSemester(GetMahasiswaByEmail(Session["emailMahasiswa"] as string).JenjangStudi).ID;
                    informasiPertukaran.MahasiswaID = id;
                    informasiPertukaran.IsActive = true;
                    informasiPertukaran.IsDeleted = false;
                    informasiPertukaran.CreatedDate = DateTime.Now;
                    informasiPertukaran.UpdatedDate = DateTime.Now;
                    try
                    {
                        _informasiPertukaranService.Save(informasiPertukaran);
                    }
                    catch (Exception)
                    {
                        informasiPertukaran.TanggalSK = null;
                        _informasiPertukaranService.Save(informasiPertukaran);
                    }
                } else
                {
                    ip.JudulAktivitas = informasiPertukaran.JudulAktivitas;
                    ip.LokasiTugas = informasiPertukaran.LokasiTugas;
                    ip.TanggalSK = informasiPertukaran.TanggalSK;
                    ip.NoSK = informasiPertukaran.NoSK;
                    ip.UpdatedDate = DateTime.Now;
                    try
                    {
                        _informasiPertukaranService.Save(ip);
                    }
                    catch (Exception)
                    {
                        ip.TanggalSK = null;
                        _informasiPertukaranService.Save(ip);
                    }
                }
                return Json(new ServiceResponse { status = 200, message = "Data berhasil tersimpan!" });
            }
            catch (Exception e)
            {

                return Json(new ServiceResponse { status = 500, message = e.Message });
            }
        }
        public ActionResult InsertPendaftaranMK(PendaftaranMataKuliah pendaftaranMataKuliah, VMCPLMKPendaftaran cplmkPendaftaran, ApprovalPendaftaran approvalPendaftaran)
        {
            try
            {
                Int64 id = GetMahasiswaByEmail(Session["emailMahasiswa"] as string).ID;
                var cek = _pendaftaranMataKuliahService.Find(pmk => pmk.MahasiswaID == id && pmk.JadwalKuliahID == pendaftaranMataKuliah.JadwalKuliahID && !pmk.StatusPendaftaran.ToLower().Contains("rejected")).FirstOrDefault();
                if (cek == null)
                {
                    pendaftaranMataKuliah.MahasiswaID = id;
                    pendaftaranMataKuliah.IsActive = true;
                    pendaftaranMataKuliah.IsDeleted = false;
                    pendaftaranMataKuliah.CreatedDate = DateTime.Now;
                    pendaftaranMataKuliah.UpdatedDate = DateTime.Now;
                    pendaftaranMataKuliah.StatusPendaftaran = "MENUNGGU APPROVAL KAPRODI/WR BIDANG AKADEMIK";
                    //pendaftaranMataKuliah.Hasil += "a";
                    _pendaftaranMataKuliahService.Save(pendaftaranMataKuliah);
                    Int64 ID = pendaftaranMataKuliah.ID;

                    if (cplmkPendaftaran.CPLMatakuliah != null)
                    {
                        string kodeMatkul = cplmkPendaftaran.CPLMatakuliah.Split(new string[] { " - " }, StringSplitOptions.None)[0];
                        string prodiId = Session["prodiIDAsal"] as string;
                        //string prodiId = "0700";
                        var list = _cplMatakuliahService.Find(cplmk => cplmk.MasterCapaianPembelajarans.IsActive && !cplmk.MasterCapaianPembelajarans.IsDeleted && cplmk.IsActive && !cplmk.IsDeleted && cplmk.KodeMataKuliah == kodeMatkul && cplmk.MasterCapaianPembelajarans.ProdiID == prodiId).ToList();
                        foreach (var item in list)
                        {
                            CPLMKPendaftaran res = new CPLMKPendaftaran();
                            res.CPLMatakuliahID = item.ID;
                            res.PendaftaranMataKuliahID = ID;
                            res.IsActive = true;
                            res.IsDeleted = false;
                            res.CreatedDate = DateTime.Now;
                            res.UpdatedDate = DateTime.Now;
                            _cplmkPendaftaranService.Save(res);
                        }
                    } else
                    {
                        CPLMKPendaftaran res = new CPLMKPendaftaran();
                        res.CPLAsal = cplmkPendaftaran.CPLAsal;
                        res.PendaftaranMataKuliahID = ID;
                        res.IsActive = true;
                        res.IsDeleted = false;
                        res.CreatedDate = DateTime.Now;
                        res.UpdatedDate = DateTime.Now;
                        _cplmkPendaftaranService.Save(res);
                    }

                    approvalPendaftaran.StatusPendaftaran = "MENUNGGU APPROVAL KAPRODI/WR BIDANG AKADEMIK";
                    approvalPendaftaran.PendaftaranMataKuliahID = ID;
                    approvalPendaftaran.Approval = "-";
                    approvalPendaftaran.Catatan = "-";
                    approvalPendaftaran.IsActive = true;
                    approvalPendaftaran.IsDeleted = false;
                    approvalPendaftaran.CreatedDate = DateTime.Now;
                    approvalPendaftaran.UpdatedDate = DateTime.Now;
                    _approvalPendaftaranService.Save(approvalPendaftaran);
                    return Json(new ServiceResponse { status = 200, message = "Anda berhasil terdaftar, silahkan cek tracking pendaftaran untuk melihat status pendaftaran!" });
                }
                if (cek.StatusPendaftaran.Contains("ACCEPTED BY MAHASISWA"))
                {
                    return Json(new ServiceResponse { status = 400, message = "Anda sudah mendaftar matakuliah ini!" });
                }
                return Json(new ServiceResponse { status = 400, message = "Pendaftaran anda sedang diproses, silahkan cek tracking pendaftaran untuk melihat status pendaftaran!" });
            }
            catch (Exception e)
                {
                return Json(new ServiceResponse { status = 500, message = e.Message });
            }
        }
        public JadwalKuliah GetJadwalKuliah(int id)
        {
            return _jkService.Get(id);
        }
        public Mahasiswa GetMahasiswaByEmail(string email)
        {
            return _mahasiswaService.Find(m => m.Email == email).FirstOrDefault();
        }
    }
}