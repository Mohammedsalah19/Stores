namespace Stores.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ffffffff : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.BillsWithExtens", "billContentY_BillsContent_ID", c => c.Int());
            AddColumn("dbo.BillsWithExtens", "billsY_Id", c => c.Int());
            AddColumn("dbo.BillsWithExtens", "ClientsY_Client_ID", c => c.Int());
            AddColumn("dbo.BillsWithExtens", "prodCategoryY_Cate_ID", c => c.Int());
            AddColumn("dbo.BillsWithExtens", "productY_Pro_id", c => c.Int());
            CreateIndex("dbo.BillsWithExtens", "billContentY_BillsContent_ID");
            CreateIndex("dbo.BillsWithExtens", "billsY_Id");
            CreateIndex("dbo.BillsWithExtens", "ClientsY_Client_ID");
            CreateIndex("dbo.BillsWithExtens", "prodCategoryY_Cate_ID");
            CreateIndex("dbo.BillsWithExtens", "productY_Pro_id");
            AddForeignKey("dbo.BillsWithExtens", "billContentY_BillsContent_ID", "dbo.BillsContents", "BillsContent_ID");
            AddForeignKey("dbo.BillsWithExtens", "billsY_Id", "dbo.Bills", "Id");
            AddForeignKey("dbo.BillsWithExtens", "ClientsY_Client_ID", "dbo.Clients", "Client_ID");
            AddForeignKey("dbo.BillsWithExtens", "prodCategoryY_Cate_ID", "dbo.ProductCategories", "Cate_ID");
            AddForeignKey("dbo.BillsWithExtens", "productY_Pro_id", "dbo.Products", "Pro_id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BillsWithExtens", "productY_Pro_id", "dbo.Products");
            DropForeignKey("dbo.BillsWithExtens", "prodCategoryY_Cate_ID", "dbo.ProductCategories");
            DropForeignKey("dbo.BillsWithExtens", "ClientsY_Client_ID", "dbo.Clients");
            DropForeignKey("dbo.BillsWithExtens", "billsY_Id", "dbo.Bills");
            DropForeignKey("dbo.BillsWithExtens", "billContentY_BillsContent_ID", "dbo.BillsContents");
            DropIndex("dbo.BillsWithExtens", new[] { "productY_Pro_id" });
            DropIndex("dbo.BillsWithExtens", new[] { "prodCategoryY_Cate_ID" });
            DropIndex("dbo.BillsWithExtens", new[] { "ClientsY_Client_ID" });
            DropIndex("dbo.BillsWithExtens", new[] { "billsY_Id" });
            DropIndex("dbo.BillsWithExtens", new[] { "billContentY_BillsContent_ID" });
            DropColumn("dbo.BillsWithExtens", "productY_Pro_id");
            DropColumn("dbo.BillsWithExtens", "prodCategoryY_Cate_ID");
            DropColumn("dbo.BillsWithExtens", "ClientsY_Client_ID");
            DropColumn("dbo.BillsWithExtens", "billsY_Id");
            DropColumn("dbo.BillsWithExtens", "billContentY_BillsContent_ID");
        }
    }
}
