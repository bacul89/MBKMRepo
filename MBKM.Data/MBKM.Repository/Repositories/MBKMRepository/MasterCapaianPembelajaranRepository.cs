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
                var jenjangStudiParam = new SqlParameter("@JenjangStudi", jenjangStudi);
                var namaProdiParam = new SqlParameter("@NamaProdi", namaProdi);
                var searchParam = new SqlParameter("@Search", search);
                var result = context.Database
                    .SqlQuery<VMProdi>("GetLokasiByJenjangStudi @JenjangStudi, @NamaProdi, @Search", jenjangStudiParam, namaProdiParam, searchParam).ToList();
                return result;
            }
        }

        public IEnumerable<VMProdi> GetLokasiByProdiName(string jenjangStudi, string namaProdi, string search)
        {
            using (var context = new MBKMContext())
            {
                var jenjangStudiParam = new SqlParameter("@JenjangStudi", jenjangStudi);
                var namaProdiParam = new SqlParameter("@NamaProdi", namaProdi);
                var searchParam = new SqlParameter("@Search", search);
                var result = context.Database
                    .SqlQuery<VMProdi>("GetLokasiByJenjangStudi @JenjangStudi, @NamaProdi, @Search", jenjangStudiParam, namaProdiParam, searchParam).ToList();
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

        public VMListMasterCPL GetListMasterCPL(int Skip, int Length, string SearchParam, string SortBy, bool SortDir, string prodi, string jenjang, string fakultas)
        {
            VMListMasterCPL mListCPL = new VMListMasterCPL();
            if (String.IsNullOrEmpty(SearchParam))
            {
                SearchParam = "";
            }
            using (var context = new MBKMContext())
            {
                //Any() sebagai where exist => ambil duplicate item lalu ambil yg id nya paling kecil saja untuk griddata nya ^_^
                //sorting di javascript menggunakan kelompok sebagai default, karena ID tidak dapat ditampilkan, jika ditampilkan duplikat redundant lagi ~_~
                var result = context.MasterCPLS.Where(x => x.IsDeleted == false && x.NamaProdi.Contains(prodi)  && x.JenjangStudi == jenjang && x.FakultasID.Contains(fakultas) && 
                context.MasterCPLS.Any(s=> s.Kelompok == x.Kelompok && s.Capaian == x.Capaian && s.Kode == x.Kode && s.FakultasID == x.FakultasID && s.NamaProdi == x.NamaProdi &&
                s.JenjangStudi == x.JenjangStudi && s.NamaFakultas == x.NamaFakultas && s.IsActive == x.IsActive && s.ID < x.ID));
                
                mListCPL.TotalCount = result.Count();
                
                var gridfilter = result.AsQueryable().Where(y => y.Kode.Contains(SearchParam) || y.Kelompok.Contains(SearchParam) ||
                y.Capaian.Contains(SearchParam))
                    .Select(z => new VMMasterCPL
                    {
                        ID = z.ID,
                        Kode = z.Kode,
                        Kelompok = z.Kelompok,
                        Capaian = z.Capaian,
                        FakultasID = z.FakultasID,
                        NamaProdi = z.NamaProdi,
                        JenjangStudi = z.JenjangStudi,
                        NamaFakultas = z.NamaFakultas,
                        Status = z.IsActive
                        
                    })
                    .OrderBy(SortBy, SortDir);
                mListCPL.gridDatas = gridfilter.Skip(Skip).Take(Length).Distinct().GroupBy(s => new
                {
                    s.JenjangStudi,
                    s.Kode,
                    s.FakultasID,
                    s.Kelompok,
                    s.Capaian,
                    s.NamaProdi,
                    s.NamaFakultas,
                    s.Status
                    //s.CreatedBy
                }).Select(n => new GridDataCPL
                {
                    JenjangStudi = n.Key.JenjangStudi,
                    Kode = n.Key.Kode,
                    FakultasID = n.Key.FakultasID,
                    Kelompok = n.Key.Kelompok,
                    Capaian = n.Key.Capaian,
                    NamaProdi = n.Key.NamaProdi,
                    Status = n.Key.Status
                }).ToList();
                mListCPL.TotalFilterCount = gridfilter.Count();
                return mListCPL;
            }
        }
    }
}
