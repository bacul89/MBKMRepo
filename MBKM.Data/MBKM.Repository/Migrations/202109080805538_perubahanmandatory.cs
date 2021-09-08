namespace MBKM.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class perubahanmandatory : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Mahasiswa", "NoHp", c => c.String(nullable: false, maxLength: 8000, unicode: false));
            AddColumn("dbo.Mahasiswa", "NIM", c => c.String(maxLength: 50, unicode: false));
            AddColumn("dbo.Mahasiswa", "TempatLahir", c => c.String(nullable: false, maxLength: 150, unicode: false));
            AlterColumn("dbo.Mahasiswa", "NamaUniversitas", c => c.String(maxLength: 150, unicode: false));
            AlterColumn("dbo.Mahasiswa", "Agama", c => c.String(maxLength: 150, unicode: false));
            AlterColumn("dbo.Mahasiswa", "NoKTP", c => c.String(maxLength: 150, unicode: false));
            AlterColumn("dbo.Mahasiswa", "Semester", c => c.String(maxLength: 30, unicode: false));
            AlterColumn("dbo.Mahasiswa", "ProdiAsal", c => c.String(maxLength: 150, unicode: false));
            AlterColumn("dbo.Mahasiswa", "NIMAsal", c => c.String(maxLength: 50, unicode: false));
            DropColumn("dbo.Mahasiswa", "UniversitasID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Mahasiswa", "UniversitasID", c => c.Long(nullable: false));
            AlterColumn("dbo.Mahasiswa", "NIMAsal", c => c.String(nullable: false, maxLength: 50, unicode: false));
            AlterColumn("dbo.Mahasiswa", "ProdiAsal", c => c.String(nullable: false, maxLength: 150, unicode: false));
            AlterColumn("dbo.Mahasiswa", "Semester", c => c.String(nullable: false, maxLength: 30, unicode: false));
            AlterColumn("dbo.Mahasiswa", "NoKTP", c => c.String(nullable: false, maxLength: 150, unicode: false));
            AlterColumn("dbo.Mahasiswa", "Agama", c => c.String(nullable: false, maxLength: 150, unicode: false));
            AlterColumn("dbo.Mahasiswa", "NamaUniversitas", c => c.String(nullable: false, maxLength: 150, unicode: false));
            DropColumn("dbo.Mahasiswa", "TempatLahir");
            DropColumn("dbo.Mahasiswa", "NIM");
            DropColumn("dbo.Mahasiswa", "NoHp");
        }
    }
}
