using Microsoft.AspNetCore.Components.Web;
using Microsoft.VisualBasic;

namespace FilmFlock.Models;

public enum FilmSelectionMethod: ushort
{
    Upvoting = 0
}

public static class FilmSelectionMethodExtensions
{
    public static string GetDisplayName(this FilmSelectionMethod method)
    {
        switch (method)
        {
            case FilmSelectionMethod.Upvoting:
                return "Upvoting";
            default:
                throw new ArgumentOutOfRangeException(nameof(method), method, "Unexpected enum value");
        }
    }
}

public static class FilmSelectionMethodHelper
{
    public static FilmSelectionMethod[] AllCases()
    {
        return (FilmSelectionMethod[])Enum.GetValues(typeof(FilmSelectionMethod));
    }
}