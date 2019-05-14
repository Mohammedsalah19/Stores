namespace Stores.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ssw : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.BillsContents", "Status", c => c.Boolean(nullable: true));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.BillsContents", "Status", c => c.String());
        }
    }
}
