namespace Stores.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ddse : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Produt_Price", "Store_Id_Store_Id", "dbo.Storehouses");
            DropIndex("dbo.Produt_Price", new[] { "Store_Id_Store_Id" });
            AddColumn("dbo.Produt_Price", "Store_Id", c => c.Int(nullable: false));
            AlterColumn("dbo.Produt_Price", "Minmum", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            DropColumn("dbo.Produt_Price", "Store_Id_Store_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Produt_Price", "Store_Id_Store_Id", c => c.Int());
            AlterColumn("dbo.Produt_Price", "Minmum", c => c.Int(nullable: false));
            DropColumn("dbo.Produt_Price", "Store_Id");
            CreateIndex("dbo.Produt_Price", "Store_Id_Store_Id");
            AddForeignKey("dbo.Produt_Price", "Store_Id_Store_Id", "dbo.Storehouses", "Store_Id");
        }
    }
}
