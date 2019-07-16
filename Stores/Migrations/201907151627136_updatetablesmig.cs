namespace Stores.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatetablesmig : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tables", "name", c => c.String());
            AddColumn("dbo.Tables", "values", c => c.String());
            DropColumn("dbo.Tables", "Bills");
            DropColumn("dbo.Tables", "BillsCategory");
            DropColumn("dbo.Tables", "BillsContent");
            DropColumn("dbo.Tables", "Clients");
            DropColumn("dbo.Tables", "Clients_Type");
            DropColumn("dbo.Tables", "Expenses");
            DropColumn("dbo.Tables", "ExpensesType");
            DropColumn("dbo.Tables", "Payments");
            DropColumn("dbo.Tables", "PlaceInfo");
            DropColumn("dbo.Tables", "Products");
            DropColumn("dbo.Tables", "ProductCategory");
            DropColumn("dbo.Tables", "Product_Price");
            DropColumn("dbo.Tables", "Storehouse");
            DropColumn("dbo.Tables", "Users");
            DropColumn("dbo.Tables", "Users_Privileges");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Tables", "Users_Privileges", c => c.String());
            AddColumn("dbo.Tables", "Users", c => c.String());
            AddColumn("dbo.Tables", "Storehouse", c => c.String());
            AddColumn("dbo.Tables", "Product_Price", c => c.String());
            AddColumn("dbo.Tables", "ProductCategory", c => c.String());
            AddColumn("dbo.Tables", "Products", c => c.String());
            AddColumn("dbo.Tables", "PlaceInfo", c => c.String());
            AddColumn("dbo.Tables", "Payments", c => c.String());
            AddColumn("dbo.Tables", "ExpensesType", c => c.String());
            AddColumn("dbo.Tables", "Expenses", c => c.String());
            AddColumn("dbo.Tables", "Clients_Type", c => c.String());
            AddColumn("dbo.Tables", "Clients", c => c.String());
            AddColumn("dbo.Tables", "BillsContent", c => c.String());
            AddColumn("dbo.Tables", "BillsCategory", c => c.String());
            AddColumn("dbo.Tables", "Bills", c => c.String());
            DropColumn("dbo.Tables", "values");
            DropColumn("dbo.Tables", "name");
        }
    }
}
