IF DB_ID('MvcMovie') IS NULL
BEGIN
	CREATE DATABASE MvcMovie
END

USE MvcMovie
GO

CREATE TABLE Movies
(
	[MovieID]		int PRIMARY KEY,
	[Title]			varchar(255) NOT NULL,
	[Release Date]	Date,
	[Price]			smallmoney,
	[Poster]		varchar(255),
	[IMDbID]		varchar(25) UNIQUE,
	[IMDbRating]	decimal(3,1)
)

CREATE TABLE Restrictions
(
	[RestrictionID]	int PRIMARY KEY,
	[Locale]		varchar(25) NOT NULL,
	[Certification]	varchar(25) NOT NULL,
	[Description]	varchar(255),
	[Sequence]		smallint
)

CREATE TABLE Genres
(
	[GenreID]		int PRIMARY KEY,
	[Genre]			varchar(255) UNIQUE
)

CREATE TABLE MovieRestrictions
(
	[MovieID]		int FOREIGN KEY REFERENCES Movies(MovieID),
	[RestrictionID]	int FOREIGN KEY REFERENCES Restrictions(RestrictionID),
	PRIMARY KEY (MovieID, RestrictionID)
)

CREATE TABLE MovieGenres
(
	[MovieID]		int FOREIGN KEY REFERENCES Movies(MovieID),
	[GenreID]		int FOREIGN KEY REFERENCES Genres(GenreID),
	PRIMARY KEY (MovieID, GenreID)
)