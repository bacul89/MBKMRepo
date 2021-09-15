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
    public class PerjanjianKerjasamaMap : EntityTypeConfiguration<PerjanjianKerjasama>
    {
        public PerjanjianKerjasamaMap()
        {
            ToTable("PerjanjianKerjasama");
            HasKey(t => t.ID).Property(t => t.ID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.NamaInstansi).HasMaxLength(150).IsRequired();
            Property(t => t.NamaUnit).HasMaxLength(150).IsRequired();
            Property(t => t.Instansi).HasMaxLength(50).IsRequired();
            Property(t => t.JenisPertukaran).HasMaxLength(100).IsRequired();
            Property(t => t.JenisKerjasama).HasMaxLength(100).IsRequired();

        }
    }
}
