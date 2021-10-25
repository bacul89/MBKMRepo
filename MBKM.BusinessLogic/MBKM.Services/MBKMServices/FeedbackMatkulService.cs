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
    public interface IFeedbackMatkulService : IEntityService<FeedbackMatkul>
    {
    }
    public class FeedbackMatkulService : EntityService<FeedbackMatkul>, IFeedbackMatkulService
    {
        IUnitOfWork _unitOfWork;
        IFeedbackMatkulRepository _feedbackRepository;

        public FeedbackMatkulService(IUnitOfWork unitOfWork, IFeedbackMatkulRepository FeedbackRepository)
            : base(unitOfWork, FeedbackRepository)
        {
            _unitOfWork = unitOfWork;
            _feedbackRepository = FeedbackRepository;
        }
    }
}
