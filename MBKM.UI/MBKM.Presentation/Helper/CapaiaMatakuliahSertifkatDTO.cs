using MBKM.Entities.Models.MBKM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MBKM.Presentation.Helper
{
    public class CapaiaMatakuliahSertifkatDTO
    {
        public string namaMatkul { get; set; }
        public IList<CPLMatakuliah> kompetensi { get; set; }
        public string CPLAsal { get; set; }
        public string kodeMatakuliah { get; set; }
        public string angka { get; set; }
        public string huruf { get; set; }

    }
}