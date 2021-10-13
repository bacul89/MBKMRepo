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
    public class JadwalUjianMBKMMap : EntityTypeConfiguration<JadwalUjianMBKM>
    {
        public JadwalUjianMBKMMap()
        {
            ToTable("JadwalUjianMBKM");
            HasKey(t => t.ID).Property(t => t.ID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.IDMatkul).HasMaxLength(30).IsRequired();
            Property(t => t.KodeMatkul).HasMaxLength(50).IsRequired();
            Property(t => t.NamaMatkul).HasMaxLength(250).IsRequired();
            Property(t => t.JenjangStudi).HasMaxLength(10).IsRequired();
            Property(t => t.FakultasID).HasMaxLength(20).IsRequired();
            Property(t => t.NamaFakultas).HasMaxLength(150).IsRequired();
            Property(t => t.KodeClassSection).HasMaxLength(20).IsRequired();
            Property(t => t.ClassSection).HasMaxLength(10).IsRequired();
            Property(t => t.ProdiID).HasMaxLength(20);
            Property(t => t.NamaProdi).HasMaxLength(150);
            Property(t => t.KodeRuangUjian).HasMaxLength(20).IsRequired();
            Property(t => t.RuangUjian).HasMaxLength(50).IsRequired();
            Property(t => t.Lokasi).HasMaxLength(150).IsRequired();
            Property(t => t.TanggalUjian).IsRequired();
            Property(t => t.JamMulai).HasMaxLength(20).IsRequired();
            Property(t => t.JamAkhir).HasMaxLength(20).IsRequired();
            Property(t => t.STRM).HasMaxLength(20).IsRequired();
            Property(t => t.TipeUjian).HasMaxLength(20).IsRequired();
            Property(t => t.KapasitasRuangan).IsRequired();
            Property(t => t.Tersedia).IsRequired();
        }
    }
}
