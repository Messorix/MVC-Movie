namespace Mvc_Movie.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AdjustedRestrictionAndCertifier : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Restrictions", "Certification_ID", "dbo.Certifiers");
            DropIndex("dbo.Restrictions", new[] { "Certification_ID" });
            AddColumn("dbo.Certifiers", "Certification_ID", c => c.Int());
            AddColumn("dbo.Restrictions", "Certification", c => c.String());
            CreateIndex("dbo.Certifiers", "Certification_ID");
            AddForeignKey("dbo.Certifiers", "Certification_ID", "dbo.Restrictions", "ID");
            DropColumn("dbo.Certifiers", "Certification");
            DropColumn("dbo.Restrictions", "Certification_ID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Restrictions", "Certification_ID", c => c.Int());
            AddColumn("dbo.Certifiers", "Certification", c => c.String());
            DropForeignKey("dbo.Certifiers", "Certification_ID", "dbo.Restrictions");
            DropIndex("dbo.Certifiers", new[] { "Certification_ID" });
            DropColumn("dbo.Restrictions", "Certification");
            DropColumn("dbo.Certifiers", "Certification_ID");
            CreateIndex("dbo.Restrictions", "Certification_ID");
            AddForeignKey("dbo.Restrictions", "Certification_ID", "dbo.Certifiers", "ID");
        }
    }
}
