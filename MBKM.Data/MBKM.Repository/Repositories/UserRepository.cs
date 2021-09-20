using MBKM.Common.Interfaces.RepoInterfaces;
using MBKM.Entities.Models;
using MBKM.Entities.ViewModel;
using MBKM.Repository.BaseRepository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Dynamic;

namespace MBKM.Repository.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(DbContext _db) : base(_db)
        {
        }

        public VMListUser getListUserGrid(int Skip, int Length, string SearchParam, string SortBy, bool SortDir)
        {
            VMListUser mListUser = new VMListUser();
            if (String.IsNullOrEmpty(SearchParam))
            {
                // if we have an empty search then just order the results by Id ascending
                SortBy = "ID";
                SortDir = true;
                SearchParam = "";
            }
            using (var context = new MBKMContext())
            {

                var result = context.Users.Where(x => x.IsDeleted == false);
                mListUser.TotalCount = result.Count();
                var gridfilter = result.AsQueryable().Where(y => y.UserName.Contains(SearchParam) || y.NoPegawai.Contains(SearchParam) || 
                y.Password.Contains(SearchParam) || y.Email.Contains(SearchParam) || y.Roles.RoleName.Contains(SearchParam) || y.NamaProdi.Contains(SearchParam))
                    .Select(z => new GridData
                    {
                        ID = z.ID,
                        Email = z.Email,
                        Nama = z.UserName,
                        Password = z.Password,
                        //KodeProdi = Convert.ToDouble(z.KodeProdi),
                        KodeProdi = z.KodeProdi,
                        NamaProdi = z.NamaProdi,
                        RoleID = z.RoleID,
                        RoleName = z.Roles.RoleName,
                        NoPegawai = z.NoPegawai,
                        Status = z.IsActive
                    }).OrderBy(SortBy, SortDir);
                mListUser.gridDatas = gridfilter.Skip(Skip).Take(Length).ToList();
                mListUser.TotalFilterCount = gridfilter.Count();
                return mListUser;
            }
        }
    }
}
