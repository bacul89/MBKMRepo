using MBKM.Entities.Basentities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MBKM.Entities.Models.MBKM
{
    public class CPLMatakuliah : BaseEntity
    {
        public string IDMataKUliah { get; set; }
        public string KodeMataKuliah { get; set; }
        public string NamaMataKuliah { get; set; }
        public Int64 MasterCapaianPembelajaranID { get; set; }
        [JsonIgnore]
        public virtual MasterCapaianPembelajaran MasterCapaianPembelajarans { get; set; }
        [JsonIgnore]
        public virtual ICollection<CPLMKPendaftaran> CPLMKPendaftarans { get; set; }

    }
}
