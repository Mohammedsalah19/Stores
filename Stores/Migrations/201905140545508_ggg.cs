namespace Stores.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ggg : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Bills", "Cate_Id_BillCate_ID", "dbo.BillsCategories");
            DropForeignKey("dbo.Bills", "Client_ID_Client_ID", "dbo.Clients");
            DropForeignKey("dbo.Bills", "User_ID_Id", "dbo.Users");
            DropIndex("dbo.Bills", new[] { "Cate_Id_BillCate_ID" });
            DropIndex("dbo.Bills", new[] { "Client_ID_Client_ID" });
            DropIndex("dbo.Bills", new[] { "User_ID_Id" });
            AddColumn("dbo.Bills", "User_ID", c => c.Int(nullable: false));
            AddColumn("dbo.Bills", "Client_ID", c => c.Int(nullable: false));
            AddColumn("dbo.Bills", "Cate_Id", c => c.Int(nullable: false));
            DropColumn("dbo.Bills", "Cate_Id_BillCate_ID");
            DropColumn("dbo.Bills", "Client_ID_Client_ID");
            DropColumn("dbo.Bills", "User_ID_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Bills", "User_ID_Id", c => c.Int());
            AddColumn("dbo.Bills", "Client_ID_Client_ID", c => c.Int());
            AddColumn("dbo.Bills", "Cate_Id_BillCate_ID", c => c.Int());
            DropColumn("dbo.Bills", "Cate_Id");
            DropColumn("dbo.Bills", "Client_ID");
            DropColumn("dbo.Bills", "User_ID");
            CreateIndex("dbo.Bills", "User_ID_Id");
            CreateIndex("dbo.Bills", "Client_ID_Client_ID");
            CreateIndex("dbo.Bills", "Cate_Id_BillCate_ID");
            AddForeignKey("dbo.Bills", "User_ID_Id", "dbo.Users", "Id");
            AddForeignKey("dbo.Bills", "Client_ID_Client_ID", "dbo.Clients", "Client_ID");
            AddForeignKey("dbo.Bills", "Cate_Id_BillCate_ID", "dbo.BillsCategories", "BillCate_ID");
        }
    }
}
