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
    public class ApprovalPendaftaranMatakuliahController : Controller
    {
        IApprovalPendaftaranService _approvalPendaftaranService;
        IPendaftaranMataKuliahService _pendaftaranMataKuliahService;
        ICPLMKPendaftaranService _cPLMKPendaftaranService;
        IInformasiPertukaranService _informasiPertukaranService;
        IMahasiswaService _mahasiswaService;
        ICPLMatakuliahService _cPLMatakuliahService;
        IUserService _userService;

        public ApprovalPendaftaranMatakuliahController(IApprovalPendaftaranService approvalPendaftaranService, IPendaftaranMataKuliahService pendaftaranMataKuliahService, ICPLMKPendaftaranService cPLMKPendaftaranService, IInformasiPertukaranService informasiPertukaranService, IMahasiswaService mahasiswaService, ICPLMatakuliahService cPLMatakuliahService, IUserService userService)
        {
            _approvalPendaftaranService = approvalPendaftaranService;
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
            var tempInternal = _informasiPertukaranService.Find(x => x.MahasiswaID == data.PendaftaranMataKuliahs.MahasiswaID && x.JenisKerjasama.ToLower().Contains("internal")).Count();
            var tempMagang = _informasiPertukaranService.Find(x => x.MahasiswaID == data.PendaftaranMataKuliahs.MahasiswaID && x.JenisKerjasama.ToLower().Contains("magang")).Count();
            var tempKeluar = _informasiPertukaranService.Find(x => x.MahasiswaID == data.PendaftaranMataKuliahs.MahasiswaID && x.JenisKerjasama.ToLower().Contains("ke luar")).Count();

            IList<CPLMatakuliah> tempId = _cPLMatakuliahService.Find(x => x.IDMataKUliah == data.PendaftaranMataKuliahs.JadwalKuliahs.MataKuliahID).ToList();
            IList<CPLMatakuliah> capaianTujuan = tempId.Where(x => int.Parse(x.MasterCapaianPembelajarans.ProdiID) == data.PendaftaranMataKuliahs.JadwalKuliahs.ProdiID).ToList();
            
            if (data.CPLMatakuliahID != null)
            {
                IList<CPLMatakuliah> tempIDAsal = _cPLMatakuliahService.Find(x => x.IDMataKUliah == data.CPLMatakuliahs.IDMataKUliah).ToList();
                IList<CPLMatakuliah> capaianAsal = tempIDAsal.Where(x => int.Parse(x.MasterCapaianPembelajarans.ProdiID) == data.PendaftaranMataKuliahs.JadwalKuliahs.ProdiID).ToList();
                ViewData["capaianAsal"] = capaianAsal;
                ViewData["countCPAsal"] = capaianAsal.Count();

                ViewData["capaianTujuan"] = capaianTujuan;
                ViewData["countCPTujuan"] = capaianTujuan.Count();
            }else if(tempKeluar != 0)
            {
                IList<CPLMatakuliah> tempIDAsal = _cPLMatakuliahService.Find(x => x.IDMataKUliah == data.CPLMatakuliahs.IDMataKUliah).ToList();
                IList<CPLMatakuliah> capaianAsal = tempIDAsal.Where(x => int.Parse(x.MasterCapaianPembelajarans.ProdiID) == data.PendaftaranMataKuliahs.JadwalKuliahs.ProdiID).ToList();
                ViewData["capaianTujuan"] = capaianAsal;
                ViewData["countCPTujuan"] = capaianAsal.Count();

                ViewData["capaianAsal"] = capaianTujuan;
                ViewData["countCPAsal"] = capaianTujuan.Count();
            }
            else
            {
                ViewData["capaianTujuan"] = capaianTujuan;
                ViewData["countCPTujuan"] = capaianTujuan.Count();
            }


            if (data.PendaftaranMataKuliahs.mahasiswas.NIM != data.PendaftaranMataKuliahs.mahasiswas.NIMAsal)
            {
                ViewData["jenisProgram"] = "Pertukaran";
                ViewData["jenisKegiatan"] = "Eksternal";
            }
            else if(tempInternal != 0)
            {
                ViewData["jenisProgram"] = "Pertukaran";
                ViewData["jenisKegiatan"] = "Internal";
            }else if (tempMagang != 0)
            {
                ViewData["jenisProgram"] = "Non-Pertukaran";
                ViewData["jenisKegiatan"] = "Internal";
            }else if(tempKeluar != 0)
            {
                ViewData["jenisProgram"] = "Pertukaran";
                ViewData["jenisKegiatan"] = "Internal Ke Luar";
            }

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
                PendaftaranMataKuliah pendaftaran = _pendaftaranMataKuliahService.Get(TmpApproval.PendaftaranMataKuliahID);
                if (request.PendaftaranMataKuliahs.Kesenjangan == null)
                {
                    pendaftaran.Kesenjangan = "-";
                }
                else
                {
                    pendaftaran.Kesenjangan = request.PendaftaranMataKuliahs.Kesenjangan;
                }
                if (request.PendaftaranMataKuliahs.Hasil == null)
                {
                    pendaftaran.Hasil = "-";
                }
                else
                {
                    pendaftaran.Hasil = request.PendaftaranMataKuliahs.Hasil;
                }
                if (request.PendaftaranMataKuliahs.Konversi == null)
                {
                    pendaftaran.Konversi = "-";
                }
                else
                {
                    pendaftaran.Konversi = request.PendaftaranMataKuliahs.Konversi;
                }
                if (request.PendaftaranMataKuliahs.DosenID != 0)
                    {
                        pendaftaran.DosenID = request.PendaftaranMataKuliahs.DosenID;
                        pendaftaran.DosenPembimbing = request.PendaftaranMataKuliahs.DosenPembimbing;
                    }
                    pendaftaran.StatusPendaftaran = "APPROVED BY " + HttpContext.Session["RoleName"].ToString().ToUpper();
                    pendaftaran.UpdatedBy = HttpContext.Session["username"].ToString();
                    pendaftaran.UpdatedDate = DateTime.Now;
                try
                {
                    _pendaftaranMataKuliahService.Save(pendaftaran);
                }catch(Exception e)
                {
                    return Json(new ServiceResponse { status = 300, message = "Error Saat Menyimpan Data Pendaftaran" });
                }
              
                ApprovalPendaftaran tempHistoryApproval = new ApprovalPendaftaran();
                tempHistoryApproval.Approval = HttpContext.Session["RoleName"].ToString().ToUpper();
                if (request.PendaftaranMataKuliahs.Hasil == null)
                {
                    tempHistoryApproval.Catatan = "-";
                }
                else
                {
                    tempHistoryApproval.Catatan = request.PendaftaranMataKuliahs.mahasiswas.Catatan;
                }
                tempHistoryApproval.StatusPendaftaran = "APPROVED BY " + HttpContext.Session["RoleName"].ToString().ToUpper();
                tempHistoryApproval.IsActive = true;
                tempHistoryApproval.IsDeleted = false;
                tempHistoryApproval.PendaftaranMataKuliahID = TmpApproval.PendaftaranMataKuliahID;
                tempHistoryApproval.CreatedBy = HttpContext.Session["RoleName"].ToString().ToUpper();
                tempHistoryApproval.UpdatedBy = HttpContext.Session["RoleName"].ToString().ToUpper();
                tempHistoryApproval.UpdatedDate = DateTime.Now;
                tempHistoryApproval.CreatedDate = DateTime.Now;

                try
                {
                    _approvalPendaftaranService.Save(tempHistoryApproval);
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
                if (request.PendaftaranMataKuliahs.Kesenjangan == null)
                {
                    pendaftaran.Kesenjangan = "-";
                }
                else
                {
                    pendaftaran.Kesenjangan = request.PendaftaranMataKuliahs.Kesenjangan;
                }
                if(request.PendaftaranMataKuliahs.Hasil == null)
                {
                    pendaftaran.Hasil = "-";
                }
                else
                {
                pendaftaran.Hasil = request.PendaftaranMataKuliahs.Hasil;
                }
                if(request.PendaftaranMataKuliahs.Konversi == null) { 
                    pendaftaran.Konversi = "-";
                }
                else
                {
                    pendaftaran.Konversi = request.PendaftaranMataKuliahs.Konversi;
                }

                if (request.PendaftaranMataKuliahs.DosenID != 0)
                {
                    pendaftaran.DosenID = request.PendaftaranMataKuliahs.DosenID;
                    pendaftaran.DosenPembimbing = request.PendaftaranMataKuliahs.DosenPembimbing;
                }
                pendaftaran.StatusPendaftaran = "REJECTED BY " + HttpContext.Session["RoleName"].ToString().ToUpper();
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
                ApprovalPendaftaran tempHistoryApproval = new ApprovalPendaftaran();
                tempHistoryApproval.Approval = HttpContext.Session["RoleName"].ToString().ToUpper();
                if (request.PendaftaranMataKuliahs.mahasiswas.Catatan == null)
                {
                    tempHistoryApproval.Catatan = "-";
                }
                else
                {
                    tempHistoryApproval.Catatan = request.PendaftaranMataKuliahs.mahasiswas.Catatan;
                }
                tempHistoryApproval.StatusPendaftaran = "REJECTED BY " + HttpContext.Session["RoleName"].ToString().ToUpper();
                tempHistoryApproval.IsActive = true;
                tempHistoryApproval.IsDeleted = false;
                tempHistoryApproval.PendaftaranMataKuliahID = TmpApproval.PendaftaranMataKuliahID;
                tempHistoryApproval.CreatedBy = HttpContext.Session["RoleName"].ToString().ToUpper();
                tempHistoryApproval.UpdatedBy = HttpContext.Session["RoleName"].ToString().ToUpper();
                tempHistoryApproval.UpdatedDate = DateTime.Now;
                tempHistoryApproval.CreatedDate = DateTime.Now;
                try
                {
                    _approvalPendaftaranService.Save(tempHistoryApproval);
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
        public JsonResult GetCobaCoba()
        {
            var data1 = _mahasiswaService.GetNim();
            string[] data = data1.Split(new string[] { "MBKM" }, StringSplitOptions.None);
            int x = Int32.Parse(data[1]);
            _mahasiswaService.UpdateNim(x);
            var data2 = _mahasiswaService.GetNim();
            return Json(data2);
        }


    }
}