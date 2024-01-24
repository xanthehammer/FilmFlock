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
    private IRoomIdGenerator RoomIdGenerator;
    private IRoomActivityCreating ActivityCreator;

    private const FilmSelectionMethod DefaultSelectionMethod = FilmSelectionMethod.Upvoting;
    private const ushort DefaultFilmLimit = 3;

    public CreateRoomController(IRoomStorage roomStorage, IRoomIdGenerator roomIdGenerator, IRoomActivityCreating activityCreating)
    {
        RoomStorage = roomStorage;
        RoomIdGenerator = roomIdGenerator;
        ActivityCreator = activityCreating;
    }

    [HttpGet]
    public IActionResult Get()
    {
        string newRoomId = RoomIdGenerator.GenerateNew();
        Room newRoom = new Room(newRoomId, DefaultSelectionMethod, DefaultFilmLimit);
        RoomStorage.AddRoom(newRoom);

        CreateRoomResponse response = new CreateRoomResponse(newRoom);
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

        string newRoomId = RoomIdGenerator.GenerateNew();
        Room newRoom = new Room(newRoomId, selectionMethod, perUserFilmLimit);
        RoomStorage.AddRoom(newRoom);

        ActivityCreator.StoreActivity(selectionMethod, room.RoomId);

        CreateRoomResponse response = new CreateRoomResponse(newRoom);
        return Ok(response);
    }
}
