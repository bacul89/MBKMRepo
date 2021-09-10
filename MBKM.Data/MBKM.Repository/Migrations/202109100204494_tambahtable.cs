namespace MBKM.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tambahtable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.EmailTemplate",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        TipeMail = c.String(nullable: false, maxLength: 50, unicode: false),
                        SubjectMail = c.String(nullable: false, maxLength: 500, unicode: false),
                        BodyMail = c.String(nullable: false, maxLength: 8000, unicode: false),
                        CreatedBy = c.String(maxLength: 100, unicode: false),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedBy = c.String(maxLength: 100, unicode: false),
                        UpdatedDate = c.DateTime(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Lookup",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        Tipe = c.String(nullable: false, maxLength: 50, unicode: false),
                        Nama = c.String(nullable: false, maxLength: 100, unicode: false),
                        Nilai = c.String(nullable: false, maxLength: 100, unicode: false),
                        CreatedBy = c.String(maxLength: 100, unicode: false),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedBy = c.String(maxLength: 100, unicode: false),
                        UpdatedDate = c.DateTime(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            AddColumn("dbo.Mahasiswa", "NamaDarurat", c => c.String(maxLength: 250, unicode: false));
            AddColumn("dbo.Mahasiswa", "HubunganDarurat", c => c.String(maxLength: 50, unicode: false));
            AddColumn("dbo.Mahasiswa", "NoHPDarurat", c => c.String(maxLength: 50, unicode: false));
            AddColumn("dbo.Mahasiswa", "TeleponDarurat", c => c.String(maxLength: 50, unicode: false));
            AddColumn("dbo.Mahasiswa", "EmailDarurat", c => c.String(maxLength: 250, unicode: false));
            AddColumn("dbo.Mahasiswa", "AlamatDarurat", c => c.String(maxLength: 500, unicode: false));
            AddColumn("dbo.Mahasiswa", "JenjangStudi", c => c.String(maxLength: 150, unicode: false));
            AddColumn("dbo.Mahasiswa", "WargaNegara", c => c.String(maxLength: 10, unicode: false));
            AddColumn("dbo.Mahasiswa", "Gender", c => c.String(maxLength: 10, unicode: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Mahasiswa", "Gender");
            DropColumn("dbo.Mahasiswa", "WargaNegara");
            DropColumn("dbo.Mahasiswa", "JenjangStudi");
            DropColumn("dbo.Mahasiswa", "AlamatDarurat");
            DropColumn("dbo.Mahasiswa", "EmailDarurat");
            DropColumn("dbo.Mahasiswa", "TeleponDarurat");
            DropColumn("dbo.Mahasiswa", "NoHPDarurat");
            DropColumn("dbo.Mahasiswa", "HubunganDarurat");
            DropColumn("dbo.Mahasiswa", "NamaDarurat");
            DropTable("dbo.Lookup");
            DropTable("dbo.EmailTemplate");
        }
    }
}
