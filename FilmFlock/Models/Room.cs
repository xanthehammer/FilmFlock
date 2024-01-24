namespace FilmFlock.Models;

public class Room
{
    public string RoomId { get; }
    public DateTime CreationDate { get; }
    public Guid AdminId { get; }
    public FilmSelectionMethod FilmSelectionMethod { get; }
    public ushort PerUserFilmLimit { get; }
    public List<User> Users { get; set; }

    public Room(
        string roomId,
        DateTime creationDate,
        Guid adminId,
        FilmSelectionMethod selectionMethod,
        ushort perUserFilmLimit,
        List<User> users
    )
    {
        RoomId = roomId;
        CreationDate = creationDate;
        AdminId = adminId;
        FilmSelectionMethod = selectionMethod;
        PerUserFilmLimit = perUserFilmLimit;
        Users = new List<User>(users);
    }

    public Room(string roomId, FilmSelectionMethod selectionMethod, ushort perUserFilmLimit)
    : this(
        roomId,
        DateTime.UtcNow,
        System.Guid.NewGuid(),
        selectionMethod,
        perUserFilmLimit,
        []
    )
    {
    }

    public User[] GetUsers()
    {
        return Users.ToArray();
    }

}
