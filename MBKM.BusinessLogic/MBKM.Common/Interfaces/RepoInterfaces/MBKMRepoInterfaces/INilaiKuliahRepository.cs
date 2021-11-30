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
        VMBobot GetBobot(string idMatkul);
        IEnumerable<VMSubBobot> GetSubBobot(string idMatkul);
        VMListNilaiKuliah GetNilaiMahasiswa();
        VMDNR GetDNR(int idJadwalKuliah);
        IEnumerable<VMMataKuliah> GetMatkulEn(string kodeMataKuliah, int mataKuilahID, int sTRM);
        VMNilaiBobot GetBobotNilai(decimal nilaiTotal);
        VMNilaiDiakui GetNilaiDiakui(string Jenjang, string Strm, string MatkulId, string KodeMatkul, string Nim, string classSection);
        VMNilaiGrade GetNilaiGradeByNilaiTotal(int nilaiTotal);
    }
}
