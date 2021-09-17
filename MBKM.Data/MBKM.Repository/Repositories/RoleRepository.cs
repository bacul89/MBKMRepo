using MBKM.Common.Interfaces.RepoInterfaces;
using MBKM.Entities.Models;
using MBKM.Entities.ViewModel;
using MBKM.Repository.BaseRepository;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace MBKM.Repository.Repositories
{
    public class RoleRepository : GenericRepository<Role>, IRoleRepository
    {
        public RoleRepository(DbContext _db) : base(_db)
        {
        }

        public List<VMLookup> getLookupRole()
        {
            using (var context = new MBKMContext())
            {
                var listmodel = context.Roles.Select(
                    x => new VMLookup
                    {
                        Nama = x.RoleName,
                        Nilai = x.ID.ToString()
                    });
                return listmodel.ToList();
            }
        }
    }
}
