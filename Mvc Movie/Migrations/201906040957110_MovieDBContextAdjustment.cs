namespace Mvc_Movie.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MovieDBContextAdjustment : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Certifiers", "ParentMovie_ID", "dbo.Movies");
            DropIndex("dbo.Certifiers", new[] { "ParentMovie_ID" });
            AlterColumn("dbo.Certifiers", "ParentMovie_ID", c => c.Int(nullable: false));
            CreateIndex("dbo.Certifiers", "ParentMovie_ID");
            AddForeignKey("dbo.Certifiers", "ParentMovie_ID", "dbo.Movies", "ID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Certifiers", "ParentMovie_ID", "dbo.Movies");
            DropIndex("dbo.Certifiers", new[] { "ParentMovie_ID" });
            AlterColumn("dbo.Certifiers", "ParentMovie_ID", c => c.Int());
            CreateIndex("dbo.Certifiers", "ParentMovie_ID");
            AddForeignKey("dbo.Certifiers", "ParentMovie_ID", "dbo.Movies", "ID");
        }
    }
}
