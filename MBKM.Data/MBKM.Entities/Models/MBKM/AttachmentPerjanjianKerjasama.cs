using MBKM.Entities.Basentities;
using System;

namespace MBKM.Entities.Models.MBKM
{
    public class AttachmentPerjanjianKerjasama : BaseEntity
    {
        public string FileName { get; set; }
        public string FileExt { get; set; }
        public Int64 FileSze { get; set; }
        public Int64 PerjanjianKerjasamaID { get; set; }
        public virtual PerjanjianKerjasama perjanjianKerjasamas { get; set; }
    }
}
