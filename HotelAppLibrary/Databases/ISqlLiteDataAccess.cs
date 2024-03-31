namespace HotelAppLibrary.Databases;

public interface ISqlLiteDataAccess
{
    List<T> LoadData<T, U>(string sqlStatement, U parameters, string connectionStringName);
    void SaveData<T>(string sqlStatement, T parameters, string connectionStringName);
}