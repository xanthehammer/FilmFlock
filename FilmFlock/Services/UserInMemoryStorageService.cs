using System.Collections.Generic;
using FilmFlock.Models;

public interface IUserStorage
{
    public void AddUser(UserModel newUser);
    public UserModel? GetUser(Guid userId, string roomId);
    public UserModel[] GetUsers(string roomId);
}

public class UserInMemoryStorageService : IUserStorage
{
    private List<UserModel> Users;

    public UserInMemoryStorageService()
    {
        Users = new List<UserModel>{};
    }

    public void AddUser(UserModel newUser)
    {
        Users.Add(newUser);
    }

    public UserModel? GetUser(Guid userId, string roomId)
    {
        return Users.Find(user => user.UserId == userId && user.RoomId == roomId);
    }

    public UserModel[] GetUsers(string roomId)
    {
        return Users.FindAll(user => user.RoomId == roomId).ToArray();
    }
}