using FilmFlock.Models;
using System.ComponentModel.DataAnnotations;

public class UpvoteFilmPostBody
{
    public Guid UserId { get; set; }
    public string RoomId { get; set; }
    public string Film { get; set; }

    public UpvoteFilmPostBody(Guid userId, string roomId, string film)
    {
        UserId = UserId;
        RoomId = roomId;
        Film = film;
    }
}