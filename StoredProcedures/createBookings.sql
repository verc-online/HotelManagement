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
    StartDate date          not null,
    EndDate   date          not null,
    CheckedIn bit default 0 not null,
    TotalCost money         not null
)
go


