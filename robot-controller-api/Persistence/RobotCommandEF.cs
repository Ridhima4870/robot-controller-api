using robot_controller_api.Models; // used to access RobotCommand model
using Microsoft.EntityFrameworkCore; // used for Entity Framework features

namespace robot_controller_api.Persistence;

public class RobotCommandEF : IRobotCommandDataAccess    // this class is used to handle robot command data using Entity Framework
{
    private readonly RobotContext _context; // this stores database context

    public RobotCommandEF(RobotContext context)    // constructor to get database context from program
    {
        _context = context; // assigning context
    }

    public List<RobotCommand> GetRobotCommands()    // this method gets all robot commands from database
    {
        return _context.RobotCommands.ToList(); // fetch all records
    }

    public List<RobotCommand> GetMoveCommandsOnly()      // this method gets only move commands 
    {
        return _context.RobotCommands
            .Where(c => c.IsMoveCommand) // filter only move commands
            .ToList(); // convert to list
    }

    public RobotCommand? GetRobotCommandById(int id)  // this method finds a command using its id
    {
        return _context.RobotCommands.Find(id); // search by id
    }

    public RobotCommand AddRobotCommand(RobotCommand command)   // this method adds a new robot command
    {
        command.CreatedDate = DateTime.Now; // set created time
        command.ModifiedDate = DateTime.Now; // set modified time

        _context.RobotCommands.Add(command); // add to database
        _context.SaveChanges(); // save changes

        return command; // return added command
    }

    public bool UpdateRobotCommand(int id, RobotCommand command)  // this method updates an existing command
    {
        var existing = _context.RobotCommands.Find(id); // find command

        if (existing == null)
            return false; // if not found return false

        existing.Name = command.Name;      // update values
        existing.Description = command.Description;
        existing.IsMoveCommand = command.IsMoveCommand;
        existing.ModifiedDate = DateTime.Now;

        _context.SaveChanges(); // save changes

        return true; // return success
    }

    public bool DeleteRobotCommand(int id)    // this methodwill delete a command
    {
        var command = _context.RobotCommands.Find(id); // find command

        if (command == null)
            return false; // if not found return false

        _context.RobotCommands.Remove(command); // remove from database
        _context.SaveChanges(); // save changes

        return true; // return success
    }
}