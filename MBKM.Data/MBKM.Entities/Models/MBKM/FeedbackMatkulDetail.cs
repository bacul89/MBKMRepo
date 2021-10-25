using MBKM.Entities.Basentities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MBKM.Entities.Models.MBKM
{
    public class FeedbackMatkulDetail : BaseEntity
    {
        public Int64 FeedbackMatkulID { get; set; }
        [JsonIgnore]
        public virtual FeedbackMatkul FeedbackMatkuls { get; set; }
        public string PertanyaanID { get; set; }
        public string Pertanyaan { get; set; }
        public string KategoriPertanyaan { get; set; }
        public int Nilai { get; set; }
    }
}
