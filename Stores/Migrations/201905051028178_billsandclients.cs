namespace Stores.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class billsandclients : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Bills",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        cost = c.Decimal(nullable: false, precision: 18, scale: 2),
                        date = c.DateTime(nullable: false),
                        discount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        status = c.String(),
                        Comment = c.String(),
                        Cate_Id_BillCate_ID = c.Int(),
                        Client_ID_Client_ID = c.Int(),
                        User_ID_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.BillsCategories", t => t.Cate_Id_BillCate_ID)
                .ForeignKey("dbo.Clients", t => t.Client_ID_Client_ID)
                .ForeignKey("dbo.Users", t => t.User_ID_Id)
                .Index(t => t.Cate_Id_BillCate_ID)
                .Index(t => t.Client_ID_Client_ID)
                .Index(t => t.User_ID_Id);
            
            CreateTable(
                "dbo.BillsCategories",
                c => new
                    {
                        BillCate_ID = c.Int(nullable: false, identity: true),
                        name = c.String(),
                        description = c.String(),
                    })
                .PrimaryKey(t => t.BillCate_ID);
            
            CreateTable(
                "dbo.Clients",
                c => new
                    {
                        Client_ID = c.Int(nullable: false, identity: true),
                        name = c.String(),
                        phone = c.String(),
                        nationalID = c.String(),
                        Address = c.String(),
                        Comment = c.String(),
                        Active = c.Boolean(nullable: false),
                        minimum_bills = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Clients_Type_ID_Clients_Type_id = c.Int(),
                    })
                .PrimaryKey(t => t.Client_ID)
                .ForeignKey("dbo.Clients_Type", t => t.Clients_Type_ID_Clients_Type_id)
                .Index(t => t.Clients_Type_ID_Clients_Type_id);
            
            CreateTable(
                "dbo.Clients_Type",
                c => new
                    {
                        Clients_Type_id = c.Int(nullable: false, identity: true),
                        name = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Clients_Type_id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Bills", "User_ID_Id", "dbo.Users");
            DropForeignKey("dbo.Bills", "Client_ID_Client_ID", "dbo.Clients");
            DropForeignKey("dbo.Clients", "Clients_Type_ID_Clients_Type_id", "dbo.Clients_Type");
            DropForeignKey("dbo.Bills", "Cate_Id_BillCate_ID", "dbo.BillsCategories");
            DropIndex("dbo.Clients", new[] { "Clients_Type_ID_Clients_Type_id" });
            DropIndex("dbo.Bills", new[] { "User_ID_Id" });
            DropIndex("dbo.Bills", new[] { "Client_ID_Client_ID" });
            DropIndex("dbo.Bills", new[] { "Cate_Id_BillCate_ID" });
            DropTable("dbo.Clients_Type");
            DropTable("dbo.Clients");
            DropTable("dbo.BillsCategories");
            DropTable("dbo.Bills");
        }
    }
}
