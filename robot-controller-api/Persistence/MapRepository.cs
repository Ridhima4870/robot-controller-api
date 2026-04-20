using robot_controller_api;     // using the Map class created earlier

namespace robot_controller_api.Persistence;

// this class is responsible for getting map data from the database
public class MapRepository : IMapDataAccess, IRepository
{
    // this line allows this class to use the generic repository method
    private IRepository _repo => this;

    // this method gets all maps from the database
    public List<Map> GetMaps()
    {
        // calling the generic ExecuteReader method to run the SQL query
        // this query selects all records from the map table
        var maps = _repo.ExecuteReader<Map>(
            "SELECT * FROM public.map");

        // returning the list of maps to the controller
        return maps;
    }
}