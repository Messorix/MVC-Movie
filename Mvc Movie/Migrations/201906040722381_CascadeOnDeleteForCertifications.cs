namespace Mvc_Movie.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CascadeOnDeleteForCertifications : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Certifiers", name: "Movie_ID", newName: "ParentMovie_ID");
            RenameIndex(table: "dbo.Certifiers", name: "IX_Movie_ID", newName: "IX_ParentMovie_ID");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Certifiers", name: "IX_ParentMovie_ID", newName: "IX_Movie_ID");
            RenameColumn(table: "dbo.Certifiers", name: "ParentMovie_ID", newName: "Movie_ID");
        }
    }
}
