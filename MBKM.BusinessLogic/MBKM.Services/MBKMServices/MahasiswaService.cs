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
    public interface IMahasiswaService : IEntityService<Mahasiswa>
    {
        VMLogin getLoginInternal(string StudentID, string Password);
        List<Mahasiswa> getMahasiswasNotYetVer(string Universitas, string Prodi);
        VMListMahasiswa getMahasiswasNotYetVer(DataTableAjaxPostModel model);
        string GetNim(int strm);
        void UpdateNim(int Nilai, int strm);
        void GenerateAbsence(long jadwalKuliahId, long mahasiswaId, string kodeMk, string classSection, string strm, string fakultasId);
        IEnumerable<VMSemester> GetDataSemester(string jenjangStudi);
        IEnumerable<VMAllProdi> GetAllDataProdi();
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

        public void GenerateAbsence(long jadwalKuliahId, long mahasiswaId, string kodeMk, string classSection, string strm, string fakultasId)
        {
             _mahasiswaRepository.GenerateAbsence(jadwalKuliahId, mahasiswaId, kodeMk, classSection, strm, fakultasId);
        }

        public IEnumerable<VMAllProdi> GetAllDataProdi()
        {
            return _mahasiswaRepository.GetAllDataProdi();
        }

        public IEnumerable<VMSemester> GetDataSemester(string jenjangStudi)
        {
            return _mahasiswaRepository.GetDataSemester(jenjangStudi);
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

        public VMListMahasiswa getMahasiswasNotYetVer(DataTableAjaxPostModel model)
        {
            var searchBy = (model.search != null) ? model.search.value : null;
            var take = model.length;
            var skip = model.start;
            string sortBy = "";
            bool sortDir = true;

            if (model.order != null)
            {
                // in this example we just default sort on the 1st column
                sortBy = model.columns[model.order[0].column].data ;
                sortDir = model.order[0].dir.ToLower() == "asc";
            }
            if (sortBy == null)
                sortBy = "ID";
            sortBy = sortBy + " " + model.order[0].dir.ToUpper();
            return _mahasiswaRepository.getMahasiswasNotYetVer(skip, take, searchBy, sortBy, sortDir);
        }

        public string GetNim(int strm)
        {
            return _mahasiswaRepository.GetNim(strm);
        }

        public void UpdateNim(int Nilai, int strm)
        {
             _mahasiswaRepository.UpdateNim(Nilai, strm);
        }

        //public int updateRangeVer(long[] listId)
        //{
        //    return _mahasiswaRepository.updateRangeVer(listId);
        //}
    }
}
