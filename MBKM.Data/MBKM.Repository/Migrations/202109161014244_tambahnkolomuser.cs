namespace MBKM.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tambahnkolomuser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.User", "KodeProdi", c => c.String(maxLength: 50, unicode: false));
            AddColumn("dbo.User", "NamaProdi", c => c.String(maxLength: 150, unicode: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.User", "NamaProdi");
            DropColumn("dbo.User", "KodeProdi");
        }
    }
}
