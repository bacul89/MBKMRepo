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
    public interface IUserService : IEntityService<User>
    {
        //VMListUser getListUserGrid(int Skip, int Length, string SearchParama);
        VMListUser getListUserGrid(DataTableAjaxPostModel model);
    }

    public class UserService : EntityService<User>, IUserService
    {
        IUnitOfWork _unitOfWork;
        IUserRepository _userRepository;

        public UserService(IUnitOfWork unitOfWork, IUserRepository userRepository)
            : base(unitOfWork, userRepository)
        {
            _unitOfWork = unitOfWork;
            _userRepository = userRepository;
        }

        //public VMListUser getListUserGrid(int Skip, int Length, string SearchParam)
        //{
        //    return _userRepository.getListUserGrid(Skip, Length, SearchParam);
        //}

        public VMListUser getListUserGrid(DataTableAjaxPostModel model)
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
            return _userRepository.getListUserGrid(skip, take, searchBy, sortBy, sortDir);
        }
    }
}
