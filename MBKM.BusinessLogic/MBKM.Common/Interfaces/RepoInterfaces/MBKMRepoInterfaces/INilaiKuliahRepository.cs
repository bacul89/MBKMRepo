using MBKM.Entities.Models.MBKM;
using MBKM.Entities.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MBKM.Common.Interfaces.RepoInterfaces.MBKMRepoInterfaces
{
    public interface INilaiKuliahRepository : IGenericRepository<NilaiKuliah>
    {
        VMListNilaiKuliah GetNilaiMahasiswa();
        IEnumerable<VMMataKuliah> GetMatkulEn(string kodeMataKuliah, int mataKuilahID, int sTRM);
    }
}
