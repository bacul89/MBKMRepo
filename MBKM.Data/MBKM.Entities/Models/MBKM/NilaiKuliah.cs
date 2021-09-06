using MBKM.Entities.Basentities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MBKM.Entities.Models.MBKM
{
    public class NilaiKuliah : BaseEntity
    {
        public string NamaMatakuliah { get; set; }
        public int Nilai { get; set; }
        public int Persentase { get; set; }

        public Int64 JadwalKuliahMahasiswaID { get; set; }
        public virtual JadwalKuliahMahasiswa JadwalKuliahMahasiswas { get; set; }
    }
}
