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
    public class AbsensiRepository : GenericRepository<Absensi>, IAbsensiRepository
    {
        public AbsensiRepository(DbContext _db) : base(_db)
        {
        }
        public string GetSemesterBySTRM(int strm)
        {
            using (var context = new MBKMContext())
            {
                var strmParam = new SqlParameter("@STRM", strm);
                var result = context.Database
                    .SqlQuery<string>("GetSemesterBySTRM @STRM", strmParam).FirstOrDefault();
                return result;
            }
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
        public IEnumerable<VMLookup> GetLokasiByFakultas(string search, string jenjangStudi, string fakultas)
        {
            using (var context = new MBKMContext())
            {
                var searchParam = new SqlParameter("@Search", search);
                var jenjangStudiParam = new SqlParameter("@JenjangStudi", jenjangStudi);
                var fakultasParam = new SqlParameter("@Fakultas", fakultas);
                var result = context.Database
                    .SqlQuery<VMLookup>("GetLokasiByFakultas @JenjangStudi, @Fakultas, @Search", jenjangStudiParam, fakultasParam, searchParam).ToList();
                return result;
            }
        }
        public IEnumerable<VMLookup> GetProdiByLokasi(string search, string jenjangStudi, string lokasi)
        {
            using (var context = new MBKMContext())
            {
                var searchParam = new SqlParameter("@Search", search);
                var jenjangStudiParam = new SqlParameter("@JenjangStudi", jenjangStudi);
                var lokasiParam = new SqlParameter("@Lokasi", lokasi);
                var result = context.Database
                    .SqlQuery<VMLookup>("GetProdiByLokasi @JenjangStudi, @Lokasi, @Search", jenjangStudiParam, lokasiParam, searchParam).ToList();
                return result;
            }
        }
        public IEnumerable<VMLookup> GetMatkulByProdi(string search, string jenjangStudi, string prodi)
        {
            using (var context = new MBKMContext())
            {
                var searchParam = new SqlParameter("@Search", search);
                var jenjangStudiParam = new SqlParameter("@JenjangStudi", jenjangStudi);
                var prodiParam = new SqlParameter("@Prodi", prodi);
                var result = context.Database
                    .SqlQuery<VMLookup>("GetMatkulByProdi @JenjangStudi, @Prodi, @Search", jenjangStudiParam, prodiParam, searchParam).ToList();
                return result;
            }
        }
        public IEnumerable<VMLookup> GetSeksiByMatkul(string search, string jenjangStudi, string matkul)
        {
            using (var context = new MBKMContext())
            {
                var searchParam = new SqlParameter("@Search", search);
                var jenjangStudiParam = new SqlParameter("@JenjangStudi", jenjangStudi);
                var matkulParam = new SqlParameter("@Matkul", matkul);
                var result = context.Database
                    .SqlQuery<VMLookup>("GetSeksiByMatkul @JenjangStudi, @Matkul, @Search", jenjangStudiParam, matkulParam, searchParam).ToList();
                return result;
            }
        }
        public IEnumerable<VMPresensi> GetPresensi(int strm, string jenjangStudi, string fakultas, string lokasi, string prodi, string matkul, string seksi)
        {
            using (var context = new MBKMContext())
            {
                var strmParam = new SqlParameter("@STRM", strm);
                var jenjangStudiParam = new SqlParameter("@JenjangStudi", jenjangStudi);
                var fakultasParam = new SqlParameter("@Fakultas", fakultas);
                var lokasiParam = new SqlParameter("@Lokasi", lokasi);
                var prodiParam = new SqlParameter("@Prodi", prodi);
                var matkulParam = new SqlParameter("@Matkul", matkul);
                var seksiParam = new SqlParameter("@Seksi", seksi);
                var result = context.Database
                    .SqlQuery<VMPresensi>("GetPresensi @STRM, @JenjangStudi, @Fakultas, @Lokasi, @Prodi, @Matkul, @Seksi", strmParam, jenjangStudiParam, fakultasParam, lokasiParam, prodiParam, matkulParam, seksiParam).ToList();
                return result;
            }
        }
    }
}
