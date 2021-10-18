using MBKM.Common.Interfaces.RepoInterfaces.MBKMRepoInterfaces;
using MBKM.Entities.Models.MBKM;
using MBKM.Entities.ViewModel;
using MBKM.Repository.BaseRepository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Dynamic;
using System.Text;
using System.Threading.Tasks;

namespace MBKM.Repository.Repositories.MBKMRepository
{
    public class LinkFasilitasRepository : GenericRepository<JadwalKuliah>, ILinkFasilitasRepository
    {
        public LinkFasilitasRepository(DbContext _db) : base(_db)
        {
        }
        public VMLinkFasilitas GetListJadwalKuliah(int Skip, int Length, string SearchParam, string SortBy, bool SortDir)
        {
            throw new NotImplementedException();
        }

        public List<VMClassSection> GetListSeksi()
        {
            using (var context = new MBKMContext())
            {
                var result = context.jadwalKuliahs.Where(x => x.IsActive && !x.IsDeleted).Select(x => new VMClassSection
                {
                   
                    
                    Nama = x.ClassSection
                });
                return result.ToList();
            }
        }

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
                var PageNumberParam = new SqlParameter("@PageNumber", 1);
                var PageSizeParam = new SqlParameter("@PageSize", 1000000);
                //var PageNumberParam = new SqlParameter("@PageNumber", skip);
                //var PageSizeParam = new SqlParameter("@PageSize", take);
                var searchParam = new SqlParameter("@Search", searchBy);
                var idProdiParam = new SqlParameter("@ProdiID", idProdi);
                var idFakultasParam = new SqlParameter("@FakultasID", idFakultas);

                // .Skip(skip).Take(take)
                var result = context.Database
                    .SqlQuery<VMMataKuliah>("GetMatkul @PageNumber, @PageSize, @Search, @ProdiID, @FakultasID", PageNumberParam, PageSizeParam, searchParam,
                    idProdiParam, idFakultasParam).ToList();
                //).Where(x =>
                //    x.ProdiID == idProdi &&
                //    x.FakultasID == idFakultas).ToList();
                return result;
            }



           

        }

        //public IEnumerable<VMLinkFasilitas> GetSeksi(string search)
        //{
        //    throw new NotImplementedException();
        //}


        public VMLinkFasilitas SearchListJadwalKuliah(int Skip, int Length, string SearchParam, string SortBy, bool SortDir, string idProdi, string lokasi, string idFakultas, string jenjangStudi, string idMatakuliah, string seksi)
        {
            VMLinkFasilitas mListCPL = new VMLinkFasilitas();
            if (String.IsNullOrEmpty(SearchParam))
            {
                // if we have an empty search then just order the results by Id ascending
                //SortBy = "ID";
                //SortDir = true;
                SearchParam = "";
            }
            using (var context = new MBKMContext())
            {

                var result = context.jadwalKuliahs.Where(
                    x =>
                    x.IsDeleted == false &&
                    x.ProdiID == Convert.ToInt64(idProdi) &&
                    x.FakultasID == Convert.ToInt64(idFakultas) &&
                    x.JenjangStudi == jenjangStudi &&
                    x.Lokasi == lokasi &&
                    x.MataKuliahID == idMatakuliah &&
                    x.ClassSection == seksi
                   
                );
                mListCPL.TotalCount = result.Count();
                var gridfilter = result.AsQueryable().Where(y => y.NamaMataKuliah.Contains(SearchParam) || y.KodeMataKuliah.Contains(SearchParam)
               )
                    .Select(z => new GridDataLinkFasilitas
                    {
                        ID = z.ID,
                        SKS = z.SKS,
                        NamaMataKuliah = z.NamaMataKuliah,
                        KodeMataKuliah = z.KodeMataKuliah,
                        ClassSection = z.ClassSection,
                        Hari = z.Hari,
                        JamMasuk = z.JamMasuk,
                        RuangKelas = z.RuangKelas 
                    }).OrderBy(SortBy, SortDir);
                mListCPL.gridDatas = gridfilter.Skip(Skip).Take(Length).ToList();
                mListCPL.TotalFilterCount = gridfilter.Count();
                return mListCPL;
            }
        }

        VMLinkFasilitas ILinkFasilitasRepository.GetSeksi(string seksi)
        {
            //using (var context = new MBKMContext())
            //{
            //    var jenjangStudiParam = new SqlParameter("@JenjangStudi", jenjangStudi);
            //    var result = context.Database
            //        .SqlQuery<VMSemester>("GetSemester @JenjangStudi", jenjangStudiParam).FirstOrDefault();
            //    return result;
            //}
            throw new NotImplementedException();
        }
    }
}
