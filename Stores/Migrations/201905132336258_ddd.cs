namespace Stores.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ddd : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Produt_Price", "Quantity", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Produt_Price", "Quantity", c => c.Int(nullable: false));
        }
    }
}
