using HotelAppLibrary.Databases;
using HotelAppLibrary.Models;
using Microsoft.VisualBasic;

namespace HotelAppLibrary.Data;

public class SqliteData : IDatabaseData
{
    private readonly ISqlLiteDataAccess _db;
    private const string connectionStringName = "SqliteDB";

    public SqliteData(ISqlLiteDataAccess db)
    {
        _db = db;
    }

    public List<RoomTypeModel> GetAvailableRoomTypes(DateTime startDate, DateTime endDate)
    {
        string sql = @"select RT.Id, RT.Title, RT.Description, RT.Price 
                        from Rooms r
                                 inner join RoomTypes RT on r.RoomTypeId = RT.Id

                        where r.Id not in (select b.RoomId
                                           from Bookings b
                                           where (@startDate < b.StartDate and @endDate > b.EndDate)
                                              or (b.StartDate <= @endDate and @endDate < b.EndDate)
                                              or (b.StartDate <= @startDate and @startDate < b.EndDate))
                        group by RT.Id, RT.Title, RT.Description, RT.Price;";
        
        var output = _db.LoadData<RoomTypeModel, dynamic>(
            sql,
            new { startDate, endDate },
            connectionStringName);
        output.ForEach(x => x.Price = x.Price / 100);
        return output;
    }

    public void BookGuest(string firstName, string lastName, DateTime startDate, DateTime endDate, int roomTypeId)
    {
        string sql = @"select 1 from Guests where @firstName = FirstName and @lastName = LastName";
        int results = _db.LoadData<dynamic, dynamic>(sql, new { firstName, lastName }, connectionStringName).Count();

        if (results == 0)
        {
            sql = @" insert into Guests (FirstName, LastName) values (@firstName, @lastName)"; ;
        }
        _db.SaveData(sql, new { firstName, lastName }, connectionStringName);

        sql = @"select Id, FirstName, LastName
                    from Guests
                    where FirstName = @firstName
                        and LastName = @lastName
                LIMIT 1;"; //SQLite

        GuestModel guest = _db.LoadData<GuestModel, dynamic>(
                sql,
                new { firstName, lastName },
                connectionStringName)
            .First();

        RoomTypeModel roomType = _db.LoadData<RoomTypeModel, dynamic>(
            "select * from RoomTypes where Id = @Id",
            new { Id = roomTypeId },
            connectionStringName).First();


        sql = @"select r.Id, r.RoomNumber, r.RoomTypeId
                from Rooms r
                         inner join RoomTypes t on t.Id = r.RoomTypeId

                where r.RoomTypeId = @roomTypeId
                  and r.Id not in (select b.RoomId
                                   from Bookings b
                                   where (@startDate < b.StartDate and @endDate > b.EndDate)
                                      or (b.StartDate <= @endDate and @endDate < b.EndDate)
                                      or (b.StartDate <= @startDate and @startDate < b.EndDate));";
        List<RoomModel> availableRooms = _db.LoadData<RoomModel, dynamic>(
            sql,
            new { roomTypeId, startDate, endDate },
            connectionStringName);

        TimeSpan timeStaying = endDate.Date.Subtract(startDate.Date);

        sql = @"insert into Bookings (RoomId, GuestId, StartDate, EndDate, TotalCost)
                values (@roomId, @guestId, @startDate, @endDate, @totalCost);";
        _db.SaveData(sql,
            new
            {
                roomId = availableRooms.First().Id,
                guestId = guest.Id,
                startDate = startDate,
                endDate = endDate,
                totalCost = roomType.Price * timeStaying.Days
            },
            connectionStringName);
    }

    public List<BookingFullModel> SearchBookings(string lastName)
    {
        string sql = @"select b.Id,
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
                        from Bookings b
                                 inner join Guests g on b.GuestId = g.Id
                                 inner join Rooms r on b.RoomId = r.Id
                                 inner join RoomTypes rt on r.RoomTypeId = rt.Id
                        where b.StartDate = @startDate
                          and g.LastName = @lastName;";

        var output =  _db.LoadData<BookingFullModel, dynamic>(
           sql,
           new { lastName, startDate = DateTime.Now.Date },
           connectionStringName);
        output.ForEach(x =>
        {
            x.Price = x.Price / 100;
            x.TotalCost = x.TotalCost / 100;
        });

        return output;
    }

    public void CheckInGuest(int bookingId)
    {
        string sql = @"update Bookings
                        set CheckedIn = 1
                        where @Id = Id;";
        _db.SaveData(
            sql,
            new { Id = bookingId },
            connectionStringName);
    }

    public RoomTypeModel GetRoomTypeById(int id)
    {
        string sql = @"select Id, Title, Description, Price 
                        from RoomTypes 
                        where Id = @id;";
        return _db.LoadData<RoomTypeModel, dynamic>(
             sql,
             new { id },
             connectionStringName
         ).FirstOrDefault();
    }

}

  