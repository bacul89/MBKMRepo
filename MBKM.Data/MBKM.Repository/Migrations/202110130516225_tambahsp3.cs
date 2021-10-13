namespace MBKM.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tambahsp3 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.JadwalUjianMBKM",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        JenjangStudi = c.String(nullable: false, maxLength: 10, unicode: false),
                        STRM = c.String(nullable: false, maxLength: 20, unicode: false),
                        KodeTipeUjian = c.String(maxLength: 8000, unicode: false),
                        TipeUjian = c.String(nullable: false, maxLength: 20, unicode: false),
                        FakultasID = c.String(nullable: false, maxLength: 20, unicode: false),
                        NamaFakultas = c.String(nullable: false, maxLength: 150, unicode: false),
                        Lokasi = c.String(nullable: false, maxLength: 150, unicode: false),
                        IDMatkul = c.String(nullable: false, maxLength: 30, unicode: false),
                        KodeMatkul = c.String(nullable: false, maxLength: 50, unicode: false),
                        NamaMatkul = c.String(nullable: false, maxLength: 250, unicode: false),
                        ProdiID = c.String(maxLength: 20, unicode: false),
                        NamaProdi = c.String(maxLength: 150, unicode: false),
                        TanggalUjian = c.DateTime(nullable: false),
                        JamMulai = c.String(nullable: false, maxLength: 20, unicode: false),
                        JamAkhir = c.String(nullable: false, maxLength: 20, unicode: false),
                        KodeRuangUjian = c.String(nullable: false, maxLength: 20, unicode: false),
                        RuangUjian = c.String(nullable: false, maxLength: 50, unicode: false),
                        KapasitasRuangan = c.Int(nullable: false),
                        Tersedia = c.Int(nullable: false),
                        ClassSection = c.String(nullable: false, maxLength: 10, unicode: false),
                        KodeClassSection = c.String(nullable: false, maxLength: 20, unicode: false),
                        CreatedBy = c.String(maxLength: 100, unicode: false),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedBy = c.String(maxLength: 100, unicode: false),
                        UpdatedDate = c.DateTime(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.JadwalUjianMBKMDetail",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        JadwalUjianMBKMID = c.Long(nullable: false),
                        MahasiswaID = c.Long(nullable: false),
                        Present = c.Boolean(nullable: false),
                        CreatedBy = c.String(maxLength: 100, unicode: false),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedBy = c.String(maxLength: 100, unicode: false),
                        UpdatedDate = c.DateTime(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.JadwalUjianMBKM", t => t.JadwalUjianMBKMID, cascadeDelete: true)
                .ForeignKey("dbo.Mahasiswa", t => t.MahasiswaID, cascadeDelete: true)
                .Index(t => t.JadwalUjianMBKMID)
                .Index(t => t.MahasiswaID);
            
            AddColumn("dbo.JadwalKuliah", "LinkMoodle", c => c.String(maxLength: 350, unicode: false));
            AddColumn("dbo.JadwalKuliah", "LinkAtmaZeds", c => c.String(maxLength: 350, unicode: false));
            AddColumn("dbo.JadwalKuliah", "LinkTeams", c => c.String(maxLength: 350, unicode: false));
            AddColumn("dbo.JadwalKuliah", "LinkOthers", c => c.String(maxLength: 350, unicode: false));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.JadwalUjianMBKMDetail", "MahasiswaID", "dbo.Mahasiswa");
            DropForeignKey("dbo.JadwalUjianMBKMDetail", "JadwalUjianMBKMID", "dbo.JadwalUjianMBKM");
            DropIndex("dbo.JadwalUjianMBKMDetail", new[] { "MahasiswaID" });
            DropIndex("dbo.JadwalUjianMBKMDetail", new[] { "JadwalUjianMBKMID" });
            DropColumn("dbo.JadwalKuliah", "LinkOthers");
            DropColumn("dbo.JadwalKuliah", "LinkTeams");
            DropColumn("dbo.JadwalKuliah", "LinkAtmaZeds");
            DropColumn("dbo.JadwalKuliah", "LinkMoodle");
            DropTable("dbo.JadwalUjianMBKMDetail");
            DropTable("dbo.JadwalUjianMBKM");
        }
    }
}
