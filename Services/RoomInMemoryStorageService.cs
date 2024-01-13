
using System.Collections.Generic;
using FilmFlock.Models;

public interface IRoomStorage
{
    public void AddRoom(RoomModel room);
}

public class RoomInMemoryStorageService : IRoomStorage
{
    List<RoomModel> rooms;

    public RoomInMemoryStorageService()
    {
        rooms = new List<RoomModel>{};
    }

    public void AddRoom(RoomModel room)
    {
        rooms.Add(room);
    }
}
