using MBKM.Entities.Basentities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MBKM.Entities.ViewModel
{
    public class VMCPLMKPendaftaran
    {
        public string CPLAsal { get; set; }
        public Int64 PendaftaranMataKuliahID { get; set; }
        public string CPLMatakuliah { get; set; }
    }
}
