namespace MBKM.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tambahtahunsemester : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Mahasiswa", "TahunSemester", c => c.String(maxLength: 100, unicode: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Mahasiswa", "TahunSemester");
        }
    }
}
