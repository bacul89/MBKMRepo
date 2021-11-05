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
        VMListMapingCPL SearchListMapingCPL(DataTableAjaxPostModel model, string idProdi,  string idFakultas, string jenjangStudi, string idMatakuliah); //string lokasi,

        IEnumerable<VMMataKuliah> GetMatkul(int skip, int take, string searchBy, string idProdi, string idFakultas);
        IEnumerable<VMListProdi> GetProdiLocByFakultas(string jenjangStudi, string idFakultas, string search);

        //VMMataKuliah GetMatkul(DataTableAjaxPostModel model, string idProdi, string idFakultas);
        //VMMataKuliah GetMatkul(DataTableAjaxPostModel model, string search, string idProdi, string idFakultas);
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

        public IEnumerable<VMMataKuliah> GetMatkul(int skip, int take, string searchBy, string idProdi, string idFakultas)
        {
            return _cplRepository.GetMatkul(skip, take, searchBy, idProdi, idFakultas);
        }

        public IEnumerable<VMListProdi> GetProdiLocByFakultas(string jenjangStudi, string idFakultas, string search)
        {
            return _cplRepository.GetProdiLocByFakultas(jenjangStudi, idFakultas, search);
        }

        /*        public VMMataKuliah GetMatkul(DataTableAjaxPostModel model, string search, string idProdi, string idFakultas)
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
                    return _cplRepository.GetMatkul(skip, take, searchBy, idProdi, idFakultas);
                }
        */

        public VMListMapingCPL SearchListMapingCPL(DataTableAjaxPostModel model, string idProdi,  string idFakultas, string jenjangStudi, string idMatakuliah) //string lokasi,
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
            return _cplRepository.SearchListMapingCPL(skip, take, searchBy, sortBy, sortDir, idProdi,  idFakultas, jenjangStudi, idMatakuliah); //lokasi,
        }
    }





}
