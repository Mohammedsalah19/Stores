namespace Stores.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BillsContent : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BillsContents",
                c => new
                    {
                        BillsContent_ID = c.Int(nullable: false, identity: true),
                        Quantity = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Cost = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Status = c.String(),
                        Comment = c.String(),
                        Bill_ID_Id = c.Int(),
                        Product_ID_Pro_id = c.Int(),
                    })
                .PrimaryKey(t => t.BillsContent_ID)
                .ForeignKey("dbo.Bills", t => t.Bill_ID_Id)
                .ForeignKey("dbo.Products", t => t.Product_ID_Pro_id)
                .Index(t => t.Bill_ID_Id)
                .Index(t => t.Product_ID_Pro_id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BillsContents", "Product_ID_Pro_id", "dbo.Products");
            DropForeignKey("dbo.BillsContents", "Bill_ID_Id", "dbo.Bills");
            DropIndex("dbo.BillsContents", new[] { "Product_ID_Pro_id" });
            DropIndex("dbo.BillsContents", new[] { "Bill_ID_Id" });
            DropTable("dbo.BillsContents");
        }
    }
}
