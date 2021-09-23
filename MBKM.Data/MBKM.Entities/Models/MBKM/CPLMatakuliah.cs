using MBKM.Entities.Basentities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MBKM.Entities.Models.MBKM
{
    public class CPLMatakuliah : BaseEntity
    {
        public string KodeMataKuliah { get; set; }
        public string NamaMataKuliah { get; set; }
        public Int64 CapaianPembelajaranID { get; set; }
        public MasterCapaianPembelajaran MasterCapaianPembelajarans { get; set; }
    }
}
