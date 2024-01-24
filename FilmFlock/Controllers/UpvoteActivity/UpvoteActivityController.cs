using System.Diagnostics;
using FilmFlock.Models.Activities;
using FilmFlock.Mongo;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using MongoDB.Driver;

namespace FilmFlock.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UpvoteActivityController: ControllerBase
{
    private IUpvoteActivityStorage ActivityStorage;

    public UpvoteActivityController(IUpvoteActivityStorage activityStorage)
    {
        ActivityStorage = activityStorage;
    }

    [HttpPost("Upvote")]
    public IActionResult Post([FromBody] UpvoteFilmPostBody postBody)
    {
        UpvoteActivity? activity = ActivityStorage.GetActivity(postBody.RoomId);
        if (activity is null)
            return BadRequest("Room matching provided RoomId is not selecting a film using the Upvoting method. Vote rejected.");
        UpvoteActivity safeActivity = activity;

        safeActivity.RecordVote(postBody.UserId, postBody.Film);
        ActivityStorage.UpdateActivity(safeActivity);

        return Ok();
    }

    [HttpGet("VoteTally")]
    public IActionResult Get([FromQuery] string roomId)
    {
        UpvoteActivity? activity = ActivityStorage.GetActivity(roomId);
        if (activity is null)
            return BadRequest("Room matching provided RoomId is not selecting a film using the Upvoting method. No vote tally to provide.");
        UpvoteActivity safeActivity = activity;

        Dictionary<string, int> voteTally = safeActivity.UserVoterLogs
            .SelectMany(voterLog => voterLog.Votes)
            .GroupBy(filmCountPair => filmCountPair.Key)
            .ToDictionary(
                group => group.Key,
                group => group.Sum(filmCountPair => filmCountPair.Value)
            );

        var response = new VoteTallyResponse(voteTally);
        return Ok(response);
    }
}
