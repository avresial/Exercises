using Confab.Shared.Abstractions.Events;
using System;

namespace Confab.Modules.Saga.Messages;

internal record SignedUp(Guid UserId, string Email) : IEvent;