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
    public interface IJadwalKuliahService : IEntityService<JadwalKuliah>
    {
        IEnumerable<VMSemester> GetSemesterAll(int skip, int take, string search);
    }
    public class JadwalKuliahService : EntityService<JadwalKuliah>, IJadwalKuliahService
    {
        IUnitOfWork _unitOfWork;
        IJadwalKuliahRepository _jadwalKuliahRepository;

        public JadwalKuliahService(IUnitOfWork unitOfWork, IJadwalKuliahRepository JadwalKuliahRepository)
            : base(unitOfWork, JadwalKuliahRepository)
        {
            _unitOfWork = unitOfWork;
            _jadwalKuliahRepository = JadwalKuliahRepository;
        }

        public IEnumerable<VMSemester> GetSemesterAll(int skip, int take, string search)
        {
            return _jadwalKuliahRepository.GetSemesterAll(skip, take, search);
        }
    }
}
