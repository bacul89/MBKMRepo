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
    public class CPLMatakuliahMap : EntityTypeConfiguration<CPLMatakuliah>
    {
        public CPLMatakuliahMap()
        {
            ToTable("CPLMatakuliah");
            HasKey(t => t.ID).Property(t => t.ID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.KodeMataKuliah).HasMaxLength(50).IsRequired();
            Property(t => t.NamaMataKuliah).HasMaxLength(350).IsRequired();
        }
    }
}
