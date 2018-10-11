namespace LaptopWebSite.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddtblCategories : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.tblCategories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 250),
                        Image = c.String(maxLength: 250),
                        ParentId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.tblCategories", t => t.ParentId)
                .Index(t => t.ParentId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.tblCategories", "ParentId", "dbo.tblCategories");
            DropIndex("dbo.tblCategories", new[] { "ParentId" });
            DropTable("dbo.tblCategories");
        }
    }
}
