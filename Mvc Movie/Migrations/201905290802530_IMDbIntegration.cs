namespace Mvc_Movie.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IMDbIntegration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Movies", "Poster", c => c.String());
            AddColumn("dbo.Movies", "IMDbRating", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Movies", "IMDbRating");
            DropColumn("dbo.Movies", "Poster");
        }
    }
}
