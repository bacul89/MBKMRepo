using MBKM.Entities.Basentities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MBKM.Entities.Models.MBKM
{
    public class JenisKerjasamaModel : BaseEntity
    {
        public string JenisPertukaran { get; set; }
        public string JenisKerjasama { get; set; }
    }
}
