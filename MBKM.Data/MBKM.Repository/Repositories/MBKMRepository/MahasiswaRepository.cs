
using MBKM.Common.Interfaces.RepoInterfaces.MBKMRepoInterfaces;
using MBKM.Entities.Models.MBKM;
using MBKM.Entities.ViewModel;
using MBKM.Repository.BaseRepository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Dynamic;

namespace MBKM.Repository.Repositories.MBKMRepository
{
    public class MahasiswaRepository : GenericRepository<Mahasiswa>, IMahasiswaRepository
    {
        public MahasiswaRepository(DbContext _db) : base(_db)
        {
        }
        public VMLogin getLoginInternal(string StudentID, string Password)
        {
            using (var context = new MBKMContext())
            {
                var studentIdParameter = new SqlParameter("@StudentID", StudentID);
                var PasswordParameter = new SqlParameter("@Password", Password);

                var result = context.Database
                    .SqlQuery<VMLogin>("GetLoginInternal @Password, @StudentID", PasswordParameter, studentIdParameter).FirstOrDefault();
                return result;
            }
        }

        public List<Mahasiswa> getMahasiswasNotYetVer(string Universitas, string Prodi)
        {
            using (var context = new MBKMContext())
            {
                var result = context.Mahasiswas.Where(x => x.StatusVerifikasi == "MENUNGGU VERIFIKASI" && x.NamaUniversitas == Universitas 
                            && x.ProdiAsal == Prodi).ToList();
                return result;
            }
        }

        public VMListMahasiswa getMahasiswasNotYetVer(int Skip, int Length, string SearchParam, string SortBy, bool SortDir)
        {
            VMListMahasiswa mListMahasiswa = new VMListMahasiswa();
            if (String.IsNullOrEmpty(SearchParam))
            {
                // if we have an empty search then just order the results by Id ascending
                //SortBy = "ID";
                //SortDir = true;
                SearchParam = "";
            }
            using (var context = new MBKMContext())
            {

                var result = context.Mahasiswas.Where(x => x.IsDeleted == false && x.StatusVerifikasi == "MENUNGGU VERIFIKASI");
                mListMahasiswa.TotalCount = result.Count();
                var gridfilter = result
                    .AsQueryable()
                    .Where(y => y.NamaUniversitas.Contains(SearchParam) ||
                                        y.Email.Contains(SearchParam) || y.ProdiAsal.Contains(SearchParam) || y.NIMAsal.Contains(SearchParam)
                                        || y.Nama.Contains(SearchParam) || y.JenjangStudi.Contains(SearchParam))
                    .OrderBy(SortBy, SortDir); 
                mListMahasiswa.gridDatas = gridfilter.Skip(Skip).Take(Length)
                    .Select(z => new GridDataMahasiswa
                    {
                        ID = z.ID,
                        Email = z.Email,
                        Nama = z.Nama,
                        NamaUniversitas = z.NamaUniversitas,
                        Gender = context.Lookups.Where(x => x.Tipe == "Gender" && x.Nilai == z.Gender).Select(x => x.Nama).FirstOrDefault(),
                        NoHp = z.NoHp,
                        NIMAsal = z.NIMAsal,
                        ProdiAsal = z.ProdiAsal,
                        JenjangStudi = z.JenjangStudi,
                        StatusVerifikasi = z.StatusVerifikasi
                    }).ToList();
                mListMahasiswa.TotalFilterCount = gridfilter.Count();
                return mListMahasiswa;
            }
        }

        public string GetNim()
        {
            using (var context = new MBKMContext())
            {
                var Tahun = new SqlParameter("@TAHUN", DateTime.Now.Year);
                var result = context.Database
                    .SqlQuery<string>("GetNIM @TAHUN", Tahun).Last();
                return result;
            }
        }

        public void UpdateNim(int Nilai)
        {
            using (var context = new MBKMContext())
            {
                try
                {
                    var tmpNilai = new SqlParameter("@Nilai", Nilai);
                    var tmpTahun = new SqlParameter("@Tahun", DateTime.Now.Year);
                    context.Database.ExecuteSqlCommand("UpdateNourut @Nilai, @Tahun", tmpNilai, tmpTahun);
                }catch(Exception e)
                {

                }

            }
        }

        public IEnumerable<VMSemester> GetDataSemester(string jenjangStudi)
        {
            using (var context = new MBKMContext())
            {
                if(jenjangStudi != null)
                {
                    var jenjangStudiParam = new SqlParameter("@JenjangStudi", jenjangStudi);
                    var result = context.Database
                        .SqlQuery<VMSemester>("GetSemester @JenjangStudi", jenjangStudiParam).ToList();
                    return result;
                }
                else
                {
                    var result = context.Database
                        .SqlQuery<VMSemester>("GetSemester").ToList();
                    return result;
                }
                
            }
        }

        public void GenerateAbsence(long jadwalKuliahId, long mahasiswaId, string kodeMk, string classSection, string strm, string fakultasId)
        {
            try
            {
                using (var context = new MBKMContext())
                {
                    var tempjadwalKuliahID = new SqlParameter("@JadwalKuliahID", jadwalKuliahId);
                    var tempmahasiswaId = new SqlParameter("@MahasiswaID", mahasiswaId);
                    var tempkodeMk = new SqlParameter("@Kodemk", kodeMk);
                    var tempclassSection = new SqlParameter("@ClassSection", classSection);
                    var tempstrm = new SqlParameter("@STRM", strm);
                    var tempfakultasId = new SqlParameter("@FakultasID", fakultasId);
                    var result = context.Database
                        .ExecuteSqlCommand("MahasiswaAccepetedDaftar @JadwalKuliahID, @MahasiswaID, @Kodemk, @ClassSection, @STRM, @FakultasID",
                        tempjadwalKuliahID, tempmahasiswaId, tempkodeMk, tempclassSection, tempstrm, tempfakultasId);
                }
            }catch(Exception e)
            {

            }
        }


        public IEnumerable<VMAllProdi> GetAllDataProdi()
        {
            using (var context = new MBKMContext())
            {
                var result = context.Database
                    .SqlQuery<VMAllProdi>("GetListProdi").ToList();
                return result;
            }
        }


        //public int updateRangeVer(Int64[] listId)
        //{
        //    using (var context = new MBKMContext())
        //    {
        //        var model = context.Mahasiswas.Where(f => listId.Contains(f.ID)).ToList();
        //        model.ForEach(a => a.isVerifikasi = true);
        //        return context.SaveChanges();
        //    }
        //}
    }
}
