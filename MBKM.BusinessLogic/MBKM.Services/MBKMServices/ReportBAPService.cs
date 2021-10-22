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
    public interface IReportBAPService : IEntityService<Absensi>
    {
        IEnumerable<VMListReportBAP> GetBAPBYAbsenID(int id);
    }
    public class ReportBAPService : EntityService<Absensi>, IReportBAPService
    {
        IUnitOfWork _unitOfWork;
        IReportBAPRepository _bapRepository;

        public ReportBAPService(IUnitOfWork unitOfWork, IReportBAPRepository BAPRepository)
            : base(unitOfWork, BAPRepository)
        {
            _unitOfWork = unitOfWork;
            _bapRepository = BAPRepository;
        }

        public IEnumerable<VMListReportBAP> GetBAPBYAbsenID(int id)
        {
            return _bapRepository.GetBAPByAbsensiID(id);
        }
    }
}
