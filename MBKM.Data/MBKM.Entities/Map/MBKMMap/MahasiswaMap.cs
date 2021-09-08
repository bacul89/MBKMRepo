using MBKM.Entities.Models.MBKM;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MBKM.Entities.Map.MBKMMap
{
    public class MahasiswaMap : EntityTypeConfiguration<Mahasiswa>
    {
        public MahasiswaMap()
        {
            ToTable("Mahasiswa");
            HasKey(t => t.ID).Property(t => t.ID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.NamaUniversitas).HasMaxLength(150);
            Property(t => t.Nama).HasMaxLength(250).IsRequired();
            Property(t => t.Email).HasMaxLength(250).IsRequired();
            Property(t => t.Telepon).HasMaxLength(150).IsRequired();
            Property(t => t.TanggalLahir).IsRequired();
            Property(t => t.NoKTP).HasMaxLength(150);
            Property(t => t.Password).HasMaxLength(500).IsRequired();
            Property(t => t.Agama).HasMaxLength(150);
            Property(t => t.NIMAsal).HasMaxLength(50);
            Property(t => t.ProdiAsal).HasMaxLength(150);
            Property(t => t.Semester).HasMaxLength(30);
            Property(t => t.NIM).HasMaxLength(50);
            Property(t => t.TempatLahir).HasMaxLength(150).IsRequired();
        }
    }
}
