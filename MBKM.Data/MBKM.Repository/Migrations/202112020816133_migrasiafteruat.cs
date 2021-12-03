namespace MBKM.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migrasiafteruat : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Mahasiswa", "EmailInternal", c => c.String(maxLength: 350, unicode: false));
            AddColumn("dbo.User", "KodeFakultas", c => c.String(maxLength: 50, unicode: false));
            AddColumn("dbo.User", "NamaFakultas", c => c.String(maxLength: 150, unicode: false));
            AddColumn("dbo.User", "KPTSDIN", c => c.String(maxLength: 150, unicode: false));
            AlterColumn("dbo.Role", "RoleName", c => c.String(nullable: false, maxLength: 150, unicode: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Role", "RoleName", c => c.String(nullable: false, maxLength: 20, unicode: false));
            DropColumn("dbo.User", "KPTSDIN");
            DropColumn("dbo.User", "NamaFakultas");
            DropColumn("dbo.User", "KodeFakultas");
            DropColumn("dbo.Mahasiswa", "EmailInternal");
        }
    }
}
