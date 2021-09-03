using MBKM.Entities.Basentities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MBKM.Entities.Models
{
    public class Role : BaseEntity
    {
        public string Code { get; set; }
        public string RoleName { get; set; }
        public virtual ICollection<MenuRole> MenuRoles { get; set; }
    }
}
