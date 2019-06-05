namespace Mvc_Movie.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MovieParameterDeprecated : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Movies", "Rating");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Movies", "Rating", c => c.String(maxLength: 5));
        }
    }
}
