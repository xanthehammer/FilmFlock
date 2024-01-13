using System.Net.NetworkInformation;
using FilmFlock.Models;
using Microsoft.AspNetCore.Mvc;

namespace FilmFlock.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CreateRoomController: ControllerBase
{

    private IRoomStorage RoomStorage;

    public CreateRoomController(IRoomStorage roomStorage)
    {
        RoomStorage = roomStorage;
    }

    [HttpGet]
    public IActionResult Get()
    {
        RoomModel room = new RoomModel();
        RoomStorage.AddRoom(room);
        return Ok(room);
    }

}