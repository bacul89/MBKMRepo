using MBKM.Common.Interfaces;
using MBKM.Common.Interfaces.RepoInterfaces.MBKMRepoInterfaces;
using MBKM.Entities.Models.MBKM;
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
    }
}
