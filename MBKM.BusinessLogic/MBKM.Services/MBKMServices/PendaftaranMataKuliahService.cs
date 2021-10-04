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
    public interface IPendaftaranMataKuliahService : IEntityService<PendaftaranMataKuliah>
    {
        IEnumerable<VMFakultas> GetFakultas(string jenjangStudi, string search);
        IEnumerable<VMProdi> GetProdiByFakultas(string jenjangStudi, string idFakultas, string search);
        IEnumerable<VMProdi> GetLokasiByProdi(string jenjangStudi, string idProdi, string search);
        VMSemester getOngoingSemester(string jenjangStudi);
    }
    public class PendaftaranMataKuliahService : EntityService<PendaftaranMataKuliah>, IPendaftaranMataKuliahService
    {
        IUnitOfWork _unitOfWork;
        IPendaftaranMataKuliahRepository _pmkRepository;

        public PendaftaranMataKuliahService(IUnitOfWork unitOfWork, IPendaftaranMataKuliahRepository PMKRepository)
            : base(unitOfWork, PMKRepository)
        {
            _unitOfWork = unitOfWork;
            _pmkRepository = PMKRepository;
        }

        public IEnumerable<VMFakultas> GetFakultas(string jenjangStudi, string search)
        {
            return _pmkRepository.GetFakultas(jenjangStudi, search);
        }
        public IEnumerable<VMProdi> GetProdiByFakultas(string jenjangStudi, string idFakultas, string search)
        {
            return _pmkRepository.GetProdiByFakultas(jenjangStudi, idFakultas, search);
        }
        public IEnumerable<VMProdi> GetLokasiByProdi(string jenjangStudi, string idProdi, string search)
        {
            return _pmkRepository.GetLokasiByProdi(jenjangStudi, idProdi, search);
        }
        public VMSemester getOngoingSemester(string jenjangStudi)
        {
            return _pmkRepository.getOngoingSemester(jenjangStudi);
        }
    }
}
