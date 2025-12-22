using Confab.Shared.Abstractions.Exceptions;
using System;

namespace Confab.Modules.Agendas.Domain.Agendas.Exceptions;

public class NegativeParticipantsLimitException : ConfabException
{
    public NegativeParticipantsLimitException(Guid agendaSlotId)
        : base($"Regular slot with ID: '{agendaSlotId}' defines negative participants limit.")
    {
    }
}