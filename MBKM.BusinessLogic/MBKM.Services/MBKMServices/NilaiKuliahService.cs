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
    public interface INilaiKuliahService : IEntityService<NilaiKuliah>
    {
        VMListNilaiKuliah GetNilaiMahasiswa();

        IEnumerable <VMMataKuliah> GetMatkulEn(string kodeMataKuliah, int mataKuilahID, int STRM);
    }
    public class NilaiKuliahService : EntityService<NilaiKuliah>, INilaiKuliahService
    {
        IUnitOfWork _unitOfWork;
        INilaiKuliahRepository _nilaiKuliahRepository;

        public NilaiKuliahService(IUnitOfWork unitOfWork, INilaiKuliahRepository NilaiKuliahRepository)
            : base(unitOfWork, NilaiKuliahRepository)
        {
            _unitOfWork = unitOfWork;
            _nilaiKuliahRepository = NilaiKuliahRepository;
        }


        public IEnumerable<VMMataKuliah> GetMatkulEn(string kodeMataKuliah, int mataKuilahID, int STRM)
        {
            return _nilaiKuliahRepository.GetMatkulEn(kodeMataKuliah,  mataKuilahID, STRM);
        }

        /*        public NilaiKuliah GetNilaiMahasiswa()
                {
                    return _nilaiKuliahRepository.GetNilaiMahasiswa();
                }*/

        public VMListNilaiKuliah GetNilaiMahasiswa()
        {
            return _nilaiKuliahRepository.GetNilaiMahasiswa();
        }
    }
}
