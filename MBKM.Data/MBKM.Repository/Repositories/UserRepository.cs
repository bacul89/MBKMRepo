using MBKM.Common.Interfaces.RepoInterfaces;
using MBKM.Entities.Models;
using MBKM.Repository.BaseRepository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MBKM.Repository.Repositories
{
    public class UserRepository : GenericRepository<Role>, IRoleRepository
    {
        public UserRepository(DbContext _db) : base(_db)
        {
        }
    }
}
