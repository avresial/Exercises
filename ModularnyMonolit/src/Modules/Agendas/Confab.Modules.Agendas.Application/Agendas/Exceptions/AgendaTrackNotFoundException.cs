using Confab.Shared.Abstractions.Exceptions;
using System;

namespace Confab.Modules.Agendas.Application.Agendas.Exceptions;

public class AgendaTrackNotFoundException : ConfabException
{
    public AgendaTrackNotFoundException(Guid agendaTrackId)
        : base($"Agenda track with ID: '{agendaTrackId} was not found.'")
    {
    }
}