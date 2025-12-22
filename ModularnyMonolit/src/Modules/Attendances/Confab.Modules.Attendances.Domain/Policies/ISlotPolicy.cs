using Confab.Modules.Attendances.Domain.Entities;
using System.Collections.Generic;

namespace Confab.Modules.Attendances.Domain.Policies
{
    public interface ISlotPolicy
    {
        IEnumerable<Slot> Generate(int participantsLimit);
    }
}