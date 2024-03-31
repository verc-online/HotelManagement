if not exists (select 1
               from dbo.RoomTypes)
    begin
        insert into dbo.RoomTypes(Title, Description, Price)
        values ('King Size Bed', 'A room with a king-size bed and a window.', 100),
               ('Two Queen Size Beds', 'A room with two queen-size beds and a window.', 115),
               ('Executive Suite', 'Two rooms, each with a king-size bed and a window.', 205);
    end

if not exists (select 1
               from dbo.Rooms)
    begin
        declare @roomId1 int;
        declare @roomId2 int;
        declare @roomId3 int;

        select @roomId1 = Id from dbo.RoomTypes where Title = 'King Size Bed';
        select @roomId2 = Id from dbo.RoomTypes where Title = 'Two Queen Size Beds';
        select @roomId3 = Id from dbo.RoomTypes where Title = 'Executive Suite';

        insert into dbo.Rooms (RoomNumber, RoomTypeId)
        values ('101', @roomId1),
               ('102', @roomId1),
               ('103', @roomId1),
               ('201', @roomId2),
               ('202', @roomId2),
               ('301', @roomId3);
    end