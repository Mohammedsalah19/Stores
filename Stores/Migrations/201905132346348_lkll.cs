namespace Stores.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class lkll : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Produt_Price", "Discount", c => c.Decimal(precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Produt_Price", "Discount", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
    }
}
