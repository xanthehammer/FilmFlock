using FilmFlock.Models;
using System.ComponentModel.DataAnnotations;

public class CreateRoomPostBody
{
    [EnumDataType(typeof(FilmSelectionMethod), ErrorMessage = "Invalid enum value")]
    public FilmSelectionMethod? FilmSelectionMethod { get; set; }
    public ushort? PerUserFilmLimit { get; set; }

    public CreateRoomPostBody(FilmSelectionMethod? filmSelectionMethod, ushort? perUserFilmLimit)
    {
        FilmSelectionMethod = filmSelectionMethod;
        PerUserFilmLimit = perUserFilmLimit;
    }
}