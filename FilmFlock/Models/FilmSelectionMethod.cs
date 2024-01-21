namespace FilmFlock.Models;

public enum FilmSelectionMethod: ushort
{
    Upvoting = 0
}

public static class FilmSelectionMethodHelper
{
    public static FilmSelectionMethod[] AllCases()
    {
        return (FilmSelectionMethod[])Enum.GetValues(typeof(FilmSelectionMethod));
    }
}