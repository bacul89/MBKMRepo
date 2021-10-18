using MBKM.Common.Helpers;
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
    public interface IJadwalKuliahMahasiswaService : IEntityService<JadwalKuliahMahasiswa>
    {
        VMListJadwalKuliah ListJadwalKuliahMahasiswa(DataTableAjaxPostModel model, string idProdi, string lokasi, string idFakultas, string jenjangStudi, string strm);
        VMSemester getOngoingSemester(string jenjangStudi);
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

        public VMSemester getOngoingSemester(string jenjangStudi)
        {
            return _jadwalkmRepository.getOngoingSemester(jenjangStudi);
        }

        public VMListJadwalKuliah ListJadwalKuliahMahasiswa(DataTableAjaxPostModel model, string idProdi, string lokasi, string idFakultas, string jenjangStudi, string strm)
        {
            throw new NotImplementedException();
        }
    }
}
