using MBKM.Common.Helpers;
using MBKM.Entities.Models;
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
    public class PenilaianController : Controller
    {
        private IPendaftaranMataKuliahService _pendaftaranMataKuliahService;
        private IJadwalKuliahService _jadwalKuliahService;
        private IAbsensiService _absensiService;
        private INilaiKuliahService _nilaiKuliahService;
        private INilaiSubCWService _nilaiSubCWService;
        public PenilaianController(INilaiSubCWService nilaiSubCWService, INilaiKuliahService nilaiKuliahService, IAbsensiService absensiService, IJadwalKuliahService jadwalKuliahService, IPendaftaranMataKuliahService pendaftaranMataKuliahService)
        {
            _pendaftaranMataKuliahService = pendaftaranMataKuliahService;
            _jadwalKuliahService = jadwalKuliahService;
            _absensiService = absensiService;
            _nilaiKuliahService = nilaiKuliahService;
            _nilaiSubCWService = nilaiSubCWService;
        }
        // GET: Admin/UserManage
        public ActionResult Index()
        {
            return View(_absensiService.GetTahunSemester());
        } 
        [HttpPost]
        public ActionResult DaftarNilaiRincian(int idJadwalKuliah)
        {
            var model = _nilaiKuliahService.GetDNR(idJadwalKuliah);
            return View(model);
        }
        public ActionResult DetailPenilaian(int idMatkul)
        {
            var model = _jadwalKuliahService.Get(idMatkul);
            return View(model);
        }
        public ActionResult GetKelas(string jenjangStudi, string fakultas, string lokasi, string prodi, string matkul, string seksi)
        {
            var result = _jadwalKuliahService.Find(_ => _.JenjangStudi == jenjangStudi && _.NamaFakultas == fakultas && _.Lokasi == lokasi && _.NamaProdi == prodi && _.KodeMataKuliah + " - " + _.NamaMataKuliah == matkul && _.ClassSection == seksi).ToList();
            return new ContentResult { Content = JsonConvert.SerializeObject(result), ContentType = "application/json" };
        }
        public ActionResult GetBobot(string idMatkul)
        {
            var result = _nilaiKuliahService.GetBobot(idMatkul);
            return new ContentResult { Content = JsonConvert.SerializeObject(result), ContentType = "application/json" };
        }
        public ActionResult GetSubBobot(string idMatkul)
        {
            var result = _nilaiKuliahService.GetSubBobot(idMatkul);
            return new ContentResult { Content = JsonConvert.SerializeObject(result), ContentType = "application/json" };
        }
        public ActionResult GetSemester(int strm)
        {
            var result = _absensiService.GetSemesterBySTRM(strm);
            return new ContentResult { Content = JsonConvert.SerializeObject(result), ContentType = "application/json" };
        }
        public ActionResult GetNilai(IEnumerable<int> ids, int idJadwalKuliah)
        {
            var result = new List<NilaiKuliah>();
            if (ids == null) return null;
            foreach (var id in ids)
            {
                var tmp = _nilaiKuliahService.Find(_ => _.MahasiswaID == id && _.JadwalKuliahID == idJadwalKuliah && !_.IsDeleted).FirstOrDefault();
                if (tmp != null) result.Add(tmp);
            }
            return new ContentResult { Content = JsonConvert.SerializeObject(result), ContentType = "application/json" };
        }
        public ActionResult GetNilaiSubCW(string headCW, int idJadwalKuliah, IEnumerable<int> ids)
        {
            var result = new List<NilaiSubCW>();
            if (ids == null) return null;
            foreach (var id in ids)
            {
                var tmp = _nilaiSubCWService.Find(_ => _.HeadCW == headCW && _.NilaiKuliahs.JadwalKuliahID == idJadwalKuliah && _.NilaiKuliahs.MahasiswaID == id && !_.IsDeleted).FirstOrDefault();
                if (tmp != null) result.Add(tmp);
            }
            return new ContentResult { Content = JsonConvert.SerializeObject(result), ContentType = "application/json" };
        }
        public ActionResult GetMahasiswa(int idJadwalKuliah)
        {
            var result = _pendaftaranMataKuliahService.Find(_ => _.JadwalKuliahID == idJadwalKuliah && _.StatusPendaftaran == "ACCEPTED BY MAHASISWA");
            return new ContentResult { Content = JsonConvert.SerializeObject(result), ContentType = "application/json" };
        }
        public ActionResult ResetNilaiMahasiswa(IEnumerable<int> ids, int idJadwalKuliah, string flag)
        {
            try
            {
                foreach (var id in ids)
                {
                    var nilai = _nilaiKuliahService.Find(_ => _.MahasiswaID == id && _.JadwalKuliahID == idJadwalKuliah && !_.IsDeleted).FirstOrDefault();
                    if (flag.Equals("MT")) nilai.UTS = 0;
                    else if (flag.Equals("CW1")) nilai.CW1 = 0;
                    else if (flag.Equals("CW2")) nilai.CW2 = 0;
                    else if (flag.Equals("CW3")) nilai.CW3 = 0;
                    else if (flag.Equals("CW4")) nilai.CW4 = 0;
                    else if (flag.Equals("CW5")) nilai.CW5 = 0;
                    else if (flag.Equals("FE")) nilai.Final = 0;
                    _nilaiKuliahService.Save(nilai);
                }
                return Json(new ServiceResponse { status = 200, message = "Nilai berhasil direset!" });
            }
            catch (Exception e)
            {
                return Json(new ServiceResponse { status = 500, message = e.Message });
            }
        }
        public ActionResult ResetSubNilaiMahasiswa(IEnumerable<int> ids, int idJadwalKuliah, string flag)
        {
            try
            {
                foreach (var id in ids)
                {
                    var nilai = _nilaiSubCWService.Find(_ => _.NilaiKuliahs.MahasiswaID == id && _.NilaiKuliahs.JadwalKuliahID == idJadwalKuliah && _.HeadCW == flag && !_.IsDeleted).FirstOrDefault();
                    nilai.IsDeleted = true;
                    _nilaiSubCWService.Save(nilai);
                }
                return Json(new ServiceResponse { status = 200, message = "Nilai berhasil direset!" });
            }
            catch (Exception e)
            {
                return Json(new ServiceResponse { status = 500, message = e.Message });
            }
        }
        public ActionResult SubmitNilai(IEnumerable<NilaiKuliah> listNilai, NilaiSubCW[] CW1, NilaiSubCW[] CW2, NilaiSubCW[] CW3, NilaiSubCW[] CW4, NilaiSubCW[] CW5, bool isSub, int idJadwalKuliah)
        {
            try
            {
                InsertNilai(listNilai, CW1, CW2, CW3, CW4, CW5, isSub);
                var tmp = _jadwalKuliahService.Get(idJadwalKuliah);
                tmp.IsActive = false;
                _jadwalKuliahService.Save(tmp);
                return Json(new ServiceResponse { status = 200, message = "Nilai berhasil disubmit!" });
            }
            catch (Exception e)
            {
                return Json(new ServiceResponse { status = 500, message = e.Message });
            }
        }
        public ActionResult UnlockSubmit(int idJadwalKuliah)
        {
            try
            {
                //if ((Session["RoleName"] as string).Equals("Kepala Program Studi"))
                //{
                    var tmp = _jadwalKuliahService.Get(idJadwalKuliah);
                    tmp.IsActive = true;
                    _jadwalKuliahService.Save(tmp);
                    return Json(new ServiceResponse { status = 200, message = "Unlock berhasil!" });
                //}
                //return Json(new ServiceResponse { status = 400, message = "Hanya kepala BAA yang dapat melakukan unlock!" });
            }
            catch (Exception e)
            {
                return Json(new ServiceResponse { status = 500, message = e.Message });
            }
        }
        public ActionResult InsertNilai(IEnumerable<NilaiKuliah> listNilai, NilaiSubCW[] CW1, NilaiSubCW[] CW2, NilaiSubCW[] CW3, NilaiSubCW[] CW4, NilaiSubCW[] CW5, bool isSub)
        {
            try
            {
                int index = 0;
                foreach (var item in listNilai)
                {
                    var tmp = item;
                    var cek = _nilaiKuliahService.Find(_ => _.JadwalKuliahID == item.JadwalKuliahID && _.MahasiswaID == item.MahasiswaID && _.IsActive && !_.IsDeleted).FirstOrDefault();
                    if (cek == null)
                    {
                        tmp.FlagCetak = true;
                        tmp.CreatedDate = DateTime.Now;
                        tmp.IsActive = true;
                        tmp.IsDeleted = false;
                        tmp.UpdatedDate = DateTime.Now;
                    }
                    if (isSub)
                    {
                        var tmpCW = CW1[index];
                        var cek2 = _nilaiSubCWService.Find(_ => _.NilaiKuliahs.JadwalKuliahID == item.JadwalKuliahID && _.NilaiKuliahs.MahasiswaID == item.MahasiswaID && _.HeadCW == "CW1" && _.IsActive && !_.IsDeleted).FirstOrDefault();
                        if (cek2 != null)
                        {
                            cek2.CWSub1 = tmpCW.CWSub1;
                            cek2.CWSub2 = tmpCW.CWSub2;
                            cek2.CWSub3 = tmpCW.CWSub3;
                            cek2.CWSub4 = tmpCW.CWSub4;
                            cek2.CWSub5 = tmpCW.CWSub5;
                            cek2.CWSub6 = tmpCW.CWSub6;
                            cek2.CWSub7 = tmpCW.CWSub7;
                            cek2.CWSub8 = tmpCW.CWSub8;
                            cek2.CWSub9 = tmpCW.CWSub9;
                            cek2.CWSub10 = tmpCW.CWSub10;
                            cek2.NilaiTotal = tmpCW.NilaiTotal;
                            cek2.UpdatedDate = DateTime.Now;
                            _nilaiSubCWService.Save(cek2);
                        }
                        else
                        {
                            tmp.NilaiSubCWs = new List<NilaiSubCW>();
                            tmpCW.CreatedDate = DateTime.Now;
                            tmpCW.IsActive = true;
                            tmpCW.IsDeleted = false;
                            tmpCW.UpdatedDate = DateTime.Now;
                            tmp.NilaiSubCWs.Add(tmpCW);
                        }

                        tmpCW = null;

                        tmpCW = CW2[index];
                        tmpCW.NilaiKuliahID = tmp.ID;
                        cek2 = _nilaiSubCWService.Find(_ => _.NilaiKuliahs.JadwalKuliahID == item.JadwalKuliahID && _.NilaiKuliahs.MahasiswaID == item.MahasiswaID && _.HeadCW == "CW2" && _.IsActive && !_.IsDeleted).FirstOrDefault();
                        if (cek2 != null)
                        {
                            cek2.CWSub1 = tmpCW.CWSub1;
                            cek2.CWSub2 = tmpCW.CWSub2;
                            cek2.CWSub3 = tmpCW.CWSub3;
                            cek2.CWSub4 = tmpCW.CWSub4;
                            cek2.CWSub5 = tmpCW.CWSub5;
                            cek2.CWSub6 = tmpCW.CWSub6;
                            cek2.CWSub7 = tmpCW.CWSub7;
                            cek2.CWSub8 = tmpCW.CWSub8;
                            cek2.CWSub9 = tmpCW.CWSub9;
                            cek2.CWSub10 = tmpCW.CWSub10;
                            cek2.NilaiTotal = tmpCW.NilaiTotal;
                            cek2.UpdatedDate = DateTime.Now;
                            _nilaiSubCWService.Save(cek2);
                        }
                        else
                        {
                            tmpCW.CreatedDate = DateTime.Now;
                            tmpCW.IsActive = true;
                            tmpCW.IsDeleted = false;
                            tmpCW.UpdatedDate = DateTime.Now;
                            tmp.NilaiSubCWs.Add(tmpCW);
                        }

                        tmpCW = null;

                        tmpCW = CW3[index];
                        tmpCW.NilaiKuliahID = tmp.ID;
                        cek2 = _nilaiSubCWService.Find(_ => _.NilaiKuliahs.JadwalKuliahID == item.JadwalKuliahID && _.NilaiKuliahs.MahasiswaID == item.MahasiswaID && _.HeadCW == "CW3" && _.IsActive && !_.IsDeleted).FirstOrDefault();
                        if (cek2 != null)
                        {
                            cek2.CWSub1 = tmpCW.CWSub1;
                            cek2.CWSub2 = tmpCW.CWSub2;
                            cek2.CWSub3 = tmpCW.CWSub3;
                            cek2.CWSub4 = tmpCW.CWSub4;
                            cek2.CWSub5 = tmpCW.CWSub5;
                            cek2.CWSub6 = tmpCW.CWSub6;
                            cek2.CWSub7 = tmpCW.CWSub7;
                            cek2.CWSub8 = tmpCW.CWSub8;
                            cek2.CWSub9 = tmpCW.CWSub9;
                            cek2.CWSub10 = tmpCW.CWSub10;
                            cek2.NilaiTotal = tmpCW.NilaiTotal;
                            cek2.UpdatedDate = DateTime.Now;
                            _nilaiSubCWService.Save(cek2);
                        }
                        else
                        {
                            tmpCW.CreatedDate = DateTime.Now;
                            tmpCW.IsActive = true;
                            tmpCW.IsDeleted = false;
                            tmpCW.UpdatedDate = DateTime.Now;
                            tmp.NilaiSubCWs.Add(tmpCW);
                        }

                        tmpCW = null;

                        tmpCW = CW4[index];
                        tmpCW.NilaiKuliahID = tmp.ID;
                        cek2 = _nilaiSubCWService.Find(_ => _.NilaiKuliahs.JadwalKuliahID == item.JadwalKuliahID && _.NilaiKuliahs.MahasiswaID == item.MahasiswaID && _.HeadCW == "CW4" && _.IsActive && !_.IsDeleted).FirstOrDefault();
                        if (cek2 != null)
                        {
                            cek2.CWSub1 = tmpCW.CWSub1;
                            cek2.CWSub2 = tmpCW.CWSub2;
                            cek2.CWSub3 = tmpCW.CWSub3;
                            cek2.CWSub4 = tmpCW.CWSub4;
                            cek2.CWSub5 = tmpCW.CWSub5;
                            cek2.CWSub6 = tmpCW.CWSub6;
                            cek2.CWSub7 = tmpCW.CWSub7;
                            cek2.CWSub8 = tmpCW.CWSub8;
                            cek2.CWSub9 = tmpCW.CWSub9;
                            cek2.CWSub10 = tmpCW.CWSub10;
                            cek2.NilaiTotal = tmpCW.NilaiTotal;
                            cek2.UpdatedDate = DateTime.Now;
                            _nilaiSubCWService.Save(cek2);
                        }
                        else
                        {
                            tmpCW.CreatedDate = DateTime.Now;
                            tmpCW.IsActive = true;
                            tmpCW.IsDeleted = false;
                            tmpCW.UpdatedDate = DateTime.Now;
                            tmp.NilaiSubCWs.Add(tmpCW);
                        }

                        tmpCW = null;

                        tmpCW = CW5[index];
                        tmpCW.NilaiKuliahID = tmp.ID;
                        cek2 = _nilaiSubCWService.Find(_ => _.NilaiKuliahs.JadwalKuliahID == item.JadwalKuliahID && _.NilaiKuliahs.MahasiswaID == item.MahasiswaID && _.HeadCW == "CW5" && _.IsActive && !_.IsDeleted).FirstOrDefault();
                        if (cek2 != null)
                        {
                            cek2.CWSub1 = tmpCW.CWSub1;
                            cek2.CWSub2 = tmpCW.CWSub2;
                            cek2.CWSub3 = tmpCW.CWSub3;
                            cek2.CWSub4 = tmpCW.CWSub4;
                            cek2.CWSub5 = tmpCW.CWSub5;
                            cek2.CWSub6 = tmpCW.CWSub6;
                            cek2.CWSub7 = tmpCW.CWSub7;
                            cek2.CWSub8 = tmpCW.CWSub8;
                            cek2.CWSub9 = tmpCW.CWSub9;
                            cek2.CWSub10 = tmpCW.CWSub10;
                            cek2.NilaiTotal = tmpCW.NilaiTotal;
                            cek2.UpdatedDate = DateTime.Now;
                            _nilaiSubCWService.Save(cek2);
                        }
                        else
                        {
                            tmpCW.CreatedDate = DateTime.Now;
                            tmpCW.IsActive = true;
                            tmpCW.IsDeleted = false;
                            tmpCW.UpdatedDate = DateTime.Now;
                            tmp.NilaiSubCWs.Add(tmpCW);
                        }
                    }
                    index++;
                    if (cek == null)
                    {
                        _nilaiKuliahService.Save(tmp);
                    }
                    else
                    {
                        cek.UTS = tmp.UTS;
                        cek.CW1 = tmp.CW1;
                        cek.CW2 = tmp.CW2;
                        cek.CW3 = tmp.CW3;
                        cek.CW4 = tmp.CW4;
                        cek.CW5 = tmp.CW5;
                        cek.Final = tmp.Final;
                        cek.NilaiTotal = tmp.NilaiTotal;
                        cek.Grade = tmp.Grade;
                        cek.UpdatedDate = DateTime.Now;
                        _nilaiKuliahService.Save(cek);
                    }
                }
                return Json(new ServiceResponse { status = 200, message = "Nilai berhasil disimpan!" });
            }
            catch (Exception e)
            {
                return Json(new ServiceResponse { status = 500, message = e.Message });
            }
        }
        public VMSemester getOngoingSemester(string jenjangStudi)
        {
            return _pendaftaranMataKuliahService.getOngoingSemester(jenjangStudi);
        }
    }
}