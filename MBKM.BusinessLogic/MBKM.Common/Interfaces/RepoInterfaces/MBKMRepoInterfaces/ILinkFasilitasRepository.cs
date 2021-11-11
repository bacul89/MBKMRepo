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
        VMLinkFasilitas SearchListJadwalKuliah(int Skip, int Length, string SearchParam, string SortBy, bool SortDir, string idProdi, string lokasi, string idFakultas, string jenjangStudi, string idMatakuliah, string seksi, int strm);
        IEnumerable<VMMataKuliah> GetMatkul(int skip, int take, string searchBy, string idProdi, string idFakultas);
        VMLinkFasilitas GetSeksi(string seksi);
        List<VMClassSection> GetListSeksi();
        IEnumerable<VMLookup> GetMatkulByLokasi(string search, string jenjangStudi, string prodi, string lokasi);
        IEnumerable<VMLookup> GetProdiByFakultas(string search, string jenjangStudi, string fakultas);
        IEnumerable<VMLookup> GetLokasiByProdi(string search, string jenjangStudi, string prodi);
        
        IEnumerable<VMLookup> GetSeksiByMatkul(string search, string jenjangStudi, string matkul, string lokasi);
        IEnumerable<VMLookup> GetFakultasByJenjangStudi(string search, string jenjangStudi);
    }
}
