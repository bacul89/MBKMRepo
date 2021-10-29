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
    public class NilaiKuliahRepository : GenericRepository<NilaiKuliah>, INilaiKuliahRepository
    {
        public NilaiKuliahRepository(DbContext _db) : base(_db)
        {
        }
        public VMBobot GetBobot(string idMatkul)
        {
            using (var context = new MBKMContext())
            {
                var courseParam = new SqlParameter("@CourseID", idMatkul);
                var result = context.Database
                    .SqlQuery<VMBobot>("GetBobotByCourseID @CourseID", courseParam).FirstOrDefault();
                return result;
            }
        }
        public IEnumerable<VMSubBobot> GetSubBobot(string idMatkul)
        {
            using (var context = new MBKMContext())
            {
                var courseParam = new SqlParameter("@CourseID", idMatkul);
                var result = context.Database
                    .SqlQuery<VMSubBobot>("GetSubBobotByCourseID @CourseID", courseParam).ToList();
                return result;
            }
        }
    }
}
