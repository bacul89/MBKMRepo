namespace MBKM.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addklmprodiidmahasiswa : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Mahasiswa", "ProdiAsalID", c => c.String(maxLength: 30, unicode: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Mahasiswa", "ProdiAsalID");
        }
    }
}
