using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MBKM.Entities.ViewModel
{
    public class VMPresensi
    {
        public DateTime TanggalAbsen { get; set; }
        public DateTime JamMasuk { get; set; }
        public DateTime JamKeluar { get; set; }
        public string TanggalAbsen2 { get; set; }
        public string JamMasuk2 { get; set; }
        public string JamKeluar2 { get; set; }
        public string KodeMataKuliah { get; set; }
        public string NamaMataKuliah { get; set; }
        public string Seksi { get; set; }
        public Int64 IDJadwalKuliah { get; set; }
        public string RuangKelas { get; set; }
        public string NamaDosen { get; set; }
    }
}
