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

    public interface ILookupService : IEntityService<Lookup>
    {
        IEnumerable<VMLookup> getLookupByTipe(string tipe);
        List<VMListProdi> getListProdi();
        List<VMUserProdi> getUserProdi();
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

        public List<VMListProdi> getListProdi()
        {
            return _lookupRepository.getListProdi();
        }

        public List<VMUserProdi> getUserProdi()
        {
            return _lookupRepository.getUserProdi();
        }

        public IEnumerable<VMLookup> getLookupByTipe(string tipe)
        {
            return _lookupRepository.getLookupByTipe(tipe);
        }
    }
}
