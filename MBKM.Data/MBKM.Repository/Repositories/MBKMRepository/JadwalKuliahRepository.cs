using MBKM.Common.Interfaces.RepoInterfaces.MBKMRepoInterfaces;
using MBKM.Entities.Models.MBKM;
using MBKM.Entities.ViewModel;
using MBKM.Repository.BaseRepository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
