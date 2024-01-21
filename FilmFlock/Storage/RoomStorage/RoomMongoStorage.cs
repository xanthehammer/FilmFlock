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

    public void AddRoom(Room room)
    {
        RoomMongoModel newRoom = new RoomMongoModel(room);
        RoomCollection.InsertOne(newRoom);
    }

    public void UpdateRoom(Room updatedRoom)
    {
        var findModelToUpdate = Builders<RoomMongoModel>.Filter
                                    .Eq(room => room.RoomId, updatedRoom.RoomId);
        List<UserMongoModel> updatedMongoUsers = updatedRoom.Users.Select(user => new UserMongoModel(user)).ToList();
        var update = Builders<RoomMongoModel>.Update
                        .Set(roomToUpdate => roomToUpdate.Users, updatedMongoUsers);
        RoomCollection.UpdateOne(findModelToUpdate, update);
    }

    public Room? GetRoom(string roomId)
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

    public RoomMongoModel(Room room)
    {
        RoomId = room.RoomId;
        AdminId = room.AdminId;
        FilmSelectionMethod = room.FilmSelectionMethod;
        PerUserFilmLimit = room.PerUserFilmLimit;
        Users = room.Users.Select(user => new UserMongoModel(user)).ToList();
    }

    public Room AsAppModel()
    {
        List<User> users = Users.Select(user => user.AsAppModel()).ToList();
        return new Room(RoomId, AdminId, FilmSelectionMethod, PerUserFilmLimit, users);
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

    public UserMongoModel(User user)
    {
        UserId = user.UserId;
        Username = user.Username;
        SuggestedMovies = user.SuggestedMovies;
    }

    public User AsAppModel()
    {
        return new User(UserId, Username, SuggestedMovies);
    }
}
