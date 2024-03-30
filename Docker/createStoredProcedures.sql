CREATE PROCEDURE dbo.spBookings_Insert @roomId int,
                                       @guestId int,
                                       @startDate date,
                                       @endDate date,
                                       @totalCost money
AS
begin
    set
nocount on;

insert into dbo.Bookings (RoomId, GuestId, StartDate, EndDate, TotalCost)
values (@roomId, @guestId, @startDate, @endDate, @totalCost);
end
go

CREATE PROCEDURE dbo.spBookings_Search @lastName nvarchar(50),
                                       @startDate date
AS
begin
    set
nocount on;

select b.Id,
       b.RoomId,
       b.GuestId,
       b.StartDate,
       b.EndDate,
       b.CheckedIn,
       b.TotalCost,
       r.RoomNumber,
       r.RoomTypeId,
       g.FirstName,
       g.LastName,
       rt.Title,
       rt.Description,
       rt.Price
from dbo.Bookings b
         inner join dbo.Guests g on b.GuestId = g.Id
         inner join dbo.Rooms r on b.RoomId = r.Id
         inner join dbo.RoomTypes rt on r.RoomTypeId = rt.Id
where b.StartDate = @startDate
  and g.LastName = @lastName;
end
go
CREATE PROCEDURE dbo.spGuests_Insert @firstName nvarchar(50),
                                     @lastName nvarchar(50)
AS
begin
    set
nocount on;

    if
not exists(select 1 from dbo.Guests where @firstName = FirstName and @lastName = LastName)
begin
insert into dbo.Guests (FirstName, LastName)
values (@firstName, @lastName);
end

select top 1 Id, FirstName, LastName
from dbo.Guests
where FirstName = @firstName
  and LastName = @lastName;
end
go

CREATE PROCEDURE dbo.spRooms_GetAvailableRooms @startDate date,
                                               @endDate date,
                                               @roomTypeId int
AS
begin
    set
nocount on;

select r.Id, r.RoomNumber, r.RoomTypeId
from dbo.Rooms r
         inner join dbo.RoomTypes t on t.Id = r.RoomTypeId

where r.RoomTypeId = @roomTypeId
  and r.Id not in (select b.RoomId
                   from dbo.Bookings b
                   where (@startDate < b.StartDate and @endDate > b.EndDate)
                      or (b.StartDate <= @endDate and @endDate < b.EndDate)
                      or (b.StartDate <= @startDate and @startDate < b.EndDate));

end
go


CREATE PROCEDURE dbo.spRoomTypes_GetAvailableTypes @startDate date,
                                                   @endDate date
AS
begin
    set
nocount on;

select RT.Id, RT.Title, RT.Description, RT.Price
from dbo.Rooms r
         inner join dbo.RoomTypes RT on r.RoomTypeId = RT.Id

where r.Id not in (select b.RoomId
                   from dbo.Bookings b
                   where (@startDate < b.StartDate and @endDate > b.EndDate)
                      or (b.StartDate <= @endDate and @endDate < b.EndDate)
                      or (b.StartDate <= @startDate and @startDate < b.EndDate))
group by RT.Id, RT.Title, RT.Description, RT.Price;
end
go


CREATE PROCEDURE dbo.spRoomTypes_GetById @id int
AS
begin
    set
nocount on;
select Id, Title, Description, Price
from dbo.RoomTypes
where Id = @Id;
end
go







