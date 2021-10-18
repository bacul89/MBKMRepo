using MBKM.Entities.Basentities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MBKM.Entities.Models.MBKM
{
    public class FeedbackMataKuliah : BaseEntity
    {
        public string KategoriPertanyaan { get; set; }
        public string Pertanyaan { get; set; }
        public int NilaiFeed { get; set; }
        public Int64 DosenID { get; set; }
        public string NamaDosen { get; set; }
        public string KritikSaran { get; set; }
        public Int64 JadwalKuliahID { get; set; }
        [JsonIgnore]
        public virtual JadwalKuliah JadwalKuliahs { get; set; }
        public Int64 MahasiswaID { get; set; }
        [JsonIgnore]
        public virtual Mahasiswa Mahasiswas { get; set; }
    }
}
