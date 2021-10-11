using MBKM.Entities.Models;
using MBKM.Entities.Models.MBKM;
using MBKM.Entities.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MBKM.Common.Interfaces.RepoInterfaces.MBKMRepoInterfaces
{
    public interface IMahasiswaRepository : IGenericRepository<Mahasiswa>
    {
        VMLogin getLoginInternal(string StudentID, string Password);
        List<Mahasiswa> getMahasiswasNotYetVer(string Universitas, string Prodi);
        VMListMahasiswa getMahasiswasNotYetVer(int Skip, int Length, string SearchParam, string SortBy, bool SortDir);
        string GetNim();
        void UpdateNim(int Nilai);
        //int updateRangeVer(Int64[] listId);
    }
}
