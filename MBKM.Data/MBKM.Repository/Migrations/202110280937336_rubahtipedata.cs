namespace MBKM.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class rubahtipedata : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Nilai", "UTS", c => c.Decimal(nullable: false, precision: 3, scale: 2));
            AlterColumn("dbo.Nilai", "CW1", c => c.Decimal(nullable: false, precision: 3, scale: 2));
            AlterColumn("dbo.Nilai", "CW2", c => c.Decimal(nullable: false, precision: 3, scale: 2));
            AlterColumn("dbo.Nilai", "CW3", c => c.Decimal(nullable: false, precision: 3, scale: 2));
            AlterColumn("dbo.Nilai", "CW4", c => c.Decimal(nullable: false, precision: 3, scale: 2));
            AlterColumn("dbo.Nilai", "CW5", c => c.Decimal(nullable: false, precision: 3, scale: 2));
            AlterColumn("dbo.Nilai", "Final", c => c.Decimal(nullable: false, precision: 3, scale: 2));
            AlterColumn("dbo.Nilai", "NilaiTotal", c => c.Decimal(nullable: false, precision: 3, scale: 2));
            AlterColumn("dbo.Nilai", "Grade", c => c.String(nullable: false, maxLength: 3, unicode: false));
            AlterColumn("dbo.NilaiSubCW", "CWSub1", c => c.Decimal(nullable: false, precision: 3, scale: 2));
            AlterColumn("dbo.NilaiSubCW", "CWSub2", c => c.Decimal(nullable: false, precision: 3, scale: 2));
            AlterColumn("dbo.NilaiSubCW", "CWSub3", c => c.Decimal(nullable: false, precision: 3, scale: 2));
            AlterColumn("dbo.NilaiSubCW", "CWSub4", c => c.Decimal(nullable: false, precision: 3, scale: 2));
            AlterColumn("dbo.NilaiSubCW", "CWSub5", c => c.Decimal(nullable: false, precision: 3, scale: 2));
            AlterColumn("dbo.NilaiSubCW", "CWSub6", c => c.Decimal(nullable: false, precision: 3, scale: 2));
            AlterColumn("dbo.NilaiSubCW", "CWSub7", c => c.Decimal(nullable: false, precision: 3, scale: 2));
            AlterColumn("dbo.NilaiSubCW", "CWSub8", c => c.Decimal(nullable: false, precision: 3, scale: 2));
            AlterColumn("dbo.NilaiSubCW", "CWSub9", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.NilaiSubCW", "CWSub10", c => c.Decimal(nullable: false, precision: 3, scale: 2));
            AlterColumn("dbo.NilaiSubCW", "NilaiTotal", c => c.Decimal(nullable: false, precision: 3, scale: 2));
            DropColumn("dbo.Nilai", "Nilai");
            DropColumn("dbo.Nilai", "Persentase");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Nilai", "Persentase", c => c.Int(nullable: false));
            AddColumn("dbo.Nilai", "Nilai", c => c.Int(nullable: false));
            AlterColumn("dbo.NilaiSubCW", "NilaiTotal", c => c.Int(nullable: false));
            AlterColumn("dbo.NilaiSubCW", "CWSub10", c => c.Int(nullable: false));
            AlterColumn("dbo.NilaiSubCW", "CWSub9", c => c.Int(nullable: false));
            AlterColumn("dbo.NilaiSubCW", "CWSub8", c => c.Int(nullable: false));
            AlterColumn("dbo.NilaiSubCW", "CWSub7", c => c.Int(nullable: false));
            AlterColumn("dbo.NilaiSubCW", "CWSub6", c => c.Int(nullable: false));
            AlterColumn("dbo.NilaiSubCW", "CWSub5", c => c.Int(nullable: false));
            AlterColumn("dbo.NilaiSubCW", "CWSub4", c => c.Int(nullable: false));
            AlterColumn("dbo.NilaiSubCW", "CWSub3", c => c.Int(nullable: false));
            AlterColumn("dbo.NilaiSubCW", "CWSub2", c => c.Int(nullable: false));
            AlterColumn("dbo.NilaiSubCW", "CWSub1", c => c.Int(nullable: false));
            AlterColumn("dbo.Nilai", "Grade", c => c.String(maxLength: 8000, unicode: false));
            AlterColumn("dbo.Nilai", "NilaiTotal", c => c.Int(nullable: false));
            AlterColumn("dbo.Nilai", "Final", c => c.Int(nullable: false));
            AlterColumn("dbo.Nilai", "CW5", c => c.Int(nullable: false));
            AlterColumn("dbo.Nilai", "CW4", c => c.Int(nullable: false));
            AlterColumn("dbo.Nilai", "CW3", c => c.Int(nullable: false));
            AlterColumn("dbo.Nilai", "CW2", c => c.Int(nullable: false));
            AlterColumn("dbo.Nilai", "CW1", c => c.Int(nullable: false));
            AlterColumn("dbo.Nilai", "UTS", c => c.Int(nullable: false));
        }
    }
}
