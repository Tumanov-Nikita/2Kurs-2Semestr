namespace FabricService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class firstmigration : DbMigration
    {
        public override void Up()
        {
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
                .ForeignKey("dbo.Customers", t => t.CustomerId, cascadeDelete: true)
                .ForeignKey("dbo.Executers", t => t.ExecuterId)
                .ForeignKey("dbo.Stuffs", t => t.StuffId, cascadeDelete: true)
                .Index(t => t.CustomerId)
                .Index(t => t.StuffId)
                .Index(t => t.ExecuterId);
            
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CustomerFIO = c.String(nullable: false),
                        Mail = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Executers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ExecuterFIO = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
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
                "dbo.StuffParts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StuffId = c.Int(nullable: false),
                        PartId = c.Int(nullable: false),
                        Count = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Parts", t => t.PartId, cascadeDelete: true)
                .ForeignKey("dbo.Stuffs", t => t.StuffId, cascadeDelete: true)
                .Index(t => t.StuffId)
                .Index(t => t.PartId);
            
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
            
            CreateTable(
                "dbo.MessageInfoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MessageId = c.String(),
                        FromMailAddress = c.String(),
                        Subject = c.String(),
                        Body = c.String(),
                        DateDelivery = c.DateTime(nullable: false),
                        CustomerId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Customers", t => t.CustomerId)
                .Index(t => t.CustomerId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MessageInfoes", "CustomerId", "dbo.Customers");
            DropForeignKey("dbo.StuffParts", "StuffId", "dbo.Stuffs");
            DropForeignKey("dbo.StuffParts", "PartId", "dbo.Parts");
            DropForeignKey("dbo.StorageParts", "StorageId", "dbo.Storages");
            DropForeignKey("dbo.StorageParts", "PartId", "dbo.Parts");
            DropForeignKey("dbo.Bookings", "StuffId", "dbo.Stuffs");
            DropForeignKey("dbo.Bookings", "ExecuterId", "dbo.Executers");
            DropForeignKey("dbo.Bookings", "CustomerId", "dbo.Customers");
            DropIndex("dbo.MessageInfoes", new[] { "CustomerId" });
            DropIndex("dbo.StorageParts", new[] { "PartId" });
            DropIndex("dbo.StorageParts", new[] { "StorageId" });
            DropIndex("dbo.StuffParts", new[] { "PartId" });
            DropIndex("dbo.StuffParts", new[] { "StuffId" });
            DropIndex("dbo.Bookings", new[] { "ExecuterId" });
            DropIndex("dbo.Bookings", new[] { "StuffId" });
            DropIndex("dbo.Bookings", new[] { "CustomerId" });
            DropTable("dbo.MessageInfoes");
            DropTable("dbo.Storages");
            DropTable("dbo.StorageParts");
            DropTable("dbo.Parts");
            DropTable("dbo.StuffParts");
            DropTable("dbo.Stuffs");
            DropTable("dbo.Executers");
            DropTable("dbo.Customers");
            DropTable("dbo.Bookings");
        }
    }
}
