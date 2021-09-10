using MBKM.Entities.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MBKM.Entities.Map
{
    public class EmailTemplateMap : EntityTypeConfiguration<EmailTemplate>
    {
        public EmailTemplateMap()
        {
            ToTable("EmailTemplate");
            HasKey(t => t.ID).Property(t => t.ID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.TipeMail).HasMaxLength(50).IsRequired();
            Property(t => t.SubjectMail).HasMaxLength(500).IsRequired();
            Property(t => t.BodyMail).HasMaxLength(8000).IsRequired();
        }
    }
}
