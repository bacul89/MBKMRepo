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
    public interface IJadwalUjianMBKMDetailService : IEntityService<JadwalUjianMBKMDetail>
    {
       // object GetListSeksi();
    }
    public class JadwalUjianMBKMDetailService : EntityService<JadwalUjianMBKMDetail>, IJadwalUjianMBKMDetailService
    {
        IUnitOfWork _unitOfWork;
        IJadwalUjianMBKMDetailRepository _jadwalRepository;

        public JadwalUjianMBKMDetailService(IUnitOfWork unitOfWork, IJadwalUjianMBKMDetailRepository JadwalRepository)
            : base(unitOfWork, JadwalRepository)
        {
            _unitOfWork = unitOfWork;
            _jadwalRepository = JadwalRepository;
        }
    }
}
