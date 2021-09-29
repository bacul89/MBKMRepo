namespace MBKM.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addtblclmsprint2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PendaftaranMataKuliah",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        MatkulIDAsal = c.String(maxLength: 50, unicode: false),
                        MatkulKodeAsal = c.String(maxLength: 150, unicode: false),
                        MatkulAsal = c.String(maxLength: 250, unicode: false),
                        Kesenjangan = c.String(maxLength: 5000, unicode: false),
                        Nilai = c.String(maxLength: 150, unicode: false),
                        Konversi = c.String(maxLength: 150, unicode: false),
                        Hasil = c.String(nullable: false, maxLength: 10, unicode: false),
                        DosenID = c.Long(nullable: false),
                        DosenPembimbing = c.String(nullable: false, maxLength: 150, unicode: false),
                        MahasiswaID = c.Long(nullable: false),
                        JadwalKuliahID = c.Long(nullable: false),
                        CreatedBy = c.String(maxLength: 100, unicode: false),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedBy = c.String(maxLength: 100, unicode: false),
                        UpdatedDate = c.DateTime(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        InformasiPertukaran_ID = c.Long(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.JadwalKuliah", t => t.JadwalKuliahID, cascadeDelete: true)
                .ForeignKey("dbo.Mahasiswa", t => t.MahasiswaID, cascadeDelete: true)
                .ForeignKey("dbo.InformasiPertukaran", t => t.InformasiPertukaran_ID)
                .Index(t => t.MahasiswaID)
                .Index(t => t.JadwalKuliahID)
                .Index(t => t.InformasiPertukaran_ID);
            
            CreateTable(
                "dbo.ApprovalPendaftaran",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        StatusPendaftaran = c.String(nullable: false, maxLength: 50, unicode: false),
                        Approval = c.String(nullable: false, maxLength: 150, unicode: false),
                        Catatan = c.String(nullable: false, maxLength: 5000, unicode: false),
                        PendaftaranMataKuliahID = c.Long(nullable: false),
                        CreatedBy = c.String(maxLength: 100, unicode: false),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedBy = c.String(maxLength: 100, unicode: false),
                        UpdatedDate = c.DateTime(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.PendaftaranMataKuliah", t => t.PendaftaranMataKuliahID, cascadeDelete: true)
                .Index(t => t.PendaftaranMataKuliahID);
            
            CreateTable(
                "dbo.CPLMatakuliah",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        IDMataKUliah = c.String(nullable: false, maxLength: 8000, unicode: false),
                        KodeMataKuliah = c.String(nullable: false, maxLength: 50, unicode: false),
                        NamaMataKuliah = c.String(nullable: false, maxLength: 350, unicode: false),
                        CapaianPembelajaranID = c.Long(nullable: false),
                        CreatedBy = c.String(maxLength: 100, unicode: false),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedBy = c.String(maxLength: 100, unicode: false),
                        UpdatedDate = c.DateTime(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        MasterCapaianPembelajarans_ID = c.Long(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.MasterCapaianPembelajaran", t => t.MasterCapaianPembelajarans_ID)
                .Index(t => t.MasterCapaianPembelajarans_ID);
            
            CreateTable(
                "dbo.MasterCapaianPembelajaran",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        FakultasID = c.String(nullable: false, maxLength: 30, unicode: false),
                        NamaFakultas = c.String(nullable: false, maxLength: 150, unicode: false),
                        ProdiID = c.String(nullable: false, maxLength: 30, unicode: false),
                        NamaProdi = c.String(nullable: false, maxLength: 20, unicode: false),
                        Kelompok = c.String(nullable: false, maxLength: 50, unicode: false),
                        Kode = c.String(nullable: false, maxLength: 20, unicode: false),
                        Capaian = c.String(maxLength: 8000, unicode: false),
                        CreatedBy = c.String(maxLength: 100, unicode: false),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedBy = c.String(maxLength: 100, unicode: false),
                        UpdatedDate = c.DateTime(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.JenisKerjasama",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        JenisPertukaran = c.String(nullable: false, maxLength: 150, unicode: false),
                        JenisKerjasama = c.String(nullable: false, maxLength: 150, unicode: false),
                        CreatedBy = c.String(maxLength: 100, unicode: false),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedBy = c.String(maxLength: 100, unicode: false),
                        UpdatedDate = c.DateTime(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.InformasiPertukaran",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        MahasiswaID = c.Long(nullable: false),
                        STRM = c.Int(nullable: false),
                        JenisPertukaran = c.String(nullable: false, maxLength: 150, unicode: false),
                        JenisKerjasama = c.String(nullable: false, maxLength: 150, unicode: false),
                        NoKerjasama = c.String(maxLength: 150, unicode: false),
                        CreatedBy = c.String(maxLength: 100, unicode: false),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedBy = c.String(maxLength: 100, unicode: false),
                        UpdatedDate = c.DateTime(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Mahasiswa", t => t.MahasiswaID, cascadeDelete: true)
                .Index(t => t.MahasiswaID);
            
            CreateTable(
                "dbo.CPLMKPendaftaran",
                c => new
                    {
                        PendaftaranID = c.Long(nullable: false),
                        CPLMKID = c.Long(nullable: false),
                    })
                .PrimaryKey(t => new { t.PendaftaranID, t.CPLMKID })
                .ForeignKey("dbo.PendaftaranMataKuliah", t => t.PendaftaranID, cascadeDelete: true)
                .ForeignKey("dbo.CPLMatakuliah", t => t.CPLMKID, cascadeDelete: true)
                .Index(t => t.PendaftaranID)
                .Index(t => t.CPLMKID);
            
            AddColumn("dbo.JadwalKuliah", "MataKuliahID", c => c.String(nullable: false, maxLength: 8000, unicode: false));
            AddColumn("dbo.JadwalKuliah", "NamaMataKuliah", c => c.String(nullable: false, maxLength: 8000, unicode: false));
            AddColumn("dbo.JadwalKuliah", "JamMasuk", c => c.String(nullable: false, maxLength: 10, unicode: false));
            AddColumn("dbo.JadwalKuliah", "JamSelesai", c => c.String(nullable: false, maxLength: 10, unicode: false));
            AddColumn("dbo.JadwalKuliah", "TglAwalKuliah", c => c.DateTime(nullable: false));
            AddColumn("dbo.JadwalKuliah", "TglAkhirKuliah", c => c.DateTime(nullable: false));
            AddColumn("dbo.JadwalKuliah", "RuangKelas", c => c.String(nullable: false, maxLength: 150, unicode: false));
            AddColumn("dbo.JadwalKuliah", "Lokasi", c => c.String(nullable: false, maxLength: 250, unicode: false));
            AddColumn("dbo.JadwalKuliah", "STRM", c => c.Int(nullable: false));
            AddColumn("dbo.JadwalKuliah", "SKS", c => c.String(nullable: false, maxLength: 50, unicode: false));
            AddColumn("dbo.JadwalKuliah", "ClassSection", c => c.String(nullable: false, maxLength: 150, unicode: false));
            AddColumn("dbo.JadwalKuliah", "JenjangStudi", c => c.String(nullable: false, maxLength: 150, unicode: false));
            AddColumn("dbo.JadwalKuliah", "FakultasID", c => c.Long(nullable: false));
            AddColumn("dbo.JadwalKuliah", "NamaFakultas", c => c.String(nullable: false, maxLength: 250, unicode: false));
            AddColumn("dbo.JadwalKuliah", "ProdiID", c => c.Long(nullable: false));
            AddColumn("dbo.JadwalKuliah", "NamaProdi", c => c.String(nullable: false, maxLength: 250, unicode: false));
            DropColumn("dbo.JadwalKuliah", "MataKuliah");
            DropColumn("dbo.JadwalKuliah", "Waktu");
            DropColumn("dbo.JadwalKuliah", "Kuota");
            DropColumn("dbo.JadwalKuliah", "LinkMateri");
            DropColumn("dbo.JadwalKuliah", "OpenRegistration");
            DropColumn("dbo.JadwalKuliah", "CloseRegistration");
        }
        
        public override void Down()
        {
            AddColumn("dbo.JadwalKuliah", "CloseRegistration", c => c.String(maxLength: 8000, unicode: false));
            AddColumn("dbo.JadwalKuliah", "OpenRegistration", c => c.String(maxLength: 8000, unicode: false));
            AddColumn("dbo.JadwalKuliah", "LinkMateri", c => c.String(nullable: false, maxLength: 250, unicode: false));
            AddColumn("dbo.JadwalKuliah", "Kuota", c => c.Int(nullable: false));
            AddColumn("dbo.JadwalKuliah", "Waktu", c => c.DateTime(nullable: false));
            AddColumn("dbo.JadwalKuliah", "MataKuliah", c => c.Long(nullable: false));
            DropForeignKey("dbo.PendaftaranMataKuliah", "InformasiPertukaran_ID", "dbo.InformasiPertukaran");
            DropForeignKey("dbo.InformasiPertukaran", "MahasiswaID", "dbo.Mahasiswa");
            DropForeignKey("dbo.PendaftaranMataKuliah", "MahasiswaID", "dbo.Mahasiswa");
            DropForeignKey("dbo.PendaftaranMataKuliah", "JadwalKuliahID", "dbo.JadwalKuliah");
            DropForeignKey("dbo.CPLMKPendaftaran", "CPLMKID", "dbo.CPLMatakuliah");
            DropForeignKey("dbo.CPLMKPendaftaran", "PendaftaranID", "dbo.PendaftaranMataKuliah");
            DropForeignKey("dbo.CPLMatakuliah", "MasterCapaianPembelajarans_ID", "dbo.MasterCapaianPembelajaran");
            DropForeignKey("dbo.ApprovalPendaftaran", "PendaftaranMataKuliahID", "dbo.PendaftaranMataKuliah");
            DropIndex("dbo.CPLMKPendaftaran", new[] { "CPLMKID" });
            DropIndex("dbo.CPLMKPendaftaran", new[] { "PendaftaranID" });
            DropIndex("dbo.InformasiPertukaran", new[] { "MahasiswaID" });
            DropIndex("dbo.CPLMatakuliah", new[] { "MasterCapaianPembelajarans_ID" });
            DropIndex("dbo.ApprovalPendaftaran", new[] { "PendaftaranMataKuliahID" });
            DropIndex("dbo.PendaftaranMataKuliah", new[] { "InformasiPertukaran_ID" });
            DropIndex("dbo.PendaftaranMataKuliah", new[] { "JadwalKuliahID" });
            DropIndex("dbo.PendaftaranMataKuliah", new[] { "MahasiswaID" });
            DropColumn("dbo.JadwalKuliah", "NamaProdi");
            DropColumn("dbo.JadwalKuliah", "ProdiID");
            DropColumn("dbo.JadwalKuliah", "NamaFakultas");
            DropColumn("dbo.JadwalKuliah", "FakultasID");
            DropColumn("dbo.JadwalKuliah", "JenjangStudi");
            DropColumn("dbo.JadwalKuliah", "ClassSection");
            DropColumn("dbo.JadwalKuliah", "SKS");
            DropColumn("dbo.JadwalKuliah", "STRM");
            DropColumn("dbo.JadwalKuliah", "Lokasi");
            DropColumn("dbo.JadwalKuliah", "RuangKelas");
            DropColumn("dbo.JadwalKuliah", "TglAkhirKuliah");
            DropColumn("dbo.JadwalKuliah", "TglAwalKuliah");
            DropColumn("dbo.JadwalKuliah", "JamSelesai");
            DropColumn("dbo.JadwalKuliah", "JamMasuk");
            DropColumn("dbo.JadwalKuliah", "NamaMataKuliah");
            DropColumn("dbo.JadwalKuliah", "MataKuliahID");
            DropTable("dbo.CPLMKPendaftaran");
            DropTable("dbo.InformasiPertukaran");
            DropTable("dbo.JenisKerjasama");
            DropTable("dbo.MasterCapaianPembelajaran");
            DropTable("dbo.CPLMatakuliah");
            DropTable("dbo.ApprovalPendaftaran");
            DropTable("dbo.PendaftaranMataKuliah");
        }
    }
}
