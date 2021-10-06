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
    public interface IPerjanjianKerjasamaService : IEntityService<PerjanjianKerjasama>
    {
        VMListPerjanjianKerjasama getListPKGrid(DataTableAjaxPostModel model);
        List<VMLookupNoKerjasama> getNoKerjasama(int Skip, int Length, string Search, string NamaInstansi);
        int getBiaya(string NamaInstansi);
        List<VMLookupNoKerjasama> getNamaInstansi(int Skip, int Length, string Search);
    }
    public class PerjanjianKerjasamaService : EntityService<PerjanjianKerjasama>, IPerjanjianKerjasamaService
    {
        IUnitOfWork _unitOfWork;
        IPerjanjianKerjasamaRepository _perjanjianKerjasamaRepository;

        public PerjanjianKerjasamaService(IUnitOfWork unitOfWork, IPerjanjianKerjasamaRepository PerjanjianKerjasamaRepository)
            : base(unitOfWork, PerjanjianKerjasamaRepository)
        {
            _unitOfWork = unitOfWork;
            _perjanjianKerjasamaRepository = PerjanjianKerjasamaRepository;
        }

        public int getBiaya(string NoKerjaSama)
        {
            return _perjanjianKerjasamaRepository.getBiaya(NoKerjaSama);
        }

        public VMListPerjanjianKerjasama getListPKGrid(DataTableAjaxPostModel model)
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
            return _perjanjianKerjasamaRepository.getListPerjanjianKerjasama(skip, take, searchBy, sortBy, sortDir);
        }

        public List<VMLookupNoKerjasama> getNamaInstansi(int Skip, int Length, string Search)
        {
            return _perjanjianKerjasamaRepository.getNamaInstansi(Skip, Length, Search);
        }

        public List<VMLookupNoKerjasama> getNoKerjasama(int Skip, int Length, string Search, string NamaInstansi)
        {
            return _perjanjianKerjasamaRepository.getNoKerjasama(Skip, Length, Search, NamaInstansi);
        }
    }
}
