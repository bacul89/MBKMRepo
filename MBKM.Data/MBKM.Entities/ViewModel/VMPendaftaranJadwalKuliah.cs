using MBKM.Entities.Basentities;
using MBKM.Entities.Models.MBKM;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace MBKM.Entities.ViewModel
{
    public class VMPendaftaranJadwalKuliah
    {
        public Int64 ID { get; set; }
        public Int64 DosenID { get; set; }
        public string NamaDosen { get; set; }
        public string MataKuliahID { get; set; }
        public string KodeMataKuliah { get; set; }
        public string NamaMataKuliah { get; set; }
        public string Hari { get; set; }
        public string JamMasuk { get; set; }
        public string JamSelesai { get; set; }
        public DateTime TglAwalKuliah { get; set; }
        public DateTime TglAkhirKuliah { get; set; }
        public string RuangKelas { get; set; }
        public string Lokasi { get; set; }
        public int STRM { get; set; }
        public int SKS { get; set; }
        public string ClassSection { get; set; }
        public string JenjangStudi { get; set; }
        public bool FlagOpen { get; set; }
        public Int64 FakultasID { get; set; }
        public string NamaFakultas { get; set; }
        public Int64 ProdiID { get; set; }
        public string NamaProdi { get; set; }
        public string JenisProgramMBKM { get; set; }
        public string JenisKegiatanMBKM { get; set; }
        public string NoKerjasama { get; set; }

        public VMPendaftaranJadwalKuliah(JadwalKuliah jk)
        {
            DosenID = jk.DosenID;
            NamaDosen = jk.NamaDosen;
            MataKuliahID = jk.MataKuliahID;
            KodeMataKuliah = jk.KodeMataKuliah;
            NamaMataKuliah = jk.NamaMataKuliah;
            Hari = jk.Hari;
            JamMasuk = jk.JamMasuk;
            JamSelesai = jk.JamSelesai;
            TglAwalKuliah = jk.TglAwalKuliah;
            TglAkhirKuliah = jk.TglAkhirKuliah;
            RuangKelas = jk.RuangKelas;
            Lokasi = jk.Lokasi;
            STRM = jk.STRM;
            SKS = (int) float.Parse(jk.SKS);
            ClassSection = jk.ClassSection;
            JenjangStudi = jk.JenjangStudi;
            FlagOpen = jk.FlagOpen;
            FakultasID = jk.FakultasID;
            NamaFakultas = jk.NamaFakultas;
            ProdiID = jk.ProdiID;
            NamaProdi = jk.NamaProdi;
        }

    }

}
