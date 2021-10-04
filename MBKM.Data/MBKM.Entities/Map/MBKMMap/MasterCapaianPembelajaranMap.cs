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
    public class MasterCapaianPembelajaranMap : EntityTypeConfiguration<MasterCapaianPembelajaran>
    {
        public MasterCapaianPembelajaranMap()
        {
            ToTable("MasterCapaianPembelajaran");
            HasKey(t => t.ID).Property(t => t.ID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.Kelompok).HasMaxLength(50).IsRequired();
            Property(t => t.Kode).HasMaxLength(20).IsRequired();
            Property(t => t.FakultasID).HasMaxLength(30).IsRequired();
            Property(t => t.NamaFakultas).HasMaxLength(150).IsRequired();
            Property(t => t.ProdiID).HasMaxLength(30).IsRequired();
            Property(t => t.NamaProdi).HasMaxLength(150);
        }
    }
}
