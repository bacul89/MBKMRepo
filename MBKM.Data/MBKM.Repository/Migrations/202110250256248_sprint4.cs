namespace MBKM.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class sprint4 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Nilai", "JadwalKuliahMahasiswaID", "dbo.JadwalKuliahMahasiswa");
            DropIndex("dbo.Nilai", new[] { "JadwalKuliahMahasiswaID" });
            CreateTable(
                "dbo.NilaiSubCW",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        CWSub1 = c.Int(nullable: false),
                        CWSub2 = c.Int(nullable: false),
                        CWSub3 = c.Int(nullable: false),
                        CWSub4 = c.Int(nullable: false),
                        CWSub5 = c.Int(nullable: false),
                        CWSub6 = c.Int(nullable: false),
                        CWSub7 = c.Int(nullable: false),
                        CWSub8 = c.Int(nullable: false),
                        CWSub9 = c.Int(nullable: false),
                        CWSub10 = c.Int(nullable: false),
                        NilaiTotal = c.Int(nullable: false),
                        NilaiKuliahID = c.Long(nullable: false),
                        CreatedBy = c.String(maxLength: 100, unicode: false),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedBy = c.String(maxLength: 100, unicode: false),
                        UpdatedDate = c.DateTime(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Nilai", t => t.NilaiKuliahID, cascadeDelete: true)
                .Index(t => t.NilaiKuliahID);
            
            AddColumn("dbo.Nilai", "JadwalKuliahID", c => c.Long(nullable: false));
            AddColumn("dbo.Nilai", "MahasiswaID", c => c.Long(nullable: false));
            AddColumn("dbo.Nilai", "UTS", c => c.Int(nullable: false));
            AddColumn("dbo.Nilai", "CW1", c => c.Int(nullable: false));
            AddColumn("dbo.Nilai", "CW2", c => c.Int(nullable: false));
            AddColumn("dbo.Nilai", "CW3", c => c.Int(nullable: false));
            AddColumn("dbo.Nilai", "CW4", c => c.Int(nullable: false));
            AddColumn("dbo.Nilai", "CW5", c => c.Int(nullable: false));
            AddColumn("dbo.Nilai", "Final", c => c.Int(nullable: false));
            AddColumn("dbo.Nilai", "NilaiTotal", c => c.Int(nullable: false));
            AddColumn("dbo.Nilai", "Grade", c => c.String(maxLength: 8000, unicode: false));
            AddColumn("dbo.Nilai", "FlagCetak", c => c.Boolean(nullable: false));
            AddColumn("dbo.InformasiPertukaran", "JudulAktivitas", c => c.String(maxLength: 350, unicode: false));
            AddColumn("dbo.InformasiPertukaran", "LokasiTugas", c => c.String(maxLength: 150, unicode: false));
            AddColumn("dbo.InformasiPertukaran", "TanggalSK", c => c.DateTime());
            AddColumn("dbo.InformasiPertukaran", "NoSK", c => c.String(maxLength: 150, unicode: false));
            CreateIndex("dbo.Nilai", "JadwalKuliahID");
            CreateIndex("dbo.Nilai", "MahasiswaID");
            AddForeignKey("dbo.Nilai", "JadwalKuliahID", "dbo.JadwalKuliah", "ID", cascadeDelete: true);
            AddForeignKey("dbo.Nilai", "MahasiswaID", "dbo.Mahasiswa", "ID", cascadeDelete: true);
            DropColumn("dbo.Nilai", "JadwalKuliahMahasiswaID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Nilai", "JadwalKuliahMahasiswaID", c => c.Long(nullable: false));
            DropForeignKey("dbo.NilaiSubCW", "NilaiKuliahID", "dbo.Nilai");
            DropForeignKey("dbo.Nilai", "MahasiswaID", "dbo.Mahasiswa");
            DropForeignKey("dbo.Nilai", "JadwalKuliahID", "dbo.JadwalKuliah");
            DropIndex("dbo.NilaiSubCW", new[] { "NilaiKuliahID" });
            DropIndex("dbo.Nilai", new[] { "MahasiswaID" });
            DropIndex("dbo.Nilai", new[] { "JadwalKuliahID" });
            DropColumn("dbo.InformasiPertukaran", "NoSK");
            DropColumn("dbo.InformasiPertukaran", "TanggalSK");
            DropColumn("dbo.InformasiPertukaran", "LokasiTugas");
            DropColumn("dbo.InformasiPertukaran", "JudulAktivitas");
            DropColumn("dbo.Nilai", "FlagCetak");
            DropColumn("dbo.Nilai", "Grade");
            DropColumn("dbo.Nilai", "NilaiTotal");
            DropColumn("dbo.Nilai", "Final");
            DropColumn("dbo.Nilai", "CW5");
            DropColumn("dbo.Nilai", "CW4");
            DropColumn("dbo.Nilai", "CW3");
            DropColumn("dbo.Nilai", "CW2");
            DropColumn("dbo.Nilai", "CW1");
            DropColumn("dbo.Nilai", "UTS");
            DropColumn("dbo.Nilai", "MahasiswaID");
            DropColumn("dbo.Nilai", "JadwalKuliahID");
            DropTable("dbo.NilaiSubCW");
            CreateIndex("dbo.Nilai", "JadwalKuliahMahasiswaID");
            AddForeignKey("dbo.Nilai", "JadwalKuliahMahasiswaID", "dbo.JadwalKuliahMahasiswa", "ID", cascadeDelete: true);
        }
    }
}
