using System.Text.Json.Serialization;
using System.Xml;

namespace FilmFlock.Models;

[Serializable]
public class RoomModel
{
    public string RoomId { get; }
    public Guid AdminId { get; }

    public RoomModel(string roomId, Guid adminId)
    {
        RoomId = roomId;
        AdminId = adminId;
    }
    public RoomModel()
    {
        RoomId = System.Guid.NewGuid().ToString();
        AdminId = System.Guid.NewGuid();
    }

}