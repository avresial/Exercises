using Confab.Shared.Abstractions.Events;
using System;

namespace Confab.Modules.Users.Core.Events;

internal record SignedUp(Guid UserId, string Email) : IEvent;