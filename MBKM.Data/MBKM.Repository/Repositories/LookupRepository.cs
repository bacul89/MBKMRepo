using MBKM.Common.Interfaces.RepoInterfaces;
using MBKM.Entities.Models;
using MBKM.Entities.ViewModel;
using MBKM.Repository.BaseRepository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MBKM.Repository.Repositories
{
    public class LookupRepository : GenericRepository<Lookup>, ILookupRepository
    {
        private readonly MBKMContext MBKMContext;
        public LookupRepository(DbContext _db) : base(_db)
        {
        }

        public List<VMListProdi> getListProdi()
        {
            using (var context = new MBKMContext())
            {
                var result = context.Database
                    .SqlQuery<VMListProdi>("GetListProdi");
                return result.ToList();
            }
        }

        public IEnumerable<VMLookup> getLookupByTipe(string tipe)
        {
            using (var context = new MBKMContext())
            {
                var listmodel = context.Lookups.Where(x => x.Tipe == tipe).Select(
                    x => new VMLookup
                    {
                        Nama = x.Nama,
                        Nilai = x.Nilai
                    });
                return listmodel.ToList();
            }
        }
    }
}
