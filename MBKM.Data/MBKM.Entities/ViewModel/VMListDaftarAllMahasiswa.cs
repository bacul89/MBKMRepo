using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MBKM.Entities.ViewModel
{
    public class VMListDaftarAllMahasiswa
    {
        public int TotalCount { get; set; }
        public int TotalFilterCount { get; set; }
        public List<GridDataAllMahasiswa> gridDatas { get; set; }
    }
    public class GridDataAllMahasiswa
    {
        public Int64 ID { get; set; }
        public string NamaUniversitas { get; set; }
        public string JenjangStudi { get; set; }
        public string ProdiAsal { get; set; }
        public string NIMAsal { get; set; }
        public string NIM { get; set; }
        public string Nama { get; set; }
        public string Gender { get; set; }
        public string NoKerjasama { get; set; }
        public string StatusKerjasama { get; set; }
        public string StatusVerifikasi { get; set; }
        public string Telepon { get; set; }
        public string NoHp { get; set; }
        public string Email { get; set; }
    }
}
