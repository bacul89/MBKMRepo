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
    
    public interface IAttachmentService : IEntityService<Attachment>
    {
    }
    public class AttachmentService : EntityService<Attachment>, IAttachmentService
    {
        IUnitOfWork _unitOfWork;
        IAttachmentRepository _attachmentRepository;

        public AttachmentService(IUnitOfWork unitOfWork, IAttachmentRepository AttachmentRepository)
            : base(unitOfWork, AttachmentRepository)
        {
            _unitOfWork = unitOfWork;
            _attachmentRepository = AttachmentRepository;
        }
    }
}
