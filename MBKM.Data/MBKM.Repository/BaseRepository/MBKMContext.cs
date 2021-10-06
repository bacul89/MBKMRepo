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
        public DbSet<User> Users { get; set; }
        public DbSet<PerjanjianKerjasama> PerjanjianKerjasamas { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<MenuRole> MenuRoles { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<MasterCapaianPembelajaran> MasterCPLS { get; set; }
        public DbSet<JadwalKuliah> jadwalKuliahs { get; set; }
        public DbSet<PendaftaranMataKuliah> PendaftaranMataKuliahs { get; set; }
        public DbSet<CPLMatakuliah> CPLMatakuliah { get; set; }

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
            modelBuilder.Configurations.Add(new MasterCapaianPembelajaranMap());
            modelBuilder.Configurations.Add(new CPLMatakuliahMap());
            modelBuilder.Configurations.Add(new JenisKerjasamaModelMap());
            modelBuilder.Configurations.Add(new PendaftaranMataKuliahMap());
            modelBuilder.Configurations.Add(new InformasiPertukaranMap());
            modelBuilder.Configurations.Add(new ApprovalPendaftaranMap());
            modelBuilder.Configurations.Add(new CPLMKPendaftaranMap());
            base.OnModelCreating(modelBuilder);

            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Properties<string>().Configure(c => c.HasColumnType("varchar"));
        }
    }
}
