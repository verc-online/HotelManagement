using System.Data;
using Microsoft.Data.SqlClient;
using System.Data.SQLite;
using Dapper;
using Microsoft.Extensions.Configuration;
using System.Linq;

namespace HotelAppLibrary.Databases;

public class SqlLiteDataAccess : ISqlLiteDataAccess
{
    private readonly IConfiguration _config;

    public SqlLiteDataAccess(IConfiguration config)
    {
        _config = config;
    }

    public List<T> LoadData<T, U>(string sqlStatement, U parameters, string connectionStringName)
    {
        string connectionString = _config.GetConnectionString(connectionStringName);

        using (IDbConnection connection = new SQLiteConnection(connectionString))
        {
            List<T> rows = connection.Query<T>(sqlStatement, parameters).ToList();
            return rows;
        }
    }

    public void SaveData<T>(string sqlStatement, T parameters, string connectionStringName)
    {
        string connectionString = _config.GetConnectionString(connectionStringName);
        
        using (IDbConnection connection = new SQLiteConnection(connectionString))
        {
            connection.Execute(sqlStatement, parameters);
        }
    }
}