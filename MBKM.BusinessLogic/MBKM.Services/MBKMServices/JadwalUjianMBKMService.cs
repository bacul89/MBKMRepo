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
    public interface IJadwalUjianMBKMService : IEntityService<JadwalUjianMBKM>
    {
        VMListJadwalUjian GetListJadwalUjian(DataTableAjaxPostModel model, string jenjangStudi, string fakultas, string jenisUjian, string tahunSemester);
        VMListJadwalUjian GetListManageUjian(DataTableAjaxPostModel model, string jenjangStudi, string fakultas, string jenisUjian, string tahunSemester);
        IEnumerable<VMSemester> getAllSemester();
        VMInformasiStudi GetInformasiData(string prodi);
    }
    public class JadwalUjianMBKMService : EntityService<JadwalUjianMBKM>, IJadwalUjianMBKMService
    {
        IUnitOfWork _unitOfWork;
        IJadwalUjianMBKMRepository _jadwalRepository;

        public JadwalUjianMBKMService(IUnitOfWork unitOfWork, IJadwalUjianMBKMRepository JadwalRepository)
            : base(unitOfWork, JadwalRepository)
        {
            _unitOfWork = unitOfWork;
            _jadwalRepository = JadwalRepository;
        }

        public IEnumerable<VMSemester> getAllSemester()
        {
            return _jadwalRepository.getAllSemester();
        }

        public VMInformasiStudi GetInformasiData(string prodi)
        {
            return _jadwalRepository.GetInformasiData(prodi);
        }

        public VMListJadwalUjian GetListJadwalUjian(DataTableAjaxPostModel model, string jenjangStudi, string fakultas, string jenisUjian, string tahunSemester)
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
            return _jadwalRepository.GetListJadwalUjian(skip, take, searchBy, sortBy, sortDir, jenjangStudi, fakultas, jenisUjian, tahunSemester);
        }

        public VMListJadwalUjian GetListManageUjian(DataTableAjaxPostModel model, string jenjangStudi, string fakultas, string jenisUjian, string tahunSemester)
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
            return _jadwalRepository.GetListManageUjian(skip, take, searchBy, sortBy, sortDir, jenjangStudi, fakultas, jenisUjian, tahunSemester);
        }
    }
}
