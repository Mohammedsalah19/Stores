namespace Stores.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class edituses : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "PicPath", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "PicPath");
        }
    }
}
