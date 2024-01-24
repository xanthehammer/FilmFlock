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

    [HttpPost]
    public IActionResult Post([FromBody] CreateRoomPostBody postBody)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        string userName = postBody.UserName;

        FilmSelectionMethod selectionMethod = postBody.FilmSelectionMethod ?? DefaultSelectionMethod;
        ushort perUserFilmLimit = postBody.PerUserFilmLimit ?? DefaultFilmLimit;

        string newRoomId = RoomIdGenerator.GenerateNew();

        User hatMan = new User(userName);

        Room newRoom = new Room(newRoomId, hatMan.UserId, selectionMethod, perUserFilmLimit, new List<User> {hatMan});
        RoomStorage.AddRoom(newRoom);

        ActivityCreator.StoreActivity(selectionMethod, newRoom.RoomId);

        CreateRoomResponse response = new CreateRoomResponse(newRoom);
        return Ok(response);
    }
}
