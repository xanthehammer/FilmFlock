using System.Net.NetworkInformation;
using FilmFlock.Models;
using Microsoft.AspNetCore.Mvc;

namespace FilmFlock.Controllers;

/// <summary>
/// A Controller that handles requests from users to join an existing movie night room.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class JoinRoomController: ControllerBase
{

    private IRoomStorage RoomStorage;
    private IUserStorage UserStorage;

    public JoinRoomController(IRoomStorage roomStorage, IUserStorage userStorage)
    {
        RoomStorage = roomStorage;
        UserStorage = userStorage;
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] JoinRoomPostBody postBody)
    {
        RoomModel? requestedRoom = RoomStorage.GetRoom(postBody.RoomId);
        if (requestedRoom == null)
            return BadRequest("Requested room ID does not exist.");
        
        Console.WriteLine("Yo!");
        UserModel[] existingUsers = UserStorage.GetUsers(postBody.RoomId);
        if (existingUsers.Any(user => String.Equals(postBody.Username, user.Username, StringComparison.OrdinalIgnoreCase)))
            return BadRequest("Provided username already exists in this room.");

        Console.WriteLine("Yo!2");
        var newUser = new UserModel(postBody.Username, postBody.RoomId);
        UserStorage.AddUser(newUser);

        return Ok(newUser.UserId);
    }

}

public class JoinRoomPostBody
{
    public string RoomId { get; set; }
    public string Username { get; set; }

    public JoinRoomPostBody(string roomId, string username)
    {
        RoomId = roomId;
        Username = username;
    }
}
