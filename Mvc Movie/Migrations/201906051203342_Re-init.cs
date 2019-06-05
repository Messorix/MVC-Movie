namespace Mvc_Movie.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class Reinit : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Certifiers", "Certification_ID", "dbo.Restrictions");
            DropForeignKey("dbo.Certifiers", "ParentMovie_ID", "dbo.Movies");
            DropIndex("dbo.Certifiers", new[] { "Certification_ID" });
            DropIndex("dbo.Certifiers", new[] { "ParentMovie_ID" });

            CreateTable(
                "dbo.MovieRestrictions",
                c => new
                {
                    ID = c.Int(nullable: false),
                    RestrictionID = c.Int(nullable: false),
                    MovieID = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Restrictions", t => t.RestrictionID, cascadeDelete: true)
                .ForeignKey("dbo.Movies", t => t.MovieID, cascadeDelete: true)
                .Index(t => t.RestrictionID)
                .Index(t => t.MovieID);

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
            DropForeignKey("dbo.MovieRestrictions", "MovieID", "dbo.Movies");
            DropForeignKey("dbo.MovieRestrictions", "RestrictionID", "dbo.Restrictions");
            DropIndex("dbo.MovieRestrictions", new[] { "MovieID" });
            DropIndex("dbo.MovieRestrictions", new[] { "RestrictionID" });
            DropTable("dbo.MovieRestrictions");
            CreateIndex("dbo.Certifiers", "ParentMovie_ID");
            CreateIndex("dbo.Certifiers", "Certification_ID");
            AddForeignKey("dbo.Certifiers", "ParentMovie_ID", "dbo.Movies", "ID", cascadeDelete: true);
            AddForeignKey("dbo.Certifiers", "Certification_ID", "dbo.Restrictions", "ID");
        }
    }
}
