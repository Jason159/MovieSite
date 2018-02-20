USE Movie_Site
GO

DROP table SessionTimes
DROP table Cineplex
DROP table Movie


create table Cineplex
(
	CineplexID int not null identity primary key,
	Location nvarchar(max) not null,
	CDescription nvarchar(max) not null,
	ImageUrl nvarchar(max) null
);

create table Movie
(
	MovieID int not null identity primary key,
	Title nvarchar(max) not null,
	MDescription nvarchar(max) not null,
	ImageUrl nvarchar(max) null,
	Price money not null
);

create table SessionTimes
(
	SessionID int not null identity primary key,
	CineplexID int not null foreign key references Cineplex (CineplexID),
	MovieID int not null foreign key references Movie (MovieID),
	MovieTime datetime not null,
	BookedSeats nvarchar(max) null
);

declare @stKilda int;
insert into Cineplex (Location, CDescription, ImageUrl) 
values ('St Kilda', 'The St Kilda cinema is located in a suburb within the inner-city of Melbourne. This St Kilda cinema has an outdoor cinema for their customers and provide excellent movie session times for all current movies and also displays all up and coming movies.', '/img/StKilda.png');
set @stKilda = SCOPE_IDENTITY();

declare @fitzroy int;
insert into Cineplex (Location, CDescription, ImageUrl) 
values ('Fitzroy', 'The beautiful and luxurious Fitzroy cinema is located within the suburb of the inner-city of Melbourne. The Fitzroy cinema will provide movie session times for all current movies and also displays all up and coming movies.', '/img/Fitzroy.png');
set @fitzroy = SCOPE_IDENTITY();

declare @melbourneCBD int;
insert into Cineplex (Location, CDescription, ImageUrl) 
values ('Melbourne CBD', 'The MelbourneCBD cinema is located in the center of Melbourne. The venue is updated with the latest technology that will help provide customers with the best viewing if all current movies and also displays all up and coming movies.', '/img/MelbourneCBD.png');
set @melbourneCBD = SCOPE_IDENTITY();

declare @sunshine int;
insert into Cineplex (Location, CDescription, ImageUrl) 
values ('Sunshine', 'The Sunshine cinema is located in a western suburb of Melbourne. This venue is vintage and acts like an olden day theater that will help customers with their movie session times for all current movies and also displays all up and coming movies.', '/img/Sunshine.png');
set @sunshine = SCOPE_IDENTITY();

declare @lilydale int;
insert into Cineplex (Location, CDescription, ImageUrl) 
values ('Lilydale', 'The Lilydale cinema is located in the suburb of the north east of Melbourne. This venue will provide accurate movie session times for all current movies to allow their customers to come in time to watch trailers for new and upcoming movies.', '/img/Lilydale.png');
set @lilydale = SCOPE_IDENTITY();

declare @theMatrix int;
insert into Movie (Title, MDescription, ImageUrl, Price)
values ('The Matrix', 'In a distant future a hacker called Neo is seeking out a dangerous hacker called Morpheus but instead is seeked out by a woman called Trinity. Who introduce Neo to Morpheos, to which Neo will help their cause.', '/img/TheMatrix.png', 10.00);
set @theMatrix = SCOPE_IDENTITY();

declare @theMatrixReloaded int;
insert into Movie (Title, MDescription, ImageUrl, Price)
values ('The Matrix Reloaded', 'A sequel to the first film "Matrix", Neo, Trinity and Morpheos are still fighting to overthrow the Matrix against the Machine Army. Neo will achieve this feat by being the "One" and using his weaponry and attained powers.', '/img/TheMatrixReloaded.png', 15.00);
set @theMatrixReloaded = SCOPE_IDENTITY();

declare @theMatrixRevolution int;
insert into Movie (Title, MDescription, ImageUrl, Price)
values ('The Matrix Revolution', 'The Machine Army are winning against the revolting humans and are soon to complete the object. Neo is now stuck in a limbo world and is trying to escape to rescue and become the "One".', '/img/TheMatrixRevolution.png', 20.00);
set @theMatrixRevolution = SCOPE_IDENTITY();

insert into SessionTimes(CineplexID, MovieID, MovieTime) 
values (@stKilda, @theMatrix, '2018-02-12 9:30:00');

insert into SessionTimes (CineplexID, MovieID, MovieTime) 
values (@stKilda, @theMatrix, '2018-02-12 12:30:00');

insert into SessionTimes (CineplexID, MovieID, MovieTime) 
values (@stKilda, @theMatrixReloaded, '2018-02-27 1:00:00');

insert into SessionTimes (CineplexID, MovieID, MovieTime) 
values (@stKilda, @theMatrixReloaded, '2018-02-27 4:00:00');

insert into SessionTimes (CineplexID, MovieID, MovieTime) 
values (@stKilda, @theMatrixRevolution, '2018-02-19 4:00:00');

insert into SessionTimes (CineplexID, MovieID, MovieTime) 
values (@stKilda, @theMatrixRevolution, '2018-02-19 7:00:00');

insert into SessionTimes (CineplexID, MovieID, MovieTime) 
values (@fitzroy, @theMatrix, '2018-05-06 10:30:00');

insert into SessionTimes (CineplexID, MovieID, MovieTime) 
values (@fitzroy, @theMatrix, '2018-05-06 7:30:00');

insert into SessionTimes (CineplexID, MovieID, MovieTime) 
values (@fitzroy, @theMatrixReloaded, '2018-02-14 8:00:00');

insert into SessionTimes (CineplexID, MovieID, MovieTime) 
values (@fitzroy, @theMatrixReloaded, '2018-02-14 4:00:00');

insert into SessionTimes (CineplexID, MovieID, MovieTime) 
values (@melbourneCBD, @theMatrixRevolution, '2018-02-05 9:30:00');

insert into SessionTimes (CineplexID, MovieID, MovieTime) 
values (@melbourneCBD, @theMatrixRevolution, '2018-02-07 6:30:00');