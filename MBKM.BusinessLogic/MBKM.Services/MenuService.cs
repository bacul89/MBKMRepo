using MBKM.Common.Helpers;
using MBKM.Common.Interfaces;
using MBKM.Common.Interfaces.RepoInterfaces;
using MBKM.Entities.Models;
using MBKM.Entities.ViewModel;
using MBKM.Services.BaseServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MBKM.Services
{
    public interface IMenuService : IEntityService<Menu>
    {
        List<VMMenu> getListMenu();
        VMListMenu getMenu(DataTableAjaxPostModel model);
    }

    public class MenuService : EntityService<Menu>, IMenuService
    {
        IUnitOfWork _unitOfWork;
        IMenuRepository _MenuRepository;

        public MenuService(IUnitOfWork unitOfWork, IMenuRepository MenuRepository)
            : base(unitOfWork, MenuRepository)
        {
            _unitOfWork = unitOfWork;
            _MenuRepository = MenuRepository;
        }
        public List<VMMenu> getListMenu()
        {
            return _MenuRepository.getListMenu();
        }
        public VMListMenu getMenu(DataTableAjaxPostModel model)
        {
            var searchBy = (model.search != null) ? model.search.value : null;
            var take = model.length;
            var skip = model.start;
            string sortBy = "";
            bool sortDir = true;

            if (model.order != null)
            {
                // in this example we just default sort on the 1st column
                sortBy = model.columns[model.order[0].column].data;
                sortDir = model.order[0].dir.ToLower() == "asc";
            }
            if (sortBy == null)
                sortBy = "ID";
            sortBy = sortBy + " " + model.order[0].dir.ToUpper();
            return _MenuRepository.getMenu(skip, take, searchBy, sortBy, sortDir);
        }
    }
}
