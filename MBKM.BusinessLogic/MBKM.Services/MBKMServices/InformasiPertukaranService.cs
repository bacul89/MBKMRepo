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
    public interface IInformasiPertukaranService : IEntityService<InformasiPertukaran>
    {
    }
    public class InformasiPertukaranService : EntityService<InformasiPertukaran>, IInformasiPertukaranService
    {
        IUnitOfWork _unitOfWork;
        IInformasiPertukaranRepository _infoRepository;

        public InformasiPertukaranService(IUnitOfWork unitOfWork, IInformasiPertukaranRepository InfoRepository)
            : base(unitOfWork, InfoRepository)
        {
            _unitOfWork = unitOfWork;
            _infoRepository = InfoRepository;
        }
    }
}
