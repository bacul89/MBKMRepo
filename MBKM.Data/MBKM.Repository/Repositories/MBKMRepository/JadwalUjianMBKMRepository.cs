using MBKM.Common.Interfaces.RepoInterfaces.MBKMRepoInterfaces;
using MBKM.Entities.Models.MBKM;
using MBKM.Entities.ViewModel;
using MBKM.Repository.BaseRepository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Dynamic;
using System.Text;
using System.Threading.Tasks;

namespace MBKM.Repository.Repositories.MBKMRepository
{
    public class JadwalUjianMBKMRepository : GenericRepository<JadwalUjianMBKM>, IJadwalUjianMBKMRepository
    {
        public JadwalUjianMBKMRepository(DbContext _db) : base(_db)
        {
        }

        public VMListJadwalUjian GetListManageUjian(int Skip, int Length, string SearchParam, string SortBy, bool SortDir, string jenjangStudi, string fakultas, string jenisUjian, string tahunSemester)
        {
            VMListJadwalUjian mListJadwalUjian = new VMListJadwalUjian();
            if (String.IsNullOrEmpty(SearchParam))
            {
                // if we have an empty search then just order the results by Id ascending
                //SortBy = "ID";
                //SortDir = true;
                SearchParam = "";
            }
            using (var context = new MBKMContext())
            {

                var result = context.jadwalUjians.Where(x => x.IsDeleted == false &&
                x.JenjangStudi == jenjangStudi &&
/*                x.FakultasID == "02" &&
*//*                x.TipeUjian == jenisUjian &&
*/                x.STRM == "1910");

                mListJadwalUjian.TotalCount = result.Count();

                var gridfilter = result
                    .AsQueryable()
                    .Where(y => y.KodeMatkul.Contains(SearchParam)
                        || y.NamaMatkul.Contains(SearchParam)
                        || y.ClassSection.Contains(SearchParam)
                        || y.JamMulai.Contains(SearchParam)
                        || y.JamAkhir.Contains(SearchParam)
                    )
                    .OrderBy(SortBy, SortDir);

                mListJadwalUjian.gridDatas = gridfilter.Skip(Skip).Take(Length)
                    .Select(z => new GridDataJadwalUjian
                    {

                        KodeMatkul = z.KodeMatkul,
                        NamaMatkul = z.NamaMatkul,
                        FakultasID = z.FakultasID,
                        NamaFakultas = z.NamaFakultas,
                        ClassSection = z.ClassSection,
                        TanggalUjian = z.TanggalUjian,
                        JamMulai = z.JamMulai,
                        JamAkhir = z.JamAkhir,

                    }).ToList();
                mListJadwalUjian.TotalFilterCount = gridfilter.Count();
                return mListJadwalUjian;
            }
        }


    }
}
