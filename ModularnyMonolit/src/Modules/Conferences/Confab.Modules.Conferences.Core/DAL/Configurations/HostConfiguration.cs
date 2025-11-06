using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Confab.Modules.Conferences.Core.DAL.Configurations;
internal class HostConfiguration : IEntityTypeConfiguration<Host>
{
    public void Configure(EntityTypeBuilder<Host> builder)
    {
    }
}