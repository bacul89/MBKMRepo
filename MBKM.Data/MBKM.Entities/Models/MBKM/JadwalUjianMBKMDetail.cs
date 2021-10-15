using MBKM.Entities.Basentities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MBKM.Entities.Models.MBKM
{
    public class JadwalUjianMBKMDetail : BaseEntity
    {
        public Int64 JadwalUjianMBKMID { get; set; }
        public virtual JadwalUjianMBKM JadwalUjianMBKMs { get; set; }
        public Int64 MahasiswaID { get; set; }
        public virtual Mahasiswa Mahasiswas { get; set; }
        public bool Present { get; set; }
    }
}
