namespace Mvc_Movie.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TypeAdjustment : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Movies", "IMDbID", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Movies", "IMDbID", c => c.Int(nullable: false));
        }
    }
}
