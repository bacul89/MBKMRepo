using MBKM.Entities.Basentities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MBKM.Entities.Models
{
    public class Lookup : BaseEntity
    {
        public string Tipe { get; set; }
        public string Nama { get; set; }
        public string Nilai { get; set; }
    }
}
