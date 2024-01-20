using System.Net.NetworkInformation;
using System.Runtime.CompilerServices;
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

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CreateRoomPostBody postBody)
    {
        RoomModel room = new RoomModel(postBody.FilmSelectionMethod, postBody.PerUserFilmLimit);
        RoomStorage.AddRoom(room);
        return Ok(room);
    }

}

public class CreateRoomPostBody
{
    public FilmSelectionMethodType FilmSelectionMethod { get; set; }
    public ushort PerUserFilmLimit { get; set; }

    public CreateRoomPostBody(FilmSelectionMethodType filmSelectionMethod, ushort perUserFilmLimit)
    {
        FilmSelectionMethod = filmSelectionMethod;
        PerUserFilmLimit = perUserFilmLimit;
    }
}