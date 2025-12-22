using Confab.Shared.Abstractions.Messaging;
using System.Threading.Tasks;

namespace Confab.Shared.Infrastructure.Messaging.Dispatchers;

internal interface IAsyncMessageDispatcher
{
    Task PublishAsync<TMessage>(TMessage message) where TMessage : class, IMessage;
}