namespace Stores.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class editPrinter : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PrintTypes", "PrinterName", c => c.String());
            DropColumn("dbo.PrintTypes", "Name");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PrintTypes", "Name", c => c.String());
            DropColumn("dbo.PrintTypes", "PrinterName");
        }
    }
}
