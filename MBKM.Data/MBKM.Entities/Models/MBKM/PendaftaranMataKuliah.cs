using MBKM.Entities.Basentities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace MBKM.Entities.Models.MBKM
{
    public class PendaftaranMataKuliah : BaseEntity
    {
        public string MatkulIDAsal { get; set; }
        public string MatkulKodeAsal { get; set; }
        public string MatkulAsal { get; set; }
        public string Kesenjangan { get; set; }
        public string Nilai { get; set; }
        public string Konversi { get; set; }
        public string Hasil { get; set; }
        public Int64 DosenID { get; set; }
        public string DosenPembimbing { get; set; }
        public Int64 MahasiswaID { get; set; }
        [JsonIgnore]
        public virtual Mahasiswa mahasiswas { get; set; }
        public Int64 JadwalKuliahID { get; set; }
        [JsonIgnore]
        public virtual JadwalKuliah JadwalKuliahs { get; set; }
        public virtual ICollection<ApprovalPendaftaran> ApprovalPendaftarans { get; set; }
        public virtual ICollection<CPLMatakuliah> CPLMatakuliahs { get; set; }
        public string StatusPendaftaran { get; set; }

    }
}
