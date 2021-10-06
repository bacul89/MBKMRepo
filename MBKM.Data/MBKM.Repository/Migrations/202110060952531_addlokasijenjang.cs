namespace MBKM.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addlokasijenjang : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MasterCapaianPembelajaran", "JenjangStudi", c => c.String(nullable: false, maxLength: 10, unicode: false));
            AddColumn("dbo.MasterCapaianPembelajaran", "Lokasi", c => c.String(nullable: false, maxLength: 150, unicode: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.MasterCapaianPembelajaran", "Lokasi");
            DropColumn("dbo.MasterCapaianPembelajaran", "JenjangStudi");
        }
    }
}
