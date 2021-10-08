using MBKM.Entities.Models.MBKM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MBKM.Entities.ViewModel;


namespace MBKM.Common.Interfaces.RepoInterfaces.MBKMRepoInterfaces
{
    public interface ICPLMataKuliahRepository : IGenericRepository<CPLMatakuliah>
    {
        VMListMapingCPL GetListMapingCPL(int Skip, int Length, string SearchParam, string SortBy, bool SortDir);
        VMListMapingCPL SearchListMapingCPL(int Skip, int Length, string SearchParam, string SortBy, bool SortDir, string idProdi, string lokasi, string idFakultas, string jenjangStudi, string idMatakuliah);
        VMMataKuliah GetMatkul(int skip, int take, string searchBy, string idProdi, string idFakultas);
    }
}
