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

namespace MBKM.Presentation.Areas.Admin.Controllers
{

    [MBKMAuthorize]
    public class JadwalKuliahController : Controller
    {

        private IJadwalKuliahService _jkService;
        private ILookupService _lookupService;
        private IMasterCapaianPembelajaranService _mcpService;
        private IJadwalKuliahMahasiswaService _jkMhsService;



        public JadwalKuliahController(IJadwalKuliahService jkService, ILookupService lookupService, IMasterCapaianPembelajaranService mcpService, IJadwalKuliahMahasiswaService jkMahasiswaService)
        {
            _jkService = jkService;
            _lookupService = lookupService;
            _mcpService = mcpService;
            _jkMhsService = jkMahasiswaService;
        }


        // GET: Admin/JadwalKuliah
        public ActionResult Index()
        {


            VMSemester semester = _jkMhsService.getOngoingSemester("S1");

            ViewData["KodeSemester"] = semester.ID;
            ViewData["NamaSemester"] = semester.Nama;

            if (Session["KodeFakultas"].ToString() != "")
            {
                ViewData["KodeFakultas"] = Session["KodeFakultas"].ToString();
                ViewData["NamaFakultas"] = Session["NamaFakultas"].ToString();

                ViewData["KodeProdi"] = Session["KodeProdi"].ToString();
                ViewData["NamaProdi"] = Session["NamaProdi"].ToString();
            }
            return View();
            //Session["username"] = "Smitty Werben Jeger Man Jensen";
            //return View();
        }

        public ActionResult Manage()
        {
            VMSemester model = _jkMhsService.getOngoingSemester("S1");
            return View(model);
            //Session["username"] = "Smitty Werben Jeger Man Jensen";
            //return View();
        }

        public JsonResult SearchList(DataTableAjaxPostModel model, string idProdi, string lokasi, string idFakultas, string jenjangStudi, string strm)
        {
            VMListJadwalKuliah vmList = _jkService.SearchListJadwalKuliah(model, idProdi, lokasi, idFakultas, jenjangStudi, strm);
            return Json(new
            {
                // this is what datatables wants sending back
                draw = model.draw,
                recordsTotal = vmList.TotalCount,
                recordsFiltered = vmList.TotalFilterCount,
                data = vmList.gridDatas
            });
        }

        public JsonResult SearchMataKuliah(DataTableAjaxPostModel model, string idProdi, string lokasi, string idFakultas, string jenjangStudi, string strm)
        {
            VMListJadwalKuliah vmList = _jkService.SearchListMataKuliah(model, idProdi, lokasi, idFakultas, jenjangStudi, strm);
            return Json(new
            {
                // this is what datatables wants sending back
                draw = model.draw,
                recordsTotal = vmList.TotalCount,
                recordsFiltered = vmList.TotalFilterCount,
                data = vmList.gridDatas
            });
        }

        [HttpPost]
        public ActionResult Publish(int[] list)
        {

            var id = 0;
            try
            {
                for (int i = 0; i < list.Length; i++)
                {
                    id = list[i];

                    var data = _jkService.Get(id);
                    data.FlagOpen = true;
                    data.UpdatedBy = Session["username"] as string;

                    _jkService.Save(data);
                }


                return Json(new ServiceResponse { status = 200, message = "Data Berhasil Tersubmit" });
            }
            catch (Exception e)
            {
                return Json(new ServiceResponse { status = 500, message = "Data Gagal disubmit!!!" });
            }

        }

        [HttpPost]
        public ActionResult GetMataKuliah(string idProdi, string lokasi, string idFakultas, string jenjangStudi, string strm)
        {
            List<JadwalKuliah> MVJadwal = new List<JadwalKuliah>();
            List<string> mapJadwal = new List<string>();

            int idProdiInt = Int32.Parse(idProdi);
            int idFakultasInt = Int32.Parse(idFakultas);
            int strmInt = Int32.Parse(strm);

            foreach (var item in _jkService.Find(dataMap =>
                dataMap.ProdiID == idProdiInt &&
                dataMap.FakultasID == idFakultasInt &&
                dataMap.Lokasi == lokasi &&
                dataMap.JenjangStudi == jenjangStudi &&
                dataMap.STRM == strmInt
                //&& dataMap.FlagOpen == false

            ).ToList())
            {
                if (!mapJadwal.Contains(item.NamaMataKuliah))
                {
                    MVJadwal.Add(item);
                    mapJadwal.Add(item.NamaMataKuliah);
                }
            }
            return new ContentResult { Content = JsonConvert.SerializeObject(MVJadwal), ContentType = "application/json" };
        }


        /* Lookup --<> */
        public ActionResult getLookupByTipe(string tipe)
        {
            return Json(_lookupService.getLookupByTipe(tipe), JsonRequestBehavior.AllowGet);
        }

        /* Attribute Kuliah --<> */
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

        [HttpPost]
        public ActionResult GetSemesterAll2()
        {
            var result = _jkService.GetSemesterAll2();
            return new ContentResult { Content = JsonConvert.SerializeObject(result), ContentType = "application/json" };
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


       /* public ActionResult GetMataKuliahFlag(string idProdi, string lokasi, string idFakultas, string jenjangStudi, string strm)
        {
            List<JadwalKuliah> MVJadwal = new List<JadwalKuliah>();
            List<string> mapJadwal = new List<string>();

            int idProdiInt = Int32.Parse(idProdi);
            int idFakultasInt = Int32.Parse(idFakultas);
            int strmInt = Int32.Parse(strm);

            foreach (var item in _jkService.Find(dataMap =>
                dataMap.ProdiID == idProdiInt &&
                dataMap.FakultasID == idFakultasInt &&
                dataMap.Lokasi == lokasi &&
                dataMap.JenjangStudi == jenjangStudi &&
                dataMap.STRM == strmInt &&
                dataMap.FlagOpen == false

            ).ToList())
            {
                if (!mapJadwal.Contains(item.NamaMataKuliah))
                {
                    MVJadwal.Add(item);
                    mapJadwal.Add(item.NamaMataKuliah);
                }
            }
            return new ContentResult { Content = JsonConvert.SerializeObject(MVJadwal), ContentType = "application/json" };
        }*/


    }
}