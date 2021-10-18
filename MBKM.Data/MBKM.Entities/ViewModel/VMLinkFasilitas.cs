using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MBKM.Entities.ViewModel
{
    public class VMLinkFasilitas
    {
        public int TotalCount { get; set; }
        public int TotalFilterCount { get; set; }
        public List<GridDataLinkFasilitas> gridDatas { get; set; }
    }
    public class GridDataLinkFasilitas
    {
        public Int64 ID { get; set; }
        public Int64 DosenID { get; set; }
        public string NamaDosen { get; set; }
        public string KodeMataKuliah { get; set; }
        public string Hari { get; set; }
        public bool FlagOpen { get; set; }
        //public string CreatedBy { get; set; }
       // public DateTime CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public string MataKuliahID { get; set; }
        public string NamaMataKuliah { get; set; }
        public string JamMasuk { get; set; }
        public string JamSelesai { get; set; }
       // public DateTime TglAwalKuliah { get; set; }
       // public DateTime TglAkhirKuliah { get; set; }
        public string RuangKelas { get; set; }
        public string Lokasi { get; set; }
        public int STRM { get; set; }
        public string SKS { get; set; }
        public string ClassSection { get; set; }
        public string JenjangStudi { get; set; }
        public Int64 FakultasID { get; set; }
        public string NamaFakultas { get; set; }
        public Int64 ProdiID { get; set; }
        public string NamaProdi { get; set; }
        public string LinkMoodle { get; set; }
        public string LinkAtmaZeds { get; set; }
        public string LinkTeams { get; set; }
        public string LinkOthers { get; set; }



    }
}
