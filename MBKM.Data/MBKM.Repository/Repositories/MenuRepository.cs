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
    public class MenuRepository : GenericRepository<Menu>, IMenuRepository
    {
        public MenuRepository(DbContext _db) : base(_db)
        {

        }
        public List<VMMenu> getListMenu()
        {
            using (var context = new MBKMContext())
            {
                var result = context.Menus.Where(x => x.IsDeleted == false && x.IsActive == true);
                var listmodel = result.Select(
                    x => new VMMenu
                    {
                        Nama = x.MenuName,
                        Nilai = x.ID.ToString()
                    });
                return listmodel.ToList();
            }
        }
        public VMListMenu getMenu(int Skip, int Length, string SearchParam, string SortBy, bool SortDir)
        {
            VMListMenu mListMenu = new VMListMenu();
            if (String.IsNullOrEmpty(SearchParam))
            {
                // if we have an empty search then just order the results by Id ascending
                //SortBy = "ID";
                //SortDir = true;
                SearchParam = "";
            }
            using (var context = new MBKMContext())
            {
                var result = context.Menus.Where(x => x.IsDeleted == false);
                mListMenu.TotalCount = result.Count();
                var gridfilter = result.AsQueryable().Where(y => y.MenuName.Contains(SearchParam) || y.MenuUrl.Contains(SearchParam))
                    .Select(z => new GridMenu
                    {
                        ID = z.ID,
                        MenuName = z.MenuName,
                        MenuDescription = z.MenuDescription,
                        MenuIcon = z.MenuIcon,
                        MenuUrl = z.MenuUrl,
                        MenuOrder = z.MenuOrder,
                        MenuParent = z.MenuParent,
                        Status = z.IsActive
                    }).OrderBy(SortBy, SortDir);
                mListMenu.gridDatas = gridfilter.Skip(Skip).Take(Length).ToList();
                mListMenu.TotalFilterCount = gridfilter.Count();
                return mListMenu;
            }
        }
    }
}
