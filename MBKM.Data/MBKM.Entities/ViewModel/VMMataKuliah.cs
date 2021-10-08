using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MBKM.Entities.ViewModel
{

    public class VMMataKuliah
    {
        public int TotalCount { get; set; }
        public int TotalFilterCount { get; set; }
        public List<GridDataMataKuliah> gridDatas { get; set; }
    }
    public class GridDataMataKuliah { 
        public string CRSE_ID { get; set; }
        public string Expr1 { get; set; }
        public string DESCR { get; set; }
        public string Prodi { get; set; }
        public string Fakultas { get; set; }
        public string ProdiID { get; set; }
        public string FakultasID { get; set; }

    }

}
