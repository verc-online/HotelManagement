using HotelAppLibrary.Databases;
using HotelAppLibrary.Models;

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
                        from dbo.Rooms r
                                 inner join dbo.RoomTypes RT on r.RoomTypeId = RT.Id

                        where r.Id not in (select b.RoomId
                                           from dbo.Bookings b
                                           where (@startDate < b.StartDate and @endDate > b.EndDate)
                                              or (b.StartDate <= @endDate and @endDate < b.EndDate)
                                              or (b.StartDate <= @startDate and @startDate < b.EndDate))
                        group by RT.Id, RT.Title, RT.Description, RT.Price;";
        
        return _db.LoadData<RoomTypeModel, dynamic>(
            sql,
            new { startDate, endDate },
            connectionStringName);
    }

    public void BookGuest(string firstName, string lastName, DateTime startDate, DateTime endDate, int roomTypeId)
    {
        throw new NotImplementedException();
    }

    public List<BookingFullModel> SearchBookings(string lastName)
    {
        throw new NotImplementedException();
    }

    public void CheckInGuest(int bookingId)
    {
        throw new NotImplementedException();
    }

    public RoomTypeModel GetRoomTypeById(int id)
    {
        throw new NotImplementedException();
    }
}