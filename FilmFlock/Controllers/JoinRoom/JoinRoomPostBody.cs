using FilmFlock.Models;

namespace FilmFlock.Controllers;

public class JoinRoomPostBody
{
    public string RoomId { get; set; }
    public string Username { get; set; }

    public JoinRoomPostBody(string roomId, string username)
    {
        RoomId = roomId;
        Username = username;
    }
}