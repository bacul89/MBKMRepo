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
        /*        VMMataKuliah ICPLMataKuliahRepository.GetMatkul(int skip, int take, string searchBy, string idProdi, string idFakultas)
                {

                    VMMataKuliah mListMK = new VMMataKuliah();
                    if (String.IsNullOrEmpty(searchBy))
                    {
                        // if we have an empty search then just order the results by Id ascending
                        //SortBy = "ID";
                        //SortDir = true;
                        searchBy = "";
                    }
                    using (var context = new MBKMContext())
                    {

                        var PageNumberParam = new SqlParameter("@PageNumber", skip);
                        var PageSizeParam = new SqlParameter("@PageSize", take);
                        var searchParam = new SqlParameter("@Search", searchBy);
                        var result = context.Database
                            .SqlQuery<GridDataMataKuliah>("GetMatkul @PageNumber, @PageSize, @Search", PageNumberParam, PageSizeParam, searchParam)
                            .Where(y => y.FakultasID.Contains(idFakultas) && y.ProdiID.Contains(idProdi));


                        mListMK.TotalCount = result.Count();
                        var gridfilter = result
                            .Select(z => new GridDataMataKuliah
                            {
                                CRSE_ID = z.CRSE_ID,
                                Expr1 = z.Expr1,
                                DESCR = z.DESCR,
                                Prodi = z.Prodi,
                                Fakultas = z.Fakultas,
                                ProdiID = z.ProdiID,
                                FakultasID = z.FakultasID
                            });
                        mListMK.gridDatas = gridfilter.Skip(skip).Take(take).ToList();
                        mListMK.TotalFilterCount = gridfilter.Count();
                        return mListMK;
                    }
                }
        */
        public IEnumerable<VMMataKuliah> GetMatkul(int skip, int take, string searchBy, string idProdi, string idFakultas)
        {
            if (String.IsNullOrEmpty(searchBy))
            {
                // if we have an empty search then just order the results by Id ascending
                //SortBy = "ID";
                //SortDir = true;
                searchBy = "";
            }

            using (var context = new MBKMContext())
            {
                var PageNumberParam = new SqlParameter("@PageNumber", skip);
                var PageSizeParam = new SqlParameter("@PageSize", take);
                var searchParam = new SqlParameter("@Search", searchBy);

                var result = context.Database
                    .SqlQuery<VMMataKuliah>("GetMatkul @PageNumber, @PageSize, @Search", PageNumberParam, PageSizeParam, searchParam).Where(x =>
                    x.ProdiID == idProdi &&
                    x.FakultasID == idFakultas).ToList();
                return result;
            }
        }


    }
}
