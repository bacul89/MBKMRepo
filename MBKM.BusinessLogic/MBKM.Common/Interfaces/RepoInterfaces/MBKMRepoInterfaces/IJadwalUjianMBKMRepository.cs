using MBKM.Entities.Models.MBKM;
using MBKM.Entities.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MBKM.Common.Interfaces.RepoInterfaces.MBKMRepoInterfaces
{
    public interface IJadwalUjianMBKMRepository : IGenericRepository<JadwalUjianMBKM>
    {
        VMListJadwalUjian GetListManageUjian(int Skip, int Length, string SearchParam, string SortBy, bool SortDir, string jenjangStudi, string fakultas, string jenisUjian, string tahunSemester);
        VMListJadwalUjian GetListJadwalUjian(int Skip, int Length, string SearchParam, string SortBy, bool SortDir, string jenjangStudi, string fakultas, string jenisUjian, string tahunSemester);
        IEnumerable<VMSemester> getAllSemester();
        VMInformasiStudi GetInformasiData(string prodi);
    }
}
