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
            Property(t => t.Hari).HasMaxLength(15).IsRequired();
            Property(t => t.Waktu).IsRequired();
            Property(t => t.Kuota).IsRequired();
            Property(t => t.LinkMateri).HasMaxLength(250).IsRequired();
        }
    }
}
