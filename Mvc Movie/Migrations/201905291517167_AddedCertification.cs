namespace Mvc_Movie.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedCertification : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Certifiers",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ISO_3166_1 = c.String(),
                        Certification = c.String(),
                        Movie_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Movies", t => t.Movie_ID)
                .Index(t => t.Movie_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Certifiers", "Movie_ID", "dbo.Movies");
            DropIndex("dbo.Certifiers", new[] { "Movie_ID" });
            DropTable("dbo.Certifiers");
        }
    }
}
