
using MBKM.Common.Interfaces.RepoInterfaces.MBKMRepoInterfaces;
using MBKM.Entities.Models.MBKM;
using MBKM.Entities.ViewModel;
using MBKM.Repository.BaseRepository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;

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
                var result = context.Mahasiswas.Where(x => x.isVerifikasi == false && x.NamaUniversitas == Universitas 
                            && x.ProdiAsal == Prodi).ToList();
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
