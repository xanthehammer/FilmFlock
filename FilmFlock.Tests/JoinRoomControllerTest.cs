using FilmFlock.Controllers;
using FilmFlock.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.ObjectPool;

namespace FilmFlock.Tests;

public class JoinRoomControllerTest
{

    [Fact]
    public async void JoinNonExistantRoom()
    {
        // GIVEN that we have no existing rooms...
        var roomStorage = new RoomInMemoryStorageService();
        var userStorage = new UserInMemoryStorageService();
        var controller = new JoinRoomController(roomStorage, userStorage);

        // WHEN we attempt to join a room that doesn't exist...
        var postBody = new JoinRoomPostBody(Guid.NewGuid().ToString(), "test username");
        var response = await controller.Post(postBody);

        // THEN we receive a 400 response error as the room did not exist.
        Assert.IsType<BadRequestObjectResult>(response);
        var badRequestResponse = response as BadRequestObjectResult;
        Assert.Equal(400, badRequestResponse.StatusCode);
    }

    [Fact]
    public async void JoinRoomWithDuplicatedUsername()
    {
        // GIVEN that we have a room...
        var existingRoom = new RoomModel();
        var roomStorage = new RoomInMemoryStorageService();
        roomStorage.AddRoom(existingRoom);

        // AND there is a user already in that room...
        const string alreadyTakenUsername = "Furious George";
        var existingUser = new UserModel(alreadyTakenUsername, existingRoom.RoomId);
        var userStorage = new UserInMemoryStorageService();
        userStorage.AddUser(existingUser);

        var controller = new JoinRoomController(roomStorage, userStorage);

        // WHEN we attempt to join a room using the same username...
        var postBody = new JoinRoomPostBody(existingRoom.RoomId, alreadyTakenUsername);
        var response = await controller.Post(postBody);

        // THEN we receive a 400 response error as the username has already been taken.
        Assert.IsType<BadRequestObjectResult>(response);
        var badRequestResponse = response as BadRequestObjectResult;
        Assert.Equal(400, badRequestResponse.StatusCode);
    }

    [Fact]
    public async void JoinRoomSuccessfully()
    {
        // GIVEN that we have a room...
        var existingRoom = new RoomModel();
        var roomStorage = new RoomInMemoryStorageService();
        roomStorage.AddRoom(existingRoom);

        var userStorage = new UserInMemoryStorageService();

        var controller = new JoinRoomController(roomStorage, userStorage);

        // WHEN we attempt to join a room...
        var postBody = new JoinRoomPostBody(existingRoom.RoomId, "Johnny Unitas");
        var response = await controller.Post(postBody);

        // THEN we receive a 200 response...
        Assert.IsType<OkObjectResult>(response);
        var okResponse = response as OkObjectResult;
        Assert.Equal(200, okResponse.StatusCode);

        // AND an ID for our new user...
        var emptyGuid = new Guid();
        Assert.NotEqual(emptyGuid.ToString(), okResponse.Value.ToString());

        // AND the new user is added to storage.
        var storedUser = userStorage.GetUser(new Guid(okResponse.Value.ToString()), existingRoom.RoomId);
        Assert.NotNull(storedUser);
    }

}
