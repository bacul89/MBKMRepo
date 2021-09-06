using MBKM.Entities.Basentities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MBKM.Entities.Models
{
    public class User : BaseEntity
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string NoPegawai { get; set; }
        public string Alamat { get; set; }
        public string NoTelp { get; set; }
        public string Token { get; set; }

        public Int64 RoleID { get; set; }
        public virtual Role Roles { get; set; }
    }
}
