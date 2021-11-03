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
        IEnumerable<VMSemester> GetTahunSemester();
        IEnumerable<VMLookup> GetFakultasByJenjangStudi(string search, string jenjangStudi);
        IEnumerable<VMLookup> GetProdiByFakultas(string search, string jenjangStudi, string fakultas);
        IEnumerable<VMLookup> GetLokasiByProdi(string search, string jenjangStudi, string prodi);
        IEnumerable<VMLookup> GetMatkulByLokasi(string search, string jenjangStudi, string prodi, string lokasi);
        IEnumerable<VMLookup> GetSeksiByMatkul(string search, string jenjangStudi, string matkul, string lokasi);
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
        public IEnumerable<VMSemester> GetTahunSemester()
        {
            return _absensiRepository.GetTahunSemester();
        }public IEnumerable<VMLookup> GetFakultasByJenjangStudi(string search, string jenjangStudi)
        {
            return _absensiRepository.GetFakultasByJenjangStudi(search, jenjangStudi);
        }
        public IEnumerable<VMLookup> GetProdiByFakultas(string search, string jenjangStudi, string fakultas)
        {
            return _absensiRepository.GetProdiByFakultas(search, jenjangStudi, fakultas);
        }
        public IEnumerable<VMLookup> GetLokasiByProdi(string search, string jenjangStudi, string prodi)
        {
            return _absensiRepository.GetLokasiByProdi(search, jenjangStudi, prodi);
        }
        public IEnumerable<VMLookup> GetMatkulByLokasi(string search, string jenjangStudi, string prodi, string lokasi)
        {
            return _absensiRepository.GetMatkulByLokasi(search, jenjangStudi, prodi, lokasi);
        }
        public IEnumerable<VMLookup> GetSeksiByMatkul(string search, string jenjangStudi, string matkul, string lokasi)
        {
            return _absensiRepository.GetSeksiByMatkul(search, jenjangStudi, matkul, lokasi);
        }
        public IEnumerable<VMPresensi> GetPresensi(int strm, string jenjangStudi, string fakultas, string lokasi, string prodi, string matkul, string seksi)
        {
            return _absensiRepository.GetPresensi(strm, jenjangStudi, fakultas, lokasi, prodi, matkul, seksi);
        }
    }
}
