namespace MBKM.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addfeedbacksp4 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.KRS", "MahasiswaID", "dbo.Mahasiswa");
            DropForeignKey("dbo.JadwalKuliahMahasiswa", "JadwalKuliahID", "dbo.JadwalKuliah");
            DropForeignKey("dbo.JadwalKuliahMahasiswa", "KRSID", "dbo.KRS");
            DropIndex("dbo.KRS", new[] { "MahasiswaID" });
            DropIndex("dbo.JadwalKuliahMahasiswa", new[] { "KRSID" });
            DropIndex("dbo.JadwalKuliahMahasiswa", new[] { "JadwalKuliahID" });
            CreateTable(
                "dbo.FeedbackMataKul",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        MahasiswaID = c.Long(nullable: false),
                        DosenID = c.String(nullable: false, maxLength: 50, unicode: false),
                        NamaDosen = c.String(nullable: false, maxLength: 150, unicode: false),
                        KritikSaran = c.String(nullable: false, maxLength: 5000, unicode: false),
                        StatusFeedBack = c.Boolean(nullable: false),
                        JadwalKuliahID = c.Long(nullable: false),
                        CreatedBy = c.String(maxLength: 100, unicode: false),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedBy = c.String(maxLength: 100, unicode: false),
                        UpdatedDate = c.DateTime(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.JadwalKuliah", t => t.JadwalKuliahID, cascadeDelete: true)
                .ForeignKey("dbo.Mahasiswa", t => t.MahasiswaID, cascadeDelete: true)
                .Index(t => t.MahasiswaID)
                .Index(t => t.JadwalKuliahID);
            
            CreateTable(
                "dbo.FeedbackMatkulDetail",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        FeedbackMatkulID = c.Long(nullable: false),
                        PertanyaanID = c.String(nullable: false, maxLength: 50, unicode: false),
                        Pertanyaan = c.String(nullable: false, maxLength: 550, unicode: false),
                        KategoriPertanyaan = c.String(nullable: false, maxLength: 100, unicode: false),
                        Nilai = c.Int(nullable: false),
                        CreatedBy = c.String(maxLength: 100, unicode: false),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedBy = c.String(maxLength: 100, unicode: false),
                        UpdatedDate = c.DateTime(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.FeedbackMataKul", t => t.FeedbackMatkulID, cascadeDelete: true)
                .Index(t => t.FeedbackMatkulID);
            
            DropTable("dbo.KRS");
            DropTable("dbo.JadwalKuliahMahasiswa");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.JadwalKuliahMahasiswa",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        Feedback = c.String(maxLength: 8000, unicode: false),
                        FeedbackRating = c.String(maxLength: 8000, unicode: false),
                        JumlahPertemuan = c.Int(nullable: false),
                        KRSID = c.Long(nullable: false),
                        JadwalKuliahID = c.Long(nullable: false),
                        CreatedBy = c.String(maxLength: 100, unicode: false),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedBy = c.String(maxLength: 100, unicode: false),
                        UpdatedDate = c.DateTime(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.KRS",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        FlagBayar = c.Boolean(nullable: false),
                        TanggalBayar = c.DateTime(),
                        MahasiswaID = c.Long(nullable: false),
                        CreatedBy = c.String(maxLength: 100, unicode: false),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedBy = c.String(maxLength: 100, unicode: false),
                        UpdatedDate = c.DateTime(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            DropForeignKey("dbo.FeedbackMatkulDetail", "FeedbackMatkulID", "dbo.FeedbackMataKul");
            DropForeignKey("dbo.FeedbackMataKul", "MahasiswaID", "dbo.Mahasiswa");
            DropForeignKey("dbo.FeedbackMataKul", "JadwalKuliahID", "dbo.JadwalKuliah");
            DropIndex("dbo.FeedbackMatkulDetail", new[] { "FeedbackMatkulID" });
            DropIndex("dbo.FeedbackMataKul", new[] { "JadwalKuliahID" });
            DropIndex("dbo.FeedbackMataKul", new[] { "MahasiswaID" });
            DropTable("dbo.FeedbackMatkulDetail");
            DropTable("dbo.FeedbackMataKul");
            CreateIndex("dbo.JadwalKuliahMahasiswa", "JadwalKuliahID");
            CreateIndex("dbo.JadwalKuliahMahasiswa", "KRSID");
            CreateIndex("dbo.KRS", "MahasiswaID");
            AddForeignKey("dbo.JadwalKuliahMahasiswa", "KRSID", "dbo.KRS", "ID", cascadeDelete: true);
            AddForeignKey("dbo.JadwalKuliahMahasiswa", "JadwalKuliahID", "dbo.JadwalKuliah", "ID", cascadeDelete: true);
            AddForeignKey("dbo.KRS", "MahasiswaID", "dbo.Mahasiswa", "ID", cascadeDelete: true);
        }
    }
}
