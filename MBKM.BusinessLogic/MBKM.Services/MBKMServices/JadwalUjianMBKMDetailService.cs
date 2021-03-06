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
    public interface IJadwalUjianMBKMDetailService : IEntityService<JadwalUjianMBKMDetail>
    {
        List<VMClassSection> getSection();
        VMListJadwalUjian GetDHU(DataTableAjaxPostModel model, string idProdi, string lokasi, string idFakultas, string jenjangStudi, string strm, string idMatakuliah, string seksi);
        VMDosenMakulPertemuan GetDosen(string seksi, string kodeMataKuliah, string strm, string fakultasId);
        List<VMJadwalUjian> GetAttrubuteDHU(string idProdi, string lokasi, string idFakultas, string jenjangStudi, string strm, string idMatakuliah, string seksi);
        List<VMJadwalUjian> GetDHUByID(int iD);
    }
    public class JadwalUjianMBKMDetailService : EntityService<JadwalUjianMBKMDetail>, IJadwalUjianMBKMDetailService
    {
        IUnitOfWork _unitOfWork;
        IJadwalUjianMBKMDetailRepository _jadwalRepository;

        public JadwalUjianMBKMDetailService(IUnitOfWork unitOfWork, IJadwalUjianMBKMDetailRepository JadwalRepository)
            : base(unitOfWork, JadwalRepository)
        {
            _unitOfWork = unitOfWork;
            _jadwalRepository = JadwalRepository;
        }

        public VMDosenMakulPertemuan GetDosen(string seksi, string kodeMataKuliah, string strm, string fakultasId)
        {
            return _jadwalRepository.GetDosen(seksi, kodeMataKuliah, strm, fakultasId);
        }

        public List<VMClassSection> getSection()
        {
            return _jadwalRepository.GetListSeksi();
        }

        public VMListJadwalUjian GetDHU(DataTableAjaxPostModel model, string idProdi, string lokasi, string idFakultas, string jenjangStudi, string strm, string idMatakuliah, string seksi)
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

            return _jadwalRepository.GetDHU(skip, take, searchBy, sortBy, sortDir, idProdi, lokasi, idFakultas, jenjangStudi, strm, idMatakuliah, seksi);
        }

        public List<VMJadwalUjian> GetAttrubuteDHU(string idProdi, string lokasi, string idFakultas, string jenjangStudi, string strm, string idMatakuliah, string seksi)
        {
            return _jadwalRepository.GetDHUTest(idProdi, lokasi, idFakultas, jenjangStudi, strm, idMatakuliah, seksi);
        }

        public List<VMJadwalUjian> GetDHUByID(int iD)
        {
            return _jadwalRepository.GetDHUbyID(iD);
        }
    }
}
