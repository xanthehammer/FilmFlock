using FilmFlock.Models;
using System.ComponentModel.DataAnnotations;

public class CreateRoomPostBody
{
    public string UserName { get; set; }
    [EnumDataType(typeof(FilmSelectionMethod), ErrorMessage = "Invalid enum value")]
    public FilmSelectionMethod? FilmSelectionMethod { get; set; }
    public ushort? PerUserFilmLimit { get; set; }

    public CreateRoomPostBody(string userName, FilmSelectionMethod? filmSelectionMethod, ushort? perUserFilmLimit)
    {
        UserName = userName;
        FilmSelectionMethod = filmSelectionMethod;
        PerUserFilmLimit = perUserFilmLimit;
    }
}