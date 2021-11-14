using MBKM.Common.Interfaces.RepoInterfaces.MBKMRepoInterfaces;
using MBKM.Entities.Models.MBKM;
using MBKM.Entities.ViewModel;
using MBKM.Repository.BaseRepository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Dynamic;
using System.Text;
using System.Threading.Tasks;

namespace MBKM.Repository.Repositories.MBKMRepository
{
    public class DaftarAllMahasiswaRepository : GenericRepository<Mahasiswa>, IDaftarAllMahasiswaRepository
    {
        public DaftarAllMahasiswaRepository(DbContext _db) : base(_db)
        {
        }
        public VMListDaftarAllMahasiswa GetListAllMhs(int Skip, int Length, string SearchParam, string SortBy, bool SortDir)
        {
            VMListDaftarAllMahasiswa mListMahasiswa = new VMListDaftarAllMahasiswa();
            if (String.IsNullOrEmpty(SearchParam))
            {
                // if we have an empty search then just order the results by Id ascending
                //SortBy = "ID";
                //SortDir = true;
                SearchParam = "";
            }
            //using (var context = new MBKMContext())
            //{

            //    var result = context.Mahasiswas.Where(x => x.IsDeleted == false);
            //    mListMahasiswa.TotalCount = result.Count();
            //    var gridfilter = result
            //        .AsQueryable()
            //        .Where(y => y.NamaUniversitas.Contains(SearchParam) ||
            //                            y.JenjangStudi.Contains(SearchParam) || y.ProdiAsal.Contains(SearchParam) || y.NIMAsal.Contains(SearchParam)
            //                            || y.Nama.Contains(SearchParam) || y.Gender.Contains(SearchParam) || y.NoKerjasama.Contains(SearchParam)
            //                            || y.StatusKerjasama.Contains(SearchParam) || y.StatusVerifikasi.Contains(SearchParam))
            //        .OrderBy(SortBy, SortDir);
            //    mListMahasiswa.gridDatas = gridfilter.Skip(Skip).Take(Length)
            //        .Select(z => new GridDataAllMahasiswa
            //        {
            //            ID = z.ID,
            //            Email = z.Email,
            //            Nama = z.Nama,
            //            NamaUniversitas = z.NamaUniversitas,
            //            Gender = context.Lookups.Where(x => x.Tipe == "Gender" && x.Nilai == z.Gender).Select(x => x.Nama).FirstOrDefault(),
            //            NoHp = z.NoHp,
            //            NIMAsal = z.NIMAsal,
            //            NIM = z.NIM,
            //            NoKerjasama = z.NoKerjasama,
            //            StatusKerjasama = z.StatusKerjasama,
            //            Telepon = z.Telepon,
            //            ProdiAsal = z.ProdiAsal,
            //            JenjangStudi = z.JenjangStudi,
            //            StatusVerifikasi = z.StatusVerifikasi
            //        }).ToList();
            //    mListMahasiswa.TotalFilterCount = gridfilter.Count();
            //    return mListMahasiswa;
            //}
            using (var context = new MBKMContext())
            {
                var searchParam = new SqlParameter("@Search", SearchParam); 
                var totalParam = new SqlParameter("@TotalCount", SqlDbType.Int)
                {
                    Direction = System.Data.ParameterDirection.Output
                };

                var totalFilterParam = new SqlParameter("@TotalFilterCount", SqlDbType.Int)
                {
                    Direction = System.Data.ParameterDirection.Output
                };
                var result = context.Database
                    .SqlQuery<GridDataAllMahasiswa>("GetListAllMhs @Search, @TotalCount out, @TotalFilterCount out", searchParam, totalParam, totalFilterParam)
                    .OrderBy(SortBy, SortDir)
                    .Skip(Skip)
                    .Take(Length)
                    .ToList();
                mListMahasiswa.TotalCount = (int) totalParam.Value;
                mListMahasiswa.TotalFilterCount = (int) totalFilterParam.Value;
                mListMahasiswa.gridDatas = result;
                return mListMahasiswa;
            }
        }
    }
}
