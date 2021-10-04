using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MBKM.Entities.ViewModel
{
    public class VMListMenu
    {
        public int TotalCount { get; set; }
        public int TotalFilterCount { get; set; }
        public List<GridMenu> gridDatas { get; set; }
    }
    public class GridMenu
    {
        public Int64 ID { get; set; }
        public string MenuName { get; set; }
        public string MenuDescription { get; set; }
        public string MenuUrl { get; set; }
        public string MenuParent { get; set; }
        public string MenuIcon { get; set; }
        public string MenuOrder { get; set; }
        public bool Status { get; set; }
    }
}
