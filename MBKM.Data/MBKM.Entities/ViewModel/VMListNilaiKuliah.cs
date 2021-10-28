using MBKM.Entities.Models.MBKM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MBKM.Entities.ViewModel
{

    public class VMListNilaiKuliah
    {
        //public int TotalCount { get; set; }
        //public int TotalFilterCount { get; set; }
        public List<GridDataNilaiKuliah> gridDatas { get; set; }
    }
    public class GridDataNilaiKuliah
    {
        public long MahasiswaID { get; set; }
        //public Int64 ID { get; set; }
        public string NamaUniversitas { get; set; }
        public string NoKerjasama { get; set; }
        public string JenjangStudi { get; set; }
        public string NIM { get; set; }
        public string Nama { get; set; }
        public bool FlagCetak { get; set; }
        


        /*        public Mahasiswa Mahasiswas { get; set; }
                public JadwalKuliah JadwalKuliahs { get; set; }*/
    }
}
