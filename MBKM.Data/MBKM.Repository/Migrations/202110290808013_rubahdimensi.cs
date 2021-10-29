namespace MBKM.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class rubahdimensi : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Nilai", "UTS", c => c.Decimal(nullable: false, precision: 5, scale: 2));
            AlterColumn("dbo.Nilai", "CW1", c => c.Decimal(nullable: false, precision: 5, scale: 2));
            AlterColumn("dbo.Nilai", "CW2", c => c.Decimal(nullable: false, precision: 5, scale: 2));
            AlterColumn("dbo.Nilai", "CW3", c => c.Decimal(nullable: false, precision: 5, scale: 2));
            AlterColumn("dbo.Nilai", "CW4", c => c.Decimal(nullable: false, precision: 5, scale: 2));
            AlterColumn("dbo.Nilai", "CW5", c => c.Decimal(nullable: false, precision: 5, scale: 2));
            AlterColumn("dbo.Nilai", "Final", c => c.Decimal(nullable: false, precision: 5, scale: 2));
            AlterColumn("dbo.Nilai", "NilaiTotal", c => c.Decimal(nullable: false, precision: 5, scale: 2));
            AlterColumn("dbo.NilaiSubCW", "CWSub1", c => c.Decimal(nullable: false, precision: 5, scale: 2));
            AlterColumn("dbo.NilaiSubCW", "CWSub2", c => c.Decimal(nullable: false, precision: 5, scale: 2));
            AlterColumn("dbo.NilaiSubCW", "CWSub3", c => c.Decimal(nullable: false, precision: 5, scale: 2));
            AlterColumn("dbo.NilaiSubCW", "CWSub4", c => c.Decimal(nullable: false, precision: 5, scale: 2));
            AlterColumn("dbo.NilaiSubCW", "CWSub5", c => c.Decimal(nullable: false, precision: 5, scale: 2));
            AlterColumn("dbo.NilaiSubCW", "CWSub6", c => c.Decimal(nullable: false, precision: 5, scale: 2));
            AlterColumn("dbo.NilaiSubCW", "CWSub7", c => c.Decimal(nullable: false, precision: 5, scale: 2));
            AlterColumn("dbo.NilaiSubCW", "CWSub8", c => c.Decimal(nullable: false, precision: 5, scale: 2));
            AlterColumn("dbo.NilaiSubCW", "CWSub10", c => c.Decimal(nullable: false, precision: 5, scale: 2));
            AlterColumn("dbo.NilaiSubCW", "NilaiTotal", c => c.Decimal(nullable: false, precision: 5, scale: 2));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.NilaiSubCW", "NilaiTotal", c => c.Decimal(nullable: false, precision: 3, scale: 2));
            AlterColumn("dbo.NilaiSubCW", "CWSub10", c => c.Decimal(nullable: false, precision: 3, scale: 2));
            AlterColumn("dbo.NilaiSubCW", "CWSub8", c => c.Decimal(nullable: false, precision: 3, scale: 2));
            AlterColumn("dbo.NilaiSubCW", "CWSub7", c => c.Decimal(nullable: false, precision: 3, scale: 2));
            AlterColumn("dbo.NilaiSubCW", "CWSub6", c => c.Decimal(nullable: false, precision: 3, scale: 2));
            AlterColumn("dbo.NilaiSubCW", "CWSub5", c => c.Decimal(nullable: false, precision: 3, scale: 2));
            AlterColumn("dbo.NilaiSubCW", "CWSub4", c => c.Decimal(nullable: false, precision: 3, scale: 2));
            AlterColumn("dbo.NilaiSubCW", "CWSub3", c => c.Decimal(nullable: false, precision: 3, scale: 2));
            AlterColumn("dbo.NilaiSubCW", "CWSub2", c => c.Decimal(nullable: false, precision: 3, scale: 2));
            AlterColumn("dbo.NilaiSubCW", "CWSub1", c => c.Decimal(nullable: false, precision: 3, scale: 2));
            AlterColumn("dbo.Nilai", "NilaiTotal", c => c.Decimal(nullable: false, precision: 3, scale: 2));
            AlterColumn("dbo.Nilai", "Final", c => c.Decimal(nullable: false, precision: 3, scale: 2));
            AlterColumn("dbo.Nilai", "CW5", c => c.Decimal(nullable: false, precision: 3, scale: 2));
            AlterColumn("dbo.Nilai", "CW4", c => c.Decimal(nullable: false, precision: 3, scale: 2));
            AlterColumn("dbo.Nilai", "CW3", c => c.Decimal(nullable: false, precision: 3, scale: 2));
            AlterColumn("dbo.Nilai", "CW2", c => c.Decimal(nullable: false, precision: 3, scale: 2));
            AlterColumn("dbo.Nilai", "CW1", c => c.Decimal(nullable: false, precision: 3, scale: 2));
            AlterColumn("dbo.Nilai", "UTS", c => c.Decimal(nullable: false, precision: 3, scale: 2));
        }
    }
}
