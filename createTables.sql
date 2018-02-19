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
values ('St Kilda', 'description...', '~/img/StKilda.png');
set @stKilda = SCOPE_IDENTITY();

declare @fitzroy int;
insert into Cineplex (Location, CDescription, ImageUrl) 
values ('Fitzroy', 'description...', '~/img/Fitzroy.png');
set @fitzroy = SCOPE_IDENTITY();

declare @melbourneCBD int;
insert into Cineplex (Location, CDescription, ImageUrl) 
values ('Melbourne CBD', 'description...', '~/img/MelbourneCBD.png');
set @melbourneCBD = SCOPE_IDENTITY();

declare @sunshine int;
insert into Cineplex (Location, CDescription, ImageUrl) 
values ('Sunshine', 'description...', '~/img/Sunshine.png');
set @sunshine = SCOPE_IDENTITY();

declare @lilydale int;
insert into Cineplex (Location, CDescription, ImageUrl) 
values ('Lilydale', 'description...', '~/img/Lilydale.png');
set @lilydale = SCOPE_IDENTITY();

declare @theMatrix int;
insert into Movie (Title, MDescription, ImageUrl, Price)
values ('The Matrix', 'description...', '/img/TheMatrix.png', 10.00);
set @theMatrix = SCOPE_IDENTITY();

declare @theMatrixReloaded int;
insert into Movie (Title, MDescription, ImageUrl, Price)
values ('The Matrix Reloaded', 'description...', '/img/TheMatrixReloaded.png', 15.00);
set @theMatrixReloaded = SCOPE_IDENTITY();

declare @theMatrixRevolution int;
insert into Movie (Title, MDescription, ImageUrl, Price)
values ('The Matrix Revolution', 'description...', '/img/TheMatrixRevolution.png', 20.00);
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