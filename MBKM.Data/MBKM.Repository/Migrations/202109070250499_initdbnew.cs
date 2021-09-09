namespace MBKM.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initdbnew : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Menu",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        MenuName = c.String(nullable: false, maxLength: 150, unicode: false),
                        MenuDescription = c.String(maxLength: 500, unicode: false),
                        MenuUrl = c.String(nullable: false, maxLength: 100, unicode: false),
                        MenuParent = c.String(maxLength: 8000, unicode: false),
                        MenuIcon = c.String(maxLength: 20, unicode: false),
                        MenuOrder = c.String(maxLength: 20, unicode: false),
                        CreatedBy = c.String(maxLength: 100, unicode: false),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedBy = c.String(maxLength: 100, unicode: false),
                        UpdatedDate = c.DateTime(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.MenuRole",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        IsView = c.Boolean(nullable: false),
                        IsCreate = c.Boolean(nullable: false),
                        IsDelete = c.Boolean(nullable: false),
                        IsUpdate = c.Boolean(nullable: false),
                        RoleID = c.Long(nullable: false),
                        MenuID = c.Long(nullable: false),
                        CreatedBy = c.String(maxLength: 100, unicode: false),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedBy = c.String(maxLength: 100, unicode: false),
                        UpdatedDate = c.DateTime(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Menu", t => t.MenuID, cascadeDelete: true)
                .ForeignKey("dbo.Role", t => t.RoleID, cascadeDelete: true)
                .Index(t => t.RoleID)
                .Index(t => t.MenuID);
            
            CreateTable(
                "dbo.Role",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        Code = c.String(nullable: false, maxLength: 20, unicode: false),
                        RoleName = c.String(nullable: false, maxLength: 20, unicode: false),
                        CreatedBy = c.String(maxLength: 100, unicode: false),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedBy = c.String(maxLength: 100, unicode: false),
                        UpdatedDate = c.DateTime(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.PerjanjianKerjasama",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        NoPerjanjian = c.String(maxLength: 8000, unicode: false),
                        TanggalMulai = c.DateTime(nullable: false),
                        TanggalAkhir = c.DateTime(nullable: false),
                        NamaUniversitas = c.String(nullable: false, maxLength: 150, unicode: false),
                        CreatedBy = c.String(maxLength: 100, unicode: false),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedBy = c.String(maxLength: 100, unicode: false),
                        UpdatedDate = c.DateTime(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Mahasiswa",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        UniversitasID = c.Long(nullable: false),
                        NamaUniversitas = c.String(nullable: false, maxLength: 150, unicode: false),
                        Nama = c.String(nullable: false, maxLength: 250, unicode: false),
                        Email = c.String(nullable: false, maxLength: 250, unicode: false),
                        Telepon = c.String(nullable: false, maxLength: 150, unicode: false),
                        TanggalLahir = c.DateTime(nullable: false),
                        Alamat = c.String(maxLength: 8000, unicode: false),
                        Agama = c.String(nullable: false, maxLength: 150, unicode: false),
                        NoKTP = c.String(nullable: false, maxLength: 150, unicode: false),
                        UserName = c.String(nullable: false, maxLength: 50, unicode: false),
                        Password = c.String(nullable: false, maxLength: 500, unicode: false),
                        Token = c.String(maxLength: 8000, unicode: false),
                        ReferenceNumber = c.String(maxLength: 8000, unicode: false),
                        isVerifikasi = c.Boolean(nullable: false),
                        CreatedBy = c.String(maxLength: 100, unicode: false),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedBy = c.String(maxLength: 100, unicode: false),
                        UpdatedDate = c.DateTime(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Attachment",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        FileName = c.String(maxLength: 8000, unicode: false),
                        FileExt = c.String(maxLength: 8000, unicode: false),
                        FileSze = c.Long(nullable: false),
                        MahasiswaID = c.Long(nullable: false),
                        PerjanjianKerjasamaID = c.Long(nullable: false),
                        CreatedBy = c.String(maxLength: 100, unicode: false),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedBy = c.String(maxLength: 100, unicode: false),
                        UpdatedDate = c.DateTime(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Mahasiswa", t => t.MahasiswaID, cascadeDelete: true)
                .ForeignKey("dbo.PerjanjianKerjasama", t => t.PerjanjianKerjasamaID, cascadeDelete: true)
                .Index(t => t.MahasiswaID)
                .Index(t => t.PerjanjianKerjasamaID);
            
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
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Mahasiswa", t => t.MahasiswaID, cascadeDelete: true)
                .Index(t => t.MahasiswaID);
            
            CreateTable(
                "dbo.User",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        UserName = c.String(nullable: false, maxLength: 50, unicode: false),
                        Password = c.String(nullable: false, maxLength: 500, unicode: false),
                        Email = c.String(nullable: false, maxLength: 150, unicode: false),
                        NoPegawai = c.String(nullable: false, maxLength: 50, unicode: false),
                        Alamat = c.String(maxLength: 500, unicode: false),
                        NoTelp = c.String(nullable: false, maxLength: 50, unicode: false),
                        Token = c.String(maxLength: 500, unicode: false),
                        RoleID = c.Long(nullable: false),
                        CreatedBy = c.String(maxLength: 100, unicode: false),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedBy = c.String(maxLength: 100, unicode: false),
                        UpdatedDate = c.DateTime(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Role", t => t.RoleID, cascadeDelete: true)
                .Index(t => t.RoleID);
            
            CreateTable(
                "dbo.JadwalKuliah",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        DosenID = c.Long(nullable: false),
                        NamaDosen = c.String(nullable: false, maxLength: 150, unicode: false),
                        MataKuliah = c.Long(nullable: false),
                        KodeMataKuliah = c.String(nullable: false, maxLength: 50, unicode: false),
                        Hari = c.String(nullable: false, maxLength: 15, unicode: false),
                        Waktu = c.DateTime(nullable: false),
                        Kuota = c.Int(nullable: false),
                        LinkMateri = c.String(nullable: false, maxLength: 250, unicode: false),
                        FlagOpen = c.Boolean(nullable: false),
                        OpenRegistration = c.String(maxLength: 8000, unicode: false),
                        CloseRegistration = c.String(maxLength: 8000, unicode: false),
                        CreatedBy = c.String(maxLength: 100, unicode: false),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedBy = c.String(maxLength: 100, unicode: false),
                        UpdatedDate = c.DateTime(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
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
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.JadwalKuliah", t => t.JadwalKuliahID, cascadeDelete: true)
                .ForeignKey("dbo.KRS", t => t.KRSID, cascadeDelete: true)
                .Index(t => t.KRSID)
                .Index(t => t.JadwalKuliahID);
            
            CreateTable(
                "dbo.Nilai",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        NamaMatakuliah = c.String(maxLength: 8000, unicode: false),
                        Nilai = c.Int(nullable: false),
                        Persentase = c.Int(nullable: false),
                        JadwalKuliahMahasiswaID = c.Long(nullable: false),
                        CreatedBy = c.String(maxLength: 100, unicode: false),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedBy = c.String(maxLength: 100, unicode: false),
                        UpdatedDate = c.DateTime(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.JadwalKuliahMahasiswa", t => t.JadwalKuliahMahasiswaID, cascadeDelete: true)
                .Index(t => t.JadwalKuliahMahasiswaID);
            
            CreateTable(
                "dbo.Absensi",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        JadwalKuliahMahasiswaID = c.Long(nullable: false),
                        TanggalAbsen = c.DateTime(nullable: false),
                        CreatedBy = c.String(maxLength: 100, unicode: false),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedBy = c.String(maxLength: 100, unicode: false),
                        UpdatedDate = c.DateTime(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.JadwalKuliahMahasiswa", t => t.JadwalKuliahMahasiswaID, cascadeDelete: true)
                .Index(t => t.JadwalKuliahMahasiswaID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Absensi", "JadwalKuliahMahasiswaID", "dbo.JadwalKuliahMahasiswa");
            DropForeignKey("dbo.Nilai", "JadwalKuliahMahasiswaID", "dbo.JadwalKuliahMahasiswa");
            DropForeignKey("dbo.JadwalKuliahMahasiswa", "KRSID", "dbo.KRS");
            DropForeignKey("dbo.JadwalKuliahMahasiswa", "JadwalKuliahID", "dbo.JadwalKuliah");
            DropForeignKey("dbo.User", "RoleID", "dbo.Role");
            DropForeignKey("dbo.KRS", "MahasiswaID", "dbo.Mahasiswa");
            DropForeignKey("dbo.Attachment", "PerjanjianKerjasamaID", "dbo.PerjanjianKerjasama");
            DropForeignKey("dbo.Attachment", "MahasiswaID", "dbo.Mahasiswa");
            DropForeignKey("dbo.MenuRole", "RoleID", "dbo.Role");
            DropForeignKey("dbo.MenuRole", "MenuID", "dbo.Menu");
            DropIndex("dbo.Absensi", new[] { "JadwalKuliahMahasiswaID" });
            DropIndex("dbo.Nilai", new[] { "JadwalKuliahMahasiswaID" });
            DropIndex("dbo.JadwalKuliahMahasiswa", new[] { "JadwalKuliahID" });
            DropIndex("dbo.JadwalKuliahMahasiswa", new[] { "KRSID" });
            DropIndex("dbo.User", new[] { "RoleID" });
            DropIndex("dbo.KRS", new[] { "MahasiswaID" });
            DropIndex("dbo.Attachment", new[] { "PerjanjianKerjasamaID" });
            DropIndex("dbo.Attachment", new[] { "MahasiswaID" });
            DropIndex("dbo.MenuRole", new[] { "MenuID" });
            DropIndex("dbo.MenuRole", new[] { "RoleID" });
            DropTable("dbo.Absensi");
            DropTable("dbo.Nilai");
            DropTable("dbo.JadwalKuliahMahasiswa");
            DropTable("dbo.JadwalKuliah");
            DropTable("dbo.User");
            DropTable("dbo.KRS");
            DropTable("dbo.Attachment");
            DropTable("dbo.Mahasiswa");
            DropTable("dbo.PerjanjianKerjasama");
            DropTable("dbo.Role");
            DropTable("dbo.MenuRole");
            DropTable("dbo.Menu");
        }
    }
}
