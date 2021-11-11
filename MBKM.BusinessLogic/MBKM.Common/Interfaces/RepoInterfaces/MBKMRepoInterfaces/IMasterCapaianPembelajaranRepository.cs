using MBKM.Entities.Models.MBKM;
using MBKM.Entities.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MBKM.Common.Interfaces.RepoInterfaces.MBKMRepoInterfaces
{
    public interface IMasterCapaianPembelajaranRepository : IGenericRepository<MasterCapaianPembelajaran>
    {
        IEnumerable<VMMataKuliah> GetMatkul(int PageNumber, int PageSize, string search);
        IEnumerable<VMFakultas> GetFakultas(string jenjangStudi, string search);
        IEnumerable<VMProdi> GetProdiByFakultas(string jenjangStudi, string idFakultas, string search);
        IEnumerable<VMProdi> GetLokasiByProdi(string jenjangStudi, string namaProdi, string search);
        IEnumerable<VMProdi> GetLokasiByProdiName(string jenjangStudi, string namaProdi, string search);
        VMListMasterCPL GetListMasterCPL(int Skip, int Length, string SearchParam, string SortBy, bool SortDir, string prodi, string jenjang, string fakultas);
    }
}
