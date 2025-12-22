using Confab.Modules.Attendances.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Confab.Modules.Attendances.Domain.Policies
{
    public class OverbookingSlotPolicy : ISlotPolicy
    {
        public IEnumerable<Slot> Generate(int participantsLimit)
            => Enumerable.Range(0, (int)(1.1 * participantsLimit)).Select(x => new Slot(Guid.NewGuid()));
    }
}