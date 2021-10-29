using MBKM.Entities.Models.MBKM;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MBKM.Entities.ViewModel
{
    public class VMKHS
    {
        public string NamaMatakuliah { get; set; }
        public int Nilai { get; set; }
        public int Persentase { get; set; }

        //public Int64 JadwalKuliahMahasiswaID { get; set; }
        //public virtual JadwalKuliahMahasiswa JadwalKuliahMahasiswas { get; set; }

        #region sprint 4
        public Int64 JadwalKuliahID { get; set; }
        [JsonIgnore]
        public virtual JadwalKuliah JadwalKuliahs { get; set; }
        public Int64 MahasiswaID { get; set; }
        [JsonIgnore]
        public virtual Mahasiswa Mahasiswas { get; set; }

        public string Grade { get; set; }
        [JsonIgnore]
        public virtual ICollection<NilaiSubCW> NilaiSubCWs { get; set; }
        #endregion
    }
}
