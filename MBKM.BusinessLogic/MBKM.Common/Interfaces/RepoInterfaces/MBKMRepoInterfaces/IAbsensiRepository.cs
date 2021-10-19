using MBKM.Entities.Models.MBKM;
using MBKM.Entities.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MBKM.Common.Interfaces.RepoInterfaces.MBKMRepoInterfaces
{
    public interface IAbsensiRepository : IGenericRepository<Absensi>
    {
        string GetSemesterBySTRM(int strm);
        IEnumerable<VMLookup> GetFakultasByJenjangStudi(string search, string jenjangStudi);
        IEnumerable<VMLookup> GetLokasiByFakultas(string search, string jenjangStudi, string fakultas);
        IEnumerable<VMLookup> GetProdiByLokasi(string search, string jenjangStudi, string lokasi);
        IEnumerable<VMLookup> GetMatkulByProdi(string search, string jenjangStudi, string prodi);
        IEnumerable<VMLookup> GetSeksiByMatkul(string search, string jenjangStudi, string matkul);
        IEnumerable<VMPresensi> GetPresensi(int strm, string jenjangStudi, string fakultas, string lokasi, string prodi, string matkul, string seksi);
    }
}
