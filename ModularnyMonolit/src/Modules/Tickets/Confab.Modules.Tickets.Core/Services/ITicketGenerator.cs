using Confab.Modules.Tickets.Core.Entities;
using System;

namespace Confab.Modules.Tickets.Core.Services;

public interface ITicketGenerator
{
    Ticket Generate(Guid conferenceId, Guid ticketSaleId, decimal? price);
}