using System.Text.Json.Serialization;
using System.Xml;

/// <summary>
/// A User in a Room that is participating in selecting a movie.
/// </summary>
[Serializable]
public readonly struct UserModel
{
    /// <summary>
    /// The unique ID of the user. Although the ID is generated to be globally
    /// unique all comparisons between two UserModels should also compare the RoomIds
    /// of each. For example, 8E32D810-5446-45F6-B44E-4544708D3168.
    /// </summary>
    public Guid UserId { get; }
    /// <summary>
    /// The username selected by the user to whom this model belongs.
    /// For example, "Poo Manchu".
    /// </summary>
    public string Username { get; }
    /// <summary>
    /// The ID of the room to which this user belongs. Essentially a foreign key.
    /// For example, "4ED6".
    /// </summary>
    public string RoomId { get; }

    /// <summary>
    /// Create a new UserModel that does not already exist.
    /// </summary>
    /// <param name="username">The personalized name selected by the user.</param>
    /// <param name="roomId">The ID of the room to which this user belongs.</param>
    public UserModel(string username, string roomId)
    {
        UserId = System.Guid.NewGuid();
        Username = username;
        RoomId = roomId;
    }

    /// <summary>
    /// Create a UserModel from pre-existing data, such as a database table.
    /// </summary>
    /// <param name="userId">The unique ID of the UserModel.</param>
    /// <param name="username">The personalized name selected by the user.</param>
    /// <param name="roomId">The ID of the room to which this user belongs.</param>
    public UserModel(Guid userId, string username, string roomId)
    {
        UserId = userId;
        Username = username;
        RoomId = roomId;
    }
}