using Confab.Modules.Agendas.Domain.Submissions.Entities;
using Confab.Shared.Abstractions.Kernel.Types;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Confab.Modules.Agendas.Domain.Submissions.Repositories;

public interface ISpeakerRepository
{
    Task<bool> ExistsAsync(AggregateId id);
    Task<IEnumerable<Speaker>> BrowseAsync(IEnumerable<AggregateId> ids);
    Task AddAsync(Speaker speaker);
}