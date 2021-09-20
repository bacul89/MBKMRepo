using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MBKM.Entities.ViewModel
{
    public class VMLogin
    {
        public string PasswordData { get; set; }
        public string Nama { get; set; }
        public string Email { get; set; }
        public string Alamat { get; set; }
        public DateTime TanggalLahir { get; set; }
        public string Gender { get; set; }
        public string Prodi { get; set; }
        public string Agama { get; set; }
        public string NIM { get; set; }
        public string Phone { get; set; }
        public Int64 IdMahasiswa { get; set; }
    }
}

