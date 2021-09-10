
using MBKM.Common.Interfaces.RepoInterfaces.MBKMRepoInterfaces;
using MBKM.Entities.Models.MBKM;
using MBKM.Entities.ViewModel;
using MBKM.Repository.BaseRepository;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;

namespace MBKM.Repository.Repositories.MBKMRepository
{
    public class MahasiswaRepository : GenericRepository<Mahasiswa>, IMahasiswaRepository
    {
        public MahasiswaRepository(DbContext _db) : base(_db)
        {
        }
        public VMLogin getLoginInternal(string StudentID, string Password)
        {
            using (var context = new MBKMContext())
            {
                var studentIdParameter = new SqlParameter("@StudentID", StudentID);
                var PasswordParameter = new SqlParameter("@Password", Password);

                var result = context.Database
                    .SqlQuery<VMLogin>("GetLoginInternal @Password, @StudentID", PasswordParameter, studentIdParameter);
                return result.FirstOrDefault();
            }
        }
    }
}
