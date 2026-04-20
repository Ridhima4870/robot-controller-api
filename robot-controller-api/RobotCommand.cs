namespace robot_controller_api;

public class RobotCommand
{
    public int Id { get; set; }

    // Its Name property this will store the name of the robot command 
    public string Name { get; set; }

    //  can store additional information about the command
    public string? Description { get; set; }

    // This property will indicate whether the command moves the robot or not
    public bool IsMoveCommand { get; set; }

    // This will store the date and time when the command was created
    public DateTime CreatedDate { get; set; }

    // This will store the last date and time when the command was updatEd
    public DateTime ModifiedDate { get; set; }
    // Required for ORM mapping
    public RobotCommand()
    {
    }
    // This constructor will initialize all properties when a new Robot Command objecty is created
    public RobotCommand(
        int id,
        string name,
        bool isMoveCommand,
        DateTime createdDate,
        DateTime modifiedDate,
        string? description = null)
    {

        Id = id; // Assigns a unique id to the command

        Name = name; // Stores the name of the robot command

        IsMoveCommand = isMoveCommand; // Specifies whether the command moves the robot

        CreatedDate = createdDate; // Stores when the command was created

        ModifiedDate = modifiedDate; // Stores when the command was last modified

        Description = description; // Stores optional description of the command
    }
}