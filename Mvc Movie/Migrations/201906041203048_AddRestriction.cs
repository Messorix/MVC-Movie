namespace Mvc_Movie.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddRestriction : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Restrictions",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                        Order = c.Int(nullable: false),
                        Certification_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Certifiers", t => t.Certification_ID)
                .Index(t => t.Certification_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Restrictions", "Certification_ID", "dbo.Certifiers");
            DropIndex("dbo.Restrictions", new[] { "Certification_ID" });
            DropTable("dbo.Restrictions");
        }
    }
}
