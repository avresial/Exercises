using MySpot.Application.Abstractions;

namespace MySpot.Infrastructure.DAL.Decorators;

internal class UnitOfWorkCommandHandlerDecorator<TCommand> : ICommandHandler<TCommand> where TCommand : class, ICommand
{
    private readonly ICommandHandler<TCommand> _reserveParkingSpotForCleaningHandler;
    private readonly IUnitOfWork _unitOfWork;

    public UnitOfWorkCommandHandlerDecorator(ICommandHandler<TCommand> reserveParkingSpotForCleaningHandler, IUnitOfWork unitOfWork)
    {
        _reserveParkingSpotForCleaningHandler = reserveParkingSpotForCleaningHandler;
        _unitOfWork = unitOfWork;
    }

    public async Task HandleAsync(TCommand command)
    {
        await _unitOfWork.ExecuteAsync(() => _reserveParkingSpotForCleaningHandler.HandleAsync(command));
    }
}