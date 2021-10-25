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
    public class ReportBAPRepository : GenericRepository<Absensi>, IReportBAPRepository
    {
        public ReportBAPRepository(DbContext _db) : base(_db)
        {
        }

        public IEnumerable<VMListReportBAP> GetBAPByAbsensiID(int id)
        {
            using (var context = new MBKMContext())
            {
                var absensiIDParam = new SqlParameter("@ABSENSIID", id);
                var result = context.Database
                    .SqlQuery<VMListReportBAP>("ReportBAP @ABSENSIID", absensiIDParam).ToList();
                return result;
            }
        }
    }
}
