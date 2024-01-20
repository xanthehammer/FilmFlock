using System.Text.Json.Serialization;
using System.Xml;

namespace FilmFlock.Models;

[Serializable]
public class RoomModel
{
    public string RoomId { get; }
    public Guid AdminId { get; }
    public FilmSelectionMethodType FilmSelectionMethod { get; }
    public ushort PerUserFilmLimit { get; }
    public List<UserModel> Users { get; set; }
    public List<string> Movies { get; set; }

    public RoomModel(string roomId, Guid adminId, FilmSelectionMethodType selectionMethod, ushort perUserFilmLimit, UserModel[] users)
    {
        RoomId = roomId;
        AdminId = adminId;
        FilmSelectionMethod = selectionMethod;
        PerUserFilmLimit = perUserFilmLimit;

        Users = new List<UserModel>(users);
        Movies = [];
    }

    public RoomModel(FilmSelectionMethodType selectionMethod, ushort perUserFilmLimit)
    : this(System.Guid.NewGuid().ToString(), System.Guid.NewGuid(), selectionMethod, perUserFilmLimit, [])
    {
    }
    
    public void AddUser(UserModel newUser)
    {
        if (Users.Any(user => String.Equals(newUser.Username, user.Username, StringComparison.OrdinalIgnoreCase)))
            return;

        Users.Add(newUser);
    }

    public UserModel[] GetUsers()
    {
        return Users.ToArray();
    }

}