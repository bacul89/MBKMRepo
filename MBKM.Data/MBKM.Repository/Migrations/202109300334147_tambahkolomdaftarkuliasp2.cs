namespace MBKM.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tambahkolomdaftarkuliasp2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.PendaftaranMataKuliah", "InformasiPertukaran_ID", "dbo.InformasiPertukaran");
            DropIndex("dbo.PendaftaranMataKuliah", new[] { "InformasiPertukaran_ID" });
            AddColumn("dbo.PendaftaranMataKuliah", "StatusPendaftaran", c => c.String(nullable: false, maxLength: 50, unicode: false));
            DropColumn("dbo.PendaftaranMataKuliah", "InformasiPertukaran_ID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PendaftaranMataKuliah", "InformasiPertukaran_ID", c => c.Long());
            DropColumn("dbo.PendaftaranMataKuliah", "StatusPendaftaran");
            CreateIndex("dbo.PendaftaranMataKuliah", "InformasiPertukaran_ID");
            AddForeignKey("dbo.PendaftaranMataKuliah", "InformasiPertukaran_ID", "dbo.InformasiPertukaran", "ID");
        }
    }
}
