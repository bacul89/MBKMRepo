using MBKM.Common.Interfaces.RepoInterfaces.MBKMRepoInterfaces;
using MBKM.Entities.Models.MBKM;
using MBKM.Entities.ViewModel;
using MBKM.Repository.BaseRepository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MBKM.Repository.Repositories.MBKMRepository
{
    public class FeedbackMatkulRepository : GenericRepository<FeedbackMatkul>, IFeedbackMatkulRepository
    {
        public FeedbackMatkulRepository(DbContext _db) : base(_db)
        {
        }
        public IEnumerable<VMDosenMakulPertemuan> GetDosenMakulPertemuans(string KodeMK, string ClassSection, string strm, string fakulId)
        {
            using (var context = new MBKMContext())
            {
                var kodeMK = new SqlParameter("@Kodemk", KodeMK);
                var classSection = new SqlParameter("@ClassSection", ClassSection);
                var strM = new SqlParameter("@STRM", strm);
                var fakultasID = new SqlParameter("@FakultasID", fakulId);
                var result = context.Database
                    .SqlQuery<VMDosenMakulPertemuan>("GetDosenMatkulPertemuan @Kodemk, @ClassSection, @STRM, @FakultasID", kodeMK, classSection, strM, fakultasID).ToList();
                return result;
            }
        }
        public IEnumerable<VMPertanyaanFeedback> GetPertanyaanFeedbacks(string jenjangStudi, string strm)
        {
            using (var context = new MBKMContext())
            {
                var JenjangStudi = new SqlParameter("@JenjangStudi", jenjangStudi);
                var strM = new SqlParameter("@STRM", strm);
                var result = context.Database
                    .SqlQuery<VMPertanyaanFeedback>("GetPertanyaanByStrmJenjang @JenjangStudi, @STRM", JenjangStudi, strM).ToList();
                return result;
            }
        }
        public IEnumerable<VMJawabanFeedback> GetJawabanFeedback(string KodeJawaban)
        {
            using (var context = new MBKMContext())
            {
                var KodeJawab = new SqlParameter("@KodeJawaban", KodeJawaban);
                var result = context.Database
                    .SqlQuery<VMJawabanFeedback>("GetJawaban @KodeJawaban", KodeJawab).ToList();
                return result;
            }
        }
    }


}
