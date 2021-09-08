using MBKM.Entities.Basentities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MBKM.Entities.Models
{
    public class MenuRole : BaseEntity
    {
        public Boolean IsView { get; set; }
        public Boolean IsCreate { get; set; }
        public Boolean IsDelete { get; set; }
        public Boolean IsUpdate { get; set; }
        public Int64 RoleID { get; set; }
        public Int64 MenuID { get; set; }
        public virtual Menu Menus { get; set; }
        public virtual Role Roles { get; set; }
    }
}
