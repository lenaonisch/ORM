
create table Books (
   ID             	bigint               identity,
   Name             varchar(50)          not null,
   PublishedIn      date          		 null,
   constraint PK_Books primary key (ID)
)
go

create table Authors (
   ID				bigint               identity,
   Name             varchar(50)          not null,
   Surname          varchar(50)          not null,
   Country          varchar(50)          null,
   constraint PK_Author primary key (ID)
)
go


create table BooksAuthors (
   BookID           bigint               not null,
   AuthorID         bigint               not null,
   constraint PK_BookAuth primary key (BookID, AuthorID),
   constraint FK_Books foreign key (BookID)
      references Books (ID),
   constraint FK_Authors foreign key (AuthorID)
      references Authors (ID)
)
go

INSERT INTO Authors (Name, Surname, Country) VALUES ('Nil', 'Gaiman', 'England')
INSERT INTO Authors (Name, Surname, Country) VALUES ('Terry', 'Pratchett', 'England')
INSERT INTO Authors (Name, Surname, Country) VALUES ('Micle', 'Rivs', 'USA')

INSERT INTO Books 	(Name, PublishedIn) VALUES ('Good omens', '2010')
INSERT INTO BooksAuthors (BookID, AuthorID) VALUES (1, 1)
INSERT INTO BooksAuthors (BookID, AuthorID) VALUES (1, 2)

INSERT INTO Books 	(Name, PublishedIn) VALUES ('American gods', '2001')
INSERT INTO BooksAuthors (BookID, AuthorID) VALUES (2, 1)

INSERT INTO Books 	(Name, PublishedIn) VALUES ('Coralina', '2002')
INSERT INTO BooksAuthors (BookID, AuthorID) VALUES (3, 1)

INSERT INTO Books 	(Name, PublishedIn) VALUES ('Small gods', '1992')
INSERT INTO BooksAuthors (BookID, AuthorID) VALUES (4, 2)

INSERT INTO Books 	(Name, PublishedIn) VALUES ('Interworld', '2008')
INSERT INTO BooksAuthors (BookID, AuthorID) VALUES (5, 1)
INSERT INTO BooksAuthors (BookID, AuthorID) VALUES (5, 3)

