namespace Stores.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class placeInfo : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PLaceInfoes",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        PlaceName = c.String(),
                        description = c.String(),
                        Number = c.String(),
                        Img = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.PLaceInfoes");
        }
    }
}
