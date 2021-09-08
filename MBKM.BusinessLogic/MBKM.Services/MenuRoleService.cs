using MBKM.Common.Interfaces;
using MBKM.Common.Interfaces.RepoInterfaces;
using MBKM.Entities.Models;
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
    }
}
