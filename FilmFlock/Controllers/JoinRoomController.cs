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

    public JoinRoomController(IRoomStorage roomStorage)
    {
        RoomStorage = roomStorage;
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] JoinRoomPostBody postBody)
    {
        RoomModel? requestedRoom = RoomStorage.GetRoom(postBody.RoomId);
        if (requestedRoom == null)
            return BadRequest("Requested room ID does not exist.");
        
        UserModel[] existingUsers = requestedRoom.GetUsers();
        if (existingUsers.Any(user => String.Equals(postBody.Username, user.Username, StringComparison.OrdinalIgnoreCase)))
            return BadRequest("Provided username already exists in this room.");

        var newUser = new UserModel(postBody.Username);
        requestedRoom.AddUser(newUser);

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
