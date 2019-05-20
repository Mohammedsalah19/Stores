namespace Stores.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addView : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Bills", "Viewed", c => c.Boolean(nullable: false));
            AddColumn("dbo.BillsContents", "Viewed", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.BillsContents", "Viewed");
            DropColumn("dbo.Bills", "Viewed");
        }
    }
}
