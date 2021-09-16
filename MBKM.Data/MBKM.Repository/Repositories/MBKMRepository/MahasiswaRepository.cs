
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
                    .SqlQuery<VMLogin>("GetLoginInternal @Password, @StudentID", PasswordParameter, studentIdParameter);
                return result.FirstOrDefault();
            }
        }

        public List<Mahasiswa> getMahasiswasNotYetVer(string Universitas, string Prodi)
        {
            using (var context = new MBKMContext())
            {
                var result = context.Mahasiswas.Where(x => x.StatusVerifikasi == "DAFTAR" && x.NamaUniversitas == Universitas 
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
                SortBy = "ID";
                SortDir = true;
                SearchParam = "";
            }
            using (var context = new MBKMContext())
            {

                var result = context.Mahasiswas.Where(x => x.IsDeleted == false && x.StatusVerifikasi == "DAFTAR");
                mListMahasiswa.TotalCount = result.Count();
                mListMahasiswa.gridDatas = result
                    .AsQueryable()
                    .Where(y => y.NamaUniversitas.Contains(SearchParam) ||
                                        y.Email.Contains(SearchParam) || y.ProdiAsal.Contains(SearchParam) || y.NIMAsal.Contains(SearchParam)
                                        || y.Nama.Contains(SearchParam) || y.JenjangStudi.Contains(SearchParam))
                    .OrderBy(SortBy, SortDir)
                    .Skip(Skip).Take(Length)
                    .Select(z => new GridDataMahasiswa
                    {
                        ID = z.ID,
                        Email = z.Email,
                        Nama = z.Nama,
                        Universitas = z.NamaUniversitas,
                        Gender = z.Gender,
                        HP = z.NoHp,
                        NIM = z.NIMAsal,
                        Prodi = z.ProdiAsal,
                        Jenjang = z.JenjangStudi,
                        StatusVerifikasi = z.StatusVerifikasi
                    })                    
                    .ToList();
                mListMahasiswa.TotalFilterCount = mListMahasiswa.gridDatas.Count();
                return mListMahasiswa;
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
