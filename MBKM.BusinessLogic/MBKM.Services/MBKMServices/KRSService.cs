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
    public interface IKRSService : IEntityService<KRS>
    {
    }
    public class KRSService : EntityService<KRS>, IKRSService
    {
        IUnitOfWork _unitOfWork;
        IKRSRepository _krsRepository;

        public KRSService(IUnitOfWork unitOfWork, IKRSRepository KRSRepository)
            : base(unitOfWork, KRSRepository)
        {
            _unitOfWork = unitOfWork;
            _krsRepository = KRSRepository;
        }
    }
}
