using MBKM.Entities.Models.MBKM;
using MBKM.Entities.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MBKM.Common.Interfaces.RepoInterfaces.MBKMRepoInterfaces
{
    public interface IFeedbackMatkulRepository : IGenericRepository<FeedbackMatkul>
    {
        IEnumerable<VMDosenMakulPertemuan> GetDosenMakulPertemuans(string KodeMK, string ClassSection, string strm, string fakulId);
        IEnumerable<VMPertanyaanFeedback> GetPertanyaanFeedbacks(string jenjangStudi, string strm, long fakultas);
        IEnumerable<VMJawabanFeedback> GetJawabanFeedback(string KodeJawaban);
        VMSemester GetSemesterByStrm(string strm);
    }
}
