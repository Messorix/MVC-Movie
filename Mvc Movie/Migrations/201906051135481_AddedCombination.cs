namespace Mvc_Movie.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedCombination : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.MovieRestriction", newName: "RestrictionMovies");
            RenameColumn(table: "dbo.RestrictionMovies", name: "MovieID", newName: "Movie_ID");
            RenameColumn(table: "dbo.RestrictionMovies", name: "RestrictionID", newName: "Restriction_ID");
            RenameIndex(table: "dbo.RestrictionMovies", name: "IX_RestrictionID", newName: "IX_Restriction_ID");
            RenameIndex(table: "dbo.RestrictionMovies", name: "IX_MovieID", newName: "IX_Movie_ID");
            DropPrimaryKey("dbo.RestrictionMovies");
            AddPrimaryKey("dbo.RestrictionMovies", new[] { "Restriction_ID", "Movie_ID" });
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.RestrictionMovies");
            AddPrimaryKey("dbo.RestrictionMovies", new[] { "MovieID", "RestrictionID" });
            RenameIndex(table: "dbo.RestrictionMovies", name: "IX_Movie_ID", newName: "IX_MovieID");
            RenameIndex(table: "dbo.RestrictionMovies", name: "IX_Restriction_ID", newName: "IX_RestrictionID");
            RenameColumn(table: "dbo.RestrictionMovies", name: "Restriction_ID", newName: "RestrictionID");
            RenameColumn(table: "dbo.RestrictionMovies", name: "Movie_ID", newName: "MovieID");
            RenameTable(name: "dbo.RestrictionMovies", newName: "MovieRestriction");
        }
    }
}
