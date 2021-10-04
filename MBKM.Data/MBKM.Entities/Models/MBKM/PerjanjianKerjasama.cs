using MBKM.Entities.Basentities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace MBKM.Entities.Models.MBKM
{
    public class PerjanjianKerjasama : BaseEntity
    {
        public string NoPerjanjian { get; set; }
        public DateTime TanggalMulai { get; set; }
        public DateTime TanggalAkhir { get; set; }
        public string NamaInstansi { get; set; }
        public string Instansi { get; set; }
        public string NamaUnit { get; set; }
        public string JenisPertukaran { get; set; }
        [JsonIgnore]
        public string JenisKerjasama { get; set; }
        public int BiayaKuliah { get; set; } = 0;
        [JsonIgnore]
        public virtual ICollection<AttachmentPerjanjianKerjasama> AttachmentPerjanjianKerjasamas { get; set; }
    }
}
