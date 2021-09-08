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
    public interface IJadwalKuliahMahasiswaService : IEntityService<JadwalKuliahMahasiswa>
    {
    }
    public class JadwalKuliahMahasiswaService : EntityService<JadwalKuliahMahasiswa>, IJadwalKuliahMahasiswaService
    {
        IUnitOfWork _unitOfWork;
        IJadwalKuliahMahasiswaRepository _jadwalkmRepository;

        public JadwalKuliahMahasiswaService(IUnitOfWork unitOfWork, IJadwalKuliahMahasiswaRepository JadwalKuliahMahasiswaRepository)
            : base(unitOfWork, JadwalKuliahMahasiswaRepository)
        {
            _unitOfWork = unitOfWork;
            _jadwalkmRepository = JadwalKuliahMahasiswaRepository;
        }
    }
}
