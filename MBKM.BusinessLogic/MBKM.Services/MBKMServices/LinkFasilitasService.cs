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
    public interface ILinkFasilitasService : IEntityService<JadwalKuliah>
    {
        List<VMClassSection> getSection();
        IEnumerable<VMMataKuliah> GetMatkul(int skip, int take, string searchBy, string idProdi, string idFakultas);
        VMLinkFasilitas SearchListJadwalKuliah(DataTableAjaxPostModel model, string idProdi, string lokasi, string idFakultas, string jenjangStudi, string idMatakuliah,string seksi);

    }
    public class LinkFasilitasService : EntityService<JadwalKuliah>, ILinkFasilitasService
    {
        IUnitOfWork _unitOfWork;
        ILinkFasilitasRepository _linkFasilitasRepository;
        public LinkFasilitasService(IUnitOfWork unitOfWork, ILinkFasilitasRepository LinkFasilitasRepository)
            : base(unitOfWork, LinkFasilitasRepository)
        {
            _unitOfWork = unitOfWork;
            _linkFasilitasRepository = LinkFasilitasRepository;
        }

        public IEnumerable<VMMataKuliah> GetMatkul(int skip, int take, string searchBy, string idProdi, string idFakultas)
        {
            return _linkFasilitasRepository.GetMatkul(skip, take, searchBy, idProdi, idFakultas);
        }

        public List<VMClassSection> getSection()
        {
            return _linkFasilitasRepository.GetListSeksi();
        }

        public VMLinkFasilitas SearchListJadwalKuliah(DataTableAjaxPostModel model, string idProdi, string lokasi, string idFakultas, string jenjangStudi, string idMatakuliah, string seksi)
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
            return _linkFasilitasRepository.SearchListJadwalKuliah(skip, take, searchBy, sortBy, sortDir, idProdi, lokasi, idFakultas, jenjangStudi, idMatakuliah,seksi);
        }
    }
}
