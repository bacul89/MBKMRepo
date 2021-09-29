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
    public class ApprovalPendaftaranMap : EntityTypeConfiguration<ApprovalPendaftaran>
    {
        public ApprovalPendaftaranMap()
        {
            ToTable("ApprovalPendaftaran");
            HasKey(t => t.ID).Property(t => t.ID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.Approval).HasMaxLength(150).IsRequired();
            Property(t => t.Catatan).HasMaxLength(5000).IsRequired();
            Property(t => t.StatusPendaftaran).HasMaxLength(50).IsRequired();
        }
    }
}
