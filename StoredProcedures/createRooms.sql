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


