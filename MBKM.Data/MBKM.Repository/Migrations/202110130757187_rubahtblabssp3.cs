namespace MBKM.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class rubahtblabssp3 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Absensi", "JadwalKuliahMahasiswaID", "dbo.JadwalKuliahMahasiswa");
            DropIndex("dbo.Absensi", new[] { "JadwalKuliahMahasiswaID" });
            AddColumn("dbo.Absensi", "Present", c => c.Boolean(nullable: false));
            AddColumn("dbo.Absensi", "CheckDosen", c => c.Boolean(nullable: false));
            AddColumn("dbo.Absensi", "LockedAbsen", c => c.Boolean(nullable: false));
            AddColumn("dbo.Absensi", "JadwalKuliahID", c => c.Long(nullable: false));
            AddColumn("dbo.Absensi", "MahasiswaID", c => c.Long(nullable: false));
            CreateIndex("dbo.Absensi", "JadwalKuliahID");
            CreateIndex("dbo.Absensi", "MahasiswaID");
            AddForeignKey("dbo.Absensi", "JadwalKuliahID", "dbo.JadwalKuliah", "ID", cascadeDelete: true);
            AddForeignKey("dbo.Absensi", "MahasiswaID", "dbo.Mahasiswa", "ID", cascadeDelete: true);
            DropColumn("dbo.Absensi", "JadwalKuliahMahasiswaID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Absensi", "JadwalKuliahMahasiswaID", c => c.Long(nullable: false));
            DropForeignKey("dbo.Absensi", "MahasiswaID", "dbo.Mahasiswa");
            DropForeignKey("dbo.Absensi", "JadwalKuliahID", "dbo.JadwalKuliah");
            DropIndex("dbo.Absensi", new[] { "MahasiswaID" });
            DropIndex("dbo.Absensi", new[] { "JadwalKuliahID" });
            DropColumn("dbo.Absensi", "MahasiswaID");
            DropColumn("dbo.Absensi", "JadwalKuliahID");
            DropColumn("dbo.Absensi", "LockedAbsen");
            DropColumn("dbo.Absensi", "CheckDosen");
            DropColumn("dbo.Absensi", "Present");
            CreateIndex("dbo.Absensi", "JadwalKuliahMahasiswaID");
            AddForeignKey("dbo.Absensi", "JadwalKuliahMahasiswaID", "dbo.JadwalKuliahMahasiswa", "ID", cascadeDelete: true);
        }
    }
}
