using robot_controller_api.Persistence;
using Microsoft.AspNetCore.Mvc;
 
namespace robot_controller_api.Controllers;        //added dependency injection using the interface IRobotCommandDataAcces noo instead of directly calling RobotCommandADO or RobotCommandRepository the controller now depends on the interface

[ApiController]
[Route("api/robot-commands")]
public class RobotCommandsController : ControllerBase
{
    private readonly IRobotCommandDataAccess _robotCommandsRepo; //Constructor receives the repository automatically and so w do not the need to manually create database objects in the controllerv

    // Constructor receives repository from the DI container
    public RobotCommandsController(IRobotCommandDataAccess robotCommandsRepo)
    {
        _robotCommandsRepo = robotCommandsRepo;
    }

    // GET all robot commands from database
    [HttpGet]
    public IEnumerable<RobotCommand> GetAllRobotCommands()
    {
        return _robotCommandsRepo.GetRobotCommands();
    }

    // Temporary in-memory list (can be removed later)
    private static readonly List<RobotCommand> _commands = new List<RobotCommand>
    {
        new RobotCommand(1, "LEFT", true, DateTime.Now, DateTime.Now),
        new RobotCommand(2, "RIGHT", true, DateTime.Now, DateTime.Now),
        new RobotCommand(3, "MOVE", true, DateTime.Now, DateTime.Now),
        new RobotCommand(4, "PLACE", false, DateTime.Now, DateTime.Now),
        new RobotCommand(5, "REPORT", false, DateTime.Now, DateTime.Now)
    };

    [HttpGet("move")]
    public IEnumerable<RobotCommand> GetMoveCommandsOnly()
    {
        return _commands.Where(c => c.IsMoveCommand);
    }

    [HttpGet("{id}", Name = "GetRobotCommand")]
    public IActionResult GetRobotCommandById(int id)
    {
        var command = _commands.FirstOrDefault(c => c.Id == id);

        if (command == null)
            return NotFound();

        return Ok(command);
    }

    [HttpPost]
    public IActionResult AddRobotCommand(RobotCommand newCommand)
    {
        if (newCommand == null)
            return BadRequest();

        if (_commands.Any(c => c.Name == newCommand.Name))
            return Conflict();

        int newId = _commands.Max(c => c.Id) + 1;

        var command = new RobotCommand(
            newId,
            newCommand.Name,
            newCommand.IsMoveCommand,
            DateTime.Now,
            DateTime.Now,
            newCommand.Description
        );

        _commands.Add(command);

        return CreatedAtRoute("GetRobotCommand", new { id = command.Id }, command);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateRobotCommand(int id, RobotCommand updatedCommand)
    {
        var command = _commands.FirstOrDefault(c => c.Id == id);

        if (command == null)
            return NotFound();

        command.Name = updatedCommand.Name;
        command.Description = updatedCommand.Description;
        command.IsMoveCommand = updatedCommand.IsMoveCommand;
        command.ModifiedDate = DateTime.Now;

        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteRobotCommand(int id)
    {
        var command = _commands.FirstOrDefault(c => c.Id == id);

        if (command == null)
            return NotFound();

        _commands.Remove(command);

        return NoContent();
    }
}