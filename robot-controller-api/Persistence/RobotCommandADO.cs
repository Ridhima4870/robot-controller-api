using Npgsql;         //library to talk C# program  to the PostgreSQL database
using robot_controller_api;    //using the code of before task

namespace robot_controller_api.Persistence;  

public class RobotCommandADO : IRobotCommandDataAccess   //  class for getting robot command data from the database
{
    //  storing the database connection details , Host = where the database is running  
    private const string CONNECTION_STRING =
        "Host=localhost;Username=postgres;Password=Pass123;Database=sit331";

    // This method will get all robot commands from the database
    public List<RobotCommand> GetRobotCommands()
    {
        var robotCommands = new List<RobotCommand>();    //  an empty list to store the robot commands

        using var conn = new NpgsqlConnection(CONNECTION_STRING);       //  creatingg  connection to the PostgreSQL database

        conn.Open();   //opening connecyion

        //  command  to tell the database  to get all records from the robotcommand table
        using var cmd = new NpgsqlCommand("SELECT * FROM robotcommand", conn);

        using var dr = cmd.ExecuteReader();       //  execute the query and reads the results from the database

        while (dr.Read())     //  loop reads each row returned from the database
        {
            //creating a RobotCommand object using the data from the database row
            var robotCommand = new RobotCommand(
                (int)dr["id"],                
                (string)dr["Name"],          
                (bool)dr["ismovecommand"],   
                (DateTime)dr["createddate"],  
                (DateTime)dr["modifieddate"], 
                dr["description"] as string   
            );

            robotCommands.Add(robotCommand);   //adding to list
        }

        return robotCommands;
    }
}