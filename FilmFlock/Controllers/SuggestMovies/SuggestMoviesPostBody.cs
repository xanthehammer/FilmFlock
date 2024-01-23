using FilmFlock.Models;

namespace FilmFlock.Controllers;

public class SuggestMoviesPostBody
{
    public string RoomId { get; set; }
    public Guid UserId { get; set; }
    public string[] Films { get; set; }

    public SuggestMoviesPostBody(string roomId, Guid userId, string[] films)
    {
        RoomId = roomId;
        UserId = userId;
        Films = films;
    }
}