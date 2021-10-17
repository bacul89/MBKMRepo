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
                x.FakultasID == fakultas &&
                x.KodeTipeUjian == jenisUjian &&
                x.STRM == tahunSemester);
                

                mListJadwalUjian.TotalCount = result.Count();

                var gridfilter = result
                    .AsQueryable()
                    .Where(y => y.KodeMatkul.Contains(SearchParam)
                        || y.NamaMatkul.Contains(SearchParam)
                        || y.ClassSection.Contains(SearchParam)
                        || y.JamMulai.Contains(SearchParam)
                        || y.JamAkhir.Contains(SearchParam)
                    )
                    .GroupBy(x => new { x.KodeMatkul, x.NamaMatkul, x.ClassSection, x.TanggalUjian, x.JamMulai }, (key, group) => 
                    new {
                        KodeMatkul = key.KodeMatkul,
                        NamaMatkul = key.NamaMatkul,
                        ClassSection = key.ClassSection,
                        TanggalUjian = key.TanggalUjian,
                        JamMulai = key.JamMulai,
                        Result = group.ToList() })
                    .OrderBy(SortBy, SortDir);

                mListJadwalUjian.gridDatas = gridfilter.Skip(Skip).Take(Length)
                    .Select(z => new GridDataJadwalUjian
                    {
                        /* KodeMatkul = z.Key1,
                         NamaMatkul = z.FirstOrDefault().NamaMatkul,
                         FakultasID = z.FirstOrDefault().FakultasID,
                         NamaFakultas = z.FirstOrDefault().NamaFakultas,
                         ClassSection = z.FirstOrDefault().ClassSection,
                         TanggalUjian = z.FirstOrDefault().TanggalUjian,
                         JamMulai = z.FirstOrDefault().JamMulai,
                         JamAkhir = z.FirstOrDefault().JamAkhir,
                         TipeUjian = z.FirstOrDefault().TipeUjian,*/

                        KodeMatkul = z.KodeMatkul,
                        NamaMatkul = z.NamaMatkul,
                        ClassSection = z.ClassSection,
                        TanggalUjian = z.TanggalUjian,
                        JamMulai = z.JamMulai,
                    })
                    .ToList();
                mListJadwalUjian.TotalFilterCount = gridfilter.Count();
                return mListJadwalUjian;
            }
        }

        public VMListJadwalUjian GetListJadwalUjian(int Skip, int Length, string SearchParam, string SortBy, bool SortDir, string jenjangStudi, string fakultas, string jenisUjian, string tahunSemester)
        {
            VMListJadwalUjian mListJadwalUjian = new VMListJadwalUjian();
            if (String.IsNullOrEmpty(SearchParam))
            {
                SearchParam = "";
            }
            using (var context = new MBKMContext())
            {

                var result = context.jadwalUjians.Where(x => x.IsDeleted == false &&
                x.JenjangStudi == jenjangStudi &&
                x.FakultasID == fakultas &&
                x.KodeTipeUjian == jenisUjian &&
                x.STRM == tahunSemester);

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
                        ID = z.ID,
                        KodeMatkul = z.KodeMatkul,
                        NamaMatkul = z.NamaMatkul,
                        FakultasID = z.FakultasID,
                        NamaFakultas = z.NamaFakultas,
                        ClassSection = z.ClassSection,
                        TanggalUjian = z.TanggalUjian,
                        JamMulai = z.JamMulai,
                        JamAkhir = z.JamAkhir,
                        RuangUjian = z.RuangUjian,
                        Lokasi = z.Lokasi,
                        TipeUjian = z.TipeUjian,
                    })
                    .ToList();
                mListJadwalUjian.TotalFilterCount = gridfilter.Count();
                return mListJadwalUjian;
            }
        }

    }
}
