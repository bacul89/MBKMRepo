using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MBKM.Entities.ViewModel
{
    public class VMListReportBAP
    {
        public string NIM { get; set; }
        public string Nama { get; set; }
        public string NamaUniversitas { get; set; }
        public string NamaProdi { get; set; }
        public string NamaMatakuliah { get; set; }
        public string KodeMataKuliah { get; set; }
        public string ClassSection { get; set; }
        public string TANGGAL { get; set; }
        public string BENTUK { get; set; }
        public string PLATFORM { get; set; }
        public string COMMENTS { get; set; }
        public string NamaDosen { get; set; }
        public bool Present { get; set; }
       
    }
}
