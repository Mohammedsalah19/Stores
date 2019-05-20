namespace Stores.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mml : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.BillsContents", "IsDeleted", c => c.Boolean(nullable: false));
            DropColumn("dbo.BillsContents", "Status");
        }
        
        public override void Down()
        {
            AddColumn("dbo.BillsContents", "Status", c => c.Boolean(nullable: false));
            DropColumn("dbo.BillsContents", "IsDeleted");
        }
    }
}
