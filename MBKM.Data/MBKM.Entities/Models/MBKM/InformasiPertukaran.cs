using MBKM.Entities.Basentities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MBKM.Entities.Models.MBKM
{
    public class InformasiPertukaran : BaseEntity
    {
        public Int64 MahasiswaID { get; set; }
        public int STRM { get; set; }
        public string JenisPertukaran { get; set; }
        public string JenisKerjasama { get; set; }
        public string NoKerjasama { get; set; }
        [JsonIgnore]
        public virtual Mahasiswa Mahasiswas { get; set; }
        //public virtual ICollection<PendaftaranMataKuliah> PendaftaranMataKuliahs { get; set; }
    }
}
