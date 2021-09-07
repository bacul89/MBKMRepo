using MBKM.Entities.Basentities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MBKM.Entities.Models.MBKM
{
    public class Mahasiswa : BaseEntity
    {
        public Int64 UniversitasID { get; set; }
        public string  NamaUniversitas { get; set; }
        public string  Nama { get; set; }
        public string Email { get; set; }
        public string Telepon { get; set; }
        public DateTime TanggalLahir { get; set; }
        public string Alamat { get; set; }
        public string Agama { get; set; }
        public string NoKTP { get; set; }
        public string Password { get; set; }
        public string Token { get; set; }
        public string Semester { get; set; }
        public string ProdiAsal { get; set; }
        public string NIMAsal { get; set; }
        public string ReferenceNumber { get; set; }
        public bool isVerifikasi { get; set; }
        public virtual ICollection<Attachment> Attachments { get; set; }
        public virtual ICollection<KRS> KRs { get; set; }
    }
}
