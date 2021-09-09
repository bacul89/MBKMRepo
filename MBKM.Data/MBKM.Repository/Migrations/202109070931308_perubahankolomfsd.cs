namespace MBKM.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class perubahankolomfsd : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Attachment", "PerjanjianKerjasamaID", "dbo.PerjanjianKerjasama");
            DropIndex("dbo.Attachment", new[] { "PerjanjianKerjasamaID" });
            CreateTable(
                "dbo.AttachmentPerjanjianKerjasama",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        FileName = c.String(maxLength: 8000, unicode: false),
                        FileExt = c.String(maxLength: 8000, unicode: false),
                        FileSze = c.Long(nullable: false),
                        PerjanjianKerjasamaID = c.Long(nullable: false),
                        CreatedBy = c.String(maxLength: 100, unicode: false),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedBy = c.String(maxLength: 100, unicode: false),
                        UpdatedDate = c.DateTime(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.PerjanjianKerjasama", t => t.PerjanjianKerjasamaID, cascadeDelete: true)
                .Index(t => t.PerjanjianKerjasamaID);
            
            AddColumn("dbo.PerjanjianKerjasama", "UniversitasID", c => c.Long(nullable: false));
            AddColumn("dbo.Mahasiswa", "Semester", c => c.String(maxLength: 8000, unicode: false));
            AddColumn("dbo.Mahasiswa", "ProdiAsal", c => c.String(maxLength: 8000, unicode: false));
            AddColumn("dbo.Mahasiswa", "NIMAsal", c => c.String(maxLength: 8000, unicode: false));
            AddColumn("dbo.Attachment", "FileType", c => c.String(maxLength: 8000, unicode: false));
            DropColumn("dbo.Mahasiswa", "UserName");
            DropColumn("dbo.Attachment", "PerjanjianKerjasamaID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Attachment", "PerjanjianKerjasamaID", c => c.Long(nullable: false));
            AddColumn("dbo.Mahasiswa", "UserName", c => c.String(nullable: false, maxLength: 50, unicode: false));
            DropForeignKey("dbo.AttachmentPerjanjianKerjasama", "PerjanjianKerjasamaID", "dbo.PerjanjianKerjasama");
            DropIndex("dbo.AttachmentPerjanjianKerjasama", new[] { "PerjanjianKerjasamaID" });
            DropColumn("dbo.Attachment", "FileType");
            DropColumn("dbo.Mahasiswa", "NIMAsal");
            DropColumn("dbo.Mahasiswa", "ProdiAsal");
            DropColumn("dbo.Mahasiswa", "Semester");
            DropColumn("dbo.PerjanjianKerjasama", "UniversitasID");
            DropTable("dbo.AttachmentPerjanjianKerjasama");
            CreateIndex("dbo.Attachment", "PerjanjianKerjasamaID");
            AddForeignKey("dbo.Attachment", "PerjanjianKerjasamaID", "dbo.PerjanjianKerjasama", "ID", cascadeDelete: true);
        }
    }
}
