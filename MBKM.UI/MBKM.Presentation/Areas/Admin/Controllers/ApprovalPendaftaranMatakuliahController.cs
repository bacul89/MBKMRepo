using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MBKM.Presentation.Helper;
using System.Web.Mvc;
using MBKM.Services.MBKMServices;
using MBKM.Common.Helpers;
using MBKM.Entities.ViewModel;
using MBKM.Entities.Models.MBKM;
using MBKM.Presentation.models;
using MBKM.Services;

namespace MBKM.Presentation.Areas.Admin.Controllers
{
    [MBKMAuthorize]
    public class ApprovalPendaftaranMatakuliahController : Controller
    {

        IPendaftaranMataKuliahService _pendaftaranMataKuliahService;
        ICPLMKPendaftaranService _cPLMKPendaftaranService;
        IInformasiPertukaranService _informasiPertukaranService;
        IMahasiswaService _mahasiswaService;
        ICPLMatakuliahService _cPLMatakuliahService;
        IUserService _userService;

        public ApprovalPendaftaranMatakuliahController(IPendaftaranMataKuliahService pendaftaranMataKuliahService, ICPLMKPendaftaranService cPLMKPendaftaranService, IInformasiPertukaranService informasiPertukaranService, IMahasiswaService mahasiswaService, ICPLMatakuliahService cPLMatakuliahService, IUserService userService)
        {
            _pendaftaranMataKuliahService = pendaftaranMataKuliahService;
            _cPLMKPendaftaranService = cPLMKPendaftaranService;
            _informasiPertukaranService = informasiPertukaranService;
            _mahasiswaService = mahasiswaService;
            _cPLMatakuliahService = cPLMatakuliahService;
            _userService = userService;
        }



        // GET: Admin/ApprovalPendaftaranMatakuliah
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult DetailCPL(int id)
        {
            var data = _cPLMKPendaftaranService.Find(x => x.IsDeleted == false && x.PendaftaranMataKuliahID == id).First();
            if(data.PendaftaranMataKuliahs.mahasiswas.NIM != data.PendaftaranMataKuliahs.mahasiswas.NIMAsal)
            {
                ViewData["jenisProgram"] = "Non-Pertukaran";
                ViewData["jenisKegiatan"] = "Internal";
                /*ViewData["jenisProgram"] = "Pertukaran";
                ViewData["jenisKegiatan"] = "Eksternal";*/
            }
            else if(data.PendaftaranMataKuliahs.mahasiswas.NoKerjasama != null)
            {
                ViewData["jenisProgram"] = "Pertukaran";
                ViewData["jenisKegiatan"] = "Internal";
            }else if (data.PendaftaranMataKuliahs.mahasiswas.NoKerjasama == null)
            {
                ViewData["jenisProgram"] = "Non-Pertukaran";
                ViewData["jenisKegiatan"] = "Internal";
            }
            var IDMakul = data.PendaftaranMataKuliahs.JadwalKuliahs.MataKuliahID;
            IList<CPLMatakuliah> capaianTujuan = _cPLMatakuliahService.Find(x => x.IDMataKUliah == data.PendaftaranMataKuliahs.JadwalKuliahs.MataKuliahID).ToList();
            ViewData["capaianTujuan"] = capaianTujuan;
            ViewData["countCPTujuan"] = capaianTujuan.Count();
            return View(data);
        }
        
        [HttpPost]
        public JsonResult GetAllApprovalPMK(DataTableAjaxPostModel model)
        {
            var data = _pendaftaranMataKuliahService.GetPendaftaranMahasiswaDataTable(model);

            return Json(new
                {
                    draw = model.draw,
                    recordsTotal = data.TotalCount,
                    recordsFiltered = data.TotalFilterCount,
                    data = data.gridDatas
                }
                );
        }

