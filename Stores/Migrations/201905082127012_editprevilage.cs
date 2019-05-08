namespace Stores.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class editprevilage : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Users_Privileges", "User_ID_Id", "dbo.Users");
            DropIndex("dbo.Users_Privileges", new[] { "User_ID_Id" });
            DropColumn("dbo.Users_Privileges", "User_ID_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users_Privileges", "User_ID_Id", c => c.Int());
            CreateIndex("dbo.Users_Privileges", "User_ID_Id");
            AddForeignKey("dbo.Users_Privileges", "User_ID_Id", "dbo.Users", "Id");
        }
    }
}
