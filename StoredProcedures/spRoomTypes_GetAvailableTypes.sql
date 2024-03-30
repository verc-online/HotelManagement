CREATE PROCEDURE dbo.spRoomTypes_GetAvailableTypes @startDate date,
                                                   @endDate date
AS
begin
    set nocount on;

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


