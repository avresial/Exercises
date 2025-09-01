using MySpot.Application.Abstractions;

namespace MySpot.Infrastructure.DAL.Decorators;

internal class LoggingCommandHandlerDecorator<TCommand> : ICommandHandler<TCommand> where TCommand : class, ICommand
{
    private readonly ICommandHandler<TCommand> _reserveParkingSpotForCleaningHandler;

    public LoggingCommandHandlerDecorator(ICommandHandler<TCommand> reserveParkingSpotForCleaningHandler)
    {
        _reserveParkingSpotForCleaningHandler = reserveParkingSpotForCleaningHandler;
    }

    public async Task HandleAsync(TCommand command)
    {
        Console.WriteLine($"Handling command {typeof(TCommand).Name} at {DateTime.UtcNow}");
        await _reserveParkingSpotForCleaningHandler.HandleAsync(command);
    }
}