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

    public interface ICPLMatakuliahService : IEntityService<CPLMatakuliah>
    {

        VMListMapingCPL GetListMapingCPL(DataTableAjaxPostModel model);
    }
    public class CPLMatakuliahService : EntityService<CPLMatakuliah>, ICPLMatakuliahService
    {
        IUnitOfWork _unitOfWork;
        ICPLMataKuliahRepository _cplRepository;

        public CPLMatakuliahService(IUnitOfWork unitOfWork, ICPLMataKuliahRepository CPLRepository)
            : base(unitOfWork, CPLRepository)
        {
            _unitOfWork = unitOfWork;
            _cplRepository = CPLRepository;
        }




       public VMListMapingCPL GetListMapingCPL(DataTableAjaxPostModel model) 
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
            return _cplRepository.GetListMapingCPL(skip, take, searchBy, sortBy, sortDir);
        }
    }





}
