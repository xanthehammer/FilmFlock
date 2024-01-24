using FilmFlock.Models.Activities;

namespace FilmFlock.Mongo;

public interface IUpvoteActivityStorage
{
    public void AddActivity(UpvoteActivity upvoteActivity);
    public void UpdateActivity(UpvoteActivity upvoteActivity);
    public UpvoteActivity? GetActivity(Guid activityId);
    public UpvoteActivity? GetActivity(string roomId);
}
