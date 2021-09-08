using MBKM.Entities.Basentities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MBKM.Entities.Models.MBKM
{
    public class JadwalKuliah : BaseEntity
    {
        public Int64 DosenID { get; set; }
        public string NamaDosen { get; set; }
        public Int64 MataKuliah { get; set; }
        public string KodeMataKuliah { get; set; }
        public string Hari { get; set; }
        public DateTime Waktu { get; set; }
        public int Kuota { get; set; }
        public string LinkMateri { get; set; }
        public bool FlagOpen { get; set; }
        public string OpenRegistration { get; set; }
        public string CloseRegistration { get; set; }

    }
}