        [HttpPost]
        public JsonResult PostDataApprovalAccepted(CPLMKPendaftaran request)
        {
            try
            {
                CPLMKPendaftaran TmpApproval = _cPLMKPendaftaranService.Get(request.ID);
                var idPEndaftaran = TmpApproval.PendaftaranMataKuliahID;
                PendaftaranMataKuliah pendaftaran = _pendaftaranMataKuliahService.Get(TmpApproval.PendaftaranMataKuliahID);
                    pendaftaran.Kesenjangan = request.PendaftaranMataKuliahs.Kesenjangan;
                    pendaftaran.Hasil = request.PendaftaranMataKuliahs.Hasil;
                    pendaftaran.Konversi = request.PendaftaranMataKuliahs.Konversi;
                    if(request.PendaftaranMataKuliahs.DosenID != 0)
                    {
                        pendaftaran.DosenID = request.PendaftaranMataKuliahs.DosenID;
                        pendaftaran.DosenPembimbing = request.PendaftaranMataKuliahs.DosenPembimbing;
                    }
                    pendaftaran.StatusPendaftaran = "Approved By " + HttpContext.Session["RoleName"].ToString();
                    pendaftaran.UpdatedBy = HttpContext.Session["username"].ToString();
                    pendaftaran.UpdatedDate = DateTime.Now;
                try
                {
                    _pendaftaranMataKuliahService.Save(pendaftaran);
                }catch(Exception e)
                {
                    return Json(new ServiceResponse { status = 300, message = "Error Saat Menyimpan Data Pendaftaran" });
                }
                Mahasiswa tmpMahasiswa = _mahasiswaService.Get(pendaftaran.MahasiswaID);
                    tmpMahasiswa.Catatan = request.PendaftaranMataKuliahs.mahasiswas.Catatan;
                    tmpMahasiswa.UpdatedBy = HttpContext.Session["username"].ToString();
                    tmpMahasiswa.UpdatedDate = DateTime.Now;
                try
                {
                    _mahasiswaService.Save(tmpMahasiswa);
                }
                catch (Exception e)
                {
                    return Json(new ServiceResponse { status = 300, message = "Error Saat Menyimpan Data Pendaftaran" });
                }
                return Json(new ServiceResponse { status = 200, message = "Done" });
            }
            catch(Exception e)
            {
                return Json(new ServiceResponse { status = 500, message = "Error" });
            }
        }


        [HttpPost]
        public JsonResult PostDataApprovalRejected(CPLMKPendaftaran request)
        {
            try
            {
                CPLMKPendaftaran TmpApproval = _cPLMKPendaftaranService.Get(request.ID);
                var idPEndaftaran = TmpApproval.PendaftaranMataKuliahID;
                PendaftaranMataKuliah pendaftaran = _pendaftaranMataKuliahService.Get(TmpApproval.PendaftaranMataKuliahID);
                pendaftaran.Kesenjangan = request.PendaftaranMataKuliahs.Kesenjangan;
                pendaftaran.Hasil = request.PendaftaranMataKuliahs.Hasil;
                pendaftaran.Konversi = request.PendaftaranMataKuliahs.Konversi;
                if (request.PendaftaranMataKuliahs.DosenID != 0)
                {
                    pendaftaran.DosenID = request.PendaftaranMataKuliahs.DosenID;
                    pendaftaran.DosenPembimbing = request.PendaftaranMataKuliahs.DosenPembimbing;
                }
                pendaftaran.StatusPendaftaran = "Rejected By " + HttpContext.Session["RoleName"].ToString();
                pendaftaran.UpdatedBy = HttpContext.Session["username"].ToString();
                pendaftaran.UpdatedDate = DateTime.Now;
                try
                {
                    _pendaftaranMataKuliahService.Save(pendaftaran);
                }
                catch (Exception e)
                {
                    return Json(new ServiceResponse { status = 300, message = "Error Saat Menyimpan Data Pendaftaran" });
                }
                Mahasiswa tmpMahasiswa = _mahasiswaService.Get(pendaftaran.MahasiswaID);
                tmpMahasiswa.Catatan = request.PendaftaranMataKuliahs.mahasiswas.Catatan;
                tmpMahasiswa.UpdatedBy = HttpContext.Session["username"].ToString();
                tmpMahasiswa.UpdatedDate = DateTime.Now;
                try
                {
                    _mahasiswaService.Save(tmpMahasiswa);
                }
                catch (Exception e)
                {
                    return Json(new ServiceResponse { status = 300, message = "Error Saat Menyimpan Data Pendaftaran" });
                }
                return Json(new ServiceResponse { status = 200, message = "Done" });
            }
            catch (Exception e)
            {
                return Json(new ServiceResponse { status = 500, message = "Error" });
            }
        }


        [HttpPost]
        public JsonResult GetAllDataDosen(int length, int skip, string search)
        {

            var final = _userService.getDosenList(skip, length, search);
            List<object> data = new List<object>();
            foreach (var p in final)
            {
                var q = new
                {
                    id = p.ID,
                    text = p.UserName
                };
                data.Add(q);
            }

            return Json(data);
        }

        [HttpPost]
        public JsonResult GetCobaCoba(int id)
        {
            var data = _userService.getDosenList(0,10,"da");
            return Json(data);
        }


    }
}