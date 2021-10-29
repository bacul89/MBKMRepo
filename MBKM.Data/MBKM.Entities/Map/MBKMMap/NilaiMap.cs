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
    public class NilaiMap : EntityTypeConfiguration<NilaiKuliah>
    {
        public NilaiMap()
        {
            ToTable("Nilai");
            HasKey(t => t.ID).Property(t => t.ID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            //Property(t => t.Nilai).IsRequired();
            //Property(t => t.Persentase).IsRequired();
            Property(t => t.UTS).HasPrecision(5,2).IsRequired();
            Property(t => t.CW1).HasPrecision(5, 2).IsRequired();
            Property(t => t.CW2).HasPrecision(5, 2).IsRequired();
            Property(t => t.CW3).HasPrecision(5, 2).IsRequired();
            Property(t => t.CW4).HasPrecision(5, 2).IsRequired();
            Property(t => t.CW5).HasPrecision(5, 2).IsRequired();
            Property(t => t.Final).HasPrecision(5, 2).IsRequired();
            Property(t => t.NilaiTotal).HasPrecision(5, 2).IsRequired();
            Property(t => t.Grade).HasMaxLength(3).IsRequired();

        }
    }
}
