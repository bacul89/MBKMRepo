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
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MBKM.Presentation.Areas.Admin.Controllers
{
    [MBKMAuthorize]
    public class MasterCPLController : Controller
    {
        private ILookupService _lookupService;
        private IMasterCapaianPembelajaranService _mcpService;
        private ICPLMatakuliahService _cplMatakuliah;
        private IJadwalKuliahService _jkService;
        private IPendaftaranMataKuliahService _pendaftaranMataKuliahService;
        public MasterCPLController(ICPLMatakuliahService cplMatakuliah, IJadwalKuliahService jkService, ILookupService lookupService, IMasterCapaianPembelajaranService mcpService, IPendaftaranMataKuliahService pendaftaranMataKuliahService)
        {
            _cplMatakuliah = cplMatakuliah;
            _lookupService = lookupService;
            _mcpService = mcpService;
            _jkService = jkService;
            _pendaftaranMataKuliahService = pendaftaranMataKuliahService;
        }

        // GET: Admin/MasterCPL
        public ActionResult Index()
        {
            IEnumerable<VMLookup> tempJenjang = _lookupService.getLookupByTipe("JenjangStudi");
            ViewData["Jenjang"] = tempJenjang;
            var RoleLogin = Session["RoleName"].ToString();
            ViewData["role"] = RoleLogin;
            if (RoleLogin == "Admin Fakultas")
            {
                ViewData["KodeFakultas"] = Session["KodeFakultas"].ToString();
                ViewData["NamaFakultas"] = Session["NamaFakultas"].ToString();
            }
            //else if (RoleLogin == "Kepala Program Studi")
            //{
            //    var prodiID = Session["KodeProdi"].ToString();
            //    var tempProdiID = Convert.ToInt64(prodiID);
            //    //var getJenjang = _jkService.Find(x => x.ProdiID == tempProdiID).FirstOrDefault();
            //    //var jenjangs = getJenjang.JenjangStudi;
            //    //ViewData["jenjangs"] = jenjangs;
            //    ViewData["KodeFakultas"] = Session["KodeFakultas"].ToString();
            //    ViewData["NamaFakultas"] = Session["NamaFakultas"].ToString();
            //    ViewData["KodeProdi"] = prodiID;
            //    ViewData["NamaProdi"] = Session["NamaProdi"].ToString();
            //}
            return View();
        }
        public ActionResult GetInformasiKampusByProdi()
        {
            var kodeProdi = Session["KodeProdi"] as string;
            var result = _pendaftaranMataKuliahService.GetInformasiKampusByIdProdi(kodeProdi);
            return new ContentResult { Content = JsonConvert.SerializeObject(result), ContentType = "application/json" };
        }

        [HttpPost]
        public JsonResult GetList(DataTableAjaxPostModel model, string prodi, string jenjang, string fakultas)
        {
            VMListMasterCPL vMListCPL = _mcpService.GetListMasterCPL(model, prodi, jenjang, fakultas);
            return Json(new
            {
                // this is what datatables wants sending back
                draw = model.draw,
                recordsTotal = vMListCPL.TotalCount,
                recordsFiltered = vMListCPL.TotalFilterCount,
                data = vMListCPL.gridDatas
            });
        }

        [HttpPost]
        public ActionResult PostDataCPL(MasterCapaianPembelajaran model)
        {
            try
            {
                if(GetMasterCPLByProdiKelompokDanKode(model.NamaProdi,model.Kelompok,model.Kode , model.JenjangStudi) == null)
                {
                    var LoopLokasi = _cplMatakuliah.GetProdiLocByFakultas(model.JenjangStudi, model.FakultasID, "");
                    var prodiTerpilih = LoopLokasi.Where(x => x.NamProdi == model.NamaProdi).ToList();
                    foreach (var d in prodiTerpilih)
                    {
                        MasterCapaianPembelajaran dataSimpan = new MasterCapaianPembelajaran();
                        dataSimpan.FakultasID = model.FakultasID;
                        dataSimpan.NamaFakultas = model.NamaFakultas;
                        dataSimpan.JenjangStudi = model.JenjangStudi;
                        dataSimpan.Kelompok = model.Kelompok;
                        dataSimpan.Kode = model.Kode;
                        dataSimpan.Capaian = model.Capaian;
                        dataSimpan.CreatedDate = DateTime.Now;
                        dataSimpan.UpdatedDate = DateTime.Now;
                        dataSimpan.IsDeleted = false;
                        dataSimpan.IsActive = model.IsActive;
                        dataSimpan.CreatedBy = Session["username"] as string;
                        dataSimpan.Lokasi = d.Lokasi;
                        dataSimpan.ProdiID = d.IDProdi;
                        dataSimpan.NamaProdi = d.NamProdi;
                        _mcpService.Save(dataSimpan);
                    }
                    return Json(new ServiceResponse { status = 200, message = "Pendaftaran CPL Berhasil!" });
                }
                else { return Json(new ServiceResponse { status = 400, message = "Gagal! CPL Ini Sudah Tersedia!" }); }

            }
            catch (Exception e)
            {
                return Json(new ServiceResponse { status = 500, message = e.Message });
            }

        }
        [HttpPost]
        public ActionResult PostUpdateCPL(MasterCapaianPembelajaran cpl)
        {
            MasterCapaianPembelajaran data = _mcpService.Get(cpl.ID);

            var LoopSemuaDatabyLokasi = _mcpService.Find(x => x.FakultasID == data.FakultasID
                && x.NamaProdi.Contains(data.NamaProdi)
                && x.JenjangStudi == data.JenjangStudi
                && x.Capaian == data.Capaian
                && x.Kode == data.Kode
                && x.Kelompok == data.Kelompok).ToList();
            if (GetMasterCPLByProdiKelompokDanKode(cpl.NamaProdi, cpl.Kelompok, cpl.Kode , cpl.JenjangStudi) == null)
            {

                foreach (var d in LoopSemuaDatabyLokasi)
                {
                    MasterCapaianPembelajaran newDAta = new MasterCapaianPembelajaran();
                    newDAta = d;
                    newDAta.Kelompok = cpl.Kelompok;
                    newDAta.Kode = cpl.Kode;
                    newDAta.Capaian = cpl.Capaian;
                    newDAta.IsActive = cpl.IsActive;
                    newDAta.UpdatedBy = Session["username"] as string;
                    newDAta.UpdatedDate = DateTime.Now;

                    try
                    {
                        _mcpService.Save(data);
                    }
                    catch (Exception e)
                    {
                        return Json(new ServiceResponse { status = 500, message = "Error saving" });
                    }


                }
            }

            else if (data.NamaProdi == cpl.NamaProdi && data.Kelompok == cpl.Kelompok && data.Kode == cpl.Kode && data.JenjangStudi == cpl.JenjangStudi && GetMasterCPLByCapaian(cpl.Capaian) == null)
            {
                foreach (var d in LoopSemuaDatabyLokasi)
                {
                    MasterCapaianPembelajaran newDAta = new MasterCapaianPembelajaran();
                    newDAta = d;
                    newDAta.Kelompok = cpl.Kelompok;
                    newDAta.Kode = cpl.Kode;
                    newDAta.Capaian = cpl.Capaian;
                    newDAta.IsActive = cpl.IsActive;
                    newDAta.UpdatedBy = Session["username"] as string;
                    newDAta.UpdatedDate = DateTime.Now;

                    try
                    {
                        _mcpService.Save(data);
                    }
                    catch (Exception e)
                    {
                        return Json(new ServiceResponse { status = 500, message = "Error saving" });
                    }
                }
            }

            //simpan tanpa edit
            else if (data.NamaProdi == cpl.NamaProdi && data.JenjangStudi == cpl.JenjangStudi && data.Kelompok == cpl.Kelompok && data.Kode == cpl.Kode && GetMasterCPLByCapaian(cpl.Capaian) != null)
            {
                foreach (var d in LoopSemuaDatabyLokasi)
                {
                    MasterCapaianPembelajaran newDAta = new MasterCapaianPembelajaran();
                    newDAta = d;
                    newDAta.Kelompok = cpl.Kelompok;
                    newDAta.Kode = cpl.Kode;
                    newDAta.Capaian = cpl.Capaian;
                    newDAta.IsActive = cpl.IsActive;
                    newDAta.UpdatedBy = Session["username"] as string;
                    newDAta.UpdatedDate = DateTime.Now;

                    try
                    {
                        _mcpService.Save(data);
                    }
                    catch (Exception e)
                    {
                        return Json(new ServiceResponse { status = 500, message = "Error saving" });
                    }
                }
                //return Json(new ServiceResponse { status = 500, message = "Update CPL Berhasil!" });
            }
            else { return Json(new ServiceResponse { status = 400, message = "Gagal! CPL Ini Sudah Tersedia!" }); }
            

            return Json(new ServiceResponse { status = 200, message = "Update CPL Berhasil!" });
        }
        [HttpPost]
        public ActionResult PostDeleteCPL(string fakultas, string prodi, string jenjang, string capaian, string kode, string kelompok)
        {
            var data = _mcpService.Find(x => x.FakultasID == fakultas
               && x.NamaProdi.Contains(prodi)
               && x.JenjangStudi == jenjang
               && x.Capaian == capaian
               && x.Kode == kode
               && x.Kelompok == kelompok
               ).ToList();
            foreach(var d in data)
            {
                MasterCapaianPembelajaran newDAta = new MasterCapaianPembelajaran();
                newDAta = d;
                newDAta.IsDeleted = true;
                newDAta.UpdatedBy = Session["username"] as string;
                newDAta.UpdatedDate = DateTime.Now;

                _mcpService.Save(newDAta);
            }
            
            return Json(new ServiceResponse { status = 200, message = "Master CPL Dihapus!" });
        }

        [HttpPost]
        public ActionResult GetProdi(string idFakultas, string search, string jenjang)
        {
            var prodi = _mcpService.GetProdiByFakultas(jenjang, idFakultas, search);
            return Json(prodi);
        }

        [HttpPost]
        public ActionResult ModalDetailMasterCpl(string fakultas, string prodi, string jenjang, string capaian, string kode, string kelompok)
        {
            var data = _mcpService.Find(x => x.FakultasID == fakultas
                && x.NamaProdi.Contains(prodi)
                && x.JenjangStudi == jenjang
                && x.Capaian == capaian
                && x.Kode == kode
                && x.Kelompok == kelompok
                ).FirstOrDefault();
            return new ContentResult { Content = JsonConvert.SerializeObject(data), ContentType = "application/json" };
        }
       
        [HttpPost]
        public ActionResult ModalEditMasterCpl(string fakultas, string prodi, string jenjang, string capaian, string kode, string kelompok)
        {
            /*var data = _mcpService.Get(id);*/
            var data = _mcpService.Find(x => x.FakultasID == fakultas 
                && x.NamaProdi.Contains(prodi) 
                && x.JenjangStudi == jenjang
                && x.Capaian == capaian
                && x.Kode == kode
                && x.Kelompok == kelompok
                ).FirstOrDefault();

            return new ContentResult { Content = JsonConvert.SerializeObject(data), ContentType = "application/json" };
        }
      
        public ActionResult GetFakultas(string search, string jenjang)
        {
            return Json(_mcpService.GetFakultas(jenjang, search), JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetProdiByFakultas(string idFakultas, string search, string jenjang)
        {
            return Json(_mcpService.GetProdiByFakultas(jenjang, idFakultas, search), JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetLokasiByProdi(string namaProdi, string search, string jenjang)
        {
            return Json(_mcpService.GetLokasiByProdi(jenjang, namaProdi, search), JsonRequestBehavior.AllowGet);
        }
        public ActionResult getLookupByTipe(string tipe)
        {
            return Json(_lookupService.getLookupByTipe(tipe), JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetProdiLocByFakultas(string JenjangStudi, string idFakultas, string search)
        {
            var data = _cplMatakuliah.GetProdiLocByFakultas(JenjangStudi, idFakultas, search);
            var data2 = data.GroupBy(z => new { z.NamProdi }).ToList();
            List<VMListProdi> final = new List<VMListProdi>();

            foreach (var d in data2)
            {
                final.Add(new VMListProdi
                {
                    NamProdi = d.Key.NamProdi
                });
            }
            return Json(final, JsonRequestBehavior.AllowGet);
        }
        public MasterCapaianPembelajaran GetMasterCPLByProdiKelompokDanKode(string namaProdi,string kelompok, string kode , string jenjang)
        {
            //ini untuk add
            return _mcpService.Find(m => m.NamaProdi == namaProdi && m.Kelompok == kelompok && m.Kode == kode && m.JenjangStudi == jenjang && m.IsDeleted == false).FirstOrDefault();
        }
        public MasterCapaianPembelajaran GetMasterCPLByProdiKelompokDanKodeUPDATE(string namaProdi, string kelompok, string kode, string capaian)
        {
            // ini utntuk update
            return _mcpService.Find(m => m.NamaProdi == namaProdi && m.Kelompok == kelompok && m.Kode == kode && m.Capaian == capaian && m.IsDeleted == false).FirstOrDefault();
        }
        public MasterCapaianPembelajaran GetMasterCPLByCapaian(string capaian)
        {
            // ini utntuk update
            return _mcpService.Find(m =>  m.Capaian == capaian && m.IsDeleted == false).FirstOrDefault();
        }
        public MasterCapaianPembelajaran GetMasterCPLByKode(string kode, string namaProdi)
        {
            // ini utntuk update
            return _mcpService.Find(m => m.NamaProdi == namaProdi && m.Kode == kode && m.IsDeleted == false).FirstOrDefault();
        }
        public MasterCapaianPembelajaran GetMasterCPLByKelompok(string kelompok, string namaProdi)
        {
            // ini utntuk update
            return _mcpService.Find(m => m.NamaProdi == namaProdi && m.Kelompok == kelompok && m.IsDeleted == false).FirstOrDefault();
        }
    }
}