namespace MBKM.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class rubahbesarlength : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AttachmentPerjanjianKerjasama", "FileName", c => c.String(nullable: false, maxLength: 250, unicode: false));
            AlterColumn("dbo.Mahasiswa", "Semester", c => c.String(nullable: false, maxLength: 30, unicode: false));
            AlterColumn("dbo.Mahasiswa", "ProdiAsal", c => c.String(nullable: false, maxLength: 150, unicode: false));
            AlterColumn("dbo.Mahasiswa", "NIMAsal", c => c.String(nullable: false, maxLength: 50, unicode: false));
            AlterColumn("dbo.Attachment", "FileType", c => c.String(nullable: false, maxLength: 50, unicode: false));
            AlterColumn("dbo.Attachment", "FileName", c => c.String(nullable: false, maxLength: 250, unicode: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Attachment", "FileName", c => c.String(maxLength: 8000, unicode: false));
            AlterColumn("dbo.Attachment", "FileType", c => c.String(maxLength: 8000, unicode: false));
            AlterColumn("dbo.Mahasiswa", "NIMAsal", c => c.String(maxLength: 8000, unicode: false));
            AlterColumn("dbo.Mahasiswa", "ProdiAsal", c => c.String(maxLength: 8000, unicode: false));
            AlterColumn("dbo.Mahasiswa", "Semester", c => c.String(maxLength: 8000, unicode: false));
            AlterColumn("dbo.AttachmentPerjanjianKerjasama", "FileName", c => c.String(maxLength: 8000, unicode: false));
        }
    }
}
