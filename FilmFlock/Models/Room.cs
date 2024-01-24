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
        Users = users;
    }

    public Room(string roomId, Guid adminId, FilmSelectionMethod selectionMethod, ushort perUserFilmLimit, List<User> users)
    : this(
        roomId,
        DateTime.UtcNow,
        adminId,
        selectionMethod,
        perUserFilmLimit,
        users
    )
    {
    }

    public User[] GetUsers()
    {
        return Users.ToArray();
    }
}
