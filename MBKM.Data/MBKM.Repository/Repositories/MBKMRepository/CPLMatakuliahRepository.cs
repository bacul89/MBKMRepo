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
using System.Data.SqlClient;

namespace MBKM.Repository.Repositories.MBKMRepository
{
    public class CPLMatakuliahRepository : GenericRepository<CPLMatakuliah>, ICPLMataKuliahRepository
    {
        public CPLMatakuliahRepository(DbContext _db) : base(_db)
        {
        }


        VMListMapingCPL ICPLMataKuliahRepository.GetListMapingCPL(int Skip, int Length, string SearchParam, string SortBy, bool SortDir)
        {
            VMListMapingCPL mListCPL = new VMListMapingCPL();
            if (String.IsNullOrEmpty(SearchParam))
            {
                // if we have an empty search then just order the results by Id ascending
                //SortBy = "ID";
                //SortDir = true;
                SearchParam = "";
            }
            using (var context = new MBKMContext())
            {

                var result = context.CPLMatakuliah.Where(x => x.IsDeleted == false);
                mListCPL.TotalCount = result.Count();
                var gridfilter = result.AsQueryable().Where(y => y.NamaMataKuliah.Contains(SearchParam) || y.KodeMataKuliah.Contains(SearchParam) ||
                y.MasterCapaianPembelajarans.Capaian.Contains(SearchParam))
                    .Select(z => new GridDataMapingCPL
                    {
                        ID = z.ID,
                        IDMataKUliah = z.IDMataKUliah,
                        NamaMataKuliah = z.NamaMataKuliah,
                        KodeMataKuliah = z.KodeMataKuliah,
                        Capaian = z.MasterCapaianPembelajarans.Capaian,
                        Kelompok = z.MasterCapaianPembelajarans.Kelompok,
                        Kode = z.MasterCapaianPembelajarans.Kode
                    }).OrderBy(SortBy, SortDir);
                mListCPL.gridDatas = gridfilter.Skip(Skip).Take(Length).ToList();
                mListCPL.TotalFilterCount = gridfilter.Count();
                return mListCPL;
            }
        }
        VMListMapingCPL ICPLMataKuliahRepository.SearchListMapingCPL(int Skip, int Length, string SearchParam, string SortBy, bool SortDir, string idProdi, string lokasi, string idFakultas, string jenjangStudi, string idMatakuliah)
        {
            VMListMapingCPL mListCPL = new VMListMapingCPL();
            if (String.IsNullOrEmpty(SearchParam))
            {
                // if we have an empty search then just order the results by Id ascending
                //SortBy = "ID";
                //SortDir = true;
                SearchParam = "";
            }
            using (var context = new MBKMContext())
            {

                var result = context.CPLMatakuliah.Where(
                    x => 
                    x.IsDeleted == false &&
                    x.MasterCapaianPembelajarans.ProdiID == idProdi &&
                    x.MasterCapaianPembelajarans.FakultasID == idFakultas &&
                    x.MasterCapaianPembelajarans.JenjangStudi == jenjangStudi &&
                    x.MasterCapaianPembelajarans.Lokasi == lokasi &&
                    x.IDMataKUliah == idMatakuliah                
                );
                mListCPL.TotalCount = result.Count();
                var gridfilter = result.AsQueryable().Where(y => y.NamaMataKuliah.Contains(SearchParam) || y.KodeMataKuliah.Contains(SearchParam) ||
                y.MasterCapaianPembelajarans.Capaian.Contains(SearchParam))
                    .Select(z => new GridDataMapingCPL
                    {
                        ID = z.ID,
                        IDMataKUliah = z.IDMataKUliah,
                        NamaMataKuliah = z.NamaMataKuliah,
                        KodeMataKuliah = z.KodeMataKuliah,
                        Capaian = z.MasterCapaianPembelajarans.Capaian,
                        Kelompok = z.MasterCapaianPembelajarans.Kelompok,
                        Kode = z.MasterCapaianPembelajarans.Kode
                    }).OrderBy(SortBy, SortDir);
                mListCPL.gridDatas = gridfilter.Skip(Skip).Take(Length).ToList();
                mListCPL.TotalFilterCount = gridfilter.Count();
                return mListCPL;
            }
        }

    }
}
