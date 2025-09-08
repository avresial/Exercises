namespace MySpot.Infrastructure.DAL;

internal class PostgresUnitOfWork : IUnitOfWork
{
    private readonly MySpotDbContext _mySpotDbContext;

    internal PostgresUnitOfWork(MySpotDbContext mySpotDbContext)
    {
        _mySpotDbContext = mySpotDbContext;
    }

    public async Task ExecuteAsync(Func<Task> action)
    {
        await using var transaction = await _mySpotDbContext.Database.BeginTransactionAsync();

        try
        {
            await action();

            await _mySpotDbContext.SaveChangesAsync();

            await transaction.CommitAsync();
        }
        catch (Exception)
        {
            await transaction.RollbackAsync();

            throw;
        }
    }
}
