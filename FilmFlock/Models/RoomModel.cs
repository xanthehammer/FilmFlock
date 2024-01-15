using System.Text.Json.Serialization;
using System.Xml;

namespace FilmFlock.Models;

[Serializable]
public class RoomModel
{
    public string RoomId { get; }
    public Guid AdminId { get; }
    public List<UserModel> Users { get; set; }
    public List<string> Movies { get; set; }

    public RoomModel(string roomId, Guid adminId, UserModel[] users)
    {
        RoomId = roomId;
        AdminId = adminId;
        Users = new List<UserModel>(users);
        Movies = new List<string>();
    }
    public RoomModel()
    {
        RoomId = System.Guid.NewGuid().ToString();
        AdminId = System.Guid.NewGuid();
        Users = new List<UserModel>();
        Movies = new List<string>();
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