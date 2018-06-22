namespace FabricService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FirstMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.StuffParts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StuffId = c.Int(nullable: false),
                        PartId = c.Int(nullable: false),
                        Count = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Stuffs", t => t.StuffId, cascadeDelete: true)
                .ForeignKey("dbo.Parts", t => t.PartId, cascadeDelete: true)
                .Index(t => t.StuffId)
                .Index(t => t.PartId);
            
            CreateTable(
                "dbo.Stuffs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StuffName = c.String(nullable: false),
                        Cost = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Bookings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CustomerId = c.Int(nullable: false),
                        StuffId = c.Int(nullable: false),
                        ExecuterId = c.Int(),
                        Count = c.Int(nullable: false),
                        Cost = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Status = c.Int(nullable: false),
                        DateBegin = c.DateTime(nullable: false),
                        DateBuilt = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Stuffs", t => t.StuffId, cascadeDelete: true)
                .ForeignKey("dbo.Executers", t => t.ExecuterId)
                .ForeignKey("dbo.Customers", t => t.CustomerId, cascadeDelete: true)
                .Index(t => t.CustomerId)
                .Index(t => t.StuffId)
                .Index(t => t.ExecuterId);
            
            CreateTable(
                "dbo.Executers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ExecuterFIO = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CustomerFIO = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Parts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PartName = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.StorageParts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StorageId = c.Int(nullable: false),
                        PartId = c.Int(nullable: false),
                        Count = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Parts", t => t.PartId, cascadeDelete: true)
                .ForeignKey("dbo.Storages", t => t.StorageId, cascadeDelete: true)
                .Index(t => t.StorageId)
                .Index(t => t.PartId);
            
            CreateTable(
                "dbo.Storages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StorageName = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.StorageParts", "StorageId", "dbo.Storages");
            DropForeignKey("dbo.StorageParts", "PartId", "dbo.Parts");
            DropForeignKey("dbo.StuffParts", "PartId", "dbo.Parts");
            DropForeignKey("dbo.Bookings", "CustomerId", "dbo.Customers");
            DropForeignKey("dbo.Bookings", "ExecuterId", "dbo.Executers");
            DropForeignKey("dbo.Bookings", "StuffId", "dbo.Stuffs");
            DropForeignKey("dbo.StuffParts", "StuffId", "dbo.Stuffs");
            DropIndex("dbo.StorageParts", new[] { "PartId" });
            DropIndex("dbo.StorageParts", new[] { "StorageId" });
            DropIndex("dbo.Bookings", new[] { "ExecuterId" });
            DropIndex("dbo.Bookings", new[] { "StuffId" });
            DropIndex("dbo.Bookings", new[] { "CustomerId" });
            DropIndex("dbo.StuffParts", new[] { "PartId" });
            DropIndex("dbo.StuffParts", new[] { "StuffId" });
            DropTable("dbo.Storages");
            DropTable("dbo.StorageParts");
            DropTable("dbo.Parts");
            DropTable("dbo.Customers");
            DropTable("dbo.Executers");
            DropTable("dbo.Bookings");
            DropTable("dbo.Stuffs");
            DropTable("dbo.StuffParts");
        }
    }
}
