using MBKM.Entities.Models.MBKM;
using MBKM.Entities.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MBKM.Common.Interfaces.RepoInterfaces.MBKMRepoInterfaces
{
    public interface ILinkFasilitasRepository : IGenericRepository<JadwalKuliah>
    {
        VMLinkFasilitas GetListJadwalKuliah(int Skip, int Length, string SearchParam, string SortBy, bool SortDir);
        VMLinkFasilitas SearchListJadwalKuliah(int Skip, int Length, string SearchParam, string SortBy, bool SortDir, string idProdi, string lokasi, string idFakultas, string jenjangStudi, string idMatakuliah, string seksi);
        IEnumerable<VMMataKuliah> GetMatkul(int skip, int take, string searchBy, string idProdi, string idFakultas);
        VMLinkFasilitas GetSeksi(string seksi);
        List<VMClassSection> GetListSeksi();
    }
}
