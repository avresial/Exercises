using Confab.Shared.Abstractions.Events;
using System;

namespace Confab.Modules.Users.Core.Events;

internal record SignedIn(Guid UserId) : IEvent;