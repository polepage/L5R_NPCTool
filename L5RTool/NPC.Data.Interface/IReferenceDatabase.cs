using NPC.Data.GameObjects;
using System.Collections.Generic;

namespace NPC.Data
{
    public interface IReferenceDatabase
    {
        IEnumerable<IObjectReference> References { get; }
    }
}
