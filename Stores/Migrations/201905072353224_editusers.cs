namespace Stores.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class editusers : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Users", "privilageID_id", "dbo.Users_Privileges");
            DropIndex("dbo.Users", new[] { "privilageID_id" });
            AddColumn("dbo.Users_Privileges", "User_ID_Id", c => c.Int());
            CreateIndex("dbo.Users_Privileges", "User_ID_Id");
            AddForeignKey("dbo.Users_Privileges", "User_ID_Id", "dbo.Users", "Id");
            DropColumn("dbo.Users", "privilageID_id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "privilageID_id", c => c.Int());
            DropForeignKey("dbo.Users_Privileges", "User_ID_Id", "dbo.Users");
            DropIndex("dbo.Users_Privileges", new[] { "User_ID_Id" });
            DropColumn("dbo.Users_Privileges", "User_ID_Id");
            CreateIndex("dbo.Users", "privilageID_id");
            AddForeignKey("dbo.Users", "privilageID_id", "dbo.Users_Privileges", "id");
        }
    }
}
