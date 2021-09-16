using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MBKM.Entities.ViewModel
{
    public class VMListMahasiswa
    {
        public int TotalCount { get; set; }
        public int TotalFilterCount { get; set; }
        public List<GridDataMahasiswa> gridDatas { get; set; }
    }
    public class GridDataMahasiswa
    {
        public Int64 ID { get; set; }
        public string Universitas { get; set; }
        public string Jenjang { get; set; }
        public string Prodi { get; set; }
        public string NIM { get; set; }
        public string Nama { get; set; }
        public string Gender { get; set; }
        public string Email { get; set; }
        public string HP { get; set; }
        public bool StatusVerifikasi { get; set; }
    }
}
