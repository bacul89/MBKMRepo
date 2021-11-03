using MBKM.Common.Interfaces.RepoInterfaces.MBKMRepoInterfaces;
using MBKM.Entities.Models.MBKM;
using MBKM.Entities.ViewModel;
using MBKM.Repository.BaseRepository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MBKM.Repository.Repositories.MBKMRepository
{
    public class JadwalUjianMBKMDetailRepository : GenericRepository<JadwalUjianMBKMDetail>, IJadwalUjianMBKMDetailRepository
    {
        public JadwalUjianMBKMDetailRepository(DbContext _db) : base(_db)
        {
        }

        public List<VMClassSection> GetListSeksi()
        {
            using (var context = new MBKMContext())
            {
                var result = context.jadwalUjians.Where(x => x.IsActive && !x.IsDeleted).Select(x => new VMClassSection
                {


                    Nama = x.ClassSection
                }).GroupBy(x => x.Nama).Select(y => y.FirstOrDefault()).OrderBy(x => x.Nama);
                return result.ToList();
            }
        }

    }
}
