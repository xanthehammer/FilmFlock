using System.Text.Json.Serialization;
using System.Xml;

namespace FilmFlock.Models;

[Serializable]
public class RoomModel
{
    public string RoomId { get; }
    public Guid AdminId { get; }
    public FilmSelectionMethod FilmSelectionMethod { get; }
    public ushort PerUserFilmLimit { get; }
    public List<UserModel> Users { get; set; }

    public RoomModel(string roomId, Guid adminId, FilmSelectionMethod selectionMethod, ushort perUserFilmLimit, List<UserModel> users)
    {
        RoomId = roomId;
        AdminId = adminId;
        FilmSelectionMethod = selectionMethod;
        PerUserFilmLimit = perUserFilmLimit;
        Users = new List<UserModel>(users);
    }

    public RoomModel(FilmSelectionMethod selectionMethod, ushort perUserFilmLimit)
    : this(System.Guid.NewGuid().ToString(), System.Guid.NewGuid(), selectionMethod, perUserFilmLimit, [])
    {
    }

    public UserModel[] GetUsers()
    {
        return Users.ToArray();
    }

}
