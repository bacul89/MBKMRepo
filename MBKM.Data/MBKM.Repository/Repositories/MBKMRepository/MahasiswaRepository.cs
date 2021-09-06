
using MBKM.Common.Interfaces.RepoInterfaces.MBKMRepoInterfaces;
using MBKM.Entities.Models.MBKM;
using MBKM.Repository.BaseRepository;

namespace MBKM.Repository.Repositories.MBKMRepository
{
    public class MahasiswaRepository : GenericRepository<Mahasiswa>, IMahasiswaRepository
    {
        public MahasiswaRepository(MBKMContext _db) : base(_db)
        {
        }
    }
}
