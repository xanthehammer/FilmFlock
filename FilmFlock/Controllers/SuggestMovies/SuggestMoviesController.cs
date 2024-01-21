using FilmFlock.Models;
using Microsoft.AspNetCore.Mvc;

namespace FilmFlock.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SuggestMoviesController : ControllerBase
{
    private IRoomStorage RoomStorage;

    public SuggestMoviesController(IRoomStorage roomStorage)
    {
        RoomStorage = roomStorage;
    }

    [HttpPost]
    public IActionResult Post([FromBody] SuggestMoviesPostBody postBody)
    {
        Room? requestedRoom = RoomStorage.GetRoom(postBody.RoomId);
        if (requestedRoom == null)
            return BadRequest("Requested room ID does not exist.");
        Room safeRequestedRoom = (Room) requestedRoom;

        User[] existingUsers = safeRequestedRoom.GetUsers();
        if (!existingUsers.Any(user => Guid.Equals(postBody.UserId, user.UserId)))
            return BadRequest("Requested user does not exist in room.");

        User? user = existingUsers.ToList().Find(user => Guid.Equals(postBody.UserId, user.UserId));
        if (user == null)
            return BadRequest("Bad UserId.");
        User safeUser = (User) user;

        var resultingFilmCount = safeUser.SuggestedMovies.Count + postBody.Films.Length;
        if (resultingFilmCount > requestedRoom.PerUserFilmLimit)
            return BadRequest("Adding the provided {postBody.Films.Count} film suggestions would put the user over the room limit of {requestedRoom.PerUserFilmLimit}. All films rejected.");
        
        safeUser.SuggestedMovies.AddRange(postBody.Films);
        
        return Ok();
    }

}