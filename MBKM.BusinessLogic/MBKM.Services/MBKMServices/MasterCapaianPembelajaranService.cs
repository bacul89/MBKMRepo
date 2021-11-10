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
    public interface IMasterCapaianPembelajaranService : IEntityService<MasterCapaianPembelajaran>
    {
        VMListMasterCPL GetListMasterCPL(DataTableAjaxPostModel model);
        IEnumerable<VMFakultas> GetFakultas(string jenjangStudi, string search);
        IEnumerable<VMProdi> GetProdiByFakultas(string jenjangStudi, string idFakultas, string search);
        IEnumerable<VMProdi> GetLokasiByProdi(string jenjangStudi, string namaProdi, string search);
        IEnumerable<VMMataKuliah> GetMatkul(int PageNumber, int PageSize, string search);

    }
    public class MasterCapaianPembelajaranService : EntityService<MasterCapaianPembelajaran>, IMasterCapaianPembelajaranService
    {
        IUnitOfWork _unitOfWork;
        IMasterCapaianPembelajaranRepository _mcpRepository;

        public MasterCapaianPembelajaranService(IUnitOfWork unitOfWork, IMasterCapaianPembelajaranRepository MCPRepository)
            : base(unitOfWork, MCPRepository)
        {
            _unitOfWork = unitOfWork;
            _mcpRepository = MCPRepository;
        }


        public IEnumerable<VMFakultas> GetFakultas(string jenjangStudi, string search)
        {
            return _mcpRepository.GetFakultas(jenjangStudi, search);
        }

        public VMListMasterCPL GetListMasterCPL(DataTableAjaxPostModel model)
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
            return _mcpRepository.GetListMasterCPL(skip, take, searchBy, sortBy, sortDir);
        }

        public IEnumerable<VMProdi> GetLokasiByProdi(string jenjangStudi, string namaProdi, string search)
        {
            return _mcpRepository.GetLokasiByProdiName(jenjangStudi, namaProdi, search);
        }

        public IEnumerable<VMProdi> GetProdiByFakultas(string jenjangStudi, string idFakultas, string search)
        {
            return _mcpRepository.GetProdiByFakultas(jenjangStudi, idFakultas, search);
        }
        public IEnumerable<VMMataKuliah> GetMatkul(int PageNumber, int PageSize, string search)
        {
            return _mcpRepository.GetMatkul(PageNumber, PageSize, search);
        }
    }
}
