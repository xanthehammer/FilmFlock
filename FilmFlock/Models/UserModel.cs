using System.Text.Json.Serialization;
using System.Xml;

/// <summary>
/// A User in a Room that is participating in selecting a movie.
/// </summary>
[Serializable]
public struct UserModel
{
    public Guid UserId { get; }
    public string Username { get; }

    public List<string> SuggestedMovies { get; set; }

    /// <summary>
    /// Create a new UserModel that does not already exist.
    /// </summary>
    public UserModel(string username)
    {
        UserId = System.Guid.NewGuid();
        Username = username;
        SuggestedMovies = [];
    }

    /// <summary>
    /// Create a UserModel from pre-existing data, such as a database table.
    /// </summary>
    public UserModel(Guid userId, string username, List<string> suggestedMovies)
    {
        UserId = userId;
        Username = username;
        SuggestedMovies = suggestedMovies;
    }
}