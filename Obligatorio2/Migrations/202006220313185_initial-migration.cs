namespace Obligatorio2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initialmigration : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Products", "Importer_Tin", "dbo.Clients");
            DropForeignKey("dbo.Imports", "ImportingClient_Tin", "dbo.Clients");
            DropIndex("dbo.Imports", new[] { "ImportingClient_Tin" });
            DropIndex("dbo.Products", new[] { "Importer_Tin" });
            RenameColumn(table: "dbo.Imports", name: "ImportedProduct_Id", newName: "ProductId");
            RenameColumn(table: "dbo.Products", name: "Importer_Tin", newName: "Tin");
            RenameColumn(table: "dbo.Imports", name: "ImportingClient_Tin", newName: "Tin");
            RenameIndex(table: "dbo.Imports", name: "IX_ImportedProduct_Id", newName: "IX_ProductId");
            AlterColumn("dbo.Imports", "Tin", c => c.Long(nullable: false));
            AlterColumn("dbo.Products", "Tin", c => c.Long(nullable: false));
            CreateIndex("dbo.Imports", "Tin");
            CreateIndex("dbo.Products", "Tin");
            AddForeignKey("dbo.Products", "Tin", "dbo.Clients", "Tin", cascadeDelete: true);
            AddForeignKey("dbo.Imports", "Tin", "dbo.Clients", "Tin", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Imports", "Tin", "dbo.Clients");
            DropForeignKey("dbo.Products", "Tin", "dbo.Clients");
            DropIndex("dbo.Products", new[] { "Tin" });
            DropIndex("dbo.Imports", new[] { "Tin" });
            AlterColumn("dbo.Products", "Tin", c => c.Long());
            AlterColumn("dbo.Imports", "Tin", c => c.Long());
            RenameIndex(table: "dbo.Imports", name: "IX_ProductId", newName: "IX_ImportedProduct_Id");
            RenameColumn(table: "dbo.Imports", name: "Tin", newName: "ImportingClient_Tin");
            RenameColumn(table: "dbo.Products", name: "Tin", newName: "Importer_Tin");
            RenameColumn(table: "dbo.Imports", name: "ProductId", newName: "ImportedProduct_Id");
            CreateIndex("dbo.Products", "Importer_Tin");
            CreateIndex("dbo.Imports", "ImportingClient_Tin");
            AddForeignKey("dbo.Imports", "ImportingClient_Tin", "dbo.Clients", "Tin");
            AddForeignKey("dbo.Products", "Importer_Tin", "dbo.Clients", "Tin");
        }
    }
}
