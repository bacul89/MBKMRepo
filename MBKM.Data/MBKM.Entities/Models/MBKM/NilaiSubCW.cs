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
        public int CWSub1 { get; set; }
        public int CWSub2 { get; set; }
        public int CWSub3 { get; set; }
        public int CWSub4 { get; set; }
        public int CWSub5 { get; set; }
        public int CWSub6 { get; set; }
        public int CWSub7 { get; set; }
        public int CWSub8 { get; set; }
        public int CWSub9 { get; set; }
        public int CWSub10 { get; set; }
        public int NilaiTotal { get; set; }
        public Int64 NilaiKuliahID { get; set; }
        public virtual NilaiKuliah NilaiKuliahs { get; set; }

    }
}
