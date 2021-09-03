using MBKM.Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace MBKM.Entities.Map
{
    public class RoleMap : EntityTypeConfiguration<Role>
    {
        public RoleMap()
        {

        }
    }
}
