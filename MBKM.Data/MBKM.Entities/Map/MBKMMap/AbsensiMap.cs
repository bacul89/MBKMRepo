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
    public class AbsensiMap : EntityTypeConfiguration<Absensi>
    {
        public AbsensiMap()
        {
            ToTable("Absensi");
            HasKey(t => t.ID).Property(t => t.ID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.TanggalAbsen).IsRequired();
            Property(t => t.InstructorId).HasMaxLength(100).IsRequired();
            Property(t => t.NamaDosen).HasMaxLength(250).IsRequired();
        }
    }
}
