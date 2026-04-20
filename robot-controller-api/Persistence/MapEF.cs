using robot_controller_api.Models; // used to access Map model
using Microsoft.EntityFrameworkCore; // used for Entity Framework features

namespace robot_controller_api.Persistence;

public class MapEF : IMapDataAccess    // this class is used to handle map data using Entity Framework
{
    private readonly RobotContext _context; // this stores database context

    public MapEF(RobotContext context)   // constructor to get database context from program
    {
        _context = context; // assigning context
    }

    public List<Map> GetMaps()   // this method gets all maps from database
    {
        return _context.Maps.ToList(); // fetch all maps
    }

    public List<Map> GetSquareMaps()    // this method gets only square maps rows = columns
    {
        return _context.Maps
            .Where(m => m.Rows == m.Columns) // filter square maps
            .ToList(); // convert to list
    }

    public Map? GetMapById(int id)      // this method finds a map using its id
    {
        return _context.Maps.Find(id); // search by id
    }

    public Map AddMap(Map map)    // this method adds a new map
    {
        map.CreatedDate = DateTime.Now; // set created time
        map.ModifiedDate = DateTime.Now; // set modified time

        _context.Maps.Add(map); // add to database
        _context.SaveChanges(); // save changes

        return map; // return added map
    }

    public bool UpdateMap(int id, Map map)   // this method updates an existing map
    {
        var existing = _context.Maps.Find(id); // find map

        if (existing == null)
            return false; // if not found return false

        existing.Name = map.Name;   // update values
        existing.Rows = map.Rows;
        existing.Columns = map.Columns;
        existing.Description = map.Description;
        existing.ModifiedDate = DateTime.Now;

        _context.SaveChanges(); // save changes

        return true; // return success
    }

    public bool DeleteMap(int id)   // this method deletes a map
    {
        var map = _context.Maps.Find(id); // find map

        if (map == null)
            return false; // if not found return false

        _context.Maps.Remove(map); // remove from database
        _context.SaveChanges(); // save changes

        return true; // return success
    }
}