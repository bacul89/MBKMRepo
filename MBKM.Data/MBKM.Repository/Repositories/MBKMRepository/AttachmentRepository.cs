using MBKM.Common.Interfaces.RepoInterfaces.MBKMRepoInterfaces;
using MBKM.Entities.Models.MBKM;
using MBKM.Repository.BaseRepository;
using System.Data.Entity;

namespace MBKM.Repository.Repositories.MBKMRepository
{
    public class AttachmentRepository : GenericRepository<Attachment>, IAttachmentRepository
    {
        public AttachmentRepository(DbContext _db) : base(_db)
        {
        }
    }
}
