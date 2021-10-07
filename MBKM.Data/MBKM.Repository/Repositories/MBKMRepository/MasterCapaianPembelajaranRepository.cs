using MBKM.Common.Interfaces.RepoInterfaces.MBKMRepoInterfaces;
using MBKM.Entities.Models.MBKM;
using MBKM.Entities.ViewModel;
using MBKM.Repository.BaseRepository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace MBKM.Repository.Repositories.MBKMRepository
{
    public class MasterCapaianPembelajaranRepository : GenericRepository<MasterCapaianPembelajaran>, IMasterCapaianPembelajaranRepository
    {
        public MasterCapaianPembelajaranRepository(DbContext _db) : base(_db)
        {
        }
        public IEnumerable<VMMataKuliah> GetMatkul(int PageNumber, int PageSize, string search)
        {
            using (var context = new MBKMContext())
            {
                var PageNumberParam = new SqlParameter("@PageNumber", PageNumber);
                var PageSizeParam = new SqlParameter("@PageSize", PageSize);
                var searchParam = new SqlParameter("@Search", search);
                var result = context.Database
                    .SqlQuery<VMMataKuliah>("GetMatkul @PageNumber, @PageSize, @Search", PageNumberParam, PageSizeParam, searchParam).ToList();
                return result;
            }
        }
        
        public IEnumerable<VMFakultas> GetFakultas(string jenjangStudi, string search)
        {
            using (var context = new MBKMContext())
            {
                //jenjangStudi = "S1";
                var jenjangStudiParam = new SqlParameter("@JenjangStudi", jenjangStudi);
                //var jenjangStudiParam = "S1";
                var searchParam = new SqlParameter("@Search", search);
                var result = context.Database
                    .SqlQuery<VMFakultas>("GetFakultas @JenjangStudi, @Search", jenjangStudiParam, searchParam).ToList();
                return result;
            }
        }

        public IEnumerable<VMProdi> GetLokasiByProdi(string jenjangStudi, string namaProdi, string search)
        {
            using (var context = new MBKMContext())
            {
                //jenjangStudi = "S1";
                var jenjangStudiParam = new SqlParameter("@JenjangStudi", jenjangStudi);
                //var jenjangStudiParam = "S1";
                var namaProdiParam = new SqlParameter("@NamaProdi", namaProdi);
                var searchParam = new SqlParameter("@Search", search);
                var result = context.Database
                    .SqlQuery<VMProdi>("GetLokasiByProdi @JenjangStudi, @NamaProdi, @Search", jenjangStudiParam, namaProdiParam, searchParam).ToList();
                return result;
            }
        }

        public IEnumerable<VMProdi> GetProdiByFakultas(string jenjangStudi, string idFakultas, string search)
        {
            using (var context = new MBKMContext())
            {
                //jenjangStudi = "S1";
                var jenjangStudiParam = new SqlParameter("@JenjangStudi", jenjangStudi);
                //var jenjangStudiParam = "S1";
                var idFakultasParam = new SqlParameter("@IdFakultas", idFakultas);
                var searchParam = new SqlParameter("@Search", search);
                var result = context.Database
                    .SqlQuery<VMProdi>("GetProdiByFakultas @JenjangStudi, @IdFakultas, @Search", jenjangStudiParam, idFakultasParam, searchParam).ToList();
                return result;
            }
        }

        public VMListMasterCPL GetListMasterCPL(int Skip, int Length, string SearchParam, string SortBy, bool SortDir)
        {
            VMListMasterCPL mListCPL = new VMListMasterCPL();
            if (String.IsNullOrEmpty(SearchParam))
            {
                // if we have an empty search then just order the results by Id ascending
                //SortBy = "ID";
                //SortDir = true;
                SearchParam = "";
            }
            using (var context = new MBKMContext())
            {

                var result = context.MasterCPLS.Where(x => x.IsDeleted == false);
                mListCPL.TotalCount = result.Count();
                var gridfilter = result.AsQueryable().Where(y => y.Kode.Contains(SearchParam) || y.Kelompok.Contains(SearchParam) ||
                y.Capaian.Contains(SearchParam))
                    .Select(z => new GridDataCPL
                    {
                        ID = z.ID,
                        //JenjangStudi = z.JenjangStudi,
                        //Lokasi = z.Lokasi,
                        Kode = z.Kode,
                        Kelompok = z.Kelompok,
                        Capaian = z.Capaian
                        //KodeProdi = Convert.ToDouble(z.KodeProdi),

                        //Status = z.IsActive
                    }).OrderBy(SortBy, SortDir);
                mListCPL.gridDatas = gridfilter.Skip(Skip).Take(Length).ToList();
                mListCPL.TotalFilterCount = gridfilter.Count();
                return mListCPL;
            }
        }
    }
}
