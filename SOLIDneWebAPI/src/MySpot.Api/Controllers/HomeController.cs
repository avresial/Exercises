using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MySpot.Infrastructure;

namespace MySpot.Api.Controllers;
[Route("")]
[ApiController]
public class HomeController : ControllerBase
{
    private readonly string _Name;

    public HomeController(IOptions<AppOptions> options)
    {
        _Name = options.Value.Name;
    }

    [HttpGet]
    public ActionResult<string> Get() => _Name;
}
