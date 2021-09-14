using MBKM.Entities.Map;
using MBKM.Entities.Map.MBKMMap;
using MBKM.Entities.Models;
using MBKM.Entities.Models.MBKM;
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
        //public DbSet<Menu> Menus { get; set; }
        public DbSet<Lookup> Lookups { get; set; }
        public DbSet<Mahasiswa> Mahasiswas { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new MenuMap());
            modelBuilder.Configurations.Add(new RoleMap());
            //modelBuilder.Configurations.Add(new MenuRoleMap());
            modelBuilder.Configurations.Add(new PerjanjianKerjasamaMap());
            modelBuilder.Configurations.Add(new MahasiswaMap());
            modelBuilder.Configurations.Add(new UserMap());
            modelBuilder.Configurations.Add(new JadwalKuliahMap());
            modelBuilder.Configurations.Add(new KRSMap());
            modelBuilder.Configurations.Add(new JadwalKuliahMahasiswaMap());
            modelBuilder.Configurations.Add(new NilaiMap());
            modelBuilder.Configurations.Add(new AbsensiMap());
            modelBuilder.Configurations.Add(new AttachmentMap());
            modelBuilder.Configurations.Add(new AttachmentPerjanjianKerjasamaMap());
            modelBuilder.Configurations.Add(new EmailTemplateMap());
            modelBuilder.Configurations.Add(new LookupMap());
            base.OnModelCreating(modelBuilder);

            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Properties<string>().Configure(c => c.HasColumnType("varchar"));
        }
    }
}
