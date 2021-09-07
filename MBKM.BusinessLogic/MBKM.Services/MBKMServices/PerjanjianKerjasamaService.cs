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
    public interface IPerjanjianKerjasamaService : IEntityService<PerjanjianKerjasama>
    {
    }
    public class PerjanjianKerjasamaService : EntityService<PerjanjianKerjasama>, IPerjanjianKerjasamaService
    {
        IUnitOfWork _unitOfWork;
        IPerjanjianKerjasamaRepository _perjanjianKerjasamaRepository;

        public PerjanjianKerjasamaService(IUnitOfWork unitOfWork, IPerjanjianKerjasamaRepository PerjanjianKerjasamaRepository)
            : base(unitOfWork, PerjanjianKerjasamaRepository)
        {
            _unitOfWork = unitOfWork;
            _perjanjianKerjasamaRepository = PerjanjianKerjasamaRepository;
        }
    }
}
