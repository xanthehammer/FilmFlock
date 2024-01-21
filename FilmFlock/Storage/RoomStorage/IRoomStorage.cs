using FilmFlock.Models;

public interface IRoomStorage
{
    public void AddRoom(Room room);
    public void UpdateRoom(Room room);
    public Room? GetRoom(string roomId);
}
