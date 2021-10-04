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
    public class MasterCapaianPembelajaranRepository : GenericRepository<MasterCapaianPembelajaran>, IMasterCapaianPembelajaranRepository
    {
        public MasterCapaianPembelajaranRepository(DbContext _db) : base(_db)
        {
        }
        public IEnumerable<VMMataKuliah> GetMatkul(int PageNumber, int PageSize, string search)
        {
            using (var context = new MBKMContext())
            {
                var PageNumberParam = new SqlParameter("@PageNumber", PageNumber);
                var PageSizeParam = new SqlParameter("@PageSize", PageSize);
                var searchParam = new SqlParameter("@Search", search);
                var result = context.Database
                    .SqlQuery<VMMataKuliah>("GetMatkul @PageNumber, @PageSize, @Search", PageNumberParam, PageSizeParam, searchParam).ToList();
                return result;
            }
        }
    }
}
