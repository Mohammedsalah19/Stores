namespace Stores.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class editexpense : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Expenses", "ExpensesType_ID_ExpensesType_ID", "dbo.ExpensesTypes");
            DropForeignKey("dbo.Expenses", "User_ID_Id", "dbo.Users");
            DropIndex("dbo.Expenses", new[] { "ExpensesType_ID_ExpensesType_ID" });
            DropIndex("dbo.Expenses", new[] { "User_ID_Id" });
            AddColumn("dbo.Expenses", "ExpensesType_ID", c => c.Int(nullable: false));
            AddColumn("dbo.Expenses", "User_ID", c => c.Int(nullable: false));
            DropColumn("dbo.Expenses", "ExpensesType_ID_ExpensesType_ID");
            DropColumn("dbo.Expenses", "User_ID_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Expenses", "User_ID_Id", c => c.Int());
            AddColumn("dbo.Expenses", "ExpensesType_ID_ExpensesType_ID", c => c.Int());
            DropColumn("dbo.Expenses", "User_ID");
            DropColumn("dbo.Expenses", "ExpensesType_ID");
            CreateIndex("dbo.Expenses", "User_ID_Id");
            CreateIndex("dbo.Expenses", "ExpensesType_ID_ExpensesType_ID");
            AddForeignKey("dbo.Expenses", "User_ID_Id", "dbo.Users", "Id");
            AddForeignKey("dbo.Expenses", "ExpensesType_ID_ExpensesType_ID", "dbo.ExpensesTypes", "ExpensesType_ID");
        }
    }
}
