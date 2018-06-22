namespace FabricService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Firstmigration : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ArticleParts", "ArticleId", "dbo.Articles");
            DropForeignKey("dbo.Contracts", "ArticleId", "dbo.Articles");
            DropForeignKey("dbo.Contracts", "BuilderId", "dbo.Builders");
            DropForeignKey("dbo.Contracts", "CustomerId", "dbo.Customers");
            DropForeignKey("dbo.ArticleParts", "PartId", "dbo.Parts");
            DropIndex("dbo.ArticleParts", new[] { "ArticleId" });
            DropIndex("dbo.ArticleParts", new[] { "PartId" });
            DropIndex("dbo.Contracts", new[] { "CustomerId" });
            DropIndex("dbo.Contracts", new[] { "ArticleId" });
            DropIndex("dbo.Contracts", new[] { "BuilderId" });
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
            
            DropTable("dbo.ArticleParts");
            DropTable("dbo.Articles");
            DropTable("dbo.Contracts");
            DropTable("dbo.Builders");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Builders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BuilderFIO = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Contracts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CustomerId = c.Int(nullable: false),
                        ArticleId = c.Int(nullable: false),
                        BuilderId = c.Int(),
                        Count = c.Int(nullable: false),
                        Cost = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Status = c.Int(nullable: false),
                        DateBegin = c.DateTime(nullable: false),
                        DateBuilt = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Articles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ArticleName = c.String(nullable: false),
                        Cost = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ArticleParts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ArticleId = c.Int(nullable: false),
                        PartId = c.Int(nullable: false),
                        Count = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            DropForeignKey("dbo.StuffParts", "StuffId", "dbo.Stuffs");
            DropForeignKey("dbo.StuffParts", "PartId", "dbo.Parts");
            DropForeignKey("dbo.Bookings", "StuffId", "dbo.Stuffs");
            DropForeignKey("dbo.Bookings", "ExecuterId", "dbo.Executers");
            DropForeignKey("dbo.Bookings", "CustomerId", "dbo.Customers");
            DropIndex("dbo.StuffParts", new[] { "PartId" });
            DropIndex("dbo.StuffParts", new[] { "StuffId" });
            DropIndex("dbo.Bookings", new[] { "ExecuterId" });
            DropIndex("dbo.Bookings", new[] { "StuffId" });
            DropIndex("dbo.Bookings", new[] { "CustomerId" });
            DropTable("dbo.StuffParts");
            DropTable("dbo.Stuffs");
            DropTable("dbo.Executers");
            DropTable("dbo.Bookings");
            CreateIndex("dbo.Contracts", "BuilderId");
            CreateIndex("dbo.Contracts", "ArticleId");
            CreateIndex("dbo.Contracts", "CustomerId");
            CreateIndex("dbo.ArticleParts", "PartId");
            CreateIndex("dbo.ArticleParts", "ArticleId");
            AddForeignKey("dbo.ArticleParts", "PartId", "dbo.Parts", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Contracts", "CustomerId", "dbo.Customers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Contracts", "BuilderId", "dbo.Builders", "Id");
            AddForeignKey("dbo.Contracts", "ArticleId", "dbo.Articles", "Id", cascadeDelete: true);
            AddForeignKey("dbo.ArticleParts", "ArticleId", "dbo.Articles", "Id", cascadeDelete: true);
        }
    }
}
