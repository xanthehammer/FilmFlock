using Microsoft.AspNetCore.Components.Web;
using Microsoft.VisualBasic;

namespace FilmFlock.Models;

[Serializable]
public readonly struct FilmSelectionMethodModel
{
    /// <summary>
    /// The type of film selection method this model represents.
    /// </summary>
    public FilmSelectionMethodType Type { get; }
    /// <summary>
    /// A user friendly title of this method.
    /// For example, "Upvoting".
    /// </summary>
    public string Title { get; }
    /// <summary>
    /// A user friendly description of how this selection method works.
    /// </summary>
    public string Description { get; }

    public static FilmSelectionMethodModel[] AllMethods
    {
        get
        {
            return new FilmSelectionMethodModel[] { FilmSelectionMethodModel.Upvoting };
        }
    }

    public static FilmSelectionMethodModel Upvoting
    {
        get
        {
            return new FilmSelectionMethodModel(
                FilmSelectionMethodType.upvoting,
                "Upvoting",
                "Each user gets 1 vote and the movie with the most votes wins! Ties are broken by random selection."
            );
        }
    }

    internal FilmSelectionMethodModel(FilmSelectionMethodType type, string title, string description)
    {
        Type = type;
        Title = title;
        Description = description;
    }

}

public enum FilmSelectionMethodType: uint
{
    upvoting = 0
}
