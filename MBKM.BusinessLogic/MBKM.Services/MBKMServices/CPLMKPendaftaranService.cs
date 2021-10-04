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
    public interface ICPLMKPendaftaranService : IEntityService<CPLMKPendaftaran>
    {
    }
    public class CPLMKPendaftaranService : EntityService<CPLMKPendaftaran>, ICPLMKPendaftaranService
    {
        IUnitOfWork _unitOfWork;
        ICPLMKPendaftaranRepository _cpmlkRepository;

        public CPLMKPendaftaranService(IUnitOfWork unitOfWork, ICPLMKPendaftaranRepository CpmlkRepository)
            : base(unitOfWork, CpmlkRepository)
        {
            _unitOfWork = unitOfWork;
            _cpmlkRepository = CpmlkRepository;
        }
    }
}
