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
    public class NilaiSubCWMap : EntityTypeConfiguration<NilaiSubCW>
    {
        public NilaiSubCWMap()
        {
            ToTable("NilaiSubCW");
            HasKey(t => t.ID).Property(t => t.ID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.HeadCW).HasMaxLength(10).IsRequired();
        }
    }
}
