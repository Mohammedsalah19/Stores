namespace Stores.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fffe : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.BillsContents", "Bill_ID_Id", "dbo.Bills");
            DropForeignKey("dbo.BillsContents", "Product_ID_Pro_id", "dbo.Products");
            DropIndex("dbo.BillsContents", new[] { "Bill_ID_Id" });
            DropIndex("dbo.BillsContents", new[] { "Product_ID_Pro_id" });
            AddColumn("dbo.BillsContents", "Bill_ID", c => c.Int(nullable: false));
            AddColumn("dbo.BillsContents", "Product_ID", c => c.Int(nullable: false));
            DropColumn("dbo.BillsContents", "Bill_ID_Id");
            DropColumn("dbo.BillsContents", "Product_ID_Pro_id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.BillsContents", "Product_ID_Pro_id", c => c.Int());
            AddColumn("dbo.BillsContents", "Bill_ID_Id", c => c.Int());
            DropColumn("dbo.BillsContents", "Product_ID");
            DropColumn("dbo.BillsContents", "Bill_ID");
            CreateIndex("dbo.BillsContents", "Product_ID_Pro_id");
            CreateIndex("dbo.BillsContents", "Bill_ID_Id");
            AddForeignKey("dbo.BillsContents", "Product_ID_Pro_id", "dbo.Products", "Pro_id");
            AddForeignKey("dbo.BillsContents", "Bill_ID_Id", "dbo.Bills", "Id");
        }
    }
}
