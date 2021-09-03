using MBKM.Entities.Basentities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MBKM.Entities.Models.MBKM
{
    public class Feedback : BaseEntity
    {
        public string Rating { get; set; }
        public string Comment { get; set; }
    }
}
