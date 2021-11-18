using MBKM.Entities.Models.MBKM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MBKM.Entities.ViewModel
{
    public class VMPenilaian
    {
        public PendaftaranMataKuliah MataKuliah { get; set; }
        public bool IsAbsent { get; set; }
    }
}
