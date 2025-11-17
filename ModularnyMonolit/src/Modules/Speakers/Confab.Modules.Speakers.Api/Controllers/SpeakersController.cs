using Confab.Modules.Speakers.Core.DTO;
using Confab.Modules.Speakers.Core.Services;
using Confab.Shared.Abstractions.Contexts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Confab.Modules.Speakers.Api.Controllers;

[Authorize(Policy = _policy)]
internal class SpeakersController(ISpeakersService service, IContext context) : BaseController
{
    private const string _policy = "speakers";

    [HttpGet("{id:guid}")]
    [AllowAnonymous]
    public async Task<ActionResult<SpeakerDto>> Get(Guid id) => OkOrNotFound(await service.GetAsync(id));

    [HttpGet]
    [AllowAnonymous]
    public async Task<ActionResult<IEnumerable<SpeakerDto>>> Get() => Ok(await service.BrowseAsync());

    [HttpPost]
    public async Task<ActionResult> Post(SpeakerDto speaker)
    {
        speaker.Id = context.Identity.Id;
        await service.CreateAsync(speaker);
        return CreatedAtAction(nameof(Get), new { id = speaker.Id }, null);
    }

    [Authorize]
    [HttpPut("{id:guid}")]
    public async Task<ActionResult> Put(SpeakerDto speaker)
    {
        speaker.Id = context.Identity.Id;
        await service.UpdateAsync(speaker);
        return NoContent();
    }
}