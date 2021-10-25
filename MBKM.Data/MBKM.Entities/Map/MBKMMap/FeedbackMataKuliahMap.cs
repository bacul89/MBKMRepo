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
    public class FeedbackMataKuliahMap : EntityTypeConfiguration<FeedbackMatkul>
    {
        public FeedbackMataKuliahMap()
        {
            ToTable("FeedbackMataKul");
            HasKey(t => t.ID).Property(t => t.ID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.DosenID).HasMaxLength(50).IsRequired();
            Property(t => t.NamaDosen).HasMaxLength(150).IsRequired();
            Property(t => t.KritikSaran).HasMaxLength(5000).IsRequired();
        }
    }
}
