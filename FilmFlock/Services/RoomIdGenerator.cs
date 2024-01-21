using FilmFlock.Models;

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
        return GenerateNew([], 0);
    }

    private string GenerateNew(List<char> chars, int attempts)
    {
        if (chars.Count == 0 || attempts < 3)
        {
            char[] fourCharId = { RandomCharacter(), RandomCharacter(), RandomCharacter(), RandomCharacter() };
            chars = new List<char>(fourCharId);
        }
        else
        {
            chars.Add(RandomCharacter());
        }
        string proposedId = new string(chars.ToArray());
        Room? existingRoom = RoomStorage.GetRoom(proposedId);
        if (existingRoom == null)
            return proposedId;
        
        return GenerateNew(chars, attempts + 1);
    }

    private char RandomCharacter()
    {
        Random random = new Random();
        int randomIndex = random.Next(0, RoomIdGenerator.RoomIdCharacters.Length);

        return RoomIdCharacters[randomIndex];
    }
}