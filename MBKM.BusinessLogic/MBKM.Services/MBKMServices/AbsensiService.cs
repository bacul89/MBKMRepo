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
    public interface IAbsensiService : IEntityService<Absensi>
    {
        string GetSemesterBySTRM(int strm);
        IEnumerable<VMLookup> GetFakultasByJenjangStudi(string search, string jenjangStudi);
        IEnumerable<VMLookup> GetLokasiByFakultas(string search, string jenjangStudi, string fakultas);
        IEnumerable<VMLookup> GetProdiByLokasi(string search, string jenjangStudi, string lokasi);
        IEnumerable<VMLookup> GetMatkulByProdi(string search, string jenjangStudi, string prodi);
        IEnumerable<VMLookup> GetSeksiByMatkul(string search, string jenjangStudi, string matkul);
        IEnumerable<VMPresensi> GetPresensi(int strm, string jenjangStudi, string fakultas, string lokasi, string prodi, string matkul, string seksi);
    }
    public class AbsensiService : EntityService<Absensi>, IAbsensiService
    {
        IUnitOfWork _unitOfWork;
        IAbsensiRepository _absensiRepository;

        public AbsensiService(IUnitOfWork unitOfWork, IAbsensiRepository AbsensiRepository)
            : base(unitOfWork, AbsensiRepository)
        {
            _unitOfWork = unitOfWork;
            _absensiRepository = AbsensiRepository;
        }
        public string GetSemesterBySTRM(int strm)
        {
            return _absensiRepository.GetSemesterBySTRM(strm);
        }
        public IEnumerable<VMLookup> GetFakultasByJenjangStudi(string search, string jenjangStudi)
        {
            return _absensiRepository.GetFakultasByJenjangStudi(search, jenjangStudi);
        }
        public IEnumerable<VMLookup> GetLokasiByFakultas(string search, string jenjangStudi, string fakultas)
        {
            return _absensiRepository.GetLokasiByFakultas(search, jenjangStudi, fakultas);
        }
        public IEnumerable<VMLookup> GetProdiByLokasi(string search, string jenjangStudi, string lokasi)
        {
            return _absensiRepository.GetProdiByLokasi(search, jenjangStudi, lokasi);
        }
        public IEnumerable<VMLookup> GetMatkulByProdi(string search, string jenjangStudi, string prodi)
        {
            return _absensiRepository.GetMatkulByProdi(search, jenjangStudi, prodi);
        }
        public IEnumerable<VMLookup> GetSeksiByMatkul(string search, string jenjangStudi, string matkul)
        {
            return _absensiRepository.GetSeksiByMatkul(search, jenjangStudi, matkul);
        }
        public IEnumerable<VMPresensi> GetPresensi(int strm, string jenjangStudi, string fakultas, string lokasi, string prodi, string matkul, string seksi)
        {
            return _absensiRepository.GetPresensi(strm, jenjangStudi, fakultas, lokasi, prodi, matkul, seksi);
        }
    }
}
