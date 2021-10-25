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
    public interface INilaiSubCWService : IEntityService<NilaiSubCW>
    {
    }
    public class NilaiSubCWService : EntityService<NilaiSubCW>, INilaiSubCWService
    {
        IUnitOfWork _unitOfWork;
        INilaiSubCWRepository _nilaisubRepository;

        public NilaiSubCWService(IUnitOfWork unitOfWork, INilaiSubCWRepository subCWRepository)
            : base(unitOfWork, subCWRepository)
        {
            _unitOfWork = unitOfWork;
            _nilaisubRepository = subCWRepository;
        }
    }
}
