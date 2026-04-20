using Npgsql;                 // library to talk C# program to the PostgreSQL database
using robot_controller_api;   // using the Map class created in the previous task

namespace robot_controller_api.Persistence;

public class MapADO : IMapDataAccess      // class responsible for getting map data from the database
{
    // storing the database connection details
    private const string CONNECTION_STRING =
        "Host=localhost;Username=postgres;Password=Pass123;Database=sit331";

    // method to get all maps from the database
    public  List<Map> GetMaps()
    {
        var maps = new List<Map>();    // creating an empty list to store maps

        // creating connection to the PostgreSQL database
        using var conn = new NpgsqlConnection(CONNECTION_STRING);

        conn.Open();   // opening the connection to the database

        // command to tell the database to get all records from the map table
        using var cmd = new NpgsqlCommand("SELECT * FROM map", conn);

        // executing the query and reading the results from the database
        using var dr = cmd.ExecuteReader();

        // loop to read each row returned from the database
        while (dr.Read())
        {
            // creating a Map object using the data from the database row
            var map = new Map(
                (int)dr["id"],             
                (int)dr["columns"],        
                (int)dr["rows"],          
                (string)dr["name"],        
                (DateTime)dr["createddate"], 
                (DateTime)dr["modifieddate"], 
                dr["description"] as string  
            );

            maps.Add(map);   // adding the map object to the list
        }

        return maps;   // returning the list of maps to the controller
    }
}