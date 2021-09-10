using MBKM.Entities.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MBKM.Entities.Map
{
    public class LookupMap : EntityTypeConfiguration<Lookup>
    {
        public LookupMap()
        {
            ToTable("Lookup");
            HasKey(t => t.ID).Property(t => t.ID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.Tipe).HasMaxLength(50).IsRequired();
            Property(t => t.Nama).HasMaxLength(100).IsRequired();
            Property(t => t.Nilai).HasMaxLength(100).IsRequired();
        }
    }
}
