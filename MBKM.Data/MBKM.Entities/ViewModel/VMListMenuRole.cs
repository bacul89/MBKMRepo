using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MBKM.Entities.ViewModel
{
    public class VMListMenuRole
    {
        public int TotalCount { get; set; }
        public int TotalFilterCount { get; set; }
        public List<GridDataMenuRole> gridDatas { get; set; }
    }
    public class GridDataMenuRole
    { 
        public Int64 ID { get; set; }
        public double MenuID { get; set; }
        public string MenuName { get; set; }
        public double RoleID { get; set; }
        public string RoleName { get; set; }
        public bool IsView { get; set; }
        public bool IsCreate { get; set; }
        public bool IsDelete { get; set; }
        public bool IsUpdate { get; set; }
        public bool Status { get; set; }


    }
}
