using MBKM.Services.MBKMServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MBKM.Entities.ViewModel;
using MBKM.Presentation.models;

using MBKM.Presentation.Helper;
using MBKM.Common.Helpers;
using MBKM.Entities.Models.MBKM;
using Newtonsoft.Json;

namespace MBKM.Presentation.Areas.Portal.Controllers
{

    [MBKMAuthorize]
    public class JadwalKuliahMahasiswaController : Controller
    {

        private IJadwalKuliahMahasiswaService _jkMhsService;
        private IJadwalKuliahService _jkService;
        private IMahasiswaService _mahasiswaService;
        private IPendaftaranMataKuliahService _pmkService;

        public JadwalKuliahMahasiswaController(IJadwalKuliahMahasiswaService jkMhsService, IJadwalKuliahService jkService, IMahasiswaService mahasiswaService, IPendaftaranMataKuliahService pmkService)
        {
            _jkService = jkService;
            _jkMhsService = jkMhsService;
            _mahasiswaService = mahasiswaService;
            _pmkService = pmkService;
        }

        // GET: Portal/JadwalKuliahMahasiswa
        public ActionResult Index()
        {
            //Session["nama"] = "Smitty Swagger Werben Jeger Man Jensen";
            //Session["email"] = "sabangsasabana@gmail.com";
            var mahasiswa = GetMahasiswaByEmail(Session["email"] as string);


            VMSemester model = _jkMhsService.getOngoingSemester(mahasiswa.JenjangStudi);
            return View(model);
        }


        [HttpPost]
        public ActionResult GetSemesterAll(int skip, int take, string search)
        {
            //return Json(, JsonRequestBehavior.AllowGet);



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


        public JsonResult SearchMataKuliah(DataTableAjaxPostModel model, string idProdi, string lokasi, string idFakultas, string jenjangStudi, string strm)
        {
            VMListJadwalKuliah vmList = _jkMhsService.ListJadwalKuliahMahasiswa(model, idProdi, lokasi, idFakultas, jenjangStudi, strm);
            return Json(new
            {
                // this is what datatables wants sending back
                draw = model.draw,
                recordsTotal = vmList.TotalCount,
                recordsFiltered = vmList.TotalFilterCount,
                data = vmList.gridDatas
            });
        }

        /*public ActionResult GetJadwalKuliah(string strm)
        {
            //string email = Session["email"] as string;

            var mahasiswa = GetMahasiswaByEmail(Session["email"] as string);

            var model = _jkMhsService.getOngoingSemester(mahasiswa.JenjangStudi);


            return new ContentResult { Content = JsonConvert.SerializeObject(_pmkService.GetFakultas(result.JenjangStudi, search)), ContentType = "application/json" };
        }*/

        [HttpPost]
        public ActionResult GetJadwalKuliah(string strm)
        {

            var mahasiswa = GetMahasiswaByEmail(Session["email"] as string);

            List<JadwalKuliah> MVJadwal = new List<JadwalKuliah>();
            List<string> mapJadwal = new List<string>();

            //int idProdiInt = Int32.Parse(idProdi);
            //int idFakultasInt = Int32.Parse(idFakultas);
            int strmInt = Int32.Parse(strm);

            foreach (var item in _pmkService.Find(dataMap =>
                dataMap.MahasiswaID == mahasiswa.ID &&

                //dataMap.JenjangStudi == mahasiswa.JenjangStudi &&
                dataMap.JadwalKuliahs.STRM == strmInt

            ).ToList())
            {
                if (!mapJadwal.Contains(item.JadwalKuliahs.NamaMataKuliah))
                {
                    MVJadwal.Add(item.JadwalKuliahs);
                    mapJadwal.Add(item.JadwalKuliahs.NamaMataKuliah);
                }
            }
            return new ContentResult { Content = JsonConvert.SerializeObject(MVJadwal), ContentType = "application/json" };
        }


        public Mahasiswa GetMahasiswaByEmail(string email)
        {
            return _mahasiswaService.Find(m => m.Email == email).FirstOrDefault();
        }

    }
}