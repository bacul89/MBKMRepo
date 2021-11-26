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

        public int getBiaya(string NoKerjasama)
        {
            using (var context = new MBKMContext())
            {
                var result = context.PerjanjianKerjasamas.Where(x => x.NoPerjanjian == NoKerjasama)
                    .Select(x => x.BiayaKuliah).SingleOrDefault();
                    
                return result;
            }
        }

        public VMListPerjanjianKerjasama getListPerjanjianKerjasama(int Skip, int Length, string SearchParam, string SortBy, bool SortDir)
        {
            VMListPerjanjianKerjasama mListmodel = new VMListPerjanjianKerjasama();
            if (String.IsNullOrEmpty(SearchParam))
            {
                // if we have an empty search then just order the results by Id ascending
                //SortBy = "ID";
                //SortDir = true;
                SearchParam = "";
            }
            using (var context = new MBKMContext())
            {

                var result = context.PerjanjianKerjasamas.Where(x => x.IsDeleted == false);
                mListmodel.TotalCount = result.Count();
                var gridfilter = result.AsQueryable().Where(y => y.NamaInstansi.Contains(SearchParam) ||
                                        y.JenisKerjasama.Contains(SearchParam) || y.JenisPertukaran.Contains(SearchParam) || y.NamaUnit.Contains(SearchParam)
                                        || y.NamaInstansi.Contains(SearchParam) || y.NoPerjanjian.Contains(SearchParam)||y.CreatedBy.Contains(SearchParam))
                    .OrderBy(SortBy, SortDir);                    
                mListmodel.gridDatas = gridfilter.Skip(Skip).Take(Length)
                    .Select(z => new GridDataPerjanjian
                    {
                        ID = z.ID,
                        Instansi = z.Instansi,
                        NamaUnit = z.NamaUnit,
                        NamaInstansi = z.NamaInstansi,
                        JenisKerjasama = context.JenisKerjasamaModels.Where(x => x.ID.ToString() == z.JenisKerjasama).Select(x => x.JenisKerjasama).FirstOrDefault(),
                        JenisPertukaran = z.JenisPertukaran,
                        NoPerjanjian = z.NoPerjanjian,
                        CreatedBy = z.CreatedBy,
                        TanggalAkhir = z.TanggalAkhir,
                        TanggalMulai = z.TanggalMulai
                    }).ToList();
                mListmodel.TotalFilterCount = gridfilter.Count();
                return mListmodel;
            }
        }
        public List<VMLookupNoKerjasama> getNamaInstansi(int Skip, int Length, string Search)
        {
            
            using (var context = new MBKMContext())
            {
                //var result = context.PerjanjianKerjasamas.Where(x => x.NamaInstansi.Contains(Search))
                //    .GroupBy(x => x.NamaInstansi).Select(x => x.FirstOrDefault());
                var getJenis = context.JenisKerjasamaModels.Where(x => x.JenisKerjasama.ToLower().Contains("eksternal")).Select(y=>y.ID).FirstOrDefault();
               // getJenis.
                var result = context.PerjanjianKerjasamas.Where(x => x.JenisKerjasama == getJenis.ToString() && x.NamaInstansi.Contains(Search))
                    .Select(x => x.NamaInstansi).Distinct();
                //var result2 = new List<VMLookupNoKerjasama>();
                var result2 = result.Select(y => new VMLookupNoKerjasama
                {
                    ID = 0,
                    NoKerjasama = y.ToString(),
                    NamaInstansi = y.ToString(),
                    Biaya = 0
                }).OrderBy("NamaInstansi").Skip(Skip).Take(Length)
                    .ToList();
                return result2;
            }
        }
        
        public List<VMLookupNoKerjasama> getNoKerjasama(int Skip, int Length, string Search, string NamaInstansi)
        {
            using (var context = new MBKMContext())
            {
                var today = DateTime.Now;
                var getJenis = context.JenisKerjasamaModels.Where(x => x.JenisKerjasama.ToLower().Contains("eksternal")).Select(y => y.ID).FirstOrDefault();
                var result = context.PerjanjianKerjasamas.Where(x => x.NoPerjanjian.Contains(Search) && x.NamaInstansi == NamaInstansi && x.JenisKerjasama ==getJenis.ToString() &&  x.TanggalAkhir >= today)
                    .OrderBy("NoPerjanjian").Skip(Skip).Take(Length).Select(x => new VMLookupNoKerjasama
                    {
                        ID = x.ID,
                        NoKerjasama = x.NoPerjanjian,
                        NamaInstansi = x.NamaInstansi,
                        Biaya = x.BiayaKuliah
                    }).ToList();
                return result;
            }

        }
    }
}
