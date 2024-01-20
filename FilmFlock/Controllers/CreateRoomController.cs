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

    private const FilmSelectionMethodType DefaultSelectionMethod = FilmSelectionMethodType.upvoting;
    private const ushort DefaultFilmLimit = 3;

    public CreateRoomController(IRoomStorage roomStorage)
    {
        RoomStorage = roomStorage;
    }

    [HttpPost]
    public IActionResult Post([FromBody] CreateRoomPostBody postBody)
    {
        FilmSelectionMethodType selectionMethod = postBody.FilmSelectionMethod ?? DefaultSelectionMethod;
        ushort perUserFilmLimit = postBody.PerUserFilmLimit ?? DefaultFilmLimit;

        RoomModel room = new RoomModel(selectionMethod, perUserFilmLimit);
        RoomStorage.AddRoom(room);
        return Ok(room);
    }

}

public class CreateRoomPostBody
{
    public FilmSelectionMethodType? FilmSelectionMethod { get; set; }
    public ushort? PerUserFilmLimit { get; set; }

    public CreateRoomPostBody(FilmSelectionMethodType? filmSelectionMethod, ushort? perUserFilmLimit)
    {
        FilmSelectionMethod = filmSelectionMethod;
        PerUserFilmLimit = perUserFilmLimit;
    }
}