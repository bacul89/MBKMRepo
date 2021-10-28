using MBKM.Common.Interfaces;
using MBKM.Common.Interfaces.RepoInterfaces.MBKMRepoInterfaces;
using MBKM.Entities.Models.MBKM;
using MBKM.Entities.ViewModel;
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
        IEnumerable<VMDosenMakulPertemuan> GetDosenMakulPertemuans(string KodeMK, string ClassSection, string strm, string fakulId);
        IEnumerable<VMPertanyaanFeedback> GetPertanyaanFeedbacks(string jenjangStudi, string strm);
        IEnumerable<VMJawabanFeedback> GetJawabanFeedback(string KodeJawaban);
        VMSemester GetSemesterByStrm(string strm);
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

        public IEnumerable<VMDosenMakulPertemuan> GetDosenMakulPertemuans(string KodeMK, string ClassSection, string strm, string fakulId)
        {
            return _feedbackRepository.GetDosenMakulPertemuans(KodeMK, ClassSection, strm, fakulId);
        }

        public IEnumerable<VMJawabanFeedback> GetJawabanFeedback(string KodeJawaban)
        {
            return _feedbackRepository.GetJawabanFeedback(KodeJawaban);
        }

        public IEnumerable<VMPertanyaanFeedback> GetPertanyaanFeedbacks(string jenjangStudi, string strm)
        {
            return _feedbackRepository.GetPertanyaanFeedbacks(jenjangStudi, strm);
        }

        public VMSemester GetSemesterByStrm(string strm)
        {
            return _feedbackRepository.GetSemesterByStrm(strm);
        }
    }
}
