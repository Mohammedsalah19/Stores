namespace Stores.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class connectusers : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "privilageID_id", c => c.Int());
            CreateIndex("dbo.Users", "privilageID_id");
            AddForeignKey("dbo.Users", "privilageID_id", "dbo.Users_Privileges", "id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Users", "privilageID_id", "dbo.Users_Privileges");
            DropIndex("dbo.Users", new[] { "privilageID_id" });
            DropColumn("dbo.Users", "privilageID_id");
        }
    }
}
