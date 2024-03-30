create table Guests
(
    Id        int identity
        constraint Guests_pk
            primary key,
    FirstName nvarchar(50) not null,
    LastName  nvarchar(50) not null
)
go


