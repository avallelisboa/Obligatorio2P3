namespace Obligatorio2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Clients",
                c => new
                    {
                        Tin = c.Long(nullable: false),
                        Name = c.String(nullable: false, maxLength: 30),
                        Seniority = c.Int(nullable: false),
                        Discount = c.Int(nullable: false),
                        RegisterDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Tin);
            
            CreateTable(
                "dbo.Imports",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProductId = c.String(nullable: false, maxLength: 128),
                        Ammount = c.Int(nullable: false),
                        PriceByUnit = c.Int(nullable: false),
                        EntryDate = c.DateTime(nullable: false),
                        DepartureDate = c.DateTime(nullable: false),
                        IsStored = c.Boolean(nullable: false),
                        Client_Tin = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .ForeignKey("dbo.Clients", t => t.Client_Tin)
                .Index(t => t.ProductId)
                .Index(t => t.Client_Tin);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        ProductId = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 50),
                        Weight = c.Int(nullable: false),
                        ClientTin = c.Long(nullable: false),
                        Ammount = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ProductId)
                .ForeignKey("dbo.Clients", t => t.ClientTin, cascadeDelete: true)
                .Index(t => t.ClientTin);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Role = c.String(maxLength: 10),
                        Password = c.String(maxLength: 30),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Imports", "Client_Tin", "dbo.Clients");
            DropForeignKey("dbo.Imports", "ProductId", "dbo.Products");
            DropForeignKey("dbo.Products", "ClientTin", "dbo.Clients");
            DropIndex("dbo.Products", new[] { "ClientTin" });
            DropIndex("dbo.Imports", new[] { "Client_Tin" });
            DropIndex("dbo.Imports", new[] { "ProductId" });
            DropTable("dbo.Users");
            DropTable("dbo.Products");
            DropTable("dbo.Imports");
            DropTable("dbo.Clients");
        }
    }
}
