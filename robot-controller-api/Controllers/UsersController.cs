using Microsoft.AspNetCore.Mvc;
using robot_controller_api.Models;

namespace robot_controller_api.Controllers;

// this controller manages users
[ApiController]
[Route("api/users")]
public class UsersController : ControllerBase
{
    // temporary list to store users
    private static List<UserModel> _users = new List<UserModel>();

    // get all users
    [HttpGet]
    public IEnumerable<UserModel> GetUsers()
    {
        return _users;
    }

    // get user by id
    [HttpGet("{id}")]
    public IActionResult GetUserById(int id)
    {
        var user = _users.FirstOrDefault(u => u.Id == id);

        if (user == null)
            return NotFound();

        return Ok(user);
    }

    // add new user (register)
    [HttpPost]
    public IActionResult AddUser(UserModel user)
    {
        user.Id = _users.Count + 1;
        user.CreatedDate = DateTime.Now;
        user.ModifiedDate = DateTime.Now;

        _users.Add(user);

        return Ok(user);
    }

    // update user
    [HttpPut("{id}")]
    public IActionResult UpdateUser(int id, UserModel updatedUser)
    {
        var user = _users.FirstOrDefault(u => u.Id == id);

        if (user == null)
            return NotFound();

        user.FirstName = updatedUser.FirstName;
        user.LastName = updatedUser.LastName;
        user.Description = updatedUser.Description;
        user.Role = updatedUser.Role;
        user.ModifiedDate = DateTime.Now;

        return NoContent();
    }

    // delete user
    [HttpDelete("{id}")]
    public IActionResult DeleteUser(int id)
    {
        var user = _users.FirstOrDefault(u => u.Id == id);

        if (user == null)
            return NotFound();

        _users.Remove(user);

        return NoContent();
    }
}