using MBKM.Entities.Models.MBKM;
using MBKM.Entities.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MBKM.Common.Interfaces.RepoInterfaces.MBKMRepoInterfaces
{
    public interface IJadwalUjianMBKMDetailRepository : IGenericRepository<JadwalUjianMBKMDetail>
    {
        List<VMClassSection> GetListSeksi();
        VMListJadwalUjian GetDHU(int skip, int take, string searchBy, string sortBy, bool sortDir, string idProdi, string lokasi, string idFakultas, string jenjangStudi, string strm, string idMatakuliah, string seksi);
        VMDosenMakulPertemuan GetDosen(string seksi, string kodeMataKuliah, string strm, string fakultasId);
        List <VMJadwalUjian> GetDHUTest(string idProdi, string lokasi, string idFakultas, string jenjangStudi, string strm, string idMatakuliah, string seksi);
        List<VMJadwalUjian> GetDHUbyID(int iD);
    }
}
