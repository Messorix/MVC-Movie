namespace Mvc_Movie.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCombiTable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Certifiers", "Certification_ID", "dbo.Restrictions");
            DropForeignKey("dbo.Certifiers", "ParentMovie_ID", "dbo.Movies");
            DropIndex("dbo.Certifiers", new[] { "Certification_ID" });
            DropIndex("dbo.Certifiers", new[] { "ParentMovie_ID" });
            CreateTable(
                "dbo.RestrictionMovies",
                c => new
                    {
                        Restriction_ID = c.Int(nullable: false),
                        Movie_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Restriction_ID, t.Movie_ID })
                .ForeignKey("dbo.Restrictions", t => t.Restriction_ID, cascadeDelete: true)
                .ForeignKey("dbo.Movies", t => t.Movie_ID, cascadeDelete: true)
                .Index(t => t.Restriction_ID)
                .Index(t => t.Movie_ID);
            
            DropColumn("dbo.Movies", "Rating");
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
            
            AddColumn("dbo.Movies", "Rating", c => c.String(maxLength: 5));
            DropForeignKey("dbo.RestrictionMovies", "Movie_ID", "dbo.Movies");
            DropForeignKey("dbo.RestrictionMovies", "Restriction_ID", "dbo.Restrictions");
            DropIndex("dbo.RestrictionMovies", new[] { "Movie_ID" });
            DropIndex("dbo.RestrictionMovies", new[] { "Restriction_ID" });
            DropTable("dbo.RestrictionMovies");
            CreateIndex("dbo.Certifiers", "ParentMovie_ID");
            CreateIndex("dbo.Certifiers", "Certification_ID");
            AddForeignKey("dbo.Certifiers", "ParentMovie_ID", "dbo.Movies", "ID", cascadeDelete: true);
            AddForeignKey("dbo.Certifiers", "Certification_ID", "dbo.Restrictions", "ID");
        }
    }
}
