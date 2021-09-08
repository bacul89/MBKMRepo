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
    public class MenuRoleMap : EntityTypeConfiguration<MenuRole>
    {
        public MenuRoleMap()
        {
            ToTable("MenuRole");
            HasKey(t => t.ID).Property(t => t.ID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            HasKey(p => new {
                p.RoleID,
                p.MenuID
            });
            
                //.Map(m => m.ToTable("Trainee_TraineeRole_Mapping"));
        }
    }
}
