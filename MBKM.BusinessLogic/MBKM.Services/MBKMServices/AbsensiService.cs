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
    public interface IAbsensiService : IEntityService<Absensi>
    {
        string GetSemesterBySTRM(int strm);
    }
    public class AbsensiService : EntityService<Absensi>, IAbsensiService
    {
        IUnitOfWork _unitOfWork;
        IAbsensiRepository _absensiRepository;

        public AbsensiService(IUnitOfWork unitOfWork, IAbsensiRepository AbsensiRepository)
            : base(unitOfWork, AbsensiRepository)
        {
            _unitOfWork = unitOfWork;
            _absensiRepository = AbsensiRepository;
        }
        public string GetSemesterBySTRM(int strm)
        {
            return _absensiRepository.GetSemesterBySTRM(strm);
        }
    }
}
