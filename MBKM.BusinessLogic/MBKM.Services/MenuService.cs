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
    }
}
