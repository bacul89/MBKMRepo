using MBKM.Entities.Basentities;
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
        public string NamaUniversitas { get; set; }
    }
}
