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
    public class FeedbackMataKuliahMap : EntityTypeConfiguration<FeedbackMataKuliah>
    {
        public FeedbackMataKuliahMap()
        {
            ToTable("FeedbackMataKuliah");
            HasKey(t => t.ID).Property(t => t.ID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.DosenID).IsRequired();
            Property(t => t.NamaDosen).HasMaxLength(150).IsRequired();
            Property(t => t.NilaiFeed).IsRequired();
            Property(t => t.KategoriPertanyaan).HasMaxLength(250).IsRequired();
            Property(t => t.Pertanyaan).HasMaxLength(5000).IsRequired();
            Property(t => t.KritikSaran).HasMaxLength(5000).IsRequired();
        }
    }
}
