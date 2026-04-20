using robot_controller_api.Persistence;
using Microsoft.AspNetCore.Mvc;

namespace robot_controller_api.Controllers;   //Dependency injection is used here to receive the map repository.

[ApiController]
[Route("api/maps")]
public class MapsController : ControllerBase
{
    // Dependency injection: repository will be provided automatically
    private readonly IMapDataAccess _mapRepo;   //automatically provides the correct repository implementation

    // Constructor receives repository from the DI container
    public MapsController(IMapDataAccess mapRepo)
    {
        _mapRepo = mapRepo;
    }

    // list of maps in the system (temporary in-memory list)
    private static readonly List<Map> _maps = new List<Map>
    {
        new Map(1, 5, 5, "MOON", DateTime.Now, DateTime.Now),
        new Map(2, 10, 8, "DEAKIN", DateTime.Now, DateTime.Now),
        new Map(3, 6, 6, "BURWOOD", DateTime.Now, DateTime.Now)
    };

    // GET all maps from database
    [HttpGet]
    public IEnumerable<Map> GetAllMaps()
    {
        return _mapRepo.GetMaps();
    }

    // GET square maps (rows == columns)
    [HttpGet("square")]
    public IEnumerable<Map> GetSquareMaps()
    {
        return _maps.Where(m => m.Rows == m.Columns);
    }

    // GET map by ID
    [HttpGet("{id}", Name = "GetMap")]
    public IActionResult GetMapById(int id)
    {
        var map = _maps.FirstOrDefault(m => m.Id == id);

        if (map == null)
            return NotFound();

        return Ok(map);
    }

    // POST new map
    [HttpPost]
    public IActionResult AddMap(Map newMap)
    {
        if (newMap == null)
            return BadRequest();

        int newId = _maps.Max(m => m.Id) + 1;

        var map = new Map(
            newId,
            newMap.Columns,
            newMap.Rows,
            newMap.Name,
            DateTime.Now,
            DateTime.Now,
            newMap.Description
        );

        _maps.Add(map);

        return CreatedAtRoute("GetMap", new { id = map.Id }, map);
    }

    // PUT update map
    [HttpPut("{id}")]
    public IActionResult UpdateMap(int id, Map updatedMap)
    {
        var map = _maps.FirstOrDefault(m => m.Id == id);

        if (map == null)
            return NotFound();

        map.Name = updatedMap.Name;
        map.Rows = updatedMap.Rows;
        map.Columns = updatedMap.Columns;
        map.Description = updatedMap.Description;
        map.ModifiedDate = DateTime.Now;

        return NoContent();
    }

    // DELETE map
    [HttpDelete("{id}")]
    public IActionResult DeleteMap(int id)
    {
        var map = _maps.FirstOrDefault(m => m.Id == id);

        if (map == null)
            return NotFound();

        _maps.Remove(map);

        return NoContent();
    }

    // Check if coordinate exists on map
    [HttpGet("{id}/{x}-{y}")]
    public IActionResult CheckCoordinate(int id, int x, int y)
    {
        if (x < 0 || y < 0)
            return BadRequest();

        var map = _maps.FirstOrDefault(m => m.Id == id);

        if (map == null)
            return NotFound();

        bool isOnMap = x < map.Columns && y < map.Rows;

        return Ok(isOnMap);
    }
}