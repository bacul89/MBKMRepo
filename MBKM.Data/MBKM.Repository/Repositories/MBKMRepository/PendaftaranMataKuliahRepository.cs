﻿using MBKM.Common.Interfaces.RepoInterfaces.MBKMRepoInterfaces;
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
                    .SqlQuery<VMProdi>("GetProdiByFakultas @IdFakultas, @JenjangStudi, @Search", idFakultasParam, jenjangStudiParam, searchParam).ToList();
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
                    .SqlQuery<VMProdi>("GetLokasiByProdi @IdProdi, @JenjangStudi, @Search", idProdiParam, jenjangStudiParam, searchParam).ToList();
                return result;
            }
        }
    }
}
