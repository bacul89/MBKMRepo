using MBKM.Common.Interfaces.RepoInterfaces.MBKMRepoInterfaces;
using MBKM.Entities.Models.MBKM;
using MBKM.Repository.BaseRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MBKM.Repository.Repositories.MBKMRepository
{
    public class JadwalKuliahMahasiswaRepository : GenericRepository<JadwalKuliahMahasiswa>, IJadwalKuliahMahasiswaRepository
    {
        public JadwalKuliahMahasiswaRepository(MBKMContext _db) : base(_db)
        {
        }
    }
}
