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
    public class PendaftaranMataKuliahMap : EntityTypeConfiguration<PendaftaranMataKuliah>
    {
        public PendaftaranMataKuliahMap()
        {
            ToTable("PendaftaranMataKuliah");
            HasKey(t => t.ID).Property(t => t.ID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.DosenID).IsRequired();
            Property(t => t.DosenPembimbing).HasMaxLength(150).IsRequired();
            Property(t => t.Hasil).HasMaxLength(10).IsRequired();
            Property(t => t.MatkulIDAsal).HasMaxLength(50);
            Property(t => t.MatkulKodeAsal).HasMaxLength(150);
            Property(t => t.MatkulAsal).HasMaxLength(250);
            Property(t => t.Kesenjangan).HasMaxLength(5000);
            Property(t => t.Nilai).HasMaxLength(150);
            Property(t => t.Konversi).HasMaxLength(150);
            HasMany(x => x.CPLMatakuliahs)
            .WithMany(x => x.PendaftaranMataKuliahs)
            .Map(x =>
            {
                x.MapLeftKey("PendaftaranID");
                x.MapRightKey("CPLMKID");
                x.ToTable("CPLMKPendaftaran");
            });
            Property(t => t.StatusPendaftaran).HasMaxLength(50).IsRequired();
        }
    }
}
