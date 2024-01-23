namespace FilmFlock.Controllers;

[Serializable]
public readonly struct VoteTallyResponse
{
    public Dictionary<string, int> Tally { get; }

    public VoteTallyResponse(Dictionary<string, int> tally)
    {
        Tally = tally;
    }
}