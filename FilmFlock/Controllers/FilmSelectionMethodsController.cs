using FilmFlock.Models;
using Microsoft.AspNetCore.Mvc;

namespace FilmFlock.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FilmSelectionMethodsController: ControllerBase
{
    public FilmSelectionMethodsController()
    {
    }

    [HttpGet]
    public IActionResult Get()
    {
        FilmSelectionMethod[] methods = FilmSelectionMethodHelper.AllCases();
        return Ok(methods);
    }

}