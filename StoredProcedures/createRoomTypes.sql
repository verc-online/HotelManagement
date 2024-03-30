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


