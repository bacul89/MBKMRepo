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
    public class UserMap : EntityTypeConfiguration<User>
    {
        public UserMap()
        {
            ToTable("User");
            HasKey(t => t.ID).Property(t => t.ID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.UserName).HasMaxLength(50).IsRequired();
            Property(t => t.Password).HasMaxLength(500).IsRequired();
            Property(t => t.Email).HasMaxLength(150).IsRequired();
            Property(t => t.NoPegawai).HasMaxLength(50).IsRequired();
            Property(t => t.Alamat).HasMaxLength(500);
            Property(t => t.NoTelp).HasMaxLength(50).IsRequired();
            Property(t => t.Token).HasMaxLength(500);
            Property(t => t.KodeProdi).HasMaxLength(50);
            Property(t => t.NamaProdi).HasMaxLength(150);
            //TODO
            Property(t => t.KodeFakultas).HasMaxLength(50);
            Property(t => t.NamaFakultas).HasMaxLength(150);
            Property(t => t.KPTSDIN).HasMaxLength(150);

        }
    }
}
