namespace Stores.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dd : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Products", "Cate_ID_Cate_ID", "dbo.ProductCategories");
            DropIndex("dbo.Products", new[] { "Cate_ID_Cate_ID" });
            AddColumn("dbo.Products", "Cate_ID", c => c.Int(nullable: false));
            DropColumn("dbo.Products", "Cate_ID_Cate_ID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Products", "Cate_ID_Cate_ID", c => c.Int());
            DropColumn("dbo.Products", "Cate_ID");
            CreateIndex("dbo.Products", "Cate_ID_Cate_ID");
            AddForeignKey("dbo.Products", "Cate_ID_Cate_ID", "dbo.ProductCategories", "Cate_ID");
        }
    }
}
