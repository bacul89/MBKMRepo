using MBKM.Entities.Models.MBKM;
using MBKM.Entities.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MBKM.Common.Interfaces.RepoInterfaces.MBKMRepoInterfaces
{
    public interface IJadwalKuliahRepository : IGenericRepository<JadwalKuliah>
    {
        IEnumerable<VMSemester> GetSemesterAll(int skip, int take, string search);
        VMListJadwalKuliah SearchListJadwalKuliah(int skip, int take, string searchBy, string sortBy, bool sortDir, string idProdi, string lokasi, string idFakultas, string jenjangStudi, string strm);
        VMListJadwalKuliah SearchListMataKuliah(int skip, int take, string searchBy, string sortBy, bool sortDir, string idProdi, string lokasi, string idFakultas, string jenjangStudi, string strm);
        IEnumerable<VMSemester> GetSemesterAll2();
        IEnumerable<JadwalKuliah> GetMatkulFlag(int skip, int take, string searchBy, string idProdi, string lokasi, string idFakultas, string jenjangStudi, string strm);
    }
}
