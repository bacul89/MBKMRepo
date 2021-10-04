namespace MBKM.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migrasi4okt : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.CPLMKPendaftaran", "PendaftaranID", "dbo.PendaftaranMataKuliah");
            DropForeignKey("dbo.CPLMKPendaftaran", "CPLMKID", "dbo.CPLMatakuliah");
            DropForeignKey("dbo.CPLMatakuliah", "MasterCapaianPembelajarans_ID", "dbo.MasterCapaianPembelajaran");
            DropIndex("dbo.CPLMatakuliah", new[] { "MasterCapaianPembelajarans_ID" });
            DropIndex("dbo.CPLMKPendaftaran", new[] { "PendaftaranID" });
            DropIndex("dbo.CPLMKPendaftaran", new[] { "CPLMKID" });
            RenameColumn(table: "dbo.CPLMatakuliah", name: "MasterCapaianPembelajarans_ID", newName: "MasterCapaianPembelajaranID");
            CreateTable(
                "dbo.PendaftaranMKCPL",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        CPLAsal = c.String(maxLength: 8000, unicode: false),
                        PendaftaranMataKuliahID = c.Long(nullable: false),
                        CPLMatakuliahID = c.Long(),
                        CreatedBy = c.String(maxLength: 100, unicode: false),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedBy = c.String(maxLength: 100, unicode: false),
                        UpdatedDate = c.DateTime(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.CPLMatakuliah", t => t.CPLMatakuliahID)
                .ForeignKey("dbo.PendaftaranMataKuliah", t => t.PendaftaranMataKuliahID, cascadeDelete: true)
                .Index(t => t.PendaftaranMataKuliahID)
                .Index(t => t.CPLMatakuliahID);
            
            AlterColumn("dbo.PendaftaranMataKuliah", "Hasil", c => c.String(maxLength: 10, unicode: false));
            AlterColumn("dbo.CPLMatakuliah", "MasterCapaianPembelajaranID", c => c.Long(nullable: false));
            AlterColumn("dbo.MasterCapaianPembelajaran", "NamaProdi", c => c.String(maxLength: 150, unicode: false));
            CreateIndex("dbo.CPLMatakuliah", "MasterCapaianPembelajaranID");
            AddForeignKey("dbo.CPLMatakuliah", "MasterCapaianPembelajaranID", "dbo.MasterCapaianPembelajaran", "ID", cascadeDelete: true);
            DropColumn("dbo.CPLMatakuliah", "CapaianPembelajaranID");
            DropTable("dbo.CPLMKPendaftaran");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.CPLMKPendaftaran",
                c => new
                    {
                        PendaftaranID = c.Long(nullable: false),
                        CPLMKID = c.Long(nullable: false),
                    })
                .PrimaryKey(t => new { t.PendaftaranID, t.CPLMKID });
            
            AddColumn("dbo.CPLMatakuliah", "CapaianPembelajaranID", c => c.Long(nullable: false));
            DropForeignKey("dbo.CPLMatakuliah", "MasterCapaianPembelajaranID", "dbo.MasterCapaianPembelajaran");
            DropForeignKey("dbo.PendaftaranMKCPL", "PendaftaranMataKuliahID", "dbo.PendaftaranMataKuliah");
            DropForeignKey("dbo.PendaftaranMKCPL", "CPLMatakuliahID", "dbo.CPLMatakuliah");
            DropIndex("dbo.CPLMatakuliah", new[] { "MasterCapaianPembelajaranID" });
            DropIndex("dbo.PendaftaranMKCPL", new[] { "CPLMatakuliahID" });
            DropIndex("dbo.PendaftaranMKCPL", new[] { "PendaftaranMataKuliahID" });
            AlterColumn("dbo.MasterCapaianPembelajaran", "NamaProdi", c => c.String(nullable: false, maxLength: 20, unicode: false));
            AlterColumn("dbo.CPLMatakuliah", "MasterCapaianPembelajaranID", c => c.Long());
            AlterColumn("dbo.PendaftaranMataKuliah", "Hasil", c => c.String(nullable: false, maxLength: 10, unicode: false));
            DropTable("dbo.PendaftaranMKCPL");
            RenameColumn(table: "dbo.CPLMatakuliah", name: "MasterCapaianPembelajaranID", newName: "MasterCapaianPembelajarans_ID");
            CreateIndex("dbo.CPLMKPendaftaran", "CPLMKID");
            CreateIndex("dbo.CPLMKPendaftaran", "PendaftaranID");
            CreateIndex("dbo.CPLMatakuliah", "MasterCapaianPembelajarans_ID");
            AddForeignKey("dbo.CPLMatakuliah", "MasterCapaianPembelajarans_ID", "dbo.MasterCapaianPembelajaran", "ID");
            AddForeignKey("dbo.CPLMKPendaftaran", "CPLMKID", "dbo.CPLMatakuliah", "ID", cascadeDelete: true);
            AddForeignKey("dbo.CPLMKPendaftaran", "PendaftaranID", "dbo.PendaftaranMataKuliah", "ID", cascadeDelete: true);
        }
    }
}
