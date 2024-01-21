using System.ComponentModel.DataAnnotations;
using FilmFlock.Models;
using Microsoft.AspNetCore.Mvc;

namespace FilmFlock.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CreateRoomController: ControllerBase
{

    private IRoomStorage RoomStorage;

    private const FilmSelectionMethod DefaultSelectionMethod = FilmSelectionMethod.Upvoting;
    private const ushort DefaultFilmLimit = 3;

    public CreateRoomController(IRoomStorage roomStorage)
    {
        RoomStorage = roomStorage;
    }

    [HttpGet]
    public IActionResult Get()
    {
        Room room = new Room(DefaultSelectionMethod, DefaultFilmLimit);
        RoomStorage.AddRoom(room);
        return Ok(room);
    }

    [HttpPost]
    public IActionResult Post([FromBody] CreateRoomPostBody postBody)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        FilmSelectionMethod selectionMethod = postBody.FilmSelectionMethod ?? DefaultSelectionMethod;
        ushort perUserFilmLimit = postBody.PerUserFilmLimit ?? DefaultFilmLimit;

        Room room = new Room(selectionMethod, perUserFilmLimit);
        RoomStorage.AddRoom(room);
        return Ok(room);
    }

}

public class CreateRoomPostBody
{
    [EnumDataType(typeof(FilmSelectionMethod), ErrorMessage = "Invalid enum value")]
    public FilmSelectionMethod? FilmSelectionMethod { get; set; }
    public ushort? PerUserFilmLimit { get; set; }

    public CreateRoomPostBody(FilmSelectionMethod? filmSelectionMethod, ushort? perUserFilmLimit)
    {
        FilmSelectionMethod = filmSelectionMethod;
        PerUserFilmLimit = perUserFilmLimit;
    }
}