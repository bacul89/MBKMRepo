using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MBKM.Entities.ViewModel
{
    public class VMListUser
    {
        public int TotalCount { get; set; }
        public int TotalFilterCount { get; set; }
        public List<GridData> gridDatas { get; set; }
    }
    public class GridData
    {
        public Int64 ID { get; set; }
        public string NoPegawai { get; set; }
        public string Nama { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public double RoleID { get; set; }
        public string RoleName { get; set; }
        public string NamaFakultas { get; set; }
        public string KodeFakultas { get; set; }
        public string KPTSDIN { get; set; }
        public string KodeProdi { get; set; }
        public string NamaProdi { get; set; }
        public bool Status { get; set; }
    }
}
