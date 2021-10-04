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
    public class PendaftaranMataKuliahRepository : GenericRepository<PendaftaranMataKuliah>, IPendaftaranMataKuliahRepository
    {
        public PendaftaranMataKuliahRepository(DbContext _db) : base(_db)
        {
        }
        public IEnumerable<VMFakultas> GetFakultas(string jenjangStudi, string search)
        {
            using (var context = new MBKMContext())
            {
                var jenjangStudiParam = new SqlParameter("@JenjangStudi", jenjangStudi);
                var searchParam = new SqlParameter("@Search", search);
                var result = context.Database
                    .SqlQuery<VMFakultas>("GetFakultas @JenjangStudi, @Search", jenjangStudiParam, searchParam).ToList();
                return result;
            }
        }
        public IEnumerable<VMProdi> GetProdiByFakultas(string jenjangStudi, string idFakultas, string search)
        {
            using (var context = new MBKMContext())
            {
                var jenjangStudiParam = new SqlParameter("@JenjangStudi", jenjangStudi);
                var idFakultasParam = new SqlParameter("@IdFakultas", idFakultas);
                var searchParam = new SqlParameter("@Search", search);
                var result = context.Database
                    .SqlQuery<VMProdi>("GetProdiByFakultas @JenjangStudi, @IdFakultas, @Search", jenjangStudiParam, idFakultasParam, searchParam).ToList();
                 return result;
            }
        }
        public IEnumerable<VMProdi> GetLokasiByProdi(string jenjangStudi, string idProdi, string search)
        {
            using (var context = new MBKMContext())
            {
                var jenjangStudiParam = new SqlParameter("@JenjangStudi", jenjangStudi);
                var idProdiParam = new SqlParameter("@IdProdi", idProdi);
                var searchParam = new SqlParameter("@Search", search);
                var result = context.Database
                    .SqlQuery<VMProdi>("GetLokasiByProdi @JenjangStudi, @IdProdi, @Search", jenjangStudiParam, idProdiParam, searchParam).ToList();
                return result;
            }
        }

        public VMListPendaftaranMataKuliah GetPendaftaranList(int Skip, int Length, string SearchParam, string SortBy, bool SortDir)
        {
            VMListPendaftaranMataKuliah mListPendaftaranMataKuliah = new VMListPendaftaranMataKuliah();
            if (String.IsNullOrEmpty(SearchParam))
            {
                SearchParam = "";
            }
            using (var context = new MBKMContext())
            {
                context.Configuration.LazyLoadingEnabled = false;
                var result = context.PendaftaranMataKuliahs.Where(x => x.IsDeleted == false).Include(x => x.mahasiswas).Include(x => x.JadwalKuliahs);
                mListPendaftaranMataKuliah.TotalCount = result.Count();
                var gridfilter = result
                    .AsQueryable()
                    .Where(y => y.Nilai.Contains(SearchParam));    
                mListPendaftaranMataKuliah.gridDatas = gridfilter.Take(Length)
                    .Select(z => new GridListPendaftaranMataKuliah
                    {
                        ID = z.ID,
                        DosenPembimbing = z.DosenPembimbing,
                        DosenID = z.DosenID,
                        Hasil = z.Hasil,
                        JadwalKuliahID = z.JadwalKuliahID,
                        JadwalKuliahs = z.JadwalKuliahs,
                        mahasiswas = z.mahasiswas,
                        MatkulKodeAsal = z.MatkulKodeAsal,
                        MatkulAsal = z.MatkulAsal,
                    }).ToList();
                mListPendaftaranMataKuliah.TotalFilterCount = gridfilter.Count();
                return mListPendaftaranMataKuliah;
            }
        }


    }
}
