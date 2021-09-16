using MBKM.Entities.Basentities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MBKM.Entities.Models.MBKM
{
    public class Mahasiswa : BaseEntity
    {
        public string NamaUniversitas { get; set; }
        [Required(ErrorMessage = "Nama harus di isi")]
        public string  Nama { get; set; }
        [Required(ErrorMessage = "Email harus di isi")]
        public string Email { get; set; }
        public string Telepon { get; set; }
        [Required(ErrorMessage = "No Hp harus di isi")]
        public string NoHp { get; set; }
        [Required(ErrorMessage = "Tanggal harus di isi")]
        public DateTime TanggalLahir { get; set; }
        public string Alamat { get; set; }
        public string Agama { get; set; }
        public string NoKTP { get; set; }
        [Required(ErrorMessage = "Password harus di isi")]
        public string Password { get; set; }
        public string Token { get; set; }
        public string Semester { get; set; }
        public string ProdiAsal { get; set; }
        public string Gender { get; set; }
        public string NIMAsal { get; set; }
        public string NIM { get; set; }
        [Required(ErrorMessage = "Tempat Lahir harus di isi")]
        public string TempatLahir { get; set; }
        public string ReferenceNumber { get; set; }
        public string NamaDarurat { get; set; }
        public string HubunganDarurat { get; set; }
        public string NoHPDarurat { get; set; }
        public string TeleponDarurat { get; set; }
        public string EmailDarurat { get; set; }
        public string AlamatDarurat { get; set; }
        public string JenjangStudi { get; set; }
        public string WargaNegara { get; set; }
        public string NoKerjasama { get; set; }
        public int BiayaKuliah { get; set; } = 0;
        public string StatusVerifikasi { get; set; }
        public string Approval { get; set; }
        public string Catatan { get; set; }
        public string StatusKerjasama { get; set; }
        public virtual ICollection<Attachment> Attachments { get; set; }
        public virtual ICollection<KRS> KRs { get; set; }
    }
}
