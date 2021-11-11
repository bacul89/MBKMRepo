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
        VMSemester GetTahunSemester();
        string GetKomponenDHK(int idJadwalKuliah);
        IEnumerable<VMLookup> GetFakultasByJenjangStudi(string search, string jenjangStudi);
        IEnumerable<VMLookup> GetProdiByFakultas(string search, string jenjangStudi, string fakultas);
        IEnumerable<VMLookup> GetLokasiByProdi(string search, string jenjangStudi, string prodi);
        IEnumerable<VMLookup> GetMatkulByLokasi(string search, string jenjangStudi, string prodi, string lokasi);
        IEnumerable<VMLookup> GetSeksiByMatkul(string search, string jenjangStudi, string matkul, string lokasi);
        IEnumerable<VMPresensi> GetPresensi(int strm, string jenjangStudi, string fakultas, string lokasi, string prodi, string matkul, string seksi);
    }
}
