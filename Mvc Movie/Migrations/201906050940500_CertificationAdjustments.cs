namespace Mvc_Movie.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CertificationAdjustments : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Certifiers", "Certification_ID", "dbo.Restrictions");
            DropForeignKey("dbo.Certifiers", "ParentMovie_ID", "dbo.Movies");
            DropIndex("dbo.Certifiers", new[] { "Certification_ID" });
            DropIndex("dbo.Certifiers", new[] { "ParentMovie_ID" });
            CreateTable(
                "dbo.MovieRestriction",
                c => new
                    {
                        MovieID = c.Int(nullable: false),
                        RestrictionID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.MovieID, t.RestrictionID })
                .ForeignKey("dbo.Movies", t => t.MovieID, cascadeDelete: true)
                .ForeignKey("dbo.Restrictions", t => t.RestrictionID, cascadeDelete: true)
                .Index(t => t.MovieID)
                .Index(t => t.RestrictionID);
            
            DropTable("dbo.Certifiers");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Certifiers",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Certification_ID = c.Int(),
                        ParentMovie_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            DropForeignKey("dbo.MovieRestriction", "RestrictionID", "dbo.Restrictions");
            DropForeignKey("dbo.MovieRestriction", "MovieID", "dbo.Movies");
            DropIndex("dbo.MovieRestriction", new[] { "RestrictionID" });
            DropIndex("dbo.MovieRestriction", new[] { "MovieID" });
            DropTable("dbo.MovieRestriction");
            CreateIndex("dbo.Certifiers", "ParentMovie_ID");
            CreateIndex("dbo.Certifiers", "Certification_ID");
            AddForeignKey("dbo.Certifiers", "ParentMovie_ID", "dbo.Movies", "ID", cascadeDelete: true);
            AddForeignKey("dbo.Certifiers", "Certification_ID", "dbo.Restrictions", "ID");
        }
    }
}
