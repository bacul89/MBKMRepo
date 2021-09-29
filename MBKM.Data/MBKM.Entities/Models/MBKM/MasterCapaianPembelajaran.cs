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
        public string Kelompok { get; set; }
        public string Kode { get; set; }
        public string Capaian { get; set; }
    }
}
