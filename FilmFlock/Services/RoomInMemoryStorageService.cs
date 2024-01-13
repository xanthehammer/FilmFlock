
using System.Collections.Generic;
using FilmFlock.Models;

public interface IRoomStorage
{
    public void AddRoom(RoomModel room);
    public RoomModel? GetRoom(string roomId);
}

public class RoomInMemoryStorageService : IRoomStorage
{
    private List<RoomModel> Rooms;

    public RoomInMemoryStorageService()
    {
        Rooms = new List<RoomModel>{};
    }

    public void AddRoom(RoomModel room)
    {
        Rooms.Add(room);
    }

    public RoomModel? GetRoom(string roomId)
    {
        return Rooms.Find(room => room.RoomId == roomId);
    }
}
