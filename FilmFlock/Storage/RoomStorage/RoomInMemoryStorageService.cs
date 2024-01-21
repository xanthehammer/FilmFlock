
using FilmFlock.Models;

public class RoomInMemoryStorageService : IRoomStorage
{
    private List<Room> Rooms;

    public RoomInMemoryStorageService()
    {
        Rooms = new List<Room>{};
    }

    public void AddRoom(Room room)
    {
        Rooms.Add(room);
    }

    public void UpdateRoom(Room updatedRoom)
    {
        Room existingRoom = Rooms.Where(room => room.RoomId == updatedRoom.RoomId).First();
        int indexOfExistingRoom = Rooms.IndexOf(existingRoom);

        if (indexOfExistingRoom != -1)
            Rooms[indexOfExistingRoom] = updatedRoom;
    }

    public Room? GetRoom(string roomId)
    {
        return Rooms.Find(room => room.RoomId == roomId);
    }
}
