namespace Stores.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class jjk : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users_Privileges", "User_ID", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users_Privileges", "User_ID");
        }
    }
}
