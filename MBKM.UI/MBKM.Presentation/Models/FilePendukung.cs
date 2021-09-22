using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MBKM.Presentation.Models
{
    public class FilePendukung
    {
        public HttpPostedFileBase fotoDiri { get; set; }
        public HttpPostedFileBase fotoKTP { get; set; }
        public HttpPostedFileBase fotoKIM { get; set; }
        public HttpPostedFileBase transkripNilai { get; set; }
        public HttpPostedFileBase suratKeterangan { get; set; }
    }
}