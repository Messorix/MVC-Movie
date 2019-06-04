namespace Mvc_Movie.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IMDbAdjustments : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Movies", "IMDbID", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Movies", "IMDbID");
        }
    }
}
