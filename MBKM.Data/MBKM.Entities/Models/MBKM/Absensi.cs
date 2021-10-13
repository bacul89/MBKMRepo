using MBKM.Entities.Basentities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MBKM.Entities.Models.MBKM
{
    public class Absensi : BaseEntity
    {
        //public Int64 JadwalKuliahMahasiswaID { get; set; }
        //public virtual JadwalKuliahMahasiswa JadwalKuliahMahasiswas { get; set; }
        public DateTime TanggalAbsen { get; set; }
        public bool Present { get; set; }
        public bool CheckDosen { get; set; }
        public bool LockedAbsen { get; set; }
        public Int64 JadwalKuliahID { get; set; }
        public virtual JadwalKuliah JadwalKuliahs { get; set; }
        public Int64 MahasiswaID { get; set; }
        public virtual Mahasiswa Mahasiswas { get; set; }
        public string InstructorId { get; set; }
        public string NamaDosen { get; set; }
    }
}
