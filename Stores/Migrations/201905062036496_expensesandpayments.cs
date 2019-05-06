namespace Stores.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class expensesandpayments : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Expenses",
                c => new
                    {
                        Expenses_ID = c.Int(nullable: false, identity: true),
                        amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        date = c.DateTime(nullable: false),
                        comment = c.String(),
                        ExpensesType_ID_ExpensesType_ID = c.Int(),
                        User_ID_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Expenses_ID)
                .ForeignKey("dbo.ExpensesTypes", t => t.ExpensesType_ID_ExpensesType_ID)
                .ForeignKey("dbo.Users", t => t.User_ID_Id)
                .Index(t => t.ExpensesType_ID_ExpensesType_ID)
                .Index(t => t.User_ID_Id);
            
            CreateTable(
                "dbo.ExpensesTypes",
                c => new
                    {
                        ExpensesType_ID = c.Int(nullable: false, identity: true),
                        name = c.String(),
                        comment = c.String(),
                    })
                .PrimaryKey(t => t.ExpensesType_ID);
            
            CreateTable(
                "dbo.Payments",
                c => new
                    {
                        Payments_ID = c.Int(nullable: false, identity: true),
                        Payment_amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        date = c.DateTime(nullable: false),
                        client_id_Client_ID = c.Int(),
                        user_id_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Payments_ID)
                .ForeignKey("dbo.Clients", t => t.client_id_Client_ID)
                .ForeignKey("dbo.Users", t => t.user_id_Id)
                .Index(t => t.client_id_Client_ID)
                .Index(t => t.user_id_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Payments", "user_id_Id", "dbo.Users");
            DropForeignKey("dbo.Payments", "client_id_Client_ID", "dbo.Clients");
            DropForeignKey("dbo.Expenses", "User_ID_Id", "dbo.Users");
            DropForeignKey("dbo.Expenses", "ExpensesType_ID_ExpensesType_ID", "dbo.ExpensesTypes");
            DropIndex("dbo.Payments", new[] { "user_id_Id" });
            DropIndex("dbo.Payments", new[] { "client_id_Client_ID" });
            DropIndex("dbo.Expenses", new[] { "User_ID_Id" });
            DropIndex("dbo.Expenses", new[] { "ExpensesType_ID_ExpensesType_ID" });
            DropTable("dbo.Payments");
            DropTable("dbo.ExpensesTypes");
            DropTable("dbo.Expenses");
        }
    }
}
