using NPC.Business.GameObjects;
using System.Collections.Generic;
using System.ComponentModel;

namespace NPC.Business
{
    public interface IReferenceDatabase
    {
        IEnumerable<IObjectReference> References { get; }
    }
}
