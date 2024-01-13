using System.Net.NetworkInformation;
using FilmFlock.Models;
using Microsoft.AspNetCore.Mvc;

namespace FilmFlock.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CreateRoomController: ControllerBase
{

    [HttpGet]
    public IActionResult Get()
    {
        RoomModel room = new RoomModel();
        return Ok(room);
    }

}