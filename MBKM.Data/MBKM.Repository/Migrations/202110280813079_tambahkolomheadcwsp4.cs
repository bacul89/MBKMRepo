namespace MBKM.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tambahkolomheadcwsp4 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.NilaiSubCW", "HeadCW", c => c.String(nullable: false, maxLength: 10, unicode: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.NilaiSubCW", "HeadCW");
        }
    }
}
