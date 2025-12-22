using Confab.Shared.Abstractions.Kernel;
using Confab.Shared.Abstractions.Messaging;
using System.Collections.Generic;

namespace Confab.Modules.Agendas.Application.Submissions.Services;

public interface IEventMapper
{
    IMessage Map(IDomainEvent @event);
    IEnumerable<IMessage> MapAll(IEnumerable<IDomainEvent> events);
}