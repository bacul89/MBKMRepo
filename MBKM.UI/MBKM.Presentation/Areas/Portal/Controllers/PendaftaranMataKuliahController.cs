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
            var mahasiswa = GetMahasiswaByEmail(Session["email"] as string);
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
            var mahasiswa = GetMahasiswaByEmail(Session["email"] as string);
            if (mahasiswa.NIM != mahasiswa.NIMAsal)
            {
                return RedirectToAction("Eksternal");
            }
            var model = _pmkService.getOngoingSemester(mahasiswa.JenjangStudi);
            return View(model);
        }
        public ActionResult Eksternal()
        {
            var mahasiswa = GetMahasiswaByEmail(Session["email"] as string);
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
            model.NoKerjasama = GetMahasiswaByEmail(Session["email"] as string).NoKerjasama;
            return View(model);
        }
        public ActionResult FormPendaftaranInternal(int idMatkul)
        {
            var matkul = GetJadwalKuliah(idMatkul);
            VMPendaftaranJadwalKuliah model = new VMPendaftaranJadwalKuliah(matkul);
            model.ID = idMatkul;
            model.NoKerjasama = GetMahasiswaByEmail(Session["email"] as string).NoKerjasama;
            return View(model);
        }
        public ActionResult GetFakultas(string search)
        {
            string email = Session["email"] as string;
            var result = GetMahasiswaByEmail(email);
            return Json(_pmkService.GetFakultas(result.JenjangStudi, search), JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetMataKuliahByProdi(int idProdi, string lokasi, int strm)
        {
            List<JadwalKuliah> jks = new List<JadwalKuliah>();
            List<string> jadwalKuliahs = new List<string>();
            foreach (var item in _jkService.Find(jk => jk.ProdiID == idProdi && jk.Lokasi == lokasi && jk.STRM == strm).ToList())
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
            string email = Session["email"] as string;
            var result = GetMahasiswaByEmail(email);
            return Json(_pmkService.GetProdiByFakultas(result.JenjangStudi, idFakultas, search), JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetLokasiByProdi(string namaProdi, string search)
        {
            string email = Session["email"] as string;
            var result = GetMahasiswaByEmail(email);
            return Json(_pmkService.GetLokasiByProdi(result.JenjangStudi, namaProdi, search), JsonRequestBehavior.AllowGet);
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
            return Json(jkm, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetKegiatanByProgram(string program)
        {
            return Json(_jenisKerjasamaService.Find(jk => jk.JenisPertukaran == program).ToList(), JsonRequestBehavior.AllowGet);
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
            return Json(pks, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetInstansiByNoKerjasama(string noKerjasama)
        {
            var result = _perjanjianKerjasamaService.Find(pk => pk.NoPerjanjian == noKerjasama).FirstOrDefault();
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetNoKerjasamaByInstansi(string instansi)
        {
            return Json(_perjanjianKerjasamaService.Find(pk => pk.NamaInstansi == instansi).ToList(), JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetInformasiPertukaran()
        {
            Int64 id = GetMahasiswaByEmail(Session["email"] as string).ID;
            return new ContentResult { Content = JsonConvert.SerializeObject(_informasiPertukaranService.Find(ip => ip.MahasiswaID == id).FirstOrDefault()), ContentType = "application/json" };
        }
        public ActionResult GetJenisKerjasama()
        {
            string noKerjasama = GetMahasiswaByEmail(Session["email"] as string).NoKerjasama;
            int jenisKerjasamaID = int.Parse(_perjanjianKerjasamaService.Find(pk => pk.NoPerjanjian == noKerjasama).FirstOrDefault().JenisKerjasama);
            var jenisKerjasama = _jenisKerjasamaService.Find(jk => jk.ID == jenisKerjasamaID).FirstOrDefault();
            return new ContentResult { Content = JsonConvert.SerializeObject(jenisKerjasama), ContentType = "application/json" };
        }
        public ActionResult GetJenisKerjasamaInternal()
        {
            Int64 id = GetMahasiswaByEmail(Session["email"] as string).ID;
            var informasiPertukaran = _informasiPertukaranService.Find(ip => ip.MahasiswaID == id).FirstOrDefault();
            return new ContentResult { Content = JsonConvert.SerializeObject(informasiPertukaran), ContentType = "application/json" };
        }
        public ActionResult GetMataKuliahAsal(string idMatkul)
        {
            return new ContentResult { Content = JsonConvert.SerializeObject(_cplMatakuliahService.Find(cplmk => cplmk.IDMataKUliah == idMatkul).ToList()), ContentType = "application/json" };
        }
        public ActionResult InsertInformasiPertukaran(InformasiPertukaran informasiPertukaran)
        {
            try
            {
                Int64 id = GetMahasiswaByEmail(Session["email"] as string).ID;
                if (_informasiPertukaranService.Find(ip => ip.MahasiswaID == id).FirstOrDefault() == null)
                {
                    informasiPertukaran.STRM = (int) _pmkService.getOngoingSemester(GetMahasiswaByEmail(Session["email"] as string).JenjangStudi).ID;
                    informasiPertukaran.MahasiswaID = id;
                    informasiPertukaran.IsActive = true;
                    informasiPertukaran.IsDeleted = false;
                    informasiPertukaran.CreatedDate = DateTime.Now;
                    informasiPertukaran.UpdatedDate = DateTime.Now;
                    _informasiPertukaranService.Save(informasiPertukaran);
                    return Json(new ServiceResponse { status = 200, message = "Data berhasil tersimpan!" });
                }
                return Json(new ServiceResponse { status = 400, message = "Data sudah pernah disubmit!" });
            }
            catch (Exception e)
            {
                return Json(new ServiceResponse { status = 500, message = e.Message });
            }
        }
        public ActionResult InsertPendaftaranMK(PendaftaranMataKuliah pendaftaranMataKuliah, CPLMKPendaftaran cplmkPendaftaran, ApprovalPendaftaran approvalPendaftaran)
        {
            try
            {
                Int64 id = GetMahasiswaByEmail(Session["email"] as string).ID;
                if (_pendaftaranMataKuliahService.Find(pmk => pmk.MahasiswaID == id && pmk.JadwalKuliahID == pendaftaranMataKuliah.JadwalKuliahID).FirstOrDefault() == null)
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
                    cplmkPendaftaran.PendaftaranMataKuliahID = ID;
                    cplmkPendaftaran.IsActive = true;
                    cplmkPendaftaran.IsDeleted = false;
                    cplmkPendaftaran.CreatedDate = DateTime.Now;
                    cplmkPendaftaran.UpdatedDate = DateTime.Now;
                    _cplmkPendaftaranService.Save(cplmkPendaftaran);
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
                return Json(new ServiceResponse { status = 400, message = "Anda sudah mendaftar matakuliah ini!" });
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