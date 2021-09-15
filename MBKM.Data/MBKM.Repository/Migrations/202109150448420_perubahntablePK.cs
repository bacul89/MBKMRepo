namespace MBKM.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class perubahntablePK : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PerjanjianKerjasama", "NamaInstansi", c => c.String(nullable: false, maxLength: 150, unicode: false));
            AddColumn("dbo.PerjanjianKerjasama", "Instansi", c => c.String(nullable: false, maxLength: 50, unicode: false));
            AddColumn("dbo.PerjanjianKerjasama", "NamaUnit", c => c.String(nullable: false, maxLength: 150, unicode: false));
            AddColumn("dbo.PerjanjianKerjasama", "JenisPertukaran", c => c.String(nullable: false, maxLength: 100, unicode: false));
            AddColumn("dbo.PerjanjianKerjasama", "JenisKerjasama", c => c.String(nullable: false, maxLength: 100, unicode: false));
            DropColumn("dbo.PerjanjianKerjasama", "NamaUniversitas");
            DropColumn("dbo.PerjanjianKerjasama", "UniversitasID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PerjanjianKerjasama", "UniversitasID", c => c.Long(nullable: false));
            AddColumn("dbo.PerjanjianKerjasama", "NamaUniversitas", c => c.String(nullable: false, maxLength: 150, unicode: false));
            DropColumn("dbo.PerjanjianKerjasama", "JenisKerjasama");
            DropColumn("dbo.PerjanjianKerjasama", "JenisPertukaran");
            DropColumn("dbo.PerjanjianKerjasama", "NamaUnit");
            DropColumn("dbo.PerjanjianKerjasama", "Instansi");
            DropColumn("dbo.PerjanjianKerjasama", "NamaInstansi");
        }
    }
}
