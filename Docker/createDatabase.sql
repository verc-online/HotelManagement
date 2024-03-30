create table Guests
(
    Id        int identity
        constraint Guests_pk
            primary key,
    FirstName nvarchar(50) not null,
    LastName  nvarchar(50) not null
)
go

create table RoomTypes
(
    Id          int identity
        constraint RoomTypes_pk
            primary key,
    Title       nvarchar(50)  not null,
    Description nvarchar(200) not null,
    Price       money         not null
)
go

create table Rooms
(
    Id         int identity
        constraint Rooms_pk
            primary key,
    RoomNumber varchar(10) not null,
    RoomTypeId int         not null
        constraint Rooms___RoomTypes
            references RoomTypes
)
go

create table Bookings
(
    Id        int identity
        constraint Bookings_pk
            primary key,
    RoomId    int           not null
        constraint Bookings___Rooms
            references Rooms,
    GuestId   int           not null
        constraint Bookings___Guests
            references Guests,
    StartDate datetime2     not null,
    EndDate   datetime2     not null,
    CheckedIn bit default 0 not null,
    TotalCost money         not null
)
go


