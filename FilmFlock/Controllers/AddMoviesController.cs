using System.Net.NetworkInformation;
using FilmFlock.Models;
using Microsoft.AspNetCore.Mvc;

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
        if (RoomStorage.GetRoom(postBody.RoomId) == null)
            return BadRequest("Requested room ID does not exist.");

        RoomModel requestedRoom = RoomStorage.GetRoom(postBody.RoomId);
        UserModel[] existingUsers = requestedRoom.GetUsers();
        if (!existingUsers.Any(user => String.Equals(postBody.UserId, user.UserId.ToString(), StringComparison.OrdinalIgnoreCase)))
            return BadRequest("Requested user does not exist in room.");

        foreach (string movie in postBody.Films)
        {
            requestedRoom.Movies.Add(movie);
        }
        
        return Ok();
    }

}

public class AddMoviesPostBody
{
    public string RoomId { get; set; }
    public string UserId { get; set; }
    public string[] Films { get; set; }

    public AddMoviesPostBody(string roomId, string userId, string[] films)
    {
        RoomId = roomId;
        UserId = userId;
        Films = films;
    }
}