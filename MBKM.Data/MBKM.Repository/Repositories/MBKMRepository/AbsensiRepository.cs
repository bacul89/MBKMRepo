using MBKM.Common.Interfaces.RepoInterfaces.MBKMRepoInterfaces;
using MBKM.Entities.Models.MBKM;
using MBKM.Repository.BaseRepository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MBKM.Repository.Repositories.MBKMRepository
{
    public class AbsensiRepository : GenericRepository<Absensi>, IAbsensiRepository
    {
        public AbsensiRepository(DbContext _db) : base(_db)
        {
        }
        public string GetSemesterBySTRM(int strm)
        {
            using (var context = new MBKMContext())
            {
                var strmParam = new SqlParameter("@STRM", strm);
                var result = context.Database
                    .SqlQuery<string>("GetSemesterBySTRM @STRM", strmParam).FirstOrDefault();
                return result;
            }
        }
    }
}
