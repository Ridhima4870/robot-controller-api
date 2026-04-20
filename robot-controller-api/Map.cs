namespace robot_controller_api;

public class Map
{
    // Here I have created an Id property. This will uniquely identify each map in the system.
    public int Id { get; set; }

    // Here I have created a Columns property. This represents the number of columns in the map x-axis.
    public int Columns { get; set; }

    // Here I have created a Rows property \his represents the number of rows in the map y-axis
    public int Rows { get; set; }

    // Here I have created a Name property. it will storei the name of the map = > MOON, DEAKIN, BURWOOD
    public string Name { get; set; }

    // Here I have created a Description property can store additional information about the map
    public string? Description { get; set; }

    // Here I have created a Created Date propertythis will store the date and time when the map was created
    public DateTime CreatedDate { get; set; }

    // Here I have created a Modified Date property this will store the last date and time when the map was  updated 
    public DateTime ModifiedDate { get; set; }

    // Required for ORM mapping
    public Map()
    {
    }
    // This constructor will initialize all properties when a new Map object is created
    public Map(
        int id,
        int columns,
        int rows,
        string name,
        DateTime createdDate,
        DateTime modifiedDate,
        string? description = null)
    {
        // Here I hada assign the given values to the Map properties

        Id = id; // Assigns unique id to the map

        Columns = columns; // sets the number of columns in the map

        Rows = rows; // sets the number of rows in the map

        Name = name; // assigns the name of the map

        CreatedDate = createdDate; // sotores when the map was created

        ModifiedDate = modifiedDate; // atores when the map was last modified

        Description = description; // stores optional description of the map
    }
}