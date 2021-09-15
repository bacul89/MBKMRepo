using MBKM.Entities.Models.MBKM;
using MBKM.Entities.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MBKM.Common.Interfaces.RepoInterfaces.MBKMRepoInterfaces
{
    public interface IPerjanjianKerjasamaRepository : IGenericRepository<PerjanjianKerjasama>
    {
        VMListPerjanjianKerjasama getListPerjanjianKerjasama(int Skip, int Length, string SearchParam, string SortBy, bool SortDir);
        List<VMLookupNoKerjasama> getNoKerjasama(int Skip, int Length,string Search);
    }
}
