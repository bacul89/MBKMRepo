using MBKM.Entities.Basentities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MBKM.Entities.Models
{
    public class EmailTemplate : BaseEntity
    {
        public string TipeMail { get; set; }
        public string SubjectMail { get; set; }
        public string BodyMail { get; set; }
    }
}
