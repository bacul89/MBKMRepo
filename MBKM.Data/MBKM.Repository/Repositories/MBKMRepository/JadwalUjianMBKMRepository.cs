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
                SearchParam = "";
            }
            using (var context = new MBKMContext())
            {

                var result1 = context.jadwalUjians.Where(x => x.IsDeleted == false &&
                x.JenjangStudi == jenjangStudi &&
                x.KodeTipeUjian == jenisUjian &&
                x.STRM == tahunSemester);

                var result = result1.AsEnumerable()
                 .Where(p => int.Parse(p.FakultasID) == int.Parse(fakultas));


                mListJadwalUjian.TotalCount = result.Count();

                var gridfilter2 = result
                    .AsQueryable()
                    .Where(y => y.KodeMatkul.Contains(SearchParam)
                        || y.NamaMatkul.Contains(SearchParam)
                        || y.ClassSection.Contains(SearchParam)
                        || y.JamMulai.Contains(SearchParam)
                        || y.JamAkhir.Contains(SearchParam)
                        || y.KodeTipeUjian.Contains(SearchParam)
                    );
                var gridfilter = gridfilter2.AsQueryable()
                    .GroupBy(x => new { x.KodeMatkul, x.KodeTipeUjian, x.NamaMatkul, x.ClassSection, x.TanggalUjian, x.JamMulai, x.JamAkhir}, (key, group) => 
                    new {
                        KodeMatkul = key.KodeMatkul,
                        NamaMatkul = key.NamaMatkul,
                        ClassSection = key.ClassSection,
                        TanggalUjian = key.TanggalUjian,
                        JamMulai = key.JamMulai,
                        JamAkhir = key.JamAkhir,
                        KodeTipeUjian = key.KodeTipeUjian,
                        Result = group.ToList() })
                    .OrderBy(SortBy, SortDir);

                mListJadwalUjian.gridDatas = gridfilter.Skip(Skip).Take(Length)
                    .Select(z => new GridDataJadwalUjian
                    {
                        /*ID = z.ID,*/
                        KodeMatkul = z.KodeMatkul,
                        NamaMatkul = z.NamaMatkul,
                        ClassSection = z.ClassSection,
                        TanggalUjian = z.TanggalUjian,
                        JamMulai = z.JamMulai,
                        JamAkhir = z.JamAkhir,
                        KodeTipeUjian = z.KodeTipeUjian
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

                var result1 = context.jadwalUjians.Where(x => x.IsDeleted == false &&
                x.JenjangStudi == jenjangStudi &&
                /*x.FakultasID == fakultas &&*/
                x.KodeTipeUjian == jenisUjian &&
                x.STRM == tahunSemester);

                var result = result1.AsEnumerable()
                 .Where(p => int.Parse(p.FakultasID) == int.Parse(fakultas));

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
                        KodeTipeUjian = z.KodeTipeUjian
                    })
                    .ToList();
                mListJadwalUjian.TotalFilterCount = gridfilter.Count();
                return mListJadwalUjian;
            }
        }
        public IEnumerable<VMSemester> getAllSemester()
        {
            using (var context = new MBKMContext())
            {
                var result = context.Database
                    .SqlQuery<VMSemester>("GetSemesterALL").ToList();
                return result;
            }
        }
    }
}
