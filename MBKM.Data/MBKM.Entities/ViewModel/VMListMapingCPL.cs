using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MBKM.Entities.ViewModel
{
    public class VMListMapingCPL
    { 
        public int TotalCount { get; set; }
        public int TotalFilterCount { get; set; }
        public List<GridDataMapingCPL> gridDatas { get; set; }
    }
    public class GridDataMapingCPL
    {
        public Int64 ID { get; set; }
        public string IDMataKUliah { get; set; }
        public string KodeMataKuliah { get; set; }
        public string NamaMataKuliah { get; set; }
        public string Kelompok { get; set; }
        public string Kode { get; set; }

        public string Capaian { get; set; }
        public bool Status { get; set; }

    }
}
