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
    public interface IFeedbackMatkulDetailService : IEntityService<FeedbackMatkulDetail>
    {
    }
    public class FeedbackMatkulDetailService : EntityService<FeedbackMatkulDetail>, IFeedbackMatkulDetailService
    {
        IUnitOfWork _unitOfWork;
        IFeedbackMatkulDetailRepository _feedbackRepository;

        public FeedbackMatkulDetailService(IUnitOfWork unitOfWork, IFeedbackMatkulDetailRepository FeedbackRepository)
            : base(unitOfWork, FeedbackRepository)
        {
            _unitOfWork = unitOfWork;
            _feedbackRepository = FeedbackRepository;
        }
    }
}
