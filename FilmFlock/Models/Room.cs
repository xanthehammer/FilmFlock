using System.Text.Json.Serialization;
using System.Xml;
using Microsoft.Extensions.ObjectPool;
using Microsoft.VisualBasic;

namespace FilmFlock.Models;

public class Room
{
    public string RoomId { get; }
    public DateTime CreationDate { get; }
    public Guid AdminId { get; }
    public FilmSelectionMethod FilmSelectionMethod { get; }
    public ushort PerUserFilmLimit { get; }
    public List<User> Users { get; }

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

public interface IRoomIdGenerator
{
    public string GenerateNew();
}

public class RoomIdGenerator : IRoomIdGenerator
{
    // We've removed ambiguous characters like 0, O, 1, and I.
    private static char[] RoomIdCharacters = {
       '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'J', 'K', 'L', 'M', 'N', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z'
    };

    private IRoomStorage RoomStorage;

    public RoomIdGenerator(IRoomStorage roomStorage)
    {
        RoomStorage = roomStorage;
    }

    public string GenerateNew()
    {
        char tumbler1 = RandomCharacter();
        char tumbler2 = RandomCharacter();
        char tumbler3 = RandomCharacter();
        char tumbler4 = RandomCharacter();
        char tumbler5 = RandomCharacter();

        string proposedId = "{tumbler1}{tumbler2}{tumbler3}{tumbler4}{tumbler5}";
        Room? existingRoom = RoomStorage.GetRoom(proposedId);
        if (existingRoom == null)
            return proposedId;
        
        return GenerateNew();
    }

    private char RandomCharacter()
    {
        Random random = new Random();
        int randomIndex = random.Next(0, RoomIdGenerator.RoomIdCharacters.Length);

        return RoomIdCharacters[randomIndex];
    }
}
