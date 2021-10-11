using MBKM.Entities.Basentities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MBKM.Entities.Models.MBKM
{
    public class JadwalUjianMBKM : BaseEntity
    {
        public string STRM { get; set; }
        public string TahunSemester { get; set; }
        public string KodeTipeUjian { get; set; }
        public string TipeUjian { get; set; }
        public string FakultasID { get; set; }
        public string NamaFakultas { get; set; }
        public string Lokasi { get; set; }
        public string KodeMatkul { get; set; }
        public string NamaMatkul { get; set; }
        public string ProdiID { get; set; }
        public string NamaProdi { get; set; }
        public string JenjangStudi { get; set; }
        public string TanggalUjian { get; set; }
        public string JamMulai { get; set; }
        public string JamAkhir { get; set; }
        public string KodeRuangUjian { get; set; }
        public string RuangUjian { get; set; }
        public string KapasitasRuangan { get; set; }
        public string Tersedia { get; set; }
    }
}
