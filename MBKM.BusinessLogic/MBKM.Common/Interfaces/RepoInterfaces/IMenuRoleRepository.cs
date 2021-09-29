using MBKM.Entities.Models;
using MBKM.Entities.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MBKM.Common.Interfaces.RepoInterfaces
{
    public interface IMenuRoleRepository : IGenericRepository<MenuRole>
    {
        VMListMenuRole getListMKGrid(int Skip, int Length, string SearchParam, string SortBy, bool SortDir);
    }
}
