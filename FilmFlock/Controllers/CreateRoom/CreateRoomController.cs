using System.Diagnostics;
using FilmFlock.Models;
using FilmFlock.Mongo;
using Microsoft.AspNetCore.Mvc;

namespace FilmFlock.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CreateRoomController: ControllerBase
{
    private IRoomStorage RoomStorage;
    private IRoomActivityCreating ActivityCreator;

    private const FilmSelectionMethod DefaultSelectionMethod = FilmSelectionMethod.Upvoting;
    private const ushort DefaultFilmLimit = 3;

    public CreateRoomController(IRoomStorage roomStorage, IRoomActivityCreating activityCreating)
    {
        RoomStorage = roomStorage;
        ActivityCreator = activityCreating;
    }

    [HttpGet]
    public IActionResult Get()
    {
        Room room = new Room(DefaultSelectionMethod, DefaultFilmLimit);
        RoomStorage.AddRoom(room);

        CreateRoomResponse response = new CreateRoomResponse(room);
        return Ok(response);
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

        ActivityCreator.StoreActivity(selectionMethod, room.RoomId);

        CreateRoomResponse response = new CreateRoomResponse(room);
        return Ok(response);
    }
}
