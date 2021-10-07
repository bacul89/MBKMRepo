using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MBKM.Entities.ViewModel
{
    public class VMListMasterCPL
    {
        public int TotalCount { get; set; }
        public int TotalFilterCount { get; set; }
        public List<GridDataCPL> gridDatas { get; set; }
    }
    public class GridDataCPL
    {
        public Int64 ID { get; set; }
        public string FakultasID { get; set; }
        public string NamaFakultas { get; set; }
        public string ProdiID { get; set; }
        public string NamaProdi { get; set; }
        public string Kelompok { get; set; }
        public string Kode { get; set; }
        public string Capaian { get; set; }
        public bool Status { get; set; }
        public string JenjangStudi { get; set; }
        public string Lokasi { get; set; }
    }
}
