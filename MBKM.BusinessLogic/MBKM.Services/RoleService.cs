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
    public interface IRoleService : IEntityService<Role>
    {
        List<VMLookup> getLookupRole();
        VMListRole getRole(DataTableAjaxPostModel model);
    }

    public class RoleService : EntityService<Role>, IRoleService
    {
        IUnitOfWork _unitOfWork;
        IRoleRepository _RoleRepository;

        public RoleService(IUnitOfWork unitOfWork, IRoleRepository RoleRepository)
            : base(unitOfWork, RoleRepository)
        {
            _unitOfWork = unitOfWork;
            _RoleRepository = RoleRepository;
        }

        public List<VMLookup> getLookupRole()
        {
            return _RoleRepository.getLookupRole();
        }
        public VMListRole getRole(DataTableAjaxPostModel model)
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
            return _RoleRepository.getRole(skip, take, searchBy, sortBy, sortDir);
        }
    }
}
