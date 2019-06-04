namespace Mvc_Movie.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MovedISOField : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Restrictions", "ISO_3166_1", c => c.String());
            DropColumn("dbo.Certifiers", "ISO_3166_1");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Certifiers", "ISO_3166_1", c => c.String());
            DropColumn("dbo.Restrictions", "ISO_3166_1");
        }
    }
}
