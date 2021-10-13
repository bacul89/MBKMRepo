namespace MBKM.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tmbhdosenabsensp3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Absensi", "InstructorId", c => c.String(nullable: false, maxLength: 100, unicode: false));
            AddColumn("dbo.Absensi", "NamaDosen", c => c.String(nullable: false, maxLength: 250, unicode: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Absensi", "NamaDosen");
            DropColumn("dbo.Absensi", "InstructorId");
        }
    }
}
