using Microsoft.AspNetCore.Mvc;

namespace Confab.Modules.Conferences.Api.Controllers;

[Route(ConferencesModule.BasePath)]
public class HomeController : ControllerBase
{
    [HttpGet]
    public ActionResult<string> Get() => "Conferences API";
}