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
    public interface IJadwalKuliahService : IEntityService<JadwalKuliah>
    {
        IEnumerable<VMSemester> GetSemesterAll(int skip, int take, string search);
        VMListJadwalKuliah SearchListJadwalKuliah(DataTableAjaxPostModel model, string idProdi, string lokasi, string idFakultas, string jenjangStudi, string strm);
        VMListJadwalKuliah SearchListMataKuliah(DataTableAjaxPostModel model, string idProdi, string lokasi, string idFakultas, string jenjangStudi, string strm);
        IEnumerable<VMSemester> GetSemesterAll2();
        IEnumerable<JadwalKuliah> GetMatkulFlag(int skip, int take, string searchBy, string idProdi, string lokasi, string idFakultas, string jenjangStudi, string strm);
    }
    public class JadwalKuliahService : EntityService<JadwalKuliah>, IJadwalKuliahService
    {
        IUnitOfWork _unitOfWork;
        IJadwalKuliahRepository _jadwalKuliahRepository;

        public JadwalKuliahService(IUnitOfWork unitOfWork, IJadwalKuliahRepository JadwalKuliahRepository)
            : base(unitOfWork, JadwalKuliahRepository)
        {
            _unitOfWork = unitOfWork;
            _jadwalKuliahRepository = JadwalKuliahRepository;
        }

        public IEnumerable<JadwalKuliah> GetMatkulFlag(int skip, int take, string searchBy, string idProdi, string lokasi, string idFakultas, string jenjangStudi, string strm)
        {
            return _jadwalKuliahRepository.GetMatkulFlag( skip,  take,  searchBy,  idProdi,  lokasi,  idFakultas,  jenjangStudi,  strm);
        }

        public IEnumerable<VMSemester> GetSemesterAll(int skip, int take, string search)
        {
            return _jadwalKuliahRepository.GetSemesterAll(skip, take, search);
        }
        public IEnumerable<VMSemester> GetSemesterAll2()
        {
            return _jadwalKuliahRepository.GetSemesterAll2();
        }

        public VMListJadwalKuliah SearchListJadwalKuliah(DataTableAjaxPostModel model, string idProdi, string lokasi, string idFakultas, string jenjangStudi, string strm)
        {

            var searchBy = (model.search != null) ? model.search.value : null;
            var take = model.length;
            var skip = model.start;
            string sortBy = "";
            bool sortDir = true;

            if (model.order != null)
            {
                // in this example we just default sort on the 1st column
                sortBy = model.columns[model.order[0].column].data;
                sortDir = model.order[0].dir.ToLower() == "asc";
            }
            if (sortBy == null)
                sortBy = "ID";
            sortBy = sortBy + " " + model.order[0].dir.ToUpper();

            return _jadwalKuliahRepository.SearchListJadwalKuliah(skip, take, searchBy, sortBy, sortDir, idProdi, lokasi, idFakultas, jenjangStudi, strm);
        }

        public VMListJadwalKuliah SearchListMataKuliah(DataTableAjaxPostModel model, string idProdi, string lokasi, string idFakultas, string jenjangStudi, string strm)
        {
            var searchBy = (model.search != null) ? model.search.value : null;
            var take = model.length;
            var skip = model.start;
            string sortBy = "";
            bool sortDir = true;

            if (model.order != null)
            {
                // in this example we just default sort on the 1st column
                sortBy = model.columns[model.order[0].column].data;
                sortDir = model.order[0].dir.ToLower() == "asc";
            }
            if (sortBy == null)
                sortBy = "ID";
            sortBy = sortBy + " " + model.order[0].dir.ToUpper();

            return _jadwalKuliahRepository.SearchListMataKuliah(skip, take, searchBy, sortBy, sortDir, idProdi, lokasi, idFakultas, jenjangStudi, strm);
        }
    }
}
