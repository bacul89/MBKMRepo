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
    public class JadwalUjianMBKMDetailRepository : GenericRepository<JadwalUjianMBKMDetail>, IJadwalUjianMBKMDetailRepository
    {
        public JadwalUjianMBKMDetailRepository(DbContext _db) : base(_db)
        {
        }

        public VMDosenMakulPertemuan GetDosen(string seksi, string kodeMataKuliah, string strm, string fakultasId)
        {


            using (var context = new MBKMContext())
            {
                var kodeMkParam = new SqlParameter("@Kodemk", kodeMataKuliah);
                var seksiParam = new SqlParameter("@ClassSection", seksi);
                var strmParam = new SqlParameter("@STRM", strm);
                var idFakultasParam = new SqlParameter("@FakultasID", fakultasId);


                var result = context.Database
                    .SqlQuery<VMDosenMakulPertemuan>("GetDosenMatkulPertemuan @Kodemk, @ClassSection, @STRM, @FakultasID", kodeMkParam, seksiParam, strmParam, idFakultasParam).First();
                //x.FakultasID == idFakultas).Skip(skip).Take(take).ToList();
                return result;
            }
        }



        public List<VMClassSection> GetListSeksi()
        {
            using (var context = new MBKMContext())
            {
                var result = context.jadwalUjians.Where(x => x.IsActive && !x.IsDeleted).Select(x => new VMClassSection
                {


                    Nama = x.ClassSection
                }).GroupBy(x => x.Nama).Select(y => y.FirstOrDefault()).OrderBy(x => x.Nama);
                return result.ToList();
            }
        }

        /*        public VMListJadwalUjian GetDHU(int skip, int take, string searchBy, string sortBy, bool sortDir, string idProdi, string lokasi, string idFakultas, string jenjangStudi, string strm, string idMatakuliah, string seksi)
                {

                    VMListJadwalUjian mListJadwalUjian = new VMListJadwalUjian();
                    if (String.IsNullOrEmpty(searchBy))
                    {
                        // if we have an empty search then just order the results by Id ascending
                        sortBy = "ID";
                        sortDir = true;
                        searchBy = "";
                    }
                    using (var context = new MBKMContext())
                    {


                        var idFakultas2nd = idFakultas.Substring(idFakultas.Length - 2);
                        //var idFakultas2nd = Int64.Parse(idFakultas);
                        //int strmInt = Int32.Parse(strm);

                        context.Configuration.LazyLoadingEnabled = false;
                        var result = context.jadwalUjians.Where(
                            x =>
                            x.IsDeleted == false &&
                            x.ProdiID == idProdi &&
                            x.FakultasID == idFakultas2nd &&
                            x.JenjangStudi == jenjangStudi &&
                            x.Lokasi == lokasi &&
                            x.IDMatkul == idMatakuliah &&
                            x.ClassSection == seksi &&
                            x.STRM == strm
                        //x.FlagOpen == true
                        ).Join(
                            context.PendaftaranMataKuliahs,
                                a => a.IDMatkul,
                                b => b.JadwalKuliahs.MataKuliahID,
                                (a, b) => new VMJadwalUjian
                                {
                                    IDMatkul = a.IDMatkul,
                                    KodeMatkul = a.KodeMatkul,
                                    NamaMatkul = a.NamaMatkul,

                                    JenjangStudi = a.JenjangStudi,
                                    FakultasID = a.FakultasID,
                                    NamaFakultas = a.NamaFakultas,
                                    ProdiID = a.ProdiID,
                                    NamaProdi = a.NamaProdi,
                                    Lokasi = a.Lokasi,
                                    ClassSection = a.ClassSection,
                                    STRM = a.STRM,
                                    SKS = b.JadwalKuliahs.SKS,
                                    JadwalKuliahs = b.JadwalKuliahs,                            
                                    RuangUjian = a.RuangUjian,                            

                                    ID = a.ID
                                }

                        ).Where(z =>

                            z.NamaMatkul == z.JadwalKuliahs.NamaMataKuliah &&
                            z.IDMatkul == z.JadwalKuliahs.MataKuliahID &&
                            z.KodeMatkul == z.JadwalKuliahs.KodeMataKuliah &&
                            z.JenjangStudi == z.JadwalKuliahs.JenjangStudi &&
                            z.NamaFakultas == z.JadwalKuliahs.NamaFakultas &&
                            z.NamaProdi == z.JadwalKuliahs.NamaProdi &&
                            z.Lokasi == z.JadwalKuliahs.Lokasi &&
                            z.ClassSection == z.JadwalKuliahs.ClassSection &&
                            //z.RuangUjian == &&
                            z.STRM == z.JadwalKuliahs.STRM.ToString()

                    ).ToList();


                        *//*               var result = context.jadwalUjians.Where(
                                           x =>
                                           x.IsDeleted == false &&
                                           x.ProdiID == idProdi &&
                                           x.FakultasID == idFakultas2nd &&
                                           x.JenjangStudi == jenjangStudi &&
                                           x.Lokasi == lokasi &&
                                           x.IDMatkul == idMatakuliah &&
                                           x.ClassSection == seksi &&
                                           x.STRM == strm
                                       );*//*

                        mListJadwalUjian.TotalCount = result.Count();
                        var gridfilter = result.AsQueryable().Where(
                            y => y.NamaMatkul.Contains(searchBy)
                            *//*|| 
                            y.KodeMataKuliah.Contains(searchBy) ||
                            y.Lokasi.Contains(searchBy) ||
                            y.JenjangStudi.Contains(searchBy) ||
                            y.NamaFakultas.Contains(searchBy) ||
                            y.NamaProdi.Contains(searchBy) ||
                            y.NamaDosen.Contains(searchBy) *//*
                            )
                            .Select(z => new GridDataJadwalUjian
                            {
                                ID = z.ID,
                                JenjangStudi = z.JenjangStudi,
                                STRM = z.STRM,
                                KodeTipeUjian = z.KodeTipeUjian,
                                TipeUjian = z.TipeUjian,
                                FakultasID = z.FakultasID,
                                NamaFakultas = z.NamaFakultas,
                                Lokasi = z.Lokasi,
                                IDMatkul = z.IDMatkul,
                                KodeMatkul = z.KodeMatkul,
                                NamaMatkul = z.NamaMatkul,
                                ProdiID = z.ProdiID,
                                NamaProdi = z.NamaProdi,
                                TanggalUjian = z.TanggalUjian,
                                JamMulai = z.JamMulai,
                                JamAkhir = z.JamAkhir,
                                KodeRuangUjian = z.KodeRuangUjian,
                                RuangUjian = z.RuangUjian,
                                KapasitasRuangan = z.KapasitasRuangan,
                                Tersedia = z.Tersedia,
                                ClassSection = z.ClassSection,
                                KodeClassSection = z.KodeClassSection,
                                //SKS = z.SKS
                                *//*CreatedBy = z.CreatedBy,
                                CreatedDate = z.CreatedDate,
                                UpdatedBy = z.UpdatedBy,
                                UpdatedDate = z.UpdatedDate,
                                IsActive = z.IsActive,
                                IsDeleted = z.IsDeleted,*//*
                            }).OrderBy(sortBy, sortDir);
                        mListJadwalUjian.gridDatas = gridfilter.Skip(skip).Take(take).ToList();
                        mListJadwalUjian.TotalFilterCount = gridfilter.Count();
                        return mListJadwalUjian;
                    }
                }
        */

        public VMListJadwalUjian GetDHU(int skip, int take, string searchBy, string sortBy, bool sortDir, string idProdi, string lokasi, string idFakultas, string jenjangStudi, string strm, string idMatakuliah, string seksi)
        {
            VMListJadwalUjian mListJadwalUjian = new VMListJadwalUjian();
            if (String.IsNullOrEmpty(searchBy))
            {
                // if we have an empty search then just order the results by Id ascending
                sortBy = "ID";
                sortDir = true;
                searchBy = "";
            }
            using (var context = new MBKMContext())
            {


                var idFakultas2nd = idFakultas.Substring(idFakultas.Length - 2);
                //var idFakultas2nd = Int64.Parse(idFakultas);
                //int strmInt = Int32.Parse(strm);

                var JenjangStudiParam = new SqlParameter("@JenjangStudi", jenjangStudi);
                var strmParam = new SqlParameter("@STRM", strm);
                var idFakultasParam = new SqlParameter("@FakultasID", idFakultas2nd);
                var idProdiParam = new SqlParameter("@ProdiID", idProdi);                
                var idMatkulParam = new SqlParameter("@MatkulID", idMatakuliah);
                var seksiParam = new SqlParameter("@Seksi", seksi);

                //context.Configuration.LazyLoadingEnabled = false;
                var result = context.Database
                    .SqlQuery<VMJadwalUjian>("GetDHU @JenjangStudi, @STRM, @FakultasID, @ProdiID, @MatkulID, @Seksi", JenjangStudiParam, strmParam, idFakultasParam, idProdiParam, idMatkulParam, seksiParam).ToList();




                /* var result = context.jadwalUjians.Where(
                                   x =>
                                   x.IsDeleted == false &&
                                   x.ProdiID == idProdi &&
                                   x.FakultasID == idFakultas2nd &&
                                   x.JenjangStudi == jenjangStudi &&
                                   x.Lokasi == lokasi &&
                                   x.IDMatkul == idMatakuliah &&
                                   x.ClassSection == seksi &&
                                   x.STRM == strm
                    );*/

                mListJadwalUjian.TotalCount = result.Count();
                var gridfilter = result.AsQueryable().Where(
                    y => y.NamaMatkul.Contains(searchBy)
                    /*|| 
                    y.KodeMataKuliah.Contains(searchBy) ||
                    y.Lokasi.Contains(searchBy) ||
                    y.JenjangStudi.Contains(searchBy) ||
                    y.NamaFakultas.Contains(searchBy) ||
                    y.NamaProdi.Contains(searchBy) ||
                    y.NamaDosen.Contains(searchBy) */
                    )
                    .Select(z => new GridDataJadwalUjian
                    {
                        ID = z.ID,
                        JenjangStudi = z.JenjangStudi,
                        STRM = z.STRM,
                        KodeTipeUjian = z.KodeTipeUjian,
                        TipeUjian = z.TipeUjian,
                        FakultasID = z.FakultasID,
                        NamaFakultas = z.NamaFakultas,
                        Lokasi = z.Lokasi,
                        IDMatkul = z.IDMatkul,
                        KodeMatkul = z.KodeMatkul,
                        NamaMatkul = z.NamaMatkul,
                        ProdiID = z.ProdiID,
                        NamaProdi = z.NamaProdi,
                        TanggalUjian = z.TanggalUjian,
                        JamMulai = z.JamMulai,
                        JamAkhir = z.JamAkhir,
                        KodeRuangUjian = z.KodeRuangUjian,
                        RuangUjian = z.RuangUjian,
                        KapasitasRuangan = z.KapasitasRuangan,
                        Tersedia = z.Tersedia,
                        ClassSection = z.ClassSection,
                        SKS = z.SKS
                        /*CreatedBy = z.CreatedBy,
                        CreatedDate = z.CreatedDate,
                        UpdatedBy = z.UpdatedBy,
                        UpdatedDate = z.UpdatedDate,
                        IsActive = z.IsActive,
                        IsDeleted = z.IsDeleted,*/
                    }).OrderBy(sortBy, sortDir);
                mListJadwalUjian.gridDatas = gridfilter.Skip(skip).Take(take).ToList();
                mListJadwalUjian.TotalFilterCount = gridfilter.Count();
                return mListJadwalUjian;
            }
        }


        public List<VMJadwalUjian> GetDHUTest(string idProdi, string lokasi, string idFakultas, string jenjangStudi, string strm, string idMatakuliah, string seksi)
        {
            
            using (var context = new MBKMContext())
            {


                //var idFakultas2nd = idFakultas.Substring(idFakultas.Length - 2);
                //var idFakultas2nd = Int64.Parse(idFakultas);
                //int strmInt = Int32.Parse(strm);

                var JenjangStudiParam = new SqlParameter("@JenjangStudi", jenjangStudi);
                var strmParam = new SqlParameter("@STRM", strm);
                var idFakultasParam = new SqlParameter("@FakultasID", idFakultas);
                var idProdiParam = new SqlParameter("@ProdiID", idProdi);
                var idMatkulParam = new SqlParameter("@MatkulID", idMatakuliah);
                var seksiParam = new SqlParameter("@Seksi", seksi);

                //context.Configuration.LazyLoadingEnabled = false;
                var result = context.Database
                    .SqlQuery<VMJadwalUjian>("GetDHU @JenjangStudi, @STRM, @FakultasID, @ProdiID, @MatkulID, @Seksi", JenjangStudiParam, strmParam, idFakultasParam, idProdiParam, idMatkulParam, seksiParam).ToList();



                return result;

            }
        }

    }
}
