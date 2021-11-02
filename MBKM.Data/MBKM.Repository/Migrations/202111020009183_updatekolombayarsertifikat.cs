namespace MBKM.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatekolombayarsertifikat : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PendaftaranMataKuliah", "FlagSertifikat", c => c.Boolean(nullable: false));
            AddColumn("dbo.Mahasiswa", "FlagBayar", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Mahasiswa", "FlagBayar");
            DropColumn("dbo.PendaftaranMataKuliah", "FlagSertifikat");
        }
    }
}
