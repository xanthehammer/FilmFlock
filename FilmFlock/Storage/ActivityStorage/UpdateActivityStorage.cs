using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using FilmFlock.Models.Activities;

namespace FilmFlock.Mongo;

public class UpdateActivityStorage : IUpvoteActivityStorage
{
    private IMongoCollection<MongoUpvoteActivity> ActivityCollection;

    public UpdateActivityStorage(IMongoDatabase database)
    {
        ActivityCollection = database.GetCollection<MongoUpvoteActivity>(Constants.Storage.Mongo.Collections.UpvoteActivities);
    }

    public void AddActivity(UpvoteActivity upvoteActivity)
    {
        MongoUpvoteActivity mongoActivity = new MongoUpvoteActivity(upvoteActivity);
        ActivityCollection.InsertOne(mongoActivity);
    }

    public void UpdateActivity(UpvoteActivity updatedActivity)
    {
        var findModelToUpdate = Builders<MongoUpvoteActivity>.Filter
                                    .Eq(activity => activity.ActivityId, updatedActivity.ActivityId);
        List<MongoUserUpvoteLog> updatedLogs = updatedActivity.UserVoterLogs.Select(log => new MongoUserUpvoteLog(log)).ToList();
        var update = Builders<MongoUpvoteActivity>.Update
                        .Set(activityToUpdate => activityToUpdate.Logs, updatedLogs);
        ActivityCollection.UpdateOne(findModelToUpdate, update);
    }

    public UpvoteActivity? GetActivity(Guid activityId)
    {
        var findActivityFilter = Builders<MongoUpvoteActivity>.Filter
                                    .Eq(activity => activity.ActivityId, activityId);
        MongoUpvoteActivity? foundActivity = ActivityCollection.Find(findActivityFilter).Limit(1).FirstOrDefault();
        if (foundActivity is null)
            return null;
        
        return foundActivity.AsAppModel();
    }

    public UpvoteActivity? GetActivity(string roomId)
    {
        var findActivityFilter = Builders<MongoUpvoteActivity>.Filter
                                    .Eq(activity => activity.RoomId, roomId);
        MongoUpvoteActivity? foundActivity = ActivityCollection.Find(findActivityFilter).Limit(1).FirstOrDefault();
        if (foundActivity is null)
            return null;
        
        return foundActivity.AsAppModel();
    }

}

class MongoUpvoteActivity
{
    [BsonId]
    [BsonRepresentation(BsonType.String)]
    public Guid ActivityId { get; set; }
    [BsonRepresentation(BsonType.String)]
    public string RoomId { get; set; }
    [BsonRepresentation(BsonType.Int32)]
    public ushort PerUserVoteLimit { get; set; }
    public List<MongoUserUpvoteLog> Logs { get; set; }

    [BsonConstructor]
    public MongoUpvoteActivity(Guid activityId, string roomId, ushort perUserVoteLimit, List<MongoUserUpvoteLog> logs)
    {
        ActivityId = activityId;
        RoomId = roomId;
        PerUserVoteLimit = perUserVoteLimit;
        Logs = logs;
    }

    public MongoUpvoteActivity(UpvoteActivity activity)
    {
        ActivityId = activity.ActivityId;
        RoomId = activity.RoomId;
        PerUserVoteLimit = activity.PerUserVoteLimit;

        Logs = activity.UserVoterLogs.Select(log => new MongoUserUpvoteLog(log)).ToList();
    }

    public UpvoteActivity AsAppModel()
    {
        UserUpvoteLog[] voteLogs = Logs.Select(log => log.AsAppModel()).ToArray();
        return new UpvoteActivity(ActivityId, RoomId, PerUserVoteLimit, voteLogs);
    }
}

class MongoUserUpvoteLog
{
    [BsonId]
    [BsonRepresentation(BsonType.String)]
    public Guid UserId { get; set; }
    public Dictionary<string, ushort> Votes { get; set; }

    [BsonConstructor]
    public MongoUserUpvoteLog(Guid userId, Dictionary<string, ushort> votes)
    {
        UserId = userId;
        Votes = votes;
    }

    public MongoUserUpvoteLog(UserUpvoteLog log)
    {
        UserId = log.UserId;
        Votes = log.Votes;
    }

    public UserUpvoteLog AsAppModel()
    {
        return new UserUpvoteLog(UserId, Votes);
    }
}
