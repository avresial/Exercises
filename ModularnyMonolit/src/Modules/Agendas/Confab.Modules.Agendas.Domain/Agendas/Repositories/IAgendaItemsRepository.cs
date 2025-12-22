using Confab.Modules.Agendas.Domain.Agendas.Entities;
using Confab.Shared.Abstractions.Kernel.Types;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Confab.Modules.Agendas.Domain.Agendas.Repositories;

public interface IAgendaItemsRepository
{
    Task<IEnumerable<AgendaItem>> BrowseAsync(IEnumerable<SpeakerId> speakerIds);
    Task<AgendaItem> GetAsync(AggregateId id);
    Task AddAsync(AgendaItem agendaItem);
    Task UpdateAsync(AgendaItem agendaItem);
    Task DeleteAsync(AgendaItem agendaItem);
}