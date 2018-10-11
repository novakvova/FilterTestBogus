namespace LaptopWebSite.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Addtblfilter : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.tblFilterNameGroups",
                c => new
                    {
                        FilterNameId = c.Int(nullable: false),
                        FilterValueId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.FilterNameId, t.FilterValueId })
                .ForeignKey("dbo.tblFilterNames", t => t.FilterNameId, cascadeDelete: true)
                .ForeignKey("dbo.tblFilterValues", t => t.FilterValueId, cascadeDelete: true)
                .Index(t => t.FilterNameId)
                .Index(t => t.FilterValueId);
            
            CreateTable(
                "dbo.tblFilterNames",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 250),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.tblFilters",
                c => new
                    {
                        FilterNameId = c.Int(nullable: false),
                        FilterValueId = c.Int(nullable: false),
                        ProductId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.FilterNameId, t.FilterValueId, t.ProductId })
                .ForeignKey("dbo.tblFilterNames", t => t.FilterNameId, cascadeDelete: true)
                .ForeignKey("dbo.tblFilterValues", t => t.FilterValueId, cascadeDelete: true)
                .ForeignKey("dbo.tlProducts", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.FilterNameId)
                .Index(t => t.FilterValueId)
                .Index(t => t.ProductId);
            
            CreateTable(
                "dbo.tblFilterValues",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 250),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.tblFilterNameGroups", "FilterValueId", "dbo.tblFilterValues");
            DropForeignKey("dbo.tblFilterNameGroups", "FilterNameId", "dbo.tblFilterNames");
            DropForeignKey("dbo.tblFilters", "ProductId", "dbo.tlProducts");
            DropForeignKey("dbo.tblFilters", "FilterValueId", "dbo.tblFilterValues");
            DropForeignKey("dbo.tblFilters", "FilterNameId", "dbo.tblFilterNames");
            DropIndex("dbo.tblFilters", new[] { "ProductId" });
            DropIndex("dbo.tblFilters", new[] { "FilterValueId" });
            DropIndex("dbo.tblFilters", new[] { "FilterNameId" });
            DropIndex("dbo.tblFilterNameGroups", new[] { "FilterValueId" });
            DropIndex("dbo.tblFilterNameGroups", new[] { "FilterNameId" });
            DropTable("dbo.tblFilterValues");
            DropTable("dbo.tblFilters");
            DropTable("dbo.tblFilterNames");
            DropTable("dbo.tblFilterNameGroups");
        }
    }
}
