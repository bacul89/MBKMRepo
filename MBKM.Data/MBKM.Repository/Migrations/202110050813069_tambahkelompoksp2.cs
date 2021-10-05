namespace MBKM.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tambahkelompoksp2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CPLMatakuliah", "Kelompok", c => c.String(nullable: false, maxLength: 100, unicode: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.CPLMatakuliah", "Kelompok");
        }
    }
}
