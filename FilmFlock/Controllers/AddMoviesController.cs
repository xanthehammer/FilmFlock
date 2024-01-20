using System.Net.NetworkInformation;
using FilmFlock.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace FilmFlock.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AddMoviesController : ControllerBase
{
    private IRoomStorage RoomStorage;

    public AddMoviesController(IRoomStorage roomStorage)
    {
        RoomStorage = roomStorage;
    }

    [HttpPost]
    public IActionResult Post([FromBody] AddMoviesPostBody postBody)
    {
        RoomModel requestedRoom = RoomStorage.GetRoom(postBody.RoomId);
        if (requestedRoom == null)
            return BadRequest("Requested room ID does not exist.");

        UserModel[] existingUsers = requestedRoom.GetUsers();
        if (!existingUsers.Any(user => Guid.Equals(postBody.UserId, user.UserId)))
            return BadRequest("Requested user does not exist in room.");

        UserModel? user = existingUsers.ToList().Find(user => Guid.Equals(postBody.UserId, user.UserId));
        if (user == null)
            return BadRequest("Bad UserId.");
        UserModel safeUser = (UserModel) user;

        var resultingFilmCount = safeUser.SuggestedMovies.Count + postBody.Films.Length;
        if (resultingFilmCount > requestedRoom.PerUserFilmLimit)
            return BadRequest("Adding the provided {postBody.Films.Count} film suggestions would put the user over the room limit of {requestedRoom.PerUserFilmLimit}. All films rejected.");
        
        safeUser.SuggestedMovies.AddRange(postBody.Films);
        
        return Ok();
    }

}

public class AddMoviesPostBody
{
    public string RoomId { get; set; }
    public Guid UserId { get; set; }
    public string[] Films { get; set; }

    public AddMoviesPostBody(string roomId, Guid userId, string[] films)
    {
        RoomId = roomId;
        UserId = userId;
        Films = films;
    }
}