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
        VMBobot GetBobot(string idMatkul);
        IEnumerable<VMSubBobot> GetSubBobot(string idMatkul);
        VMListNilaiKuliah GetNilaiMahasiswa();
        VMDNR GetDNR(int idJadwalKuliah);
        IEnumerable <VMMataKuliah> GetMatkulEn(string kodeMataKuliah, int mataKuilahID, int STRM);
        VMNilaiBobot GetBobotNilai(decimal Nilai);
        VMNilaiDiakui GetNilaiDiakui(string Jenjang, string Strm, string MatkulId, string KodeMatkul, string Nim, string classSection);
        VMNilaiGrade GetNilaiGradeByNilaiTotal(int nilaiTotal);
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
        public VMBobot GetBobot(string idMatkul)
        {
            return _nilaiKuliahRepository.GetBobot(idMatkul);
        }
        public IEnumerable<VMSubBobot> GetSubBobot(string idMatkul)
        {
            return _nilaiKuliahRepository.GetSubBobot(idMatkul);
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
        public VMDNR GetDNR(int idJadwalKuliah)
        {
            return _nilaiKuliahRepository.GetDNR(idJadwalKuliah);
        }

        public VMNilaiBobot GetBobotNilai(decimal Nilai)
        {
            return _nilaiKuliahRepository.GetBobotNilai(Nilai);
        }

        public VMNilaiDiakui GetNilaiDiakui(string Jenjang, string Strm, string MatkulId, string KodeMatkul, string Nim, string classSection)
        {
            return _nilaiKuliahRepository.GetNilaiDiakui(Jenjang, Strm, MatkulId, KodeMatkul, Nim, classSection);
        }

        public VMNilaiGrade GetNilaiGradeByNilaiTotal(int nilaiTotal)
        {
            return _nilaiKuliahRepository.GetNilaiGradeByNilaiTotal(nilaiTotal);
        }
    }
}
