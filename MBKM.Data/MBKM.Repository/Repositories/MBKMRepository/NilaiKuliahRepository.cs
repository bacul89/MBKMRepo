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
    public class NilaiKuliahRepository : GenericRepository<NilaiKuliah>, INilaiKuliahRepository
    {
        public NilaiKuliahRepository(DbContext _db) : base(_db)
        {
        }
        public VMDNR GetDNR(int idJadwalKuliah)
        {
            using (var context = new MBKMContext())
            {
                var idJadwalKuliahParam = new SqlParameter("@IdJadwalKuliah", idJadwalKuliah);
                var result = context.Database
                    .SqlQuery<VMDNR>("GetDNR @IdJadwalKuliah", idJadwalKuliahParam).FirstOrDefault();
                var idJadwalKuliahParam2 = new SqlParameter("@IdJadwalKuliah", idJadwalKuliah);
                result.mahasiswas = context.Database
                    .SqlQuery<VMMahasiswa>("GetMahasiswaDNR @IdJadwalKuliah", idJadwalKuliahParam2).ToList();
                return result;
            }
        }
        public VMBobot GetBobot(string idMatkul)
        {
            using (var context = new MBKMContext())
            {
                var courseParam = new SqlParameter("@CourseID", idMatkul);
                var result = context.Database
                    .SqlQuery<VMBobot>("GetBobotByCourseID @CourseID", courseParam).FirstOrDefault();
                return result;
            }
        }
        public IEnumerable<VMSubBobot> GetSubBobot(string idMatkul)
        {
            using (var context = new MBKMContext())
            {
                var courseParam = new SqlParameter("@CourseID", idMatkul);
                var result = context.Database
                    .SqlQuery<VMSubBobot>("GetSubBobotByCourseID @CourseID", courseParam).ToList();
                return result;
            }
        }
        public IEnumerable<VMMataKuliah> GetMatkulEn(string kodeMataKuliah, int mataKuilahID, int sTRM)
        {
            using (var context = new MBKMContext())
            {

                var strm = new SqlParameter("@STRM", sTRM);
                var kode = new SqlParameter("@KodeMatkul", kodeMataKuliah);
                var id = new SqlParameter("@IdMatkul", mataKuilahID);
                var result = context.Database
                    .SqlQuery<VMMataKuliah>("GetNamaMatkulEng @STRM, @KodeMatkul, @IdMatkul", strm, kode, id).ToList();
                return result;
            }
        }

        public VMListNilaiKuliah GetNilaiMahasiswa()
        {
            //throw new NotImplementedException();
            VMListNilaiKuliah mListNilai = new VMListNilaiKuliah();
            using (var context = new MBKMContext())
            {
                //var result = context.NilaiKuliahs.Where(x => x.IsActive && !x.IsDeleted).OrderBy(x => x.MahasiswaID).Take(1);
                /*.Select(x => new NilaiKuliah
                {
                    MahasiswaID = x.Mahasiswas.ID,
                    FlagCetak = x.FlagCetak,
                    Mahasiswas   = x.Mahasiswas,
                    JadwalKuliahs = x.JadwalKuliahs
                    Nama = x.Mahasiswas.Nama,
                    NamaUniversitas = x.Mahasiswas.NamaUniversitas,
                    JenjangStudi = x.Mahasiswas.JenjangStudi,
                    NIM = x.Mahasiswas.NIM,
                    KodeMataKuliah = x.JadwalKuliahs.KodeMataKuliah,
                    NamaMataKuliah = x.JadwalKuliahs.NamaMataKuliah,
                    SKS = x.JadwalKuliahs.SKS

                })
                var items = context.PersonSet.OrderByDescending(u => u.OnlineAccounts.Count).Take(5);
                .GroupBy(x => x.MahasiswaID).Select(y => y.FirstOrDefault()).OrderBy(x => x.MahasiswaID);*/

                /*                var result = from e1 in
                                    (from e1 in context.NilaiKuliahs
                                     group e1 by e1.MahasiswaID into grp
                                     select grp.First())
                                            join e2 in context.Mahasiswas on e1.MahasiswaID equals e2.ID
                                            select new GridDataNilaiKuliah
                                            {
                                                MahasiswaID = e1.MahasiswaID,
                                                Nama = e2.Nama,
                                                JenjangStudi = e2.JenjangStudi,
                                                NIM = e2.NIM,
                                                NamaUniversitas = e2.NamaUniversitas,
                                                //JobTitle = e2.JobTitle,
                                                FlagCetak = e1.FlagCetak
                                            }.toList();*/

                var result = context.NilaiKuliahs.Where(x => x.IsActive && !x.IsDeleted && x.Mahasiswas.NIM != x.Mahasiswas.NIMAsal);
                var gridfilter = result.AsQueryable()
                    .Select(z => new GridDataNilaiKuliah
                    {
                        MahasiswaID = z.MahasiswaID,
                        Nama = z.Mahasiswas.Nama,
                        JenjangStudi = z.Mahasiswas.JenjangStudi,
                        NIM = z.Mahasiswas.NIM,
                        NamaUniversitas = z.Mahasiswas.NamaUniversitas,
                        NoKerjasama = z.Mahasiswas.NoKerjasama,
                        //JobTitle = e2.JobTitle,
                        FlagCetak = z.FlagCetak
                    }).Distinct();
                
                mListNilai.gridDatas = gridfilter.ToList();
                return mListNilai;
            }

        }


        public VMNilaiDiakui GetNilaiDiakui(string Jenjang, string Strm, string MatkulId, string KodeMatkul, string Nim, string classSection)
        {
            using (var context = new MBKMContext())
            {

                var jenjang = new SqlParameter("@JenjangStudi", Jenjang);
                var strm = new SqlParameter("@STRM", Strm);
                var matkulId = new SqlParameter("@MatkulID", MatkulId);
                var kodeMatkul = new SqlParameter("@KodeMatkul", KodeMatkul);
                var nim = new SqlParameter("@NIM", Nim);
                var ClassSection = new SqlParameter("@ClassSection", classSection);
                var result = context.Database
                    .SqlQuery<VMNilaiDiakui>("GetNilaiDiakui @JenjangStudi, @STRM, @MatkulID, @KodeMatkul, @NIM, @ClassSection", jenjang, strm, matkulId, kodeMatkul, nim, ClassSection).FirstOrDefault();
                return result;
            }
        }


        public VMNilaiBobot GetBobotNilai(decimal Nilai)
        {
            using (var context = new MBKMContext())
            {
                //.ToInt32(value);
                //int nilaiInt = Int32.Parse(nilaiTotal);
                var courseParam = new SqlParameter("@Nilai", Nilai);
                var result = context.Database
                    .SqlQuery<VMNilaiBobot>("GetGradeByNilai @Nilai", courseParam).First();
                return result;
            }
        }
        public VMNilaiGrade GetNilaiGradeByNilaiTotal(int nilaiTotal)
        {
            using (var context = new MBKMContext())
            {
                var nilai = new SqlParameter("@Nilai", nilaiTotal);
                var result = context.Database
                    .SqlQuery<VMNilaiGrade>("GetGradeByNilai @Nilai", nilai).First();
                return result;
            }

        }



    }


}
