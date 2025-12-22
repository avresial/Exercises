using Confab.Shared.Abstractions.Exceptions;
using System;

namespace Confab.Modules.Users.Core.Exceptions;

internal class UserNotActiveException : ConfabException
{
    public Guid UserId { get; }

    public UserNotActiveException(Guid userId) : base($"User with ID: '{userId}' is not active.")
    {
        UserId = userId;
    }
}