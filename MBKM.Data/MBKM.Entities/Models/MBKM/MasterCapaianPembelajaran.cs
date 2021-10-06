using MBKM.Entities.Basentities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MBKM.Entities.Models.MBKM
{
    public class MasterCapaianPembelajaran : BaseEntity
    {
        public string FakultasID { get; set; }
        public string NamaFakultas { get; set; }
        public string ProdiID { get; set; }
        public string NamaProdi { get; set; }
        public string Kelompok { get; set; }
        public string Kode { get; set; }
        public string Capaian { get; set; }
        public virtual ICollection<CPLMatakuliah> CPLMatakuliahs { get; set; }
        public string JenjangStudi { get; set; }
        public string Lokasi { get; set; }
    }
}
