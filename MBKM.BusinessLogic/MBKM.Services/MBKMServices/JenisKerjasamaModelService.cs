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
    public interface IJenisKerjasamaModelService : IEntityService<JenisKerjasamaModel>
    {
        List<VMJenisPertukaran> getPertukaran();
        List<VMJenisKerjasama> getKerjasama();
    }
    public class JenisKerjasamaModelService : EntityService<JenisKerjasamaModel>, IJenisKerjasamaModelService
    {
        IUnitOfWork _unitOfWork;
        IJenisKerjasamaModelRepository _jenisRepository;

        public JenisKerjasamaModelService(IUnitOfWork unitOfWork, IJenisKerjasamaModelRepository JenisRepository)
            : base(unitOfWork, JenisRepository)
        {
            _unitOfWork = unitOfWork;
            _jenisRepository = JenisRepository;
        }

        public List<VMJenisKerjasama> getKerjasama()
        {
            return _jenisRepository.getKerjasama();
        }

        public List<VMJenisPertukaran> getPertukaran()
        {
            return _jenisRepository.getPertukaran();
        }
    }
}