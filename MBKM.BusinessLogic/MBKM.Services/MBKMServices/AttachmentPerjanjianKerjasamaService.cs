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

    public interface IAttachmentPerjanjianKerjasamaService : IEntityService<AttachmentPerjanjianKerjasama>
    {
    }
    public class AttachmentPerjanjianKerjasamaService : EntityService<AttachmentPerjanjianKerjasama>, IAttachmentPerjanjianKerjasamaService
    {
        IUnitOfWork _unitOfWork;
        IAttachmentPerjanjianKerjasamaRepository _attachmentPKRepository;

        public AttachmentPerjanjianKerjasamaService(IUnitOfWork unitOfWork, IAttachmentPerjanjianKerjasamaRepository AttachmentPKRepository)
            : base(unitOfWork, AttachmentPKRepository)
        {
            _unitOfWork = unitOfWork;
            _attachmentPKRepository = AttachmentPKRepository;
        }
    }
}
