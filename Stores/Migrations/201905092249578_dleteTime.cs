namespace Stores.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dleteTime : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Users", "user_current_date");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "user_current_date", c => c.DateTime(nullable: false));
        }
    }
}
