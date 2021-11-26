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
    public class InformasiPertukaranMap : EntityTypeConfiguration<InformasiPertukaran>
    {
        public InformasiPertukaranMap()
        {
            ToTable("InformasiPertukaran");
            HasKey(t => t.ID).Property(t => t.ID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.JenisKerjasama).HasMaxLength(150).IsRequired();
            Property(t => t.JenisPertukaran).HasMaxLength(150).IsRequired();
            Property(t => t.NoKerjasama).HasMaxLength(150);
            Property(t => t.MahasiswaID).IsRequired();
            Property(t => t.STRM).IsRequired();
            
            Property(t => t.JudulAktivitas).HasMaxLength(350);
            Property(t => t.LokasiTugas).HasMaxLength(150);
            Property(t => t.NoSK).HasMaxLength(150);
            Property(t => t.TanggalSK).IsOptional();
        }        
    }
}
