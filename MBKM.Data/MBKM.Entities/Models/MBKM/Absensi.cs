using MBKM.Entities.Basentities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MBKM.Entities.Models.MBKM
{
    public class Absensi : BaseEntity
    {
        public Int64 JadwalKuliahMahasiswaID { get; set; }
        public virtual JadwalKuliahMahasiswa JadwalKuliahMahasiswas { get; set; }
        public DateTime TanggalAbsen { get; set; }
    }
}
