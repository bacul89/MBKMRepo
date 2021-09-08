using MBKM.Entities.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace MBKM.Entities.Map
{
    public class MenuMap : EntityTypeConfiguration<Menu>
    {
        public MenuMap()
        {
            ToTable("Menu");
            HasKey(t => t.ID).Property(t => t.ID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.MenuName).HasMaxLength(150).IsRequired();
            Property(t => t.MenuDescription).HasMaxLength(500);
            Property(t => t.MenuIcon).HasMaxLength(20);
            Property(t => t.MenuUrl).HasMaxLength(100).IsRequired();
            Property(t => t.MenuIcon).HasMaxLength(20);
            Property(t => t.MenuOrder).HasMaxLength(20);

        }
    }
}
