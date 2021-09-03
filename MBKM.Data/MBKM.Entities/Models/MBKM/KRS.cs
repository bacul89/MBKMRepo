using MBKM.Entities.Basentities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MBKM.Entities.Models.MBKM
{
    public class KRS : BaseEntity
    {
        public bool FlagBayar { get; set; }
        public DateTime TanggalBayar { get; set; }
        public virtual Mahasiswa Mahasiswas { get; set; }
        public virtual JadwalKuliah JadwalKuliahs { get; set; }
    }
}
