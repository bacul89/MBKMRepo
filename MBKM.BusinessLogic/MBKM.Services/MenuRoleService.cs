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
    public interface IMenuRoleService : IEntityService<MenuRole>
    {
        VMListMenuRole getListMRGrid(DataTableAjaxPostModel model);
    }

    public class MenuRoleService : EntityService<MenuRole>, IMenuRoleService
    {
        IUnitOfWork _unitOfWork;
        IMenuRoleRepository _MenuRoleRepository;

        public MenuRoleService(IUnitOfWork unitOfWork, IMenuRoleRepository MenuRoleRepository)
            : base(unitOfWork, MenuRoleRepository)
        {
            _unitOfWork = unitOfWork;
            _MenuRoleRepository = MenuRoleRepository;
        }

        public VMListMenuRole getListMRGrid(DataTableAjaxPostModel model)
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
            return _MenuRoleRepository.getListMKGrid(skip, take, searchBy, sortBy, sortDir);
        }
    }
}
