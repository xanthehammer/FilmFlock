using FilmFlock.Models;
using FilmFlock.Models.Activities;
using FilmFlock.Mongo;

public interface IRoomActivityCreating
{
    public void StoreActivity(FilmSelectionMethod activityType, string roomId);
}

public class RoomActivityCreator: IRoomActivityCreating
{
    private IUpvoteActivityStorage UpvoteActivityStorage;

    public RoomActivityCreator(IUpvoteActivityStorage upvoteActivityStorage)
    {
        UpvoteActivityStorage = upvoteActivityStorage;
    }

    public void StoreActivity(FilmSelectionMethod activityType, string roomId)
    {
        switch (activityType)
        {
            case FilmSelectionMethod.Upvoting:
                UpvoteActivity newActivity = new UpvoteActivity(roomId, 3);
                UpvoteActivityStorage.AddActivity(newActivity);
                break;
            
            default:
                Console.WriteLine("Unknown FilmSelectionMethod");
                break;
        }
    }
}