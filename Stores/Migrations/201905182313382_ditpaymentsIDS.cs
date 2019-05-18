namespace Stores.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ditpaymentsIDS : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Payments", "client_id_Client_ID", "dbo.Clients");
            DropForeignKey("dbo.Payments", "user_id_Id", "dbo.Users");
            DropIndex("dbo.Payments", new[] { "client_id_Client_ID" });
            DropIndex("dbo.Payments", new[] { "user_id_Id" });
            AddColumn("dbo.Payments", "user_id", c => c.Int(nullable: false));
            AddColumn("dbo.Payments", "client_id", c => c.Int(nullable: false));
            DropColumn("dbo.Payments", "client_id_Client_ID");
            DropColumn("dbo.Payments", "user_id_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Payments", "user_id_Id", c => c.Int());
            AddColumn("dbo.Payments", "client_id_Client_ID", c => c.Int());
            DropColumn("dbo.Payments", "client_id");
            DropColumn("dbo.Payments", "user_id");
            CreateIndex("dbo.Payments", "user_id_Id");
            CreateIndex("dbo.Payments", "client_id_Client_ID");
            AddForeignKey("dbo.Payments", "user_id_Id", "dbo.Users", "Id");
            AddForeignKey("dbo.Payments", "client_id_Client_ID", "dbo.Clients", "Client_ID");
        }
    }
}
