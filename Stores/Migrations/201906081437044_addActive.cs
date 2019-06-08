namespace Stores.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addActive : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Produt_Price", "active", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Produt_Price", "active");
        }
    }
}
