using MBKM.Entities.Models;
using MBKM.Entities.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MBKM.Common.Interfaces.RepoInterfaces
{
    public interface IMenuRepository : IGenericRepository<Menu>
    {
        List<VMMenu> getListMenu();
        VMListMenu getMenu(int skip, int take, string searchBy, string sortBy, bool sortDir);
    }
}
