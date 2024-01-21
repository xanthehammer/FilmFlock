namespace FilmFlock.Controllers;

[Serializable]
public readonly struct JoinRoomResponse
{
    public Guid UserId { get; }

    public JoinRoomResponse(User user)
    {
        UserId = user.UserId;
    }
}
