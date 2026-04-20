using Npgsql;                   // library used to work with PostgreSQL
using robot_controller_api;     // using the RobotCommand class created earlier

namespace robot_controller_api.Persistence;

// this class is responsible for getting robot command data from the database
public class RobotCommandRepository : IRobotCommandDataAccess, IRepository
{
    // this allows the class to use the generic repository method
    private IRepository _repo => this;

    // this method gets all robot commands from the database
    public List<RobotCommand> GetRobotCommands()
    {
        // calling ExecuteReader to run the SQL query
        // this query selects all records from the robotcommand table
        var commands = _repo.ExecuteReader<RobotCommand>(
            "SELECT * FROM public.robotcommand");

        // returning the list of commands to the controller
        return commands;
    }
}