using Humanizer;
using Microsoft.Extensions.Logging;
using MySpot.Application.Abstractions;
using System.Diagnostics;

namespace MySpot.Infrastructure.Logging.Decorators;

internal class LoggingCommandHandlerDecorator<TCommand> : ICommandHandler<TCommand> where TCommand : class, ICommand
{
    private readonly ICommandHandler<TCommand> _reserveParkingSpotForCleaningHandler;
    private readonly ILogger<ICommandHandler<TCommand>> _logger;

    public LoggingCommandHandlerDecorator(ICommandHandler<TCommand> reserveParkingSpotForCleaningHandler, ILogger<ICommandHandler<TCommand>> logger)
    {
        _reserveParkingSpotForCleaningHandler = reserveParkingSpotForCleaningHandler;
        _logger = logger;
    }

    public async Task HandleAsync(TCommand command)
    {
        var commandName = typeof(TCommand).Name.Underscore();
        _logger.LogInformation("Started handling command {CommandName}...", commandName);
        var stopwatch = Stopwatch.StartNew();

        await _reserveParkingSpotForCleaningHandler.HandleAsync(command);

        stopwatch.Stop();
        _logger.LogInformation("Completed handling command {CommandName} at {Elapsed} in {stopwatch}.", commandName, stopwatch.Elapsed);
    }
}