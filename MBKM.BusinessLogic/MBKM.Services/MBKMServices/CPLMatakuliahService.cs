using MBKM.Common.Interfaces;
using MBKM.Common.Interfaces.RepoInterfaces.MBKMRepoInterfaces;
using MBKM.Entities.Models.MBKM;
using MBKM.Services.BaseServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MBKM.Services.MBKMServices
{

    public interface ICPLMatakuliahService : IEntityService<CPLMatakuliah>
    {
    }
    public class CPLMatakuliahService : EntityService<CPLMatakuliah>, ICPLMatakuliahService
    {
        IUnitOfWork _unitOfWork;
        ICPLMataKuliahRepository _cplRepository;

        public CPLMatakuliahService(IUnitOfWork unitOfWork, ICPLMataKuliahRepository CPLRepository)
            : base(unitOfWork, CPLRepository)
        {
            _unitOfWork = unitOfWork;
            _cplRepository = CPLRepository;
        }
    }
}
