using MBKM.Common.Interfaces.RepoInterfaces;
using MBKM.Entities.Models;
using MBKM.Repository.BaseRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MBKM.Repository.Repositories
{
    public class MenuRoleRepository : GenericRepository<Role>, IRoleRepository
    {
        public MenuRoleRepository(MBKMContext _db) : base(_db)
        {
        }
    }
}
