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
    public interface IPendaftaranMataKuliahService : IEntityService<PendaftaranMataKuliah>
    {
    }
    public class PendaftaranMataKuliahService : EntityService<PendaftaranMataKuliah>, IPendaftaranMataKuliahService
    {
        IUnitOfWork _unitOfWork;
        IPendaftaranMataKuliahRepository _pendaftaranRepository;

        public PendaftaranMataKuliahService(IUnitOfWork unitOfWork, IPendaftaranMataKuliahRepository PendaftaranRepository)
            : base(unitOfWork, PendaftaranRepository)
        {
            _unitOfWork = unitOfWork;
            _pendaftaranRepository = PendaftaranRepository;
        }
    }
}
