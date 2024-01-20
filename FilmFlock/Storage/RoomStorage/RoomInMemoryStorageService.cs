
using FilmFlock.Models;

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

    public void UpdateRoom(RoomModel updatedRoom)
    {
        RoomModel existingRoom = Rooms.Where(room => room.RoomId == updatedRoom.RoomId).First();
        int indexOfExistingRoom = Rooms.IndexOf(existingRoom);

        if (indexOfExistingRoom != -1)
            Rooms[indexOfExistingRoom] = updatedRoom;
    }

    public RoomModel? GetRoom(string roomId)
    {
        return Rooms.Find(room => room.RoomId == roomId);
    }
}
