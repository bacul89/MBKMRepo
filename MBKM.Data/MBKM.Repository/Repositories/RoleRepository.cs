using MBKM.Common.Interfaces.RepoInterfaces;
using MBKM.Entities.Models;
using MBKM.Entities.ViewModel;
using MBKM.Repository.BaseRepository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Dynamic;

namespace MBKM.Repository.Repositories
{
    public class RoleRepository : GenericRepository<Role>, IRoleRepository
    {
        public RoleRepository(DbContext _db) : base(_db)
        {
        }

        public List<VMLookup> getLookupRole()
        {
            using (var context = new MBKMContext())
            {
                var result = context.Roles.Where(x => x.IsDeleted == false && x.IsActive == true);
                var listmodel = result.Select(
                    x => new VMLookup
                    {
                        Nama = x.RoleName,
                        Nilai = x.ID.ToString()
                    });
                return listmodel.ToList();
            }
        }

        public VMListRole getRole(int Skip, int Length, string SearchParam, string SortBy, bool SortDir)
        {
            VMListRole mListRole = new VMListRole();
            if (String.IsNullOrEmpty(SearchParam))
            {
                // if we have an empty search then just order the results by Id ascending
                //SortBy = "ID";
                //SortDir = true;
                SearchParam = "";
            }
            using (var context = new MBKMContext())
            {
                var result = context.Roles.Where(x => x.IsDeleted == false);
                mListRole.TotalCount = result.Count();
                var gridfilter = result.AsQueryable().Where(y => y.Code.Contains(SearchParam) || y.RoleName.Contains(SearchParam))
                    .Select(z => new GridRole
                    {
                        ID = z.ID,
                        Code = z.Code,
                        Status = z.IsActive,
                        RoleName = z.RoleName
                    }).OrderBy(SortBy, SortDir);
                mListRole.gridDatas = gridfilter.Skip(Skip).Take(Length).ToList();
                mListRole.TotalFilterCount = gridfilter.Count();
                return mListRole;
            }
        }
    }
}
