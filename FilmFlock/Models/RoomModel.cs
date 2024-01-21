using System.Text.Json.Serialization;
using System.Xml;

namespace FilmFlock.Models;

[Serializable]
public class Room
{
    public string RoomId { get; }
    public Guid AdminId { get; }
    public FilmSelectionMethod FilmSelectionMethod { get; }
    public ushort PerUserFilmLimit { get; }
    public List<User> Users { get; set; }

    public Room(string roomId, Guid adminId, FilmSelectionMethod selectionMethod, ushort perUserFilmLimit, List<User> users)
    {
        RoomId = roomId;
        AdminId = adminId;
        FilmSelectionMethod = selectionMethod;
        PerUserFilmLimit = perUserFilmLimit;
        Users = new List<User>(users);
    }

    public Room(FilmSelectionMethod selectionMethod, ushort perUserFilmLimit)
    : this(System.Guid.NewGuid().ToString(), System.Guid.NewGuid(), selectionMethod, perUserFilmLimit, [])
    {
    }

    public User[] GetUsers()
    {
        return Users.ToArray();
    }

}
