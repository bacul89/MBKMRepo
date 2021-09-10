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
    class LookupService
    {
    }
    public interface ILookupService : IEntityService<Lookup>
    {
    }

    public class LookupService : EntityService<Lookup>, ILookupService
    {
        IUnitOfWork _unitOfWork;
        ILookupRepository _lookupRepository;

        public LookupService(IUnitOfWork unitOfWork, ILookupRepository LookupRepository)
            : base(unitOfWork, LookupRepository)
        {
            _unitOfWork = unitOfWork;
            _lookupRepository = LookupRepository;
        }
    }
}
