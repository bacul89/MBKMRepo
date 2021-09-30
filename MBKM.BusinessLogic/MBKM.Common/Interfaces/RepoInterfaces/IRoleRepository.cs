using MBKM.Entities.Models;
using MBKM.Entities.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MBKM.Common.Interfaces.RepoInterfaces
{
    public interface IRoleRepository : IGenericRepository<Role>
    {
        List<VMLookup> getLookupRole();
        VMListRole getRole(int skip, int take, string searchBy, string sortBy, bool sortDir);
    }
}
