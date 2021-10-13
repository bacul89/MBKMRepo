using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MBKM.Entities.ViewModel
{
    public class VMListPerjanjianKerjasama
    {
        public int TotalCount { get; set; }
        public int TotalFilterCount { get; set; }
        public List<GridDataPerjanjian> gridDatas { get; set; }
    }
    public class GridDataPerjanjian
    {
        public Int64 ID { get; set; }
        public string NoPerjanjian { get; set; }
        public DateTime TanggalMulai { get; set; }
        public DateTime TanggalAkhir { get; set; }
        public string Instansi { get; set; }
        public string NamaUnit { get; set; }
        public string NamaInstansi { get; set; }
        public string JenisKerjasama { get; set; }
        public string JenisPertukaran { get; set; }
        public string CreatedBy { get; set; }
    }
}
