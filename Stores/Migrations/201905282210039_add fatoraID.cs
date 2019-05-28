namespace Stores.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addfatoraID : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Payments", "fatoraID", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Payments", "fatoraID");
        }
    }
}
