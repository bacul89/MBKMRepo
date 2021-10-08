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
using System.Linq.Dynamic;

namespace MBKM.Repository.Repositories.MBKMRepository
{
    public class JenisKerjasamaModelRepository : GenericRepository<JenisKerjasamaModel>, IJenisKerjasamaModelRepository
    {
        public JenisKerjasamaModelRepository(DbContext _db) : base(_db)
        {
        }
        public List<VMJenisPertukaran> getPertukaran()
        {
            using (var context = new MBKMContext())
            {
                var result = context.JenisKerjasamaModels.Where(x => x.IsActive && !x.IsDeleted)
                    .GroupBy(x => x.JenisPertukaran).Select(x => x.FirstOrDefault());
                //var result2 = new List<VMLookupNoKerjasama>();
                var result2 = result.Select(y => new VMJenisPertukaran
                {
                    ID = y.ID,
                    JenisPertukaran = y.JenisPertukaran
                });
                return result2.ToList();
            }
        }
        public List<VMJenisKerjasama> getKerjasama()
        {
            using (var context = new MBKMContext())
            {
                var result = context.JenisKerjasamaModels.Where(x => x.IsActive && !x.IsDeleted).Select(x => new VMJenisKerjasama
                {
                    ID = x.ID,
                    JenisPertukaran = x.JenisPertukaran,
                    Nama = x.JenisKerjasama
                });
                return result.ToList();
            }
        }
    }
}