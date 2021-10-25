using MBKM.Entities.Models.MBKM;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace MBKM.Entities.Map.MBKMMap
{
    public class FeedbackMatkulDetailMap : EntityTypeConfiguration<FeedbackMatkulDetail>
    {
        public FeedbackMatkulDetailMap()
        {
            ToTable("FeedbackMatkulDetail");
            HasKey(t => t.ID).Property(t => t.ID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.KategoriPertanyaan).HasMaxLength(100).IsRequired();
            Property(t => t.Pertanyaan).HasMaxLength(550).IsRequired();
            Property(t => t.PertanyaanID).HasMaxLength(50).IsRequired();
            Property(t => t.Nilai).IsRequired();
        }
    }
}
