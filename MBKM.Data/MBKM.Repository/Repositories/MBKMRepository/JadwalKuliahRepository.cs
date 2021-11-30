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
    public class JadwalKuliahRepository : GenericRepository<JadwalKuliah>, IJadwalKuliahRepository
    {
        public JadwalKuliahRepository(DbContext _db) : base(_db)
        {
        }


        
        public IEnumerable<VMSemester> GetSemesterAll(int skip, int take, string search)
        {
            using (var context = new MBKMContext())
            {
                if (String.IsNullOrEmpty(search))
                {
                    var result = context.Database
                        .SqlQuery<VMSemester>("GetSemesterALL").Skip(skip).Take(take)
                        .Select(z => new VMSemester
                        {
                            ID = z.Nilai,
                            Nama = z.Nama
                        })
                        .ToList();
                    return result;
                }
                else
                {
                    var result = context.Database
                        .SqlQuery<VMSemester>("GetSemesterALL").Skip(skip).Take(take).Where(x => x.Nama.Contains(search))
                        .Select(z => new VMSemester
                        {
                            ID = z.Nilai,
                            Nama = z.Nama
                        })
                        .ToList();
                    return result;
                }
            }
        }

        public VMListJadwalKuliah SearchListJadwalKuliah(int skip, int take, string searchBy, string sortBy, bool sortDir, string idProdi, string lokasi, string idFakultas, string jenjangStudi, string strm)
        {
            VMListJadwalKuliah mListJadwalKuliah = new VMListJadwalKuliah();
            if (String.IsNullOrEmpty(searchBy))
            {
                // if we have an empty search then just order the results by Id ascending
                sortBy = "ID";
                sortDir = true;
                searchBy = "";
            }
            using (var context = new MBKMContext())
            {

                int ProdiIDInt = Int32.Parse(idProdi);
                int FakultasIDInt = Int32.Parse(idFakultas);
                //int IDMataKUliahInt = Int32.Parse(idMatakuliah);
                int strmInt = Int32.Parse(strm);

                var result = context.jadwalKuliahs.Where(
                    x =>
                    x.IsDeleted == false &&
                    x.ProdiID == ProdiIDInt &&
                    x.FakultasID == FakultasIDInt &&
                    x.JenjangStudi == jenjangStudi &&
                    x.Lokasi == lokasi &&
                    x.STRM == strmInt &&
                    x.FlagOpen == true
                );
                mListJadwalKuliah.TotalCount = result.Count();
                var gridfilter = result.AsQueryable().Where(
                    y => y.NamaMataKuliah.Contains(searchBy)
                    /*|| 
                    y.KodeMataKuliah.Contains(searchBy) ||
                    y.Lokasi.Contains(searchBy) ||
                    y.JenjangStudi.Contains(searchBy) ||
                    y.NamaFakultas.Contains(searchBy) ||
                    y.NamaProdi.Contains(searchBy) ||
                    y.NamaDosen.Contains(searchBy) */                   
                    )
                    .Select(z => new GridDataJadwalKuliah
                    {
                        /*ID = z.ID,
                        IDMataKUliah = z.IDMataKUliah,
                        NamaMataKuliah = z.NamaMataKuliah,
                        KodeMataKuliah = z.KodeMataKuliah,
                        Capaian = z.MasterCapaianPembelajarans.Capaian,
                        Kelompok = z.MasterCapaianPembelajarans.Kelompok,
                        Kode = z.MasterCapaianPembelajarans.Kode*/

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
                    }).OrderBy(sortBy, sortDir);
                mListJadwalKuliah.gridDatas = gridfilter.Skip(skip).Take(take).ToList();
                mListJadwalKuliah.TotalFilterCount = gridfilter.Count();
                return mListJadwalKuliah;
            }
        }

        public VMListJadwalKuliah SearchListMataKuliah(int skip, int take, string searchBy, string sortBy, bool sortDir, string idProdi, string lokasi, string idFakultas, string jenjangStudi, string strm)
        {
            VMListJadwalKuliah mListJadwalKuliah = new VMListJadwalKuliah();
            if (String.IsNullOrEmpty(searchBy))
            {
                // if we have an empty search then just order the results by Id ascending
                sortBy = "ID";
                sortDir = true;
                searchBy = "";
            }
            using (var context = new MBKMContext())
            {

                int ProdiIDInt = Int32.Parse(idProdi);
                int FakultasIDInt = Int32.Parse(idFakultas);
                //int IDMataKUliahInt = Int32.Parse(idMatakuliah);
                int strmInt = Int32.Parse(strm);

                var result = context.jadwalKuliahs.Where(
                    x =>
                    x.IsDeleted == false &&
                    x.ProdiID == ProdiIDInt &&
                    x.FakultasID == FakultasIDInt &&
                    x.JenjangStudi == jenjangStudi &&
                    x.Lokasi == lokasi &&
                    x.STRM == strmInt &&
                    x.FlagOpen == false
                );
                mListJadwalKuliah.TotalCount = result.Count();
                var gridfilter = result.AsQueryable().Where(
                    y => y.NamaMataKuliah.Contains(searchBy)
                    /*|| 
                    y.KodeMataKuliah.Contains(searchBy) ||
                    y.Lokasi.Contains(searchBy) ||
                    y.JenjangStudi.Contains(searchBy) ||
                    y.NamaFakultas.Contains(searchBy) ||
                    y.NamaProdi.Contains(searchBy) ||
                    y.NamaDosen.Contains(searchBy) */
                    )
                    .Select(z => new GridDataJadwalKuliah
                    {
                        /*ID = z.ID,
                        IDMataKUliah = z.IDMataKUliah,
                        NamaMataKuliah = z.NamaMataKuliah,
                        KodeMataKuliah = z.KodeMataKuliah,
                        Capaian = z.MasterCapaianPembelajarans.Capaian,
                        Kelompok = z.MasterCapaianPembelajarans.Kelompok,
                        Kode = z.MasterCapaianPembelajarans.Kode*/

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
                    }).OrderBy(sortBy, sortDir);
                mListJadwalKuliah.gridDatas = gridfilter.Skip(skip).Take(take).ToList();
                mListJadwalKuliah.TotalFilterCount = gridfilter.Count();
                return mListJadwalKuliah;
            }
        }

        public IEnumerable<VMSemester> GetSemesterAll2()
        {
            using (var context = new MBKMContext())
            {
                var result = context.Database
                    .SqlQuery<VMSemester>("GetSemesterAll2").ToList();
                return result;
            }
        }


        IEnumerable<JadwalKuliah> IJadwalKuliahRepository.GetMatkulFlag(int skip, int take, string searchBy, string idProdi, string lokasi, string idFakultas, string jenjangStudi, string strm)
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
                int ProdiIDInt = Int32.Parse(idProdi);
                int FakultasIDInt = Int32.Parse(idFakultas);
                //int IDMataKUliahInt = Int32.Parse(idMatakuliah);
                int strmInt = Int32.Parse(strm);

                var result = context.jadwalKuliahs.Where(
                    x =>
                    x.IsDeleted == false &&
                    x.ProdiID == ProdiIDInt &&
                    x.FakultasID == FakultasIDInt &&
                    x.JenjangStudi == jenjangStudi &&
                    x.Lokasi == lokasi &&
                    x.STRM == strmInt &&
                    x.FlagOpen == true &&
                    x.NamaMataKuliah.Contains(searchBy)
                    

                ).OrderBy("ID", true).Skip(skip).Take(take).ToList();
                return result;
            }

        }

    }
}
