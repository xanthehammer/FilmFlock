/// <summary>
/// A User in a Room that is participating in selecting a movie.
/// </summary>
[Serializable]
public struct User
{
    public Guid UserId { get; }
    public string Username { get; }

    public List<string> SuggestedMovies { get; set; }

    /// <summary>
    /// Create a new UserModel that does not already exist.
    /// </summary>
    public User(string username)
    {
        UserId = System.Guid.NewGuid();
        Username = username;
        SuggestedMovies = [];
    }

    /// <summary>
    /// Create a User from pre-existing data, such as a database table.
    /// </summary>
    public User(Guid userId, string username, List<string> suggestedMovies)
    {
        UserId = userId;
        Username = username;
        SuggestedMovies = suggestedMovies;
    }
}