using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MBKM.Entities.ViewModel
{
    public class VMListRole
    {
        public int TotalCount { get; set; }
        public int TotalFilterCount { get; set; }
        public List<GridRole> gridDatas { get; set; }
    }
    public class GridRole
    {
        public Int64 ID { get; set; }
        public string Code { get; set; }
        public string RoleName { get; set; }
        public bool Status { get; set; }
    }
}
