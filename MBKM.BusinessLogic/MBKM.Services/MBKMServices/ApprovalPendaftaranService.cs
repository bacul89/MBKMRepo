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
    public interface IApprovalPendaftaranService : IEntityService<ApprovalPendaftaran>
    {
    }
    public class ApprovalPendaftaranService : EntityService<ApprovalPendaftaran>, IApprovalPendaftaranService
    {
        IUnitOfWork _unitOfWork;
        IApprovalPendaftaranRepository _approvalRepository;

        public ApprovalPendaftaranService(IUnitOfWork unitOfWork, IApprovalPendaftaranRepository ApprovalRepository)
            : base(unitOfWork, ApprovalRepository)
        {
            _unitOfWork = unitOfWork;
            _approvalRepository = ApprovalRepository;
        }
    }
}
