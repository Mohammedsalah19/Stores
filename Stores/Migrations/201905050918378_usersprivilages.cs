namespace Stores.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class usersprivilages : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Users_Privileges",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        notifacations = c.Boolean(nullable: false),
                        salesbill = c.Boolean(nullable: false),
                        purchasebill = c.Boolean(nullable: false),
                        backbill = c.Boolean(nullable: false),
                        categories = c.Boolean(nullable: false),
                        products = c.Boolean(nullable: false),
                        expenses = c.Boolean(nullable: false),
                        payment = c.Boolean(nullable: false),
                        statistics = c.Boolean(nullable: false),
                        endday = c.Boolean(nullable: false),
                        sitting = c.Boolean(nullable: false),
                        users = c.Boolean(nullable: false),
                        clients = c.Boolean(nullable: false),
                        expenses_type = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Users_Privileges");
        }
    }
}
