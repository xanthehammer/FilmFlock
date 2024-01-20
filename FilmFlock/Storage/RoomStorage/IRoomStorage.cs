using FilmFlock.Models;

public interface IRoomStorage
{
    public void AddRoom(RoomModel room);
    public void UpdateRoom(RoomModel room);
    public RoomModel? GetRoom(string roomId);
}