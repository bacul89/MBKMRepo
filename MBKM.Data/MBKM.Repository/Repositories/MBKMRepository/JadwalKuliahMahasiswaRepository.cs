using MBKM.Common.Interfaces.RepoInterfaces.MBKMRepoInterfaces;
using MBKM.Entities.Models.MBKM;
using MBKM.Entities.ViewModel;
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
    public class JadwalKuliahMahasiswaRepository : GenericRepository<JadwalKuliahMahasiswa>, IJadwalKuliahMahasiswaRepository
    {
        public JadwalKuliahMahasiswaRepository(DbContext _db) : base(_db)
        {
        }

        public VMSemester getOngoingSemester(string jenjangStudi)
        {
            using (var context = new MBKMContext())
            {
                var jenjangStudiParam = new SqlParameter("@JenjangStudi", jenjangStudi);
                var result = context.Database
                    .SqlQuery<VMSemester>("GetSemester @JenjangStudi", jenjangStudiParam).FirstOrDefault();
                return result;
            }
        }
    }
}
