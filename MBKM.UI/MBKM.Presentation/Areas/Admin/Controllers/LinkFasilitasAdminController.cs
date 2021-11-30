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
    public class LinkFasilitasAdminController : Controller
    {
        // GET: Admin/LinkFasilitasAdmin
        private ILinkFasilitasService _linkFasilitasService;
        private ICPLMatakuliahService _cplMatakuliah;
        private ILookupService _lookupService;
        private IJadwalKuliahService _jkService;
        private IMasterCapaianPembelajaranService _mcpService;
        private IAbsensiService _absensiService;
        private IJadwalUjianMBKMDetailService _juDetailService;
        public LinkFasilitasAdminController(ICPLMatakuliahService cplMatakuliah, ILookupService lookupService, 
            IJadwalKuliahService jkService, IMasterCapaianPembelajaranService mcpService,
            ILinkFasilitasService linkFasilitasService, IAbsensiService absensiService, IJadwalUjianMBKMDetailService juDetailService)
        {
            _cplMatakuliah = cplMatakuliah;
            _jkService = jkService;
            _lookupService = lookupService;
            _linkFasilitasService = linkFasilitasService;
            _absensiService = absensiService;
            _mcpService = mcpService;
            _juDetailService = juDetailService;
        }
        public ActionResult Index()
        {
            var listSection = _linkFasilitasService.getSection();
            ViewData["listSection"] = listSection;
            return View(_absensiService.GetTahunSemester());
        }
        //batas ambil dari daftarhadirujain
        [HttpPost]
        public ActionResult GetSemesterAll(int skip, int take, string search)
        {

            var final = _jkService.GetSemesterAll(skip, take, search);
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

        public ActionResult getLookupByTipe(string tipe)
        {
            return Json(_lookupService.getLookupByTipe(tipe), JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetFakultas(string search, string JenjangStudi)
        {

            return Json(_mcpService.GetFakultas(JenjangStudi, search), JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetProdiByFakultas(string JenjangStudi, string idFakultas, string search)
        {
            return Json(_mcpService.GetProdiByFakultas(JenjangStudi, idFakultas, search), JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetLokasiByProdi(string JenjangStudi, string namaProdi, string search)
        {
            return Json(_mcpService.GetLokasiByProdi(JenjangStudi, namaProdi, search), JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult GetMataKuliahFlag(int skip, int take, string searchBy, string idProdi, string lokasi, string idFakultas, string jenjangStudi, string strm)
        {


            /*List<JadwalKuliah> MVJadwal = new List<JadwalKuliah>();
            List<string> mapJadwal = new List<string>();*/

            /*int idProdiInt = Int32.Parse(idProdi);
            int idFakultasInt = Int32.Parse(idFakultas);
            int strmInt = Int32.Parse(strm);*/

            List<JadwalKuliah> final = _jkService.GetMatkulFlag(skip, take, searchBy, idProdi, lokasi, idFakultas, jenjangStudi, strm).ToList();

            /*foreach (var item in final)
            {
                if (!mapJadwal.Contains(item.NamaMataKuliah))
                {
                    MVJadwal.Add(item);
                }
            }*/
            return new ContentResult { Content = JsonConvert.SerializeObject(final), ContentType = "application/json" };
        }
        [HttpPost]
        public ActionResult GetSection()
        {

            var final = _juDetailService.getSection();
            List<object> data = new List<object>();
            foreach (var p in final)
            {
                var q = new
                {
                    Nama = p.Nama,
                    //ID = p.ID
                };
                data.Add(q);
            }

            return Json(data);
        }
        //batas ambil dari daftarhadirujain
        public ActionResult GetTahunSemester()
        {
            return new ContentResult { Content = JsonConvert.SerializeObject(_absensiService.GetTahunSemester()), ContentType = "application/json" };
        }
        //public JsonResult GetSection()
        //{
        //    return Json(_linkFasilitasService.getSection(), JsonRequestBehavior.AllowGet);
        //}
        [HttpPost]
        public ActionResult GetMataKuliah(int skip, int take, string searchBy, string idProdi, string idFakultas)
        {

            var final = _linkFasilitasService.GetMatkul(skip, take, searchBy, idProdi, idFakultas);
            List<object> data = new List<object>();
            foreach (var p in final)
            {
                var q = new
                {
                    id = p.CRSE_ID,
                    text = p.DESCR,
                    kode = p.Expr1
                };
                data.Add(q);
            }

            return Json(data);
        }


        public ActionResult GetMatkulByLokasi(string search, string jenjangStudi, string prodi, string lokasi)
        {
            return new ContentResult { Content = JsonConvert.SerializeObject(_linkFasilitasService.GetMatkulByLokasi(search, jenjangStudi, prodi, lokasi)), ContentType = "application/json" };
        }


        /* Lookup --<> */
        //public ActionResult getLookupByTipe(string tipe)
        //{
        //    return Json(_lookupService.getLookupByTipe(tipe), JsonRequestBehavior.AllowGet);
        //}


        /* Attribute Kuliah --<> */
        //batas getnama november terbaru
        //public ActionResult GetFakultasByJenjangStudi(string search, string jenjangStudi)
        //{
        //    return new ContentResult { Content = JsonConvert.SerializeObject(_linkFasilitasService.GetFakultasByJenjangStudi(search, jenjangStudi)), ContentType = "application/json" };
        //}
        //public ActionResult GetProdiByFakultas(string search, string jenjangStudi, string fakultas)
        //{
        //    return new ContentResult { Content = JsonConvert.SerializeObject(_linkFasilitasService.GetProdiByFakultas(search, jenjangStudi, fakultas)), ContentType = "application/json" };
        //}
        //public ActionResult GetLokasiByProdi(string search, string jenjangStudi, string prodi)
        //{
        //    return new ContentResult { Content = JsonConvert.SerializeObject(_linkFasilitasService.GetLokasiByProdi(search, jenjangStudi, prodi)), ContentType = "application/json" };
        //}
        //batas bawah

        //public ActionResult GetFakultas(string search, string JenjangStudi)
        //{

        //    return Json(_mcpService.GetFakultas(JenjangStudi, search), JsonRequestBehavior.AllowGet);
        //}

        //public ActionResult GetProdiByFakultas(string JenjangStudi, string idFakultas, string search)
        //{
        //    return Json(_mcpService.GetProdiByFakultas(JenjangStudi, idFakultas, search), JsonRequestBehavior.AllowGet);
        //}

        //public ActionResult GetLokasiByProdi(string JenjangStudi, string namaProdi, string search)
        //{
        //    return Json(_mcpService.GetLokasiByProdi(JenjangStudi, namaProdi, search), JsonRequestBehavior.AllowGet);
        //}

        public ActionResult GetMataKuliahByProdi(string namaProdi, string lokasi)
        {
            //return new ContentResult { Content = JsonConvert.SerializeObject(_jkService.Find(jk => jk.NamaProdi == namaProdi && jk.Lokasi == lokasi && jk.NamaMataKuliah == search).ToList()), ContentType = "application/json" };
            List<JadwalKuliah> jks = new List<JadwalKuliah>();
            List<string> jadwalKuliahs = new List<string>();
            foreach (var item in _jkService.Find(jk => jk.NamaProdi == namaProdi && jk.Lokasi == lokasi).ToList())
            {
                if (!jadwalKuliahs.Contains(item.NamaMataKuliah))
                {
                    jks.Add(item);
                    jadwalKuliahs.Add(item.NamaMataKuliah);
                }
            }
            return new ContentResult { Content = JsonConvert.SerializeObject(jks), ContentType = "application/json" };

        }
        public ActionResult GetMataKuliahByID(string MataKuliahID)
        {
            return new ContentResult { Content = JsonConvert.SerializeObject(_jkService.Find(jk => jk.MataKuliahID == MataKuliahID).ToList()), ContentType = "application/json" };
        }
        public JsonResult SearchList(DataTableAjaxPostModel model, string idProdi, string lokasi, string idFakultas, string jenjangStudi, string idMatakuliah,string seksi,int strm)
        {
            VMLinkFasilitas vMLinkFasilitas = _linkFasilitasService.SearchListJadwalKuliah(model, idProdi, lokasi, idFakultas, jenjangStudi, idMatakuliah,seksi,strm);
            return Json(new
            {
                // this is what datatables wants sending back
                draw = model.draw,
                recordsTotal = vMLinkFasilitas.TotalCount,
                recordsFiltered = vMLinkFasilitas.TotalFilterCount,
                data = vMLinkFasilitas.gridDatas
            });
        }

        public ActionResult ModalUpdateLinkFasilitas(int id)
        {
           
            var data = _linkFasilitasService.Get(id);
            return View(data);
        }
        [HttpPost]
        public ActionResult UpdateLink(JadwalKuliah jdw)
        {
            JadwalKuliah data = _jkService.Get(jdw.ID);
            data.LinkMoodle = jdw.LinkMoodle;
            data.LinkAtmaZeds = jdw.LinkAtmaZeds;
            data.LinkTeams = jdw.LinkTeams;
            data.LinkOthers = jdw.LinkOthers;
            data.UpdatedDate = DateTime.Now;
            data.UpdatedBy = Session["username"] as string;




            _jkService.Save(data);

            //return Json(data);
            return Json(new ServiceResponse { status = 200, message = "Link Berhasil Di Update" });
        }
    }
}