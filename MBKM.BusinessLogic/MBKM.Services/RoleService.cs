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
    public interface IRoleService : IEntityService<Role>
    {
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
    }
}
