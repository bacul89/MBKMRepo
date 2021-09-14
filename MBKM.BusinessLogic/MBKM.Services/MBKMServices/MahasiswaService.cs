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
    public interface IMahasiswaService : IEntityService<Mahasiswa>
    {
        VMLogin getLoginInternal(string StudentID, string Password);
        List<Mahasiswa> getMahasiswasNotYetVer(string Universitas, string Prodi);
        //int updateRangeVer(Int64[] listId);
    }
    public class MahasiswaService : EntityService<Mahasiswa>, IMahasiswaService
    {
        IUnitOfWork _unitOfWork;
        IMahasiswaRepository _mahasiswaRepository;

        public MahasiswaService(IUnitOfWork unitOfWork, IMahasiswaRepository MahasiswaRepository)
            : base(unitOfWork, MahasiswaRepository)
        {
            _unitOfWork = unitOfWork;
            _mahasiswaRepository = MahasiswaRepository;
        }

        public VMLogin getLoginInternal(string StudentID, string Password)
        {
            VMLogin model = _mahasiswaRepository.getLoginInternal(StudentID, Password);
            return model;
        }

        public List<Mahasiswa> getMahasiswasNotYetVer(string Universitas, string Prodi)
        {
            return _mahasiswaRepository.getMahasiswasNotYetVer(Universitas, Prodi);
        }

        //public int updateRangeVer(long[] listId)
        //{
        //    return _mahasiswaRepository.updateRangeVer(listId);
        //}
    }
}
