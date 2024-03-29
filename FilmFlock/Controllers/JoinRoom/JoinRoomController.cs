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
    public IActionResult Post([FromBody] JoinRoomPostBody postBody)
    {
        Room? requestedRoom = RoomStorage.GetRoom(postBody.RoomId);
        if (requestedRoom == null)
            return BadRequest("Requested room ID does not exist.");
        Room safeRequestedRoom = requestedRoom;
        
        User[] existingUsers = requestedRoom.GetUsers();
        if (existingUsers.Any(user => String.Equals(postBody.Username, user.Username, StringComparison.OrdinalIgnoreCase)))
            return BadRequest("Provided username already exists in this room.");

        var newUser = new User(postBody.Username);
        safeRequestedRoom.Users.Add(newUser);
        RoomStorage.UpdateRoom(safeRequestedRoom);

        JoinRoomResponse response = new JoinRoomResponse(newUser);
        return Ok(response);
    }

}
