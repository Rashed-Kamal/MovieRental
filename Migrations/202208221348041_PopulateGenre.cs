namespace MovieRental.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateGenre : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO Genres(Name) VALUES ('Action')");
            Sql("INSERT INTO Genres(Name) VALUES ('Comedy')");
            Sql("INSERT INTO Genres(Name) VALUES ('Family')");
            Sql("INSERT INTO Genres(Name) VALUES ('Drama')");
            Sql("INSERT INTO Genres(Name) VALUES ('Sci-Fi')");
            Sql("INSERT INTO Genres(Name) VALUES ('Romance')");
            Sql("INSERT INTO Genres(Name) VALUES ('Western')");
        }
        
        public override void Down()
        {
        }
    }
}
