using MBKM.Common.Interfaces.RepoInterfaces.MBKMRepoInterfaces;
using MBKM.Entities.Models.MBKM;
using MBKM.Repository.BaseRepository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MBKM.Repository.Repositories.MBKMRepository
{
    public class CPLMKPendaftaranRepository : GenericRepository<CPLMKPendaftaran>, ICPLMKPendaftaranRepository
    {
        public CPLMKPendaftaranRepository(DbContext _db) : base(_db)
        {
        }
    }
}
