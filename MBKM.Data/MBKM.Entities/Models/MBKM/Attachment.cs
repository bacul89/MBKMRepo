using MBKM.Entities.Basentities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MBKM.Entities.Models.MBKM
{
    public class Attachment : BaseEntity
    {
        public string FileName { get; set; }
        public string FileExt { get; set; }
        public Int64 FileSze { get; set; }
        public Int64 MahasiswaID { get; set; }
        public virtual Mahasiswa mahasiswas { get; set; }
        public Int64 PerjanjianKerjasamaID { get; set; }
        public virtual PerjanjianKerjasama perjanjianKerjasamas { get; set; }
    }
}
