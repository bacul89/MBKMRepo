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
                var listmodel = context.Menus.Select(
                    x => new VMMenu
                    {
                        Nama = x.MenuName,
                        Nilai = x.ID.ToString()
                    });
                return listmodel.ToList();
            }
        }
    }
}
