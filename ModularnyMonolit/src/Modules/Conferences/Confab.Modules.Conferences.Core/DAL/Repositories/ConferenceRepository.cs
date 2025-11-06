using Confab.Modules.Conferences.Core.Entities;
using Confab.Modules.Conferences.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Confab.Modules.Conferences.Core.DAL.Repositories;
internal class ConferenceRepository(ConferencesDbContext context) : IConferenceRepository
{
    private DbSet<Conference> Conferences => context.Conferences;

    public Task<Conference?> GetAsync(Guid id)
        => Conferences.Include(x => x.Host).SingleOrDefaultAsync(x => x.Id == id);

    public async Task<IReadOnlyList<Conference>> BrowseAsync()
        => await Conferences.Include(x => x.Host).ToListAsync();

    public async Task AddAsync(Conference conference)
    {
        await Conferences.AddAsync(conference);
        await context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Conference conference)
    {
        Conferences.Update(conference);
        await context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Conference conference)
    {
        Conferences.Remove(conference);
        await context.SaveChangesAsync();
    }
}