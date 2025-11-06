using Confab.Modules.Conferences.Core.Entities;
using Confab.Modules.Conferences.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Confab.Modules.Conferences.Core.DAL.Repositories;
internal class HostRepository(ConferencesDbContext context) : IHostRepository
{
    private DbSet<Host> Hosts => context.Hosts;

    public Task<Host?> GetAsync(Guid id)
        => Hosts.Include(x => x.Conferences).SingleOrDefaultAsync(x => x.Id == id);

    public async Task<IReadOnlyList<Host>> BrowseAsync()
        => await Hosts.ToListAsync();

    public async Task AddAsync(Host host)
    {
        await Hosts.AddAsync(host);
        await context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Host host)
    {
        Hosts.Update(host);
        await context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Host host)
    {
        Hosts.Remove(host);
        await context.SaveChangesAsync();
    }
}