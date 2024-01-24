namespace FilmFlock.Models.Activities;

public class UpvoteActivity
{
    public Guid ActivityId { get; }
    public string RoomId { get; }
    public ushort PerUserVoteLimit { get; }
    public HashSet<UserUpvoteLog> UserVoterLogs { get; }

    /// <summary>
    /// Create a new Upvote Activity.
    /// </summary>
    public UpvoteActivity(string roomId, ushort perUserVoteLimit)
    : this(System.Guid.NewGuid(), roomId, perUserVoteLimit, [])
    {
    }

    /// <summary>
    /// Create an existing Upvote Activity. For example, from persistence.
    /// </summary>
    public UpvoteActivity(Guid activityId, string roomId, ushort perUserVoteLimit, UserUpvoteLog[] voteLogs)
    {
        ActivityId = activityId;
        RoomId = roomId;
        PerUserVoteLimit = perUserVoteLimit;
        UserVoterLogs = new HashSet<UserUpvoteLog>(voteLogs);
    }

    public void RecordVote(Guid idOfVotingUser, string film)
    {
        UserUpvoteLog? voterLog = UserVoterLogs.FirstOrDefault(log => log.UserId == idOfVotingUser);
        if (voterLog is null)
        {
            voterLog = new UserUpvoteLog(idOfVotingUser);
            UserVoterLogs.Add(voterLog);
        }
        UserUpvoteLog safeVoterLog = voterLog;

        if (safeVoterLog.VoteCount < PerUserVoteLimit)
            safeVoterLog.VoteFor(film);
    }
}

public class UserUpvoteLog
{
    public Guid UserId { get; }
    public ushort VoteCount { get; set; }
    public Dictionary<string, ushort> Votes { get; }

    public UserUpvoteLog(Guid voterId)
    : this (voterId, new Dictionary<string, ushort>())
    {
    }

    public UserUpvoteLog(Guid voterId, Dictionary<string, ushort> votes)
    {
        UserId = voterId;
        Votes = votes;
        ushort voteCount = 0;
        foreach (var keyValuePair in votes)
        {
            voteCount += keyValuePair.Value;
        }
        VoteCount = voteCount;
    }

    public void VoteFor(string targetFilm)
    {
        if (Votes.ContainsKey(targetFilm))
        {
            ushort existingVotes = Votes[targetFilm];
            Votes[targetFilm] = (ushort)(existingVotes + 1);
        }
        else
        {
            Votes[targetFilm] = 1;
        }
        VoteCount += 1;
    }

    public override bool Equals(object? obj)
    {
        if (!(obj is UserUpvoteLog otherLog))
            return false;
        
        return otherLog.UserId == UserId && otherLog.Votes == Votes;
    }

    public override int GetHashCode() {
        return HashCode.Combine(UserId, Votes);
    }
}
