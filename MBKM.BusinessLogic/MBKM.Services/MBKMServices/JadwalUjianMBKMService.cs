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
    public interface IJadwalUjianMBKMService : IEntityService<JadwalUjianMBKM>
    {
    }
    public class JadwalUjianMBKMService : EntityService<JadwalUjianMBKM>, IJadwalUjianMBKMService
    {
        IUnitOfWork _unitOfWork;
        IJadwalUjianMBKMRepository _jadwalRepository;

        public JadwalUjianMBKMService(IUnitOfWork unitOfWork, IJadwalUjianMBKMRepository JadwalRepository)
            : base(unitOfWork, JadwalRepository)
        {
            _unitOfWork = unitOfWork;
            _jadwalRepository = JadwalRepository;
        }
    }
}
