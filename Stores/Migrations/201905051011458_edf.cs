namespace Stores.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class edf : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ProductCategories",
                c => new
                    {
                        Cate_ID = c.Int(nullable: false, identity: true),
                        name = c.String(),
                        description = c.String(),
                    })
                .PrimaryKey(t => t.Cate_ID);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        Pro_id = c.Int(nullable: false, identity: true),
                        name = c.String(),
                        barcode = c.String(),
                        active = c.Boolean(nullable: false),
                        Cate_ID_Cate_ID = c.Int(),
                    })
                .PrimaryKey(t => t.Pro_id)
                .ForeignKey("dbo.ProductCategories", t => t.Cate_ID_Cate_ID)
                .Index(t => t.Cate_ID_Cate_ID);
            
            CreateTable(
                "dbo.Produt_Price",
                c => new
                    {
                        Prd_Pri_ID = c.Int(nullable: false, identity: true),
                        cost = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        many_price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Quantity = c.Int(nullable: false),
                        Minmum = c.Int(nullable: false),
                        Discount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Pro_ID_Pro_id = c.Int(),
                        Store_Id_Store_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Prd_Pri_ID)
                .ForeignKey("dbo.Products", t => t.Pro_ID_Pro_id)
                .ForeignKey("dbo.Storehouses", t => t.Store_Id_Store_Id)
                .Index(t => t.Pro_ID_Pro_id)
                .Index(t => t.Store_Id_Store_Id);
            
            CreateTable(
                "dbo.Storehouses",
                c => new
                    {
                        Store_Id = c.Int(nullable: false, identity: true),
                        name = c.String(),
                        place = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Store_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Produt_Price", "Store_Id_Store_Id", "dbo.Storehouses");
            DropForeignKey("dbo.Produt_Price", "Pro_ID_Pro_id", "dbo.Products");
            DropForeignKey("dbo.Products", "Cate_ID_Cate_ID", "dbo.ProductCategories");
            DropIndex("dbo.Produt_Price", new[] { "Store_Id_Store_Id" });
            DropIndex("dbo.Produt_Price", new[] { "Pro_ID_Pro_id" });
            DropIndex("dbo.Products", new[] { "Cate_ID_Cate_ID" });
            DropTable("dbo.Storehouses");
            DropTable("dbo.Produt_Price");
            DropTable("dbo.Products");
            DropTable("dbo.ProductCategories");
        }
    }
}
