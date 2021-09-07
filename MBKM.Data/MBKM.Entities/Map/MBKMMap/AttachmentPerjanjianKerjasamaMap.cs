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
    public class AttachmentPerjanjianKerjasamaMap : EntityTypeConfiguration<AttachmentPerjanjianKerjasama>
    {
        public AttachmentPerjanjianKerjasamaMap()
        {
            ToTable("AttachmentPerjanjianKerjasama");
            HasKey(t => t.ID).Property(t => t.ID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.FileName).HasMaxLength(250).IsRequired();
        }
    }
}
