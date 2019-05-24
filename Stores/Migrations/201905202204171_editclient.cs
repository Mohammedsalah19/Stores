namespace Stores.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class editclient : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Clients", "Clients_Type_ID_Clients_Type_id", "dbo.Clients_Type");
            DropIndex("dbo.Clients", new[] { "Clients_Type_ID_Clients_Type_id" });
            AddColumn("dbo.Clients", "Clients_Type_ID", c => c.Int(nullable: false));
            DropColumn("dbo.Clients", "Clients_Type_ID_Clients_Type_id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Clients", "Clients_Type_ID_Clients_Type_id", c => c.Int());
            DropColumn("dbo.Clients", "Clients_Type_ID");
            CreateIndex("dbo.Clients", "Clients_Type_ID_Clients_Type_id");
            AddForeignKey("dbo.Clients", "Clients_Type_ID_Clients_Type_id", "dbo.Clients_Type", "Clients_Type_id");
        }
    }
}
