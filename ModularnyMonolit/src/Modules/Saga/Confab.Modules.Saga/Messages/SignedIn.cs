using Confab.Shared.Abstractions.Events;
using System;

namespace Confab.Modules.Saga.Messages;

internal record SignedIn(Guid UserId) : IEvent;