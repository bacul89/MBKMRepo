using MBKM.Entities.Map;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MBKM.Repository.BaseRepository
{
    public class MBKMContext : DbContext
    {
        public MBKMContext() : base(getConnectionString())
        {
            Database.SetInitializer<MBKMContext>(new CreateDatabaseIfNotExists<MBKMContext>());
            //Configuration.LazyLoadingEnabled = false;
        }

        private static string getConnectionString()
        {
            string conn = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            return conn;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new MenuMap());
            modelBuilder.Configurations.Add(new RoleMap());
            modelBuilder.Configurations.Add(new MenuRoleMap());
            base.OnModelCreating(modelBuilder);

            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Properties<string>().Configure(c => c.HasColumnType("varchar"));

            //modelBuilder.Entity<BeritaDetail>()
            //    .Map(m => m.Requires("StatusData").HasValue(true))
            //    .Ignore(i => i.StatusData);
        }
    }
}
