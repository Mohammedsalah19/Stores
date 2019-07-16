namespace Stores.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tablesmif : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Tables",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Bills = c.String(),
                        BillsCategory = c.String(),
                        BillsContent = c.String(),
                        Clients = c.String(),
                        Clients_Type = c.String(),
                        Expenses = c.String(),
                        ExpensesType = c.String(),
                        Payments = c.String(),
                        PlaceInfo = c.String(),
                        Products = c.String(),
                        ProductCategory = c.String(),
                        Product_Price = c.String(),
                        Storehouse = c.String(),
                        Users = c.String(),
                        Users_Privileges = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Tables");
        }
    }
}
