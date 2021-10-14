﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MBKM.Entities.ViewModel
{
    public class VMListJadwalUjian
    {
        public int TotalCount { get; set; }
        public int TotalFilterCount { get; set; }
        public List<GridDataJadwalUjian> gridDatas { get; set; }
    }

    public class GridDataJadwalUjian
    {
        public Int64 ID { get; set; }
        public string JenjangStudi { get; set; }
        public string STRM { get; set; }
        public string KodeTipeUjian { get; set; }
        public string TipeUjian { get; set; }
        public string FakultasID { get; set; }
        public string NamaFakultas { get; set; }
        public string Lokasi { get; set; }
        public string IDMatkul { get; set; }
        public string KodeMatkul { get; set; }
        public string NamaMatkul { get; set; }
        public string ProdiID { get; set; }
        public string NamaProdi { get; set; }
        public DateTime TanggalUjian { get; set; }
        public string JamMulai { get; set; }
        public string JamAkhir { get; set; }
        public string KodeRuangUjian { get; set; }
        public string RuangUjian { get; set; }
        public int KapasitasRuangan { get; set; }
        public int Tersedia { get; set; }
        public string ClassSection { get; set; }
        public string KodeClassSection { get; set; }

    }
}
