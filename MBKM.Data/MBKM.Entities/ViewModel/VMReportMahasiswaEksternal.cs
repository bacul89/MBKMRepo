using MBKM.Entities.Models.MBKM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MBKM.Entities.ViewModel
{
    public class VMReportMahasiswaEksternal
    {
        public Int64 ID { get; set; }
        public string MatkulIDAsal { get; set; }
        public string MatkulKodeAsal { get; set; }
        public string MatkulAsal { get; set; }
        public string Kesenjangan { get; set; }
        public string Nilai { get; set; }
        public string Grade { get; set; }
        public string Konversi { get; set; }
        public string Hasil { get; set; }
        public Int64 DosenID { get; set; }
        public string DosenPembimbing { get; set; }
        public Int64 MahasiswaID { get; set; }
        public virtual Mahasiswa mahasiswas { get; set; }
        public Int64 JadwalKuliahID { get; set; }
        public virtual JadwalKuliah JadwalKuliahs { get; set; }
        public string StatusPendaftaran { get; set; }
        public Int64 informasiID { get; set; }
        public virtual InformasiPertukaran InformasiPertukaran { get; set; }
        public virtual NilaiKuliah NilaiKuliah { get; set; }
    }
}
