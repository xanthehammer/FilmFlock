using FilmFlock.Models;
using Microsoft.AspNetCore.Mvc;
using FilmFlock.Mongo;

namespace FilmFlock.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GetUsersController: ControllerBase
{
    private IRoomStorage RoomStorage;

    public GetUsersController(IRoomStorage roomStorage)
    {
        RoomStorage = roomStorage;
    }

    [HttpGet]
    public IActionResult Get([FromQuery] string roomId)
    {
        Room? requestedRoom = RoomStorage.GetRoom(roomId);
        string[] users = requestedRoom.Users.Select(user => user.Username).ToArray();
        return Ok(users);
    }

};