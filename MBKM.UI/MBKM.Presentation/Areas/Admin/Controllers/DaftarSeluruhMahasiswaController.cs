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
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MBKM.Presentation.Areas.Admin.Controllers
{
    [MBKMAuthorize]
    public class DaftarSeluruhMahasiswaController : Controller
    {
        // GET: Admin/DaftarSeluruhMahasiswa
        private ILookupService _lookupService;
        private IDaftarAllMahasiswaService _mahasiswaService;
        private IPerjanjianKerjasamaService _perjanjianKerjasamaService;
        private IJenisKerjasamaModelService _jenisKerjasamaService;
        private IInformasiPertukaranService _informasiPertukaranService;
        private IPendaftaranMataKuliahService _pendaftaranMataKuliahService;
        //
        public DaftarSeluruhMahasiswaController(IPendaftaranMataKuliahService pendaftaranMataKuliahService, IInformasiPertukaranService informasiPertukaranService, IJenisKerjasamaModelService jenisKerjasamaService, ILookupService lookupService,IDaftarAllMahasiswaService mahasiswaService, IPerjanjianKerjasamaService perjanjianKerjasamaService)
        {
            _mahasiswaService = mahasiswaService;
            _lookupService = lookupService;
            _lookupService = lookupService;
            _jenisKerjasamaService = jenisKerjasamaService;
            _perjanjianKerjasamaService = perjanjianKerjasamaService;
            _informasiPertukaranService = informasiPertukaranService;
            _pendaftaranMataKuliahService = pendaftaranMataKuliahService;
        }

        public ActionResult Index()
        {
            //var list = $(
            //Dictionary<string, string> result = new Dictionary<string, string>();
            //foreach (var item in list)
            //{
            //    result.Add(item.JadwalUjianMBKMs.STRM + "", _absensiService.GetSemesterBySTRM(int.Parse(item.JadwalUjianMBKMs.STRM)));
            //}
            return View();
        }
        public ActionResult GetNoKerjasama(int Skip, int Length, string Search, string NamaInstansi)
        {
            return Json(_perjanjianKerjasamaService.getNoKerjasama(Skip, Length, Search, NamaInstansi), JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetBiaya(string NoKerjaSama)
        {
            return Json(_perjanjianKerjasamaService.getBiaya(NoKerjaSama), JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult GetList(DataTableAjaxPostModel model)
        {
            VMListDaftarAllMahasiswa vMListAllMhs = _mahasiswaService.GetListDaftarAllMahasiswa(model);
            return Json(new
            {
                // this is what datatables wants sending back
                draw = model.draw,
                recordsTotal = vMListAllMhs.TotalCount,
                recordsFiltered = vMListAllMhs.TotalFilterCount,
                data = vMListAllMhs.gridDatas
            });
        }
        public ActionResult getLookupByTipe(string tipe)
        {
            return Json(_lookupService.getLookupByTipe(tipe), JsonRequestBehavior.AllowGet);
        }


        public ActionResult ModalDetailMhs(int id)
        {
            var model = _mahasiswaService.Get(id);
            //batas edit internal(menempelkan data) semoga bisa
            var ip = _informasiPertukaranService.Find(_ => _.MahasiswaID == id).FirstOrDefault();
            if (ip != null)
            {
            ViewData["infoPertukaran"] = ip.JenisPertukaran;

            }
            ViewData["role"] = HttpContext.Session["RoleName"].ToString().ToLower();

            return View("ModalDetailMhs", model);
        }
        public ActionResult GetMHS(int id)
        {
            var result = _mahasiswaService.Get(id);
            return new ContentResult { Content = JsonConvert.SerializeObject(result), ContentType = "application/json" };
        }
        [HttpPost]
        public ActionResult UpdateKJ(Mahasiswa mhs)
        {
            Mahasiswa data = _mahasiswaService.Get(mhs.ID);
            data.NoKerjasama = mhs.NoKerjasama;
            data.BiayaKuliah = mhs.BiayaKuliah;
            data.StatusKerjasama = mhs.StatusKerjasama;
            data.UpdatedDate = DateTime.Now;
            data.FlagBayar = mhs.FlagBayar;
            data.UpdatedBy = Session["username"] as string;

            _mahasiswaService.Save(data);

            //return Json(data);
            return Json(new ServiceResponse { status = 200, message = "Kerjasama Berhasil Di Ubah" });
        }
        public ActionResult getLookupByValue(string tipe, string value)
        {
            return new ContentResult { Content = JsonConvert.SerializeObject(_lookupService.Find(l => l.Nilai == value && l.Tipe == tipe).FirstOrDefault()), ContentType = "application/json" };
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
            return new ContentResult { Content = JsonConvert.SerializeObject(_jenisKerjasamaService.Find(jk => jk.JenisPertukaran == program && !jk.JenisKerjasama.ToLower().Equals("eksternal dari luar atma jaya")).ToList()), ContentType = "application/json" };
        }
        [HttpPost]
        public ActionResult InsertInformasiPertukaran(InformasiPertukaran informasiPertukaran, Int64 id)
        {
            try
            {
                var ip = _informasiPertukaranService.Find(_ => _.MahasiswaID == id).FirstOrDefault();
                var mahasiswa = _mahasiswaService.Get(id);
                if (ip == null)
                {
                    informasiPertukaran.STRM = (int) _pendaftaranMataKuliahService.getOngoingSemester(mahasiswa.JenjangStudi).ID;
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
                }
                else
                {
                    ip.JudulAktivitas = informasiPertukaran.JudulAktivitas;
                    ip.LokasiTugas = informasiPertukaran.LokasiTugas;
                    ip.TanggalSK = informasiPertukaran.TanggalSK;
                    ip.NoSK = informasiPertukaran.NoSK;
                    ip.UpdatedDate = DateTime.Now;
                    ip.JenisPertukaran = informasiPertukaran.JenisPertukaran;
                    ip.JenisKerjasama = informasiPertukaran.JenisKerjasama;
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
    }
}