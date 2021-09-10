using MBKM.Common.Interfaces;
using MBKM.Common.Interfaces.RepoInterfaces;
using MBKM.Entities.Models;
using MBKM.Services.BaseServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MBKM.Services
{
    public interface IEmailTemplateService : IEntityService<EmailTemplate>
    {
    }

    public class EmailTemplateService : EntityService<EmailTemplate>, IEmailTemplateService
    {
        IUnitOfWork _unitOfWork;
        IEmailTemplateRepository _emailRepository;

        public EmailTemplateService(IUnitOfWork unitOfWork, IEmailTemplateRepository EmailRepository)
            : base(unitOfWork, EmailRepository)
        {
            _unitOfWork = unitOfWork;
            _emailRepository = EmailRepository;
        }
    }
}
