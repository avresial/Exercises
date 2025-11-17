using Confab.Modules.Speakers.Core.DAL.Repositories;
using Confab.Modules.Speakers.Core.DTO;
using Confab.Modules.Speakers.Core.Events;
using Confab.Modules.Speakers.Core.Exceptions;
using Confab.Modules.Speakers.Core.Mappings;
using Confab.Shared.Abstractions.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Confab.Modules.Speakers.Core.Services;

internal sealed class SpeakersService(ISpeakersRepository repository, IMessageBroker messageBroker) : ISpeakersService
{
    public async Task<IEnumerable<SpeakerDto>> BrowseAsync()
    {
        var entities = await repository.BrowseAsync();
        return entities?.Select(e => e.AsDto());
    }

    public async Task<SpeakerDto> GetAsync(Guid speakerId)
    {
        var entity = await repository.GetAsync(speakerId);
        return entity?.AsDto();
    }

    public async Task CreateAsync(SpeakerDto speaker)
    {
        var alreadyExists = await repository.ExistsAsync(speaker.Id);
        if (alreadyExists)
        {
            throw new SpeakerAlreadyExistsException(speaker.Id);
        }

        await repository.AddAsync(speaker.AsEntity());
        await messageBroker.PublishAsync(new SpeakerCreated(speaker.Id, speaker.FullName));
    }

    public async Task UpdateAsync(SpeakerDto speaker)
    {
        var exists = await repository.ExistsAsync(speaker.Id);

        if (!exists)
        {
            throw new SpeakerNotFoundException(speaker.Id);
        }

        await repository.UpdateAsync(speaker.AsEntity());
    }
}