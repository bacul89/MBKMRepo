using MBKM.Entities.Models.MBKM;
using MBKM.Entities.ViewModel;

namespace MBKM.Common.Interfaces.RepoInterfaces.MBKMRepoInterfaces
{
    public interface IJadwalKuliahMahasiswaRepository : IGenericRepository<JadwalKuliahMahasiswa>
    {
        VMSemester getOngoingSemester(string jenjangStudi);
    }
}
