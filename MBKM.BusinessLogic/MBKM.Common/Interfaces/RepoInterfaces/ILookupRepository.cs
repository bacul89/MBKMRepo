using MBKM.Entities.Models;
using MBKM.Entities.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MBKM.Common.Interfaces.RepoInterfaces
{
    public interface ILookupRepository : IGenericRepository<Lookup>
    {
        IEnumerable<VMLookup> getLookupByTipe(string tipe);
        List<VMListProdi> getListProdi();
    }
}
