using MBKM.Common.Interfaces.RepoInterfaces;
using MBKM.Entities.Models;
using MBKM.Entities.ViewModel;
using MBKM.Repository.BaseRepository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Dynamic;

namespace MBKM.Repository.Repositories
{
    public class MenuRoleRepository : GenericRepository<MenuRole>, IMenuRoleRepository
    {
        public MenuRoleRepository(DbContext _db) : base(_db)
        {
        }

        public VMListMenuRole getListMKGrid(int Skip, int Length, string SearchParam, string SortBy, bool SortDir)
        {
            VMListMenuRole mListMenuRole = new VMListMenuRole();
            if (String.IsNullOrEmpty(SearchParam))
            {
                // if we have an empty search then just order the results by Id ascending
                //SortBy = "ID";
                //SortDir = true;
                SearchParam = "";
            }
            using (var context = new MBKMContext())
            {
                var result = context.MenuRoles.Where(x => x.IsDeleted == false);
                mListMenuRole.TotalCount = result.Count();
                var gridfilter = result.AsQueryable().Where(y => y.Roles.RoleName.Contains(SearchParam)|| y.Menus.MenuName.Contains(SearchParam))
                    .Select(z => new GridDataMenuRole {
                        ID = z.ID,
                        MenuID = z.MenuID,
                        Status = z.IsActive,
                        RoleName = z.Roles.RoleName,
                        MenuName = z.Menus.MenuName,
                        RoleID = z.RoleID,
                        IsCreate = z.IsCreate,
                        IsDelete = z.IsDelete,
                        IsUpdate = z.IsUpdate,
                        IsView = z.IsView
                    }).OrderBy(SortBy, SortDir);
                mListMenuRole.gridDatas = gridfilter.Skip(Skip).Take(Length).ToList();
                mListMenuRole.TotalFilterCount = gridfilter.Count();
                return mListMenuRole;
            }
        }
    }
}
