using MBKM.Entities.Basentities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MBKM.Entities.Models.MBKM
{
    public class JadwalKuliahMahasiswa : BaseEntity
    {
        public string Feedback { get; set; }
        public string FeedbackRating { get; set; }
        public int JumlahPertemuan { get; set; }
        public Int64 KRSID { get; set; }
        public virtual KRS  KRSs { get; set; }
        public Int64 JadwalKuliahID { get; set; }
        public virtual JadwalKuliah JadwalKuliahs { get; set; }
    }
}
