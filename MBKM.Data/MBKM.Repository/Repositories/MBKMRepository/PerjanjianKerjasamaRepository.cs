using MBKM.Common.Interfaces.RepoInterfaces.MBKMRepoInterfaces;
using MBKM.Entities.Models.MBKM;
using MBKM.Entities.ViewModel;
using MBKM.Repository.BaseRepository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Dynamic;

namespace MBKM.Repository.Repositories.MBKMRepository
{
    public class PerjanjianKerjasamaRepository : GenericRepository<PerjanjianKerjasama>, IPerjanjianKerjasamaRepository
    {
        public PerjanjianKerjasamaRepository(DbContext _db) : base(_db)
        {
        }

        public VMListPerjanjianKerjasama getListPerjanjianKerjasama(int Skip, int Length, string SearchParam, string SortBy, bool SortDir)
        {
            VMListPerjanjianKerjasama mListmodel = new VMListPerjanjianKerjasama();
            if (String.IsNullOrEmpty(SearchParam))
            {
                // if we have an empty search then just order the results by Id ascending
                SortBy = "ID";
                SortDir = true;
            }
            using (var context = new MBKMContext())
            {

                var result = context.PerjanjianKerjasamas.Where(x => x.IsDeleted == false);
                mListmodel.TotalCount = result.Count();
                mListmodel.gridDatas = result.AsQueryable().Where(y => y.NamaInstansi.Contains(SearchParam) ||
                                        y.JenisKerjasama.Contains(SearchParam) || y.JenisPertukaran.Contains(SearchParam) || y.NamaUnit.Contains(SearchParam)
                                        || y.NamaInstansi.Contains(SearchParam) || y.NoPerjanjian.Contains(SearchParam))
                    .Select(z => new GridDataPerjanjian
                    {
                        ID = z.ID,
                        Instansi = z.Instansi,
                        NamaUnit = z.NamaUnit,
                        NamaInstansi = z.NamaInstansi,
                        JenisKerjasama = z.JenisKerjasama,
                        JenisPertukaran = z.JenisPertukaran,
                        NoKerjasama = z.NoPerjanjian,
                        Inputer = z.CreatedBy,
                        TanggalAkhir = z.TanggalAkhir,
                        TanggalMulai = z.TanggalMulai
                    }).OrderBy(SortBy, SortDir).Skip(Skip).Take(Length).ToList();
                mListmodel.TotalFilterCount = mListmodel.gridDatas.Count();
                return mListmodel;
            }
        }
    }
}
