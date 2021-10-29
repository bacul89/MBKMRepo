using MBKM.Entities.Basentities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MBKM.Entities.Models.MBKM
{
    public class NilaiSubCW : BaseEntity
    {
        public decimal CWSub1 { get; set; }
        public decimal CWSub2 { get; set; }
        public decimal CWSub3 { get; set; }
        public decimal CWSub4 { get; set; }
        public decimal CWSub5 { get; set; }
        public decimal CWSub6 { get; set; }
        public decimal CWSub7 { get; set; }
        public decimal CWSub8 { get; set; }
        public decimal CWSub9 { get; set; }
        public decimal CWSub10 { get; set; }
        public decimal NilaiTotal { get; set; }
        public Int64 NilaiKuliahID { get; set; }
        [JsonIgnore]
        public virtual NilaiKuliah NilaiKuliahs { get; set; }
        public string HeadCW { get; set; }

    }
}
