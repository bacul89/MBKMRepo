using MBKM.Entities.Models.MBKM;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MBKM.Entities.Map.MBKMMap
{
    public class JadwalKuliahMap : EntityTypeConfiguration<JadwalKuliah>
    {
        public JadwalKuliahMap()
        {
            ToTable("JadwalKuliah");
            HasKey(t => t.ID).Property(t => t.ID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.DosenID).IsRequired();
            Property(t => t.NamaDosen).HasMaxLength(150).IsRequired();
            Property(t => t.KodeMataKuliah).HasMaxLength(50).IsRequired();
            Property(t => t.MataKuliahID).IsRequired();
            Property(t => t.NamaMataKuliah).IsRequired();
            Property(t => t.Hari).HasMaxLength(15).IsRequired();
            Property(t => t.STRM).IsRequired();
            Property(t => t.FakultasID).IsRequired();
            Property(t => t.NamaFakultas).HasMaxLength(250).IsRequired();
            Property(t => t.ProdiID).IsRequired();
            Property(t => t.NamaProdi).HasMaxLength(250).IsRequired();
            Property(t => t.JenjangStudi).HasMaxLength(150).IsRequired();
            Property(t => t.Lokasi).HasMaxLength(250).IsRequired();
            Property(t => t.SKS).HasMaxLength(50).IsRequired();
            Property(t => t.ClassSection).HasMaxLength(150).IsRequired();
            Property(t => t.JamMasuk).HasMaxLength(10).IsRequired();
            Property(t => t.JamSelesai).HasMaxLength(10).IsRequired();
            Property(t => t.TglAwalKuliah).IsRequired();
            Property(t => t.TglAkhirKuliah).IsRequired();
            Property(t => t.RuangKelas).HasMaxLength(150).IsRequired();
        }
    }
}
