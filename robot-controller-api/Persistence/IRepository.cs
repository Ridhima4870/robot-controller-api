using System.Linq;      // used for filtering things like Where()
using Npgsql;           // library used to connect C# with PostgreSQL database

namespace robot_controller_api.Persistence;

// this interface contains a generic method that reads data from the database
public interface IRepository
{
    // this generic method can work with any class like Map or RobotCommand
    public List<T> ExecuteReader<T>(string sqlCommand, NpgsqlParameter[] dbParams = null) where T : class, new()
    {
        // creating an empty list to store the results from database
        var entities = new List<T>();

        // creating a connection to the PostgreSQL database
        using var conn = new NpgsqlConnection("Host=localhost;Username=postgres;Password=Pass123;Database=sit331");

        conn.Open();   // opening the database connection

        // creating the SQL command that will run in the database
        using var cmd = new NpgsqlCommand(sqlCommand, conn);

        // if there are parameters in the query, add them to the command
        if (dbParams is not null)
        {
            cmd.Parameters.AddRange(dbParams.Where(x => x.Value is not null).ToArray());
        }

        // executing the query and reading the result rows from the database
        using var dr = cmd.ExecuteReader();

        // loop to read each row returned from the database
        while (dr.Read())
        {
            // creating a new object of type T (like Map or RobotCommand)
            var entity = new T();

            // mapping database columns to object properties automatically
            dr.MapTo(entity);

            // adding the object to the list
            entities.Add(entity);
        }

        // returning the final list of objects
        return entities;
    }
}