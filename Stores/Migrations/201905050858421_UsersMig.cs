namespace Stores.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UsersMig : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        username = c.String(),
                        Password = c.String(),
                        name = c.String(),
                        phone = c.String(),
                        national_id = c.String(),
                        active = c.Boolean(nullable: false),
                        printer_name = c.String(),
                        user_current_date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Users");
        }
    }
}
