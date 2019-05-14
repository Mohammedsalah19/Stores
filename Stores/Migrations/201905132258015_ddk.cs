namespace Stores.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ddk : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Produt_Price", "Pro_ID_Pro_id", "dbo.Products");
            DropIndex("dbo.Produt_Price", new[] { "Pro_ID_Pro_id" });
            AddColumn("dbo.Produt_Price", "Pro_ID", c => c.Int(nullable: false));
            DropColumn("dbo.Produt_Price", "Pro_ID_Pro_id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Produt_Price", "Pro_ID_Pro_id", c => c.Int());
            DropColumn("dbo.Produt_Price", "Pro_ID");
            CreateIndex("dbo.Produt_Price", "Pro_ID_Pro_id");
            AddForeignKey("dbo.Produt_Price", "Pro_ID_Pro_id", "dbo.Products", "Pro_id");
        }
    }
}
