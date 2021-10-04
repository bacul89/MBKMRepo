using MBKM.Entities.Basentities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MBKM.Entities.Models.MBKM
{
    public class CPLMKPendaftaran : BaseEntity
    {
        public string CPLAsal { get; set; }
        public Int64 PendaftaranMataKuliahID { get; set; }
        public Int64? CPLMatakuliahID { get; set; }
        [JsonIgnore]
        public virtual CPLMatakuliah CPLMatakuliahs { get; set; }
        [JsonIgnore]
        public virtual PendaftaranMataKuliah PendaftaranMataKuliahs { get; set; }
    }
}
