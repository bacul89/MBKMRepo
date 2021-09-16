namespace MBKM.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addkolommhspk : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Mahasiswa", "NoKerjasama", c => c.String(maxLength: 100, unicode: false));
            AddColumn("dbo.Mahasiswa", "BiayaKuliah", c => c.Int(nullable: false));
            AddColumn("dbo.Mahasiswa", "StatusVerifikasi", c => c.String(maxLength: 50, unicode: false));
            AddColumn("dbo.Mahasiswa", "Approval", c => c.String(maxLength: 10, unicode: false));
            AddColumn("dbo.Mahasiswa", "Catatan", c => c.String(maxLength: 8000, unicode: false));
            AddColumn("dbo.Mahasiswa", "StatusKerjasama", c => c.String(maxLength: 50, unicode: false));
            AddColumn("dbo.PerjanjianKerjasama", "BiayaKuliah", c => c.Int(nullable: false));
            AlterColumn("dbo.PerjanjianKerjasama", "NoPerjanjian", c => c.String(nullable: false, maxLength: 100, unicode: false));
            DropColumn("dbo.Mahasiswa", "isVerifikasi");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Mahasiswa", "isVerifikasi", c => c.Boolean(nullable: false));
            AlterColumn("dbo.PerjanjianKerjasama", "NoPerjanjian", c => c.String(maxLength: 8000, unicode: false));
            DropColumn("dbo.PerjanjianKerjasama", "BiayaKuliah");
            DropColumn("dbo.Mahasiswa", "StatusKerjasama");
            DropColumn("dbo.Mahasiswa", "Catatan");
            DropColumn("dbo.Mahasiswa", "Approval");
            DropColumn("dbo.Mahasiswa", "StatusVerifikasi");
            DropColumn("dbo.Mahasiswa", "BiayaKuliah");
            DropColumn("dbo.Mahasiswa", "NoKerjasama");
        }
    }
}
