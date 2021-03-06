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

        public IEnumerable<VMLookup> GetFakultasByJenjangStudi(string search, string jenjangStudi)
        {
            using (var context = new MBKMContext())
            {
                var searchParam = new SqlParameter("@Search", search);
                var jenjangStudiParam = new SqlParameter("@JenjangStudi", jenjangStudi);
                var result = context.Database
                    .SqlQuery<VMLookup>("GetFakultasByJenjangStudi @JenjangStudi, @Search", jenjangStudiParam, searchParam).ToList();
                return result;
            }
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
                }).GroupBy(x => x.Nama).Select(y => y.FirstOrDefault()).OrderBy(x => x.Nama);
                return result.ToList();
            }
        }

        public IEnumerable<VMLookup> GetLokasiByProdi(string search, string jenjangStudi, string prodi)
        {
            using (var context = new MBKMContext())
            {
                var searchParam = new SqlParameter("@Search", search);
                var jenjangStudiParam = new SqlParameter("@JenjangStudi", jenjangStudi);
                var prodiParam = new SqlParameter("@Prodi", prodi);
                var result = context.Database.SqlQuery<VMLookup>("GetLokasiByProdi @JenjangStudi, @Prodi, @Search", jenjangStudiParam, prodiParam, searchParam).ToList();
                return result;
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

        public IEnumerable<VMLookup> GetMatkulByLokasi(string search, string jenjangStudi, string prodi, string lokasi)
        {
            using (var context = new MBKMContext())
            {
                //int ProdiIDInt = Int32.Parse(prodi);
                var searchParam = new SqlParameter("@Search", search);
                var jenjangStudiParam = new SqlParameter("@JenjangStudi", jenjangStudi);
                var prodiParam = new SqlParameter("@Prodi", prodi);
                var lokasiParam = new SqlParameter("@Lokasi", lokasi);
                var result = context.Database.SqlQuery<VMLookup>("GetMatkulByLokasi @JenjangStudi, @Prodi, @Search, @Lokasi", jenjangStudiParam, prodiParam, searchParam, lokasiParam).ToList();
                return result;
            }
        }

        public IEnumerable<VMLookup> GetProdiByFakultas(string search, string jenjangStudi, string fakultas)
        {
            using (var context = new MBKMContext())
            {
                var searchParam = new SqlParameter("@Search", search);
                var jenjangStudiParam = new SqlParameter("@JenjangStudi", jenjangStudi);
                var fakultasParam = new SqlParameter("@Fakultas", fakultas);
                var result = context.Database.SqlQuery<VMLookup>("GetProdiByNamaFakultas @JenjangStudi, @Fakultas, @Search", jenjangStudiParam, fakultasParam, searchParam).ToList();
                return result;
            }
        }

        public IEnumerable<VMLookup> GetSeksiByMatkul(string search, string jenjangStudi, string matkul, string lokasi)
        {
            throw new NotImplementedException();
        }

        //public IEnumerable<VMLinkFasilitas> GetSeksi(string search)
        //{
        //    throw new NotImplementedException();
        //}


        public VMLinkFasilitas SearchListJadwalKuliah(int Skip, int Length, string SearchParam, string SortBy, bool SortDir, string idProdi, string lokasi, string idFakultas, string jenjangStudi, string idMatakuliah, string seksi,int strm)
        {
            VMLinkFasilitas mListCPL = new VMLinkFasilitas();
            if (String.IsNullOrEmpty(SearchParam))
            {
                // if we have an empty search then just order the results by Id ascending
                SortBy = "ID";
                SortDir = true;
                SearchParam = "";
            }
            using (var context = new MBKMContext())
            {
                var Strm = new SqlParameter("@STRM", strm);
                var semesterName = context.Database
                    .SqlQuery<VMSemester>("GetSemesterBySTRM @STRM", Strm).First();
                int ProdiIDInt = Int32.Parse(idProdi);
                int FakultasIDInt = Int32.Parse(idFakultas);
                int idmatkul = idMatakuliah.Length;
                string idmatkulAdd = idMatakuliah;
                for (int i = idmatkul; i < 6; i++)
                {
                    idmatkulAdd=idmatkulAdd.Insert(0, "0");
                }

                var result = context.jadwalKuliahs.Where(
                    x =>
                    x.IsDeleted == false &&
                    x.ProdiID == ProdiIDInt &&
                    x.FakultasID == FakultasIDInt &&
                    x.JenjangStudi == jenjangStudi &&
                    x.Lokasi == lokasi &&
                    x.MataKuliahID == idmatkulAdd &&
                    x.FlagOpen == true &&
                    x.ClassSection == seksi &&
                    x.STRM == strm
                   
                );
                mListCPL.TotalCount = result.Count();
                var gridfilter = result.AsQueryable().Where(y => y.NamaMataKuliah.Contains(SearchParam) || y.KodeMataKuliah.Contains(SearchParam)
               )
                    .Select(z => new GridDataLinkFasilitas
                    {
                        //ID = z.ID,
                        //SKS = z.SKS,
                        //NamaMataKuliah = z.NamaMataKuliah,
                        //KodeMataKuliah = z.KodeMataKuliah,
                        //ClassSection = z.ClassSection,
                        //Hari = z.Hari,
                        //JamMasuk = z.JamMasuk,
                        //RuangKelas = z.RuangKelas 
                        ID = z.ID,
                        DosenID = z.DosenID,
                        NamaDosen = z.NamaDosen,
                        KodeMataKuliah = z.KodeMataKuliah,
                        Hari = z.Hari,
                        FlagOpen = z.FlagOpen,
                        CreatedBy = z.CreatedBy,
                        CreatedDate = z.CreatedDate,
                        UpdatedBy = z.UpdatedBy,
                        UpdatedDate = z.UpdatedDate,
                        IsActive = z.IsActive,
                        IsDeleted = z.IsDeleted,
                        MataKuliahID = z.MataKuliahID,
                        NamaMataKuliah = z.NamaMataKuliah,
                        JamMasuk = z.JamMasuk,
                        JamSelesai = z.JamSelesai,
                        TglAwalKuliah = z.TglAwalKuliah,
                        TglAkhirKuliah = z.TglAkhirKuliah,
                        RuangKelas = z.RuangKelas,
                        Lokasi = z.Lokasi,
                        STRM = z.STRM,
                        semesterName = semesterName.Nama,
                        SKS = z.SKS,
                        ClassSection = z.ClassSection,
                        JenjangStudi = z.JenjangStudi,
                        FakultasID = z.FakultasID,
                        NamaFakultas = z.NamaFakultas,
                        ProdiID = z.ProdiID,
                        NamaProdi = z.NamaProdi,
                        LinkMoodle = z.LinkMoodle,
                        LinkAtmaZeds = z.LinkAtmaZeds,
                        LinkTeams = z.LinkTeams,
                        LinkOthers = z.LinkOthers
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
