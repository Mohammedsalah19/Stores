namespace Stores.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class printtype : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PrintTypes",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.PrintTypes");
        }
    }
}
