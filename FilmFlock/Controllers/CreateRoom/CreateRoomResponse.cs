using FilmFlock.Models;

namespace FilmFlock.Controllers;

[Serializable]
public readonly struct CreateRoomResponse
{
    public string RoomId { get; }
    public Guid AdminId { get; }
    public FilmSelectionMethod FilmSelectionMethod { get; }
    public ushort PerUserFilmLimit { get; }

    public CreateRoomResponse(Room room)
    {
        RoomId = room.RoomId;
        AdminId = room.AdminId;
        FilmSelectionMethod = room.FilmSelectionMethod;
        PerUserFilmLimit = room.PerUserFilmLimit;
    }
}