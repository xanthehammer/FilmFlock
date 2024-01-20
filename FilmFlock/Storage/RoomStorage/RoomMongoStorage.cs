using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Linq;
using FilmFlock.Models;

namespace FilmFlock.Mongo;

public class RoomMongoStorage : IRoomStorage
{
    private IMongoCollection<RoomMongoModel> RoomCollection;
    public RoomMongoStorage(IMongoDatabase database)
    {
        RoomCollection = database.GetCollection<RoomMongoModel>(Constants.Storage.Mongo.Collections.Rooms);
    }

    public void AddRoom(RoomModel room)
    {
        RoomMongoModel newRoom = new RoomMongoModel(room);
        RoomCollection.InsertOne(newRoom);
    }

    public void UpdateRoom(RoomModel updatedRoom)
    {
        var findModelToUpdate = Builders<RoomMongoModel>.Filter
                                    .Eq(room => room.RoomId, updatedRoom.RoomId);
        List<UserMongoModel> updatedMongoUsers = updatedRoom.Users.Select(user => new UserMongoModel(user)).ToList();
        var update = Builders<RoomMongoModel>.Update
                        .Set(roomToUpdate => roomToUpdate.Users, updatedMongoUsers);
        RoomCollection.UpdateOne(findModelToUpdate, update);
    }

    public RoomModel? GetRoom(string roomId)
    {
        var findRoomFilter = Builders<RoomMongoModel>.Filter
                                .Eq(room => room.RoomId, roomId);
        var foundRoom = RoomCollection.Find(findRoomFilter).FirstOrDefault();
        if (foundRoom == null)
            return null;
        
        return foundRoom.AsAppModel();
    }
}

class RoomMongoModel
{
    [BsonId]
    [BsonRepresentation(BsonType.String)]
    public string RoomId { get; }

    [BsonRepresentation(BsonType.String)]
    public Guid AdminId { get; }
    [BsonRepresentation(BsonType.Int32)]
    public FilmSelectionMethod FilmSelectionMethod { get; }
    [BsonRepresentation(BsonType.Int32)]
    public ushort PerUserFilmLimit { get; }
    public List<UserMongoModel> Users { get; set; }

    [BsonConstructor] // Constructor for deserialization
    public RoomMongoModel(string roomId, Guid adminId, List<UserMongoModel> users)
    {
        RoomId = roomId;
        AdminId = adminId;
        Users = users;
    }

    public RoomMongoModel(RoomModel roomModel)
    {
        RoomId = roomModel.RoomId;
        AdminId = roomModel.AdminId;
        FilmSelectionMethod = roomModel.FilmSelectionMethod;
        PerUserFilmLimit = roomModel.PerUserFilmLimit;
        Users = roomModel.Users.Select(user => new UserMongoModel(user)).ToList();
    }

    public RoomModel AsAppModel()
    {
        List<UserModel> users = Users.Select(user => user.AsAppModel()).ToList();
        return new RoomModel(RoomId, AdminId, FilmSelectionMethod, PerUserFilmLimit, users);
    }
}

class UserMongoModel
{
    [BsonId]
    [BsonRepresentation(MongoDB.Bson.BsonType.String)]
    public Guid UserId { get; set; }
    public string Username { get; set; }
    public List<string> SuggestedMovies { get; set; }

    [BsonConstructor] // Constructor for deserialization
    public UserMongoModel(Guid userId, string username, List<string> suggestedMovies)
    {
        UserId = userId;
        Username = username;
        SuggestedMovies = suggestedMovies;
    }

    public UserMongoModel(UserModel userModel)
    {
        UserId = userModel.UserId;
        Username = userModel.Username;
        SuggestedMovies = userModel.SuggestedMovies;
    }

    public UserModel AsAppModel()
    {
        return new UserModel(UserId, Username, SuggestedMovies);
    }
}
