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
        public IEnumerable<VMProdi> GetLokasiByProdi(string jenjangStudi, string namaProdi, string search)
        {
            using (var context = new MBKMContext())
            {
                var jenjangStudiParam = new SqlParameter("@JenjangStudi", jenjangStudi);
                var namaProdiParam = new SqlParameter("@Prodi", namaProdi);
                var searchParam = new SqlParameter("@Search", search);
                var result = context.Database
                    .SqlQuery<VMProdi>("GetLokasiByProdi @JenjangStudi, @Prodi, @Search", jenjangStudiParam, namaProdiParam, searchParam).ToList();
                return result;
            }
        }
        public VMKampus GetInformasiKampusByIdProdi(string idProdi)
        {
            using (var context = new MBKMContext())
            {
                var idProdiParam = new SqlParameter("@IDProdi", idProdi);
                var result = context.Database
                    .SqlQuery<VMKampus>("GetInformasiKampusByIdProdi @IDProdi", idProdiParam).FirstOrDefault();
                return result;
            }
        }
        public VMSemester getOngoingSemester(string jenjangStudi)
        {
            using (var context = new MBKMContext())
            {
                var result = new VMSemester();
                if (jenjangStudi == "null" || jenjangStudi == null)
                {
                    result = context.Database
                        .SqlQuery<VMSemester>("GetSemester").FirstOrDefault();
                    return result;
                }

                var jenjangStudiParam = new SqlParameter("@JenjangStudi", jenjangStudi);
                result = context.Database
                    .SqlQuery<VMSemester>("GetSemester @JenjangStudi", jenjangStudiParam).FirstOrDefault();
                return result;
            }
        }        
        public VMListPendaftaranMataKuliah GetPendaftaranList(int Skip, int Length, string SearchParam, string SortBy, bool SortDir, int strm)
        {
            VMListPendaftaranMataKuliah mListPendaftaranMataKuliah = new VMListPendaftaranMataKuliah();
            if (String.IsNullOrEmpty(SearchParam))
            {
                SearchParam = "";
            }
            using (var context = new MBKMContext())
            {
                context.Configuration.LazyLoadingEnabled = false;
                var result = context.PendaftaranMataKuliahs.Where(x => x.IsDeleted == false && x.StatusPendaftaran == "MENUNGGU APPROVAL KAPRODI/WR BIDANG AKADEMIK" && x.JadwalKuliahs.STRM == strm).Include(x => x.mahasiswas).Include(x => x.JadwalKuliahs);
                mListPendaftaranMataKuliah.TotalCount = result.Count();
                var gridfilter = result
                    .AsQueryable()
                    .Where(y => y.Nilai.Contains(SearchParam) 
                    || y.mahasiswas.NamaUniversitas.Contains(SearchParam) 
                    || y.mahasiswas.ProdiAsal.Contains(SearchParam)
                    || y.mahasiswas.NIMAsal.Contains(SearchParam)
                    || y.mahasiswas.Nama.Contains(SearchParam)
                    || y.mahasiswas.NoKerjasama.Contains(SearchParam)
                    || y.MatkulKodeAsal.Contains(SearchParam)
                    || y.MatkulAsal.Contains(SearchParam)
                    || y.JadwalKuliahs.NamaProdi.Contains(SearchParam)
                    || y.JadwalKuliahs.KodeMataKuliah.Contains(SearchParam)
                    || y.JadwalKuliahs.NamaMataKuliah.Contains(SearchParam)
                    ).OrderBy(SortBy, SortDir)
                    ;
                mListPendaftaranMataKuliah.gridDatas = gridfilter.Skip(Skip).Take(Length)
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
                        StatusPendaftaran = z.StatusPendaftaran,
                    }).ToList();
                mListPendaftaranMataKuliah.TotalFilterCount = gridfilter.Count();
                return mListPendaftaranMataKuliah;
            }
        }
        public VMListPendaftaranMataKuliah GetPendaftaranListFromMahasiswa(int Skip, int Length, string SearchParam, string SortBy, bool SortDir, string emailMahasiswa)
        {
            VMListPendaftaranMataKuliah mListPendaftaranMataKuliah = new VMListPendaftaranMataKuliah();
            if (String.IsNullOrEmpty(SearchParam))
            {
                SearchParam = "";
            }
            using (var context = new MBKMContext())
            {
                context.Configuration.LazyLoadingEnabled = false;
                var result = context.PendaftaranMataKuliahs.Where(x => x.IsDeleted == false && x.mahasiswas.Email == emailMahasiswa /*&& x.StatusPendaftaran != "MENUNGGU APPROVAL KAPRODI/WR BIDANG AKADEMIK"*/).Include(x => x.mahasiswas).Include(x => x.JadwalKuliahs);
                mListPendaftaranMataKuliah.TotalCount = result.Count();
                var gridfilter = result
                    .AsQueryable()
                    .Where(y => y.Nilai.Contains(SearchParam)
                    || y.mahasiswas.NamaUniversitas.Contains(SearchParam)
                    || y.mahasiswas.ProdiAsal.Contains(SearchParam)
                    || y.mahasiswas.NIMAsal.Contains(SearchParam)
                    || y.mahasiswas.Nama.Contains(SearchParam)
                    || y.mahasiswas.NoKerjasama.Contains(SearchParam)
                    || y.MatkulKodeAsal.Contains(SearchParam)
                    || y.MatkulAsal.Contains(SearchParam)
                    || y.JadwalKuliahs.NamaProdi.Contains(SearchParam)
                    || y.JadwalKuliahs.KodeMataKuliah.Contains(SearchParam)
                    || y.JadwalKuliahs.NamaMataKuliah.Contains(SearchParam)
                    ).OrderBy(SortBy, SortDir)
                    ;
                mListPendaftaranMataKuliah.gridDatas = gridfilter.Skip(Skip).Take(Length)
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
                        StatusPendaftaran = z.StatusPendaftaran,
                    }).ToList();
                mListPendaftaranMataKuliah.TotalFilterCount = gridfilter.Count();
                return mListPendaftaranMataKuliah;
            }
        }


        public IEnumerable<VMPendaftaranWithInformasipertukaran> GetListPendaftaranAndInformasiPertukaran(long strm)
        {
            using (var context = new MBKMContext())
            {
                context.Configuration.LazyLoadingEnabled = false;
                var result = context.PendaftaranMataKuliahs.Where(x => x.JadwalKuliahs.STRM == strm && x.StatusPendaftaran.ToLower().Contains("accepted"))
                    .Join(context.informasiPertukarans, 
                        pendaftaran => pendaftaran.MahasiswaID, 
                        informasi => informasi.MahasiswaID, 
                        (pendaftaran,informasi) => new VMPendaftaranWithInformasipertukaran
                        {
                            JadwalKuliahID = pendaftaran.JadwalKuliahID,
                            JadwalKuliahs = pendaftaran.JadwalKuliahs,
                            mahasiswas = pendaftaran.mahasiswas,
                            InformasiPertukaran = informasi
                        }
                        ).ToList();
                
                return result;
            }
        }

        public IEnumerable<VMReportMahasiswaInternal> GetListPendaftaranNonPertukaran(long strm)
        {
            using (var context = new MBKMContext())
            {
                context.Configuration.LazyLoadingEnabled = false;
                var result = context.PendaftaranMataKuliahs.Where(x => x.JadwalKuliahs.STRM == strm && x.StatusPendaftaran.ToLower().Contains("accepted"))
                    .Join(context.informasiPertukarans,
                        pendaftaran => pendaftaran.MahasiswaID,
                        informasi => informasi.MahasiswaID,
                        (pendaftaran, informasi) => new VMPendaftaranWithInformasipertukaran
                        {
                            JadwalKuliahID = pendaftaran.JadwalKuliahID,
                            JadwalKuliahs = pendaftaran.JadwalKuliahs,
                            mahasiswas = pendaftaran.mahasiswas,
                            InformasiPertukaran = informasi
                        }
                        )
                    .Join(context.NilaiKuliahs,
                        pendaf => pendaf.mahasiswas.ID,
                        nilai => nilai.MahasiswaID,
                        (pendaf, nilai) => new VMReportMahasiswaInternal
                        {
                            JadwalKuliahID = pendaf.JadwalKuliahID,
                            JadwalKuliahs = pendaf.JadwalKuliahs,
                            mahasiswas = pendaf.mahasiswas,
                            InformasiPertukaran = pendaf.InformasiPertukaran,
                            NilaiKuliah = nilai
                        })

                    .Where(z => z.InformasiPertukaran.JenisPertukaran.ToLower().Contains("non") && z.JadwalKuliahs.ID == z.NilaiKuliah.JadwalKuliahID).ToList();

                return result;
            }
        }

        public IEnumerable<VMReportMahasiswaInternal> GetListPendaftaranInternalPertukaran(long strm)
        {
            using (var context = new MBKMContext())
            {
                context.Configuration.LazyLoadingEnabled = false;
                var result = context.PendaftaranMataKuliahs.Where(x => x.JadwalKuliahs.STRM == strm && x.StatusPendaftaran.ToLower().Contains("accepted"))
                    .Join(context.informasiPertukarans,
                        pendaftaran => pendaftaran.MahasiswaID,
                        informasi => informasi.MahasiswaID,
                        (pendaftaran, informasi) => new VMPendaftaranWithInformasipertukaran
                        {
                            MatkulKodeAsal = pendaftaran.MatkulKodeAsal,
                            MatkulAsal = pendaftaran.MatkulAsal,
                            MatkulIDAsal = pendaftaran.MatkulIDAsal,
                            JadwalKuliahID = pendaftaran.JadwalKuliahID,
                            JadwalKuliahs = pendaftaran.JadwalKuliahs,
                            mahasiswas = pendaftaran.mahasiswas,
                            InformasiPertukaran = informasi
                        }
                        )
                    .Join(context.NilaiKuliahs,
                        pendaf => pendaf.mahasiswas.ID,
                        nilai => nilai.MahasiswaID,
                        (pendaf, nilai) => new VMReportMahasiswaInternal
                        {
                            MatkulKodeAsal = pendaf.MatkulKodeAsal,
                            MatkulAsal = pendaf.MatkulAsal,
                            MatkulIDAsal = pendaf.MatkulIDAsal,
                            JadwalKuliahID = pendaf.JadwalKuliahID,
                            JadwalKuliahs = pendaf.JadwalKuliahs,
                            mahasiswas = pendaf.mahasiswas,
                            InformasiPertukaran = pendaf.InformasiPertukaran,
                            NilaiKuliah = nilai
                        })
                    .Where(z => !z.InformasiPertukaran.JenisPertukaran.ToLower().Contains("non") && z.InformasiPertukaran.JenisKerjasama.ToLower() == "internal" && z.JadwalKuliahs.ID == z.NilaiKuliah.JadwalKuliahID).ToList();

                return result;
            }
        }
        public IEnumerable<VMReportMahasiswaInternalKeluar> GetListPendaftaranInternalPertukaranKeluar(long strm)
        {
            using (var context = new MBKMContext())
            {
                context.Configuration.LazyLoadingEnabled = false;
                var result = context.PendaftaranMataKuliahs.Where(x => x.JadwalKuliahs.STRM == strm && x.StatusPendaftaran.ToLower().Contains("accepted"))
                    .Join(context.informasiPertukarans,
                        pendaftaran => pendaftaran.MahasiswaID,
                        informasi => informasi.MahasiswaID,
                        (pendaftaran, informasi) => new VMPendaftaranWithInformasipertukaranKeluar
                        {
                            MatkulKodeAsal = pendaftaran.MatkulKodeAsal,
                            MatkulAsal = pendaftaran.MatkulAsal,
                            MatkulIDAsal = pendaftaran.MatkulIDAsal,
                            JadwalKuliahID = pendaftaran.JadwalKuliahID,
                            JadwalKuliahs = pendaftaran.JadwalKuliahs,
                            mahasiswas = pendaftaran.mahasiswas,
                            InformasiPertukaran = informasi,
                            LokasiTugas = informasi.LokasiTugas
                            
                        }
                        )
                    .Where(z => !z.InformasiPertukaran.JenisPertukaran.ToLower().Contains("non") 
                        && z.InformasiPertukaran.JenisKerjasama.ToLower().Contains("internal") 
                        && z.InformasiPertukaran.JenisKerjasama.ToLower().Contains("luar"))
                    .Join(context.PerjanjianKerjasamas,
                        awal => awal.InformasiPertukaran.NoKerjasama,
                        akhir => akhir.NoPerjanjian,
                        (awal, akhir) => new VMReportMahasiswaInternalKeluar
                        {
                            MatkulKodeAsal = awal.MatkulKodeAsal,
                            MatkulAsal = awal.MatkulAsal,
                            MatkulIDAsal = awal.MatkulIDAsal,
                            JadwalKuliahID = awal.JadwalKuliahID,
                            JadwalKuliahs = awal.JadwalKuliahs,
                            mahasiswas = awal.mahasiswas,
                            InformasiPertukaran = awal.InformasiPertukaran,
                            PerjanjianKerjasama = akhir
                        })
                    .ToList();

                return result;
            }
        }


        public IEnumerable<VMReportMahasiswaEksternal> GetListPendaftaranEksternalPertukaran(long strm)
        {
            using (var context = new MBKMContext())
            {
                context.Configuration.LazyLoadingEnabled = false;
                var result = context.PendaftaranMataKuliahs.Where(x => x.JadwalKuliahs.STRM == strm && x.StatusPendaftaran.ToLower().Contains("accepted"))
                    .Join(context.informasiPertukarans,
                        pendaftaran => pendaftaran.MahasiswaID,
                        informasi => informasi.MahasiswaID,
                        (pendaftaran, informasi) => new VMPendaftaranWithInformasipertukaran
                        {
                            MatkulKodeAsal = pendaftaran.MatkulKodeAsal,
                            MatkulAsal = pendaftaran.MatkulAsal,
                            MatkulIDAsal = pendaftaran.MatkulIDAsal,
                            JadwalKuliahID = pendaftaran.JadwalKuliahID,
                            JadwalKuliahs = pendaftaran.JadwalKuliahs,
                            mahasiswas = pendaftaran.mahasiswas,
                            InformasiPertukaran = informasi
                        }
                        )
                    .Join(context.NilaiKuliahs,
                        pendaf => pendaf.mahasiswas.ID,
                        nilai => nilai.MahasiswaID,
                        (pendaf, nilai) => new VMReportMahasiswaEksternal
                        {
                            MatkulKodeAsal = pendaf.MatkulKodeAsal,
                            MatkulAsal = pendaf.MatkulAsal,
                            MatkulIDAsal = pendaf.MatkulIDAsal,
                            JadwalKuliahID = pendaf.JadwalKuliahID,
                            JadwalKuliahs = pendaf.JadwalKuliahs,
                            mahasiswas = pendaf.mahasiswas,
                            InformasiPertukaran = pendaf.InformasiPertukaran,
                            NilaiKuliah = nilai
                        })
                    .Where(z => z.mahasiswas.NIM != z.mahasiswas.NIMAsal  && z.JadwalKuliahs.ID == z.NilaiKuliah.JadwalKuliahID).ToList();

                return result;
            }
        }
    }
}
